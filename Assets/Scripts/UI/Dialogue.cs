using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Dialogue : MonoBehaviour {
    public float printSpeed = 0.1f;     // 메시지 출력 속도
    public float blinkSpeed = 2f;       // Blinker의 페이드 속도
    public float blinkOpacity = 1f;     // Blinker의 최대 투명도
    public string[] Messages;           // 출력될 메시지
    public int msgCount = 0;            // 메시지 출력 카운트

    Transform uiObj;                    // UI 오브젝트
    Transform dialogueObj;              // 메시지 오브젝트
    Transform posterObj;                // 포스터 오브젝트
    string msgResult;                   // 메시지 내용
    int msgLength;                      // 메시지 길이
    float blinkSpeedTmp;                // Blinker의 페이드 속도
    float blinkOpacityTmp;              // Blinker의 최대 투명도

    void Start() {
        uiObj = GameObject.Find("Dialogue UI").transform;               // UI 오브젝트
        dialogueObj = uiObj.FindChild("Canvas").FindChild("Dialogue");  // 메시지 오브젝트
        posterObj = uiObj.FindChild("Canvas").FindChild("Poster");      // 포스터 오브젝트

        // 오브젝트 숨김
        uiObj.localScale = dialogueObj.localScale = posterObj.localScale = new Vector3(0f, 0f, 0f);
        
        // blinkSpeed와 blinkOpacity가 가변성을 가지므로 미리 처음 값을 백업함
        blinkSpeedTmp = blinkSpeed;
        blinkOpacityTmp = blinkOpacity;
    }

    void Update() {
        // Dialogue UI 우측 하단의 깜빡이
        Text blinker = dialogueObj.FindChild("Blinker").GetComponent<Text>();
        // 시간에 따른 투명도
        float opacity = blinker.color.a + blinkSpeedTmp * Time.deltaTime;
        
        // Blinker 투명도 설정
        blinker.color = new Color(blinker.color.r, blinker.color.g, blinker.color.b, opacity);
        if(opacity >= blinkOpacityTmp && blinkSpeedTmp >= 0f) {
            blinkSpeedTmp = -blinkSpeedTmp;
            blinkOpacityTmp = 0f;
        } else if(opacity <= 0f && blinkSpeedTmp <= 0f)
            blinkSpeedTmp = -blinkSpeedTmp;
            blinkOpacityTmp = blinkOpacity;
    }

    public void OnClick() {
        GameObject.Find(SingleTone.Instance.collidedNPC).GetComponent<NPC>().NPCTalk();
    }

    void PrintMessage() {
        // 메시지 오브젝트와 내용이 존재하는 경우
        if(dialogueObj && msgResult.Length > 0) {
            // 메시지 출력 방향에 따라 내용이 한 글자씩 추가되며 출력됨
            string tmp = msgResult.Substring(0, msgCount);
            dialogueObj.FindChild("Result").GetComponent<Text>().text = tmp;

            // 메시지 출력이 완료된 경우
            if(msgCount++ == msgLength) {
                // 카운트 초기화
                msgCount = 0;
                // 타이머 해제
                CancelInvoke("PrintMessage");
            }
        }
    }

    public void HideDialogue() {
        // 오브젝트 숨김
        uiObj.localScale = dialogueObj.localScale = posterObj.localScale = new Vector3(0f, 0f, 0f);
        // 타이머 강제 해제
        CancelInvoke("PrintMessage");
        // 캐릭터 이동 가능
        GameObject.Find("Character").GetComponent<CharacterMove>().canmove = true;
        // 초기화
        msgResult = string.Empty;
    }

    public void ShowDialogue(string name, string result) {
        // 이름 텍스트 설정
        dialogueObj.FindChild("Name").GetComponent<Text>().text = name;
        // Dialogue UI 활성화
        uiObj.localScale = dialogueObj.localScale =  new Vector3(1f, 1f, 1f);
        
        msgResult = result.Replace("\\n", "\n");            // 줄바꿈 문자 변환
        msgLength = msgResult.Length;                       // 메시지 길이
        msgCount = 0;                                       // 메시지 출력 카운트 초기화
        InvokeRepeating("PrintMessage", 0f, printSpeed);    // PrintMessage() 타이머 설정

        //캐릭터 이동 불가능
        GameObject.Find("Character").GetComponent<CharacterMove>().canmove = false;
    }
}
