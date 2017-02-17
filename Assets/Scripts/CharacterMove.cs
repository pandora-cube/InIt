using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour {
    public float moveSpeed = 3f;		// 이동 속도
    public int animSpeed = 3;			// 애니메이션 재생 속도
    public Sprite[] upSprites;			// 상향 이동 스프라이트
    public Sprite[] downSprites;		// 하향 이동 스프라이트
    public Sprite[] leftSprites;		// 좌향 이동 스프라이트
    public Sprite[] rightSprites;		// 우향 이동 스프라이트
    public bool canmove = true;			// 캐릭터 이동 가능 여부
    
    float backgroundPosX;				// 배경의 X좌표
    float backgroundPosY;				// 배경의 Y좌표
    float backgroundWidth;				// 배경의 두께
    float backgroundHeight;				// 배경의 높이
    float cameraWidth;					// 화면의 두께
    float cameraHeight;					// 화면의 높이
    int animDirection = 1;				// 애니메이션 방향 (0: 상, 1: 하, 2: 좌, 3: 우)
    int animIndex = 0;                  // 애니메이션 재생 인덱스
	bool touchMoving = false;			// 터치 이동중 여부
	Vector2 touchMove = new Vector3();	// 터치 이동 목적 좌표
    
	void Start() {
        // 초기화
        GetBackgroundStatus();
        cameraHeight = 2f * Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Camera.main.aspect;
	}
	
	void Update() {
		/*
		 *  CharacterMove.Update()
		 *      캐릭터 이동 (조작키)
		 *      캐릭터 이동 (터치)
		 *      캐릭터 이동 및 애니메이션 적용
		 *      캐릭터 좌표 데이터 갱신
		 *      화면 이동
		 */

		// 이동 불가능한 경우
		if(!canmove) {
            GetComponent<Rigidbody2D>().isKinematic = true;
			touchMoving = false;
            return;
        }

        // 이동중 여부 체크 변수
        bool ismoving = false;
        // 캐릭터 스프라이트
        SpriteRenderer charspr = GetComponent<SpriteRenderer>();

        /* 캐릭터 이동 (조작키) */
        float speedX = 0f, speedY = 0f;
        int direction = 1;
        
        // 상향 이동
        if(Input.GetKey(KeyCode.UpArrow)) {
            ismoving = true;
			touchMoving = false;
            speedY += moveSpeed;
            direction = 0;
        }
        // 하향 이동
        if(Input.GetKey(KeyCode.DownArrow)) {
            ismoving = true;
			touchMoving = false;
			speedY -= moveSpeed;
            direction = 1;
        }
        // 좌향 이동
        if(Input.GetKey(KeyCode.LeftArrow)) {
            ismoving = true;
			touchMoving = false;
			speedX -= moveSpeed;
            direction = 2;
        }
        // 우향 이동
        if(Input.GetKey(KeyCode.RightArrow)) {
            ismoving = true;
			touchMoving = false;
			speedX += moveSpeed;
            direction = 3;
        }

		/* 캐릭터 이동 (터치) */
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

		// Background 스프라이트를 마우스 왼쪽 혹은 오른쪽 버튼으로 클릭중인 경우
		if((Input.GetMouseButton(0) || Input.GetMouseButton(1)) && hit &&
			(hit.collider.name.StartsWith("Background_") || hit.collider.name.StartsWith("TalkZone/") ||
			hit.collider.tag == "NPC" || hit.collider.tag == "Entrance")) {
			touchMoving = true;
			touchMove = hit.point;
		}

		// 터치 이동중인 경우
		if(touchMoving) {
			Vector3 current = transform.position;
			current.y += GetComponent<BoxCollider2D>().offset.y/2f;

			if(touchMove.x + .1f > current.x && touchMove.x - .1f < current.x
			&& touchMove.y + .1f > current.y && touchMove.y - .1f < current.y) {
				// 도착한 경우
				touchMoving = false;
			} else {
				// 도착하지 않은 경우
				ismoving = true;

				float differX = Mathf.Abs(touchMove.x - current.x);
				float differY = Mathf.Abs(touchMove.y - current.y);
				float ratioX = differX < differY ? differX / differY : 1f;
				float ratioY = differY < differX ? differY / differX : 1f;

				if(touchMove.x > current.x) {			// 목적지가 오른쪽인 경우
					speedX = moveSpeed * ratioX;
				} else if(touchMove.x < current.x) {	// 목적지가 왼쪽인 경우
					speedX = -moveSpeed * ratioX;
				}

				if(touchMove.y > current.y) {			// 목적지가 윗쪽인 경우
					speedY = moveSpeed * ratioY;
				} else if(touchMove.y < current.y) {	// 목적지가 아랫쪽인 경우
					speedY = -moveSpeed * ratioY;
				}
				
				// 0: Up, 1: Down, 2: Left, 3: Right
				float angle = Mathf.Atan2(speedY, speedX) * 180f / Mathf.PI;
				if(angle >= 0f && angle < 45f)
					direction = 3;
				else if(angle >= 45f && angle < 135f)
					direction = 0;
				else if(angle >= 135f && angle <= 180f)
					direction = 2;
				else if(angle < 0f && angle > -45f)
					direction = 3;
				else if(angle <= -45f && angle > -135f)
					direction = 1;
				else if(angle <= -135f && angle >= -180f)
					direction = 2;
				else
					direction = 1;
			}
		}

		/* 캐릭터 이동 및 애니메이션 적용 */
		if(ismoving) { // 이동중인 경우
			// 정지 해제
			GetComponent<Rigidbody2D>().isKinematic = false;
			// 속도 처리
			GetComponent<Rigidbody2D>().velocity = new Vector3(speedX, speedY, 0f);
			// 이동 애니메이션 적용
			SetMoveAnimation(direction);
		} else {
			// 정지
			GetComponent<Rigidbody2D>().isKinematic = true;
			GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
			// 스프라이트 초기화
			Sprite sprite;
			switch(animDirection) {
				case 0:
					sprite = upSprites[0];
					break;
				case 1:
					sprite = downSprites[0];
					break;
				case 2:
					sprite = leftSprites[0];
					break;
				case 3:
					sprite = rightSprites[0];
					break;
				default:
					sprite = downSprites[0];
					break;
			}
			charspr.sprite = sprite;
		}

		/* 캐릭터 좌표 데이터 갱신 */
		PlayerData.Player.Position = new float[3] {
            charspr.transform.position.x,
            charspr.transform.position.y,
            charspr.transform.position.z
        };
        
        /* 화면 이동 */
        // 위치 정보 갱신
        PlayerData.Player.Location = GetPlayerArea();
        // 맵 정보 갱신
        GetBackgroundStatus();

        Vector3 charpos = charspr.transform.position;                   // 캐릭터의 좌표
        Vector3 campos = Camera.main.transform.position;                // 화면의 좌표
        Vector3 campos_moved = campos;                                  // 화면의 이동될 좌표
        float left = charpos.x - backgroundPosX - cameraWidth/2f;       // 화면의 왼쪽 끝
        float right = charpos.x - backgroundPosX + cameraWidth/2f;      // 화면의 오른쪽 끝
        float bottom = charpos.y - backgroundPosY - cameraHeight/2f;    // 화면의 아랫쪽 끝
        float top = charpos.y - backgroundPosY + cameraHeight/2f;       // 화면의 윗쪽 끝
        // 맵의 왼쪽 끝과 캐릭터 x좌표의 거리
        float xdis_left = backgroundPosX - backgroundWidth/2f - charpos.x;
        // 맵의 오른쪽 끝과 캐릭터 x좌표의 거리
        float xdis_right = backgroundPosX + backgroundWidth/2f - charpos.x;
        // 맵의 아랫쪽 끝과 캐릭터 y좌표의 거리
        float ydis_bottom = backgroundPosY - backgroundHeight/2f - charpos.y;
        // 맵의 윗쪽 끝과 캐릭터 y좌표의 거리
        float ydis_top = backgroundPosY + backgroundHeight/2f - charpos.y;
        
        // 화면의 좌우 영역이 맵 안쪽에 있는 경우
        if(left >= -backgroundWidth/2f && right <= backgroundWidth/2f)
            campos_moved.x = charpos.x;
        // 맵 왼쪽으로 벗어난 경우
        else if(Mathf.Abs(xdis_left) < Mathf.Abs(xdis_right))
            campos_moved.x = backgroundPosX - backgroundWidth/2f + cameraWidth/2f;
        // 맵 오른쪽으로 벗어난 경우
        else
            campos_moved.x = backgroundPosX + backgroundWidth/2f - cameraWidth/2f;
            
        // 화면의 상하 영역이 맵 안쪽에 있는 경우
        if(bottom >= -backgroundHeight/2f && top <= backgroundHeight/2f)
            campos_moved.y = charpos.y;
        // 맵 아랫쪽으로 벗어난 경우
        else if(Mathf.Abs(ydis_bottom) < Mathf.Abs(ydis_top))
            campos_moved.y = backgroundPosY - backgroundHeight/2f + cameraHeight/2f;
        // 맵 윗쪽으로 벗어난 경우
        else
            campos_moved.y = backgroundPosY + backgroundHeight/2f - cameraHeight/2f;
        // 화면 좌표를 캐릭터 좌표와 동기화
        Camera.main.transform.position = new Vector3(campos_moved.x, campos_moved.y, campos_moved.z);
	}

    void SetMoveAnimation(int direction) {
        Sprite[] moveSprites;
        switch(direction) {
            case 0:
                moveSprites = upSprites;
                break;
            case 1:
                moveSprites = downSprites;
                break;
            case 2:
                moveSprites = leftSprites;
                break;
            case 3:
                moveSprites = rightSprites;
                break;
            default:
                moveSprites = downSprites;
                break;
        }

        if(animDirection != direction) {
            animDirection = direction;
            animIndex = 0;
        } else {
            animIndex++;
            if(animIndex/animSpeed >= moveSprites.Length)
                animIndex = 0;
        }

        // 이동 스프라이트 적용
        GetComponent<SpriteRenderer>().sprite = moveSprites[animIndex/animSpeed];
    }

	void OnCollisionStay2D(Collision2D col) {
        // 출입구와 충돌한 경우
        if(col.gameObject.GetComponents<Entrance>().Length > 0) {
            col.gameObject.GetComponent<Entrance>().OnEnter();
			touchMoving = false;
		}

		// Background와 충돌한 경우
		if(col.gameObject.name.StartsWith("Background_"))
			touchMoving = false;
	}

    void OnTriggerEnter2D(Collider2D col) {
		// NPC 대화 가능 영역 진입
		if(col.name.StartsWith("TalkZone/")) {
			NPC npc = GameObject.Find(col.name.Split('/')[1]).GetComponent<NPC>();
			npc.Collided = true;
		}
		if(col.GetComponents<NPC>().Length > 0 && (col.gameObject.name == "TalkZone/" + col.name || col.name.StartsWith("Poster"))) {
            col.GetComponent<NPC>().Collided = true;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
		// NPC 대화 가능 영역 퇴장
		if(col.name.StartsWith("TalkZone/")) {
			NPC npc = GameObject.Find(col.name.Split('/')[1]).GetComponent<NPC>();
			npc.Collided = false;
		}
	}

    string GetPlayerArea() {
        // 캐릭터 좌표
        Vector3 chrPos = transform.position;
        // Y좌표는 BoxCollider 최하단을 기준으로 함
        chrPos.y += GetComponent<BoxCollider2D>().offset.y/2f;

        foreach(SpriteRenderer spr in GameObject.FindObjectsOfType<SpriteRenderer>()) {
            string[] sprInfo = spr.name.Split('_');
            Vector3 sprPos = spr.transform.position;
            Vector3 sprSize = spr.bounds.size;

            // 캐릭터가 특정 배경 스프라이트의 영역 안에 있는 경우
            if(sprInfo[0] == "Background" && sprInfo[1].Length > 0
                && chrPos.x >= sprPos.x-sprSize.x/2f && chrPos.x <= sprPos.x+sprSize.x/2f
                && chrPos.y >= sprPos.y-sprSize.y/2f && chrPos.y <= sprPos.y+sprSize.y/2f)
                return spr.name;
        }
        return PlayerData.Player.Location;
    }

    void GetBackgroundStatus() {
        // 배경 스프라이트 선택자
        SpriteRenderer obj = GameObject.Find(GetPlayerArea()).GetComponent<SpriteRenderer>();

        // 배경 스프라이트의 좌표를 구함
        backgroundPosX = obj.transform.position.x;
        backgroundPosY = obj.transform.position.y;
        // 배경 스프라이트의 너비와 높이를 구함
        backgroundWidth = obj.bounds.size.x;
        backgroundHeight = obj.bounds.size.y;
    }
}
