using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC : MonoBehaviour {
    public string Name;             // NPC의 이름
    public int Stage;               // 게임 진행 순차 중 이 NPC의 순번
    public string[] Messages;       // NPC의 대사 배열
    public Sprite Poster;           // 포스터 스프라이트
    public bool dontChat = false;   // 대화 거부 등의 대사 수행 여부

    void Start() {
        // 대화 가능 영역 검사 용도의 Collider 생성
        /*BoxCollider2D trigger = Component.Instantiate<BoxCollider2D>(GetComponent<BoxCollider2D>());
        // Trigger로 설정
        trigger.isTrigger = true;
        // 영역을 충돌 처리 Collider보다 (1f, 1f) 크게 설정
        trigger.size.Set(trigger.size.x+1f, trigger.size.y+1f);*/
    }

    void Update() {
        /* 레벨에 따른 네임태그 강조 */
        try {
            // 다음 진행 상대가 이 NPC인 경우
            if(Stage == SingleTone.Instance.Level+1) {
                // 이 NPC의 네임태그 강조 처리
                GetComponent<Nametag>().Blink = true;
            } else {
                // 네임태그 강조 해제
                GetComponent<Nametag>().Blink = false;
            }
        } catch { }
    }
}
