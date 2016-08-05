using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {
    public float talkDistance = 1f; // 대화 가능한 최소 거리
    public string Name;             // NPC의 이름
    public int Stage;               // 게임 진행 순차 중 이 NPC의 순번
    public string[] Messages;       // NPC의 대사 배열
    
    int messageIndex = 0;           // 대사 인덱스
    bool keyDowned = false;         // 키 눌림 여부

    void Update() {
        // Space 혹은 Enter 키를 누른 경우
        if(!keyDowned && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))) {
            keyDowned = true;
            NPCTalk();
        }
        // Space 혹은 Enter 키를 뗀 경우
        else if((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.KeypadEnter)))
            keyDowned = false;
    }

    public void NPCTalk() {
        // 캐릭터 좌표
        Vector3 chrPos = GameObject.Find("Character").GetComponent<SpriteRenderer>().transform.position;
        // NPC 좌표
        Vector3 npcPos = GetComponent<SpriteRenderer>().transform.position;
        // 대화 UI
        Dialogue dialogue = GameObject.Find("Dialogue UI").GetComponent<Dialogue>();
        
        // NPC가 대화를 거부하였거나 더 할 대사가 없는 경우
        if(messageIndex == -1 || messageIndex >= Messages.Length) {
            // 캐릭터 이동 가능
            GameObject.Find("Character").GetComponent<CharacterMove>().canmove = true;
            // 대화 UI 숨김
            GameObject.Find("Dialogue UI").GetComponent<Dialogue>().HideDialogue();
            // 레벨 설정
            if(messageIndex != -1)
                SingleTone.Instance.Level = Stage;
            // 대사 인덱스 초기화
            messageIndex = 0;
        }
        // NPC와 대화 가능한 거리에 있는 경우
        else if(Messages.Length > 0
            && chrPos.x >= npcPos.x-talkDistance && chrPos.x <= npcPos.x+talkDistance
            && chrPos.y >= npcPos.y-talkDistance && chrPos.y <= npcPos.y+talkDistance) {
            // 캐릭터 이동 불가능
            GameObject.Find("Character").GetComponent<CharacterMove>().canmove = false;

            // 이 NPC와 대화할 적정 단계인 경우
            if(SingleTone.Instance.Level == Stage-1) {
                // 대화 UI 출력
                dialogue.ShowDialogue(Name, Messages[messageIndex++]);
            }
            // 플레이어의 진행도가 낮은 경우
            else if(SingleTone.Instance.Level < Stage-1) {
                // 대화 거부 처리
                messageIndex = -1;
                // 대화 UI 출력
                dialogue.ShowDialogue(Name, "나 말고 다른 사람한테 먼저 가봐.");
            }
            // 플레이어의 진행도가 높은 경우
            else if(SingleTone.Instance.Level > Stage-1) {
                // 대화 거부 처리
                messageIndex = -1;
                // 대화 UI 출력
                dialogue.ShowDialogue(Name, "난 이제 해 줄 말이 없어.");
            }
        }
    }
}
