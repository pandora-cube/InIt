using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC : MonoBehaviour {
    public string Name;             // NPC의 이름
    public int Stage;               // 게임 진행 순차 중 이 NPC의 순번
    public string[] Messages;       // NPC의 대사 배열
    public Sprite Poster;           // 포스터 스프라이트
    public bool dontChat = false;   // 대화 거부 등의 대사 수행 여부
    
    int messageIndex = 0;           // 대사 인덱스

    void Update() {
        /*
         *  NPC.Update()
         *      대화 처리
         *      레벨에 따른 네임태그 강조
         */
        
        /* 대화 처리 */
        // Space 혹은 Enter 키를 누른 경우
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) {
            NPCTalk();
        }

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

    public void NPCTalk() {
        // 대화 UI
        Dialogue dialogue = GameObject.Find("Dialogue UI").GetComponent<Dialogue>();
        // 포스터 이미지
        Transform poster = dialogue.transform.FindChild("Canvas").FindChild("Poster");
        
        // 메뉴가 활성화되어 있는 경우
        if(GameObject.Find("Menu UI").transform.localScale == new Vector3(1f, 1f, 1f)) {
        }
        // 대사가 출력중인 경우
        else if(dialogue.msgCount != 0) {
        }
        // NPC가 대화를 거부하였거나 더 할 대사가 없는 경우
        else if(messageIndex == -1 || messageIndex >= Messages.Length) {
            // 대화 UI 숨김
            dialogue.HideDialogue();
            // 레벨 설정
            if(messageIndex != -1)
                SingleTone.Instance.Level = Stage;
            // 대사 인덱스 초기화
            messageIndex = 0;
        }
        // NPC와 접촉해 있는 경우
        else if(Messages.Length > 0
            && SingleTone.Instance.collidedNPC == transform.name) {
            // 이 NPC와 대화할 적정 단계인 경우
            if(SingleTone.Instance.Level == Stage-1) {
                string message = Messages[messageIndex++];

                // 포스터 이미지 출력 명령인 경우
                if(message == "Poster") {
                    // 포스터 이미지 출력
                    poster.FindChild("Image").GetComponent<Image>().sprite = Poster;
                    poster.localScale = new Vector3(1f, 1f, 1f);
                } else {
                    // 대화 UI 출력
                    dialogue.ShowDialogue(Name, message);
                }
            }
            // 플레이어의 진행도가 낮은 경우
            else if(SingleTone.Instance.Level < Stage-1) {
                // 대화 거부 처리
                messageIndex = -1;
                // 대화 UI 출력
                if(dontChat)
                    NPCTalk();
                else
                    dialogue.ShowDialogue(Name, "나 말고 다른 사람한테 먼저 가봐.");
            }
            // 플레이어의 진행도가 높은 경우
            else if(SingleTone.Instance.Level > Stage-1) {
                // 대화 거부 처리
                messageIndex = -1;
                // 대화 UI 출력
                if(dontChat)
                    NPCTalk();
                else
                    dialogue.ShowDialogue(Name, "난 이제 해 줄 말이 없어.");
            }
        }
    }
}
