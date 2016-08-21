using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC : MonoBehaviour {
    [System.Serializable]
    public struct COMMAND {
        public string Command;
        public string Parameters;
    };

    public string Name;                 // NPC의 이름
    public int Stage;                   // 게임 진행 순차 중 이 NPC의 순번
    public string[] Messages;           // NPC의 대사 배열
    public Sprite Poster;               // 포스터 스프라이트
    public bool dontChat = false;       // 대화 거부 등의 대사 수행 여부
    public float moveSpeed = 3f;        // 이동 속도
    public int animSpeed = 3;           // 애니메이션 재생 속도
    public Sprite[] movingSprites;      // 이동 애니메이션 스프라이트 (오른쪽을 보고 있어야 함)
    public COMMAND[] Commands;          // 명령
    public bool Collided = false;       // 접촉 여부

    bool eventReservated = false;       // 명령 종료 이벤트 예약 여부
    bool Executed = false;              // 명령 이행 여부
    int commandIndex = 0;               // 현재 이행한 명령 인덱스
    Sprite standingSprite;              // 정지상태 스프라이트
    int animIndex = 0;                  // 애니메이션 재생 인덱스

    void Start() {
        /*
         *  NPC.Start()
         *      초기화
         *      대화 가능 영역 트리거 추가
         */
        
        /* 초기화 */
        standingSprite = GetComponent<SpriteRenderer>().sprite;

        /* 대화 가능 영역 트리거 추가 */
        // 기존에 존재하던 충돌 처리 용도의 BoxCollider2D
        BoxCollider2D original = GetComponent<BoxCollider2D>();
        // 대화 가능 영역 검사 용도의 Collider 생성
        BoxCollider2D trigger = gameObject.AddComponent<BoxCollider2D>();
        // Trigger로 설정
        trigger.isTrigger = true;
        // 영역을 충돌 처리 Collider보다 크게 설정
        trigger.size = new Vector2(original.size.x+.25f, original.size.y+.25f);
        // Offset을 원점으로 설정
        trigger.offset = new Vector2(0f, 0f);
    }

    void Update() {
        /*
         *  NPC.Update()
         *      레벨에 따른 네임태그 강조
         *      명령 이행
         */

        /* 레벨에 따른 네임태그 강조 */
        try {
            // 다음 진행 상대가 이 NPC인 경우
            if(Stage == PlayerData.Player.Level+1) {
                // 이 NPC의 네임태그 강조 처리
                GetComponent<Nametag>().Blink = true;
            } else {
                // 네임태그 강조 해제
                GetComponent<Nametag>().Blink = false;
            }
        } catch { }

        /* 명령 이행 */
        if(Executed && Commands.Length > commandIndex)
            OnCommandUpdate();
    }

    void OnCollisionEnter2D(Collision2D col) {
        // 출입구 영역으로 들어온 경우
        if(col.gameObject.tag == "Entrance") {
            col.gameObject.GetComponent<Entrance>().OnEnter(transform);
        }
    }

    void OnEntranceEnter() {
        OnCommandEnd();
    }

    void OnCommandEnd() {
        eventReservated = false;
        
        if(GetComponents<Rigidbody2D>().Length > 0) {
            // Rigidbody2D 컴포넌트 제거
            Destroy(GetComponent<Rigidbody2D>());
        }
        // 스프라이트 초기화
        GetComponent<SpriteRenderer>().sprite = standingSprite;

        // 명령이 더 남은 경우
        if(Commands.Length-1 > commandIndex)
            commandIndex++;
        else 
            OnCommandsEnd();
    }

    void OnCommandsEnd() {
        commandIndex = 0;
        Executed = false;

        GameObject.Find(transform.name).SendMessage("OnNPCCommandsEnd", SendMessageOptions.DontRequireReceiver);
    }

    void OnCommandUpdate() {
        string command = Commands[commandIndex].Command;
        string parameters = Commands[commandIndex].Parameters;
        string[] parameter = parameters.Split(',');

        switch(command) {
            // 특정 좌표로 이동
            case "Move":
                if(GetComponents<Rigidbody2D>().Length == 0) {
                    // Rigidbody2D 컴포넌트 추가
                    Rigidbody2D rigid = gameObject.AddComponent<Rigidbody2D>();
                    rigid.freezeRotation = true;
                }

                Vector2 destination = new Vector2(float.Parse(parameter[0]), float.Parse(parameter[1]));    // 이동 목적지
                Vector3 current = transform.position;                                                       // NPC의 현재 좌표
                SpriteRenderer sprite = GetComponent<SpriteRenderer>();                                     // NPC Sprite
                Vector2 speed = new Vector2(0f, 0f);                                                        // X, Y 속도

                if((destination.x+.25f > current.x && destination.x-.25f < current.x || destination.x == 256f)
                    && (destination.y+.25f > current.y && destination.y-.25f < current.y || destination.y == 256f))
                    OnCommandEnd();
                else {
                    if(destination.x == 256f) {             // X좌표로 이동하지 않는 경우
                    } else if(destination.x > current.x) {  // 목적지가 오른쪽인 경우
                        sprite.flipX = false;
                        speed.x = moveSpeed;
                    } else if(destination.x < current.x) {  // 목적지가 왼쪽인 경우
                        sprite.flipX = true;
                        speed.x = -moveSpeed;
                    }
            
                    if(destination.y == 256f) {             // Y좌표로 이동하지 않는 경우
                    } else if(destination.y > current.y) {  // 목적지가 윗쪽인 경우
                        speed.y = moveSpeed;
                    } else if(destination.y < current.y) {  // 목적지가 아랫쪽인 경우
                        speed.y = -moveSpeed;
                    }
        
                    // 정지 해제
                    GetComponent<Rigidbody2D>().isKinematic = false;
                    // 속도 처리
                    GetComponent<Rigidbody2D>().velocity = new Vector3(speed.x, speed.y, 0f);

                    // 이동 애니메이션 인덱스 처리
                    animIndex++;
                    if(animIndex/animSpeed >= movingSprites.Length)
                        animIndex = 0;
                    // 이동 스프라이트 이미지로 전환
                    sprite.sprite = movingSprites[animIndex/animSpeed];
                }
                break;
            // 일정 시간동안 정지
            case "Stop":
                if(!eventReservated) {
                    eventReservated = true;
                    Invoke("OnCommandEnd", float.Parse(parameters));
                }
                break;
            // 특정 오브젝트에게 이동
            case "Teleport":
                Vector3 position = GameObject.Find(parameters).transform.position;
                transform.position = new Vector3(position.x, position.y, transform.position.z);
                OnCommandEnd();
                break;
            // FlipX 상태 설정
            case "Flip":
                GetComponent<SpriteRenderer>().flipX = bool.Parse(parameters);
                OnCommandEnd();
                break;
            // 특정 출입구의 잠금을 해제함
            case "Unlock":
                GameObject.Find(parameters).GetComponent<Entrance>().Locked = false;
                OnCommandEnd();
                break;
            // 특정 출입구를 잠금
            case "Lock":
                GameObject.Find(parameters).GetComponent<Entrance>().Locked = true;
                OnCommandEnd();
                break;
        }
    }

    public void CommandStart() {
        commandIndex = 0;
        Executed = true;
    }
}
