using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Dialogue : MonoBehaviour {
    public float printSpeed = 0.1f;     // 메시지 출력 속도
    public float blinkSpeed = 2f;       // Blinker의 페이드 속도
    public float blinkOpacity = 1f;     // Blinker의 최대 투명도
    public string[] Messages;           // 출력될 메시지
    public int msgCount = 0;            // 메시지 출력 카운트

    GameObject msgObj;                  // 메시지 오브젝트
    bool msgRight;                      // 메시지 출력 방향
    string msgResult;                   // 메시지 내용
    int msgLength;                      // 메시지 길이
    float blinkSpeedTmp;                // Blinker의 페이드 속도
    float blinkOpacityTmp;              // Blinker의 최대 투명도

    void Start() {
        GameObject canvasObj = GameObject.Find("Dialogue UI").transform.FindChild("Canvas").gameObject;
        GameObject rightObj = canvasObj.transform.FindChild("Right").gameObject;
        GameObject leftObj = canvasObj.transform.FindChild("Left").gameObject;
        GameObject posterObj = canvasObj.transform.FindChild("Poster").gameObject;

        rightObj.transform.localScale = leftObj.transform.localScale = posterObj.transform.localScale = new Vector3(0f, 0f, 0f);
        
        blinkSpeedTmp = blinkSpeed;
        blinkOpacityTmp = blinkOpacity;
    }

    void Update() {
        Text blinker = GameObject.Find("Dialogue UI").transform.FindChild("Canvas").transform.FindChild(msgRight ? "Right" : "Left").transform.FindChild("Blinker").GetComponent<Text>();
        float opacity = blinker.color.a + blinkSpeedTmp * Time.deltaTime;
        
        blinker.color = new Color(blinker.color.r, blinker.color.g, blinker.color.b, opacity);
        if(opacity >= blinkOpacityTmp && blinkSpeedTmp >= 0f) {
            blinkSpeedTmp = -blinkSpeedTmp;
            blinkOpacityTmp = 0f;
        } else if(opacity <= 0f && blinkSpeedTmp <= 0f)
            blinkSpeedTmp = -blinkSpeedTmp;
            blinkOpacityTmp = blinkOpacity;
    }

    public void OnClick() {
        foreach(GameObject npc in GameObject.FindGameObjectsWithTag("NPC")) {
            npc.GetComponent<NPC>().NPCTalk();
        }
    }

    void PrintMessage() {
        // 메시지 오브젝트와 내용이 존재하는 경우
        if(msgObj && msgResult.Length > 0) {
            // 메시지 출력 방향에 따라 내용이 한 글자씩 추가되며 출력됨
            string tmp = msgRight ? msgResult.Substring(msgResult.Length-msgCount) : msgResult.Substring(0, msgCount);
            msgObj.transform.FindChild("Result").GetComponent<Text>().text = tmp;

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
        if(msgObj)
            msgObj.transform.localScale = new Vector3(0f, 0f, 0f);
        // 타이머 강제 해제
        CancelInvoke("PrintMessage");
        // 초기화
        msgObj = null;
        msgResult = string.Empty;
    }

    public void ShowDialogue(string name, string result, bool isright=false) {
        GameObject uiObj = GameObject.Find("Dialogue UI");
        msgObj = uiObj.transform.FindChild("Canvas").transform.FindChild(isright ? "Right" : "Left").gameObject;
        
        msgObj.transform.FindChild("Name").GetComponent<Text>().text = name;
        msgObj.transform.localScale = new Vector3(1f, 1f, 1f);

        msgRight = isright;
        msgResult = result.Replace("\\n", "\n");
        msgLength = msgResult.Length;
        msgCount = 0;
        InvokeRepeating("PrintMessage", 0f, printSpeed);
    }
}
