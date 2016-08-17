using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC : MonoBehaviour {
    public string Name;                 // NPC의 이름
    public int Stage;                   // 게임 진행 순차 중 이 NPC의 순번
    public string[] Messages;           // NPC의 대사 배열
    public Sprite Poster;               // 포스터 스프라이트
    public bool dontChat = false;       // 대화 거부 등의 대사 수행 여부
    public Sprite[] movingSprites;      // 이동 애니메이션 스프라이트
    public Vector2[] pointPosition;     // 이동할 좌표 배열

    int movingIndex;                    // 현재 이동한 좌표 인덱스

    void Start() {
        // 기존에 존재하던 충돌 처리 용도의 BoxCollider2D
        BoxCollider2D original = GetComponent<BoxCollider2D>();
        // 대화 가능 영역 검사 용도의 Collider 생성
        BoxCollider2D trigger = gameObject.AddComponent<BoxCollider2D>();
        // Trigger로 설정
        trigger.isTrigger = true;
        // 영역을 충돌 처리 Collider보다 크게 설정
        trigger.size = new Vector2(original.size.x+.1f, original.size.y+.1f);
        // Offset을 원점으로 설정
        trigger.offset = new Vector2(0f, 0f);
    }

    void Update() {
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
    }

    public void MoveStart() {
        movingIndex = 0;
    }
}
