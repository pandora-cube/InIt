using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {
    public float talkDistance = 1f; // 대화 가능한 최소 거리
    public string Name;             // NPC의 이름
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

        // NPC가 더 할 대사가 없는 경우
        if(messageIndex >= Messages.Length) {
            // 캐릭터 이동 가능
            GameObject.Find("Character").GetComponent<CharacterMove>().canmove = true;
            // 대화 UI 숨김
            GameObject.Find("Dialogue UI").GetComponent<Dialogue>().HideDialogue();
            // 대사 인덱스 초기화
            messageIndex = 0;
        }
        // NPC와 대화 가능한 거리에 있는 경우
        else if(Messages.Length > 0
            && chrPos.x >= npcPos.x-talkDistance && chrPos.x <= npcPos.x+talkDistance
            && chrPos.y >= npcPos.y-talkDistance && chrPos.y <= npcPos.y+talkDistance) {
            // 캐릭터 이동 불가능
            GameObject.Find("Character").GetComponent<CharacterMove>().canmove = false;
            // 대화 UI 출력
            GameObject.Find("Dialogue UI").GetComponent<Dialogue>().ShowDialogue(Name, Messages[messageIndex++]);
        }
    }
}
