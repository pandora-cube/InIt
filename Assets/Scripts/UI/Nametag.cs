using UnityEngine;
using System.Collections;

public class Nametag : MonoBehaviour {
    public GameObject Prefab;       // 네임태그 프리팹
    public float tagOffset = .7f;   // 부모 오브젝트로부터의 네임태그의 거리
    public string tagText;          // 네임태그의 텍스트
    public bool Blink = false;      // 이 속성이 참이면 네임태그가 강조 처리됨

    new Object tag;                 // 네임태그 오브젝트
    string tagName;                 // 네임태그 오브젝트의 이름
    Color originalColor;            // 네임태그의 초기 색상
    bool blinkMode = true;          // 이 속성이 참이면 강조 처리중 네임태그 색상의 R값이 더해짐
    bool textChanged = false;       // 네임태그의 텍스트가 변경되었는가

	void Start() {
        // 네임태그 오브젝트 생성
	    tag = Instantiate(Prefab, new Vector3(0f, 0f), transform.rotation);
        // 네임태그 오브젝트의 이름 설정
        tag.name = tagName =  "Nametag/" + transform.tag + "/" + transform.name;

        // 네임태그의 TextMesh 컴포넌트
        TextMesh text = GameObject.Find(tag.name).GetComponent<TextMesh>();

        // 네임태그 오브젝트의 텍스트 초기화
        SetNametagTextToDefault();
        // 네임태그 오브젝트의 텍스트 크기
        text.fontSize = 30;
        // 초기 상태의 네임태그 색상
        originalColor = text.color;
	}
	
	void Update() {
        /*
         *  Nametag.Update()
         *      좌표 동기화
         *      Blink
         *      텍스트 설정
         */
        
        /* 좌표 동기화 */
        // 부모 오브젝트의 좌표
        Vector3 parentPosition = transform.position;
        // 네임태그의 좌표
        // 네임태그가 화면상 가장 앞에 오도록 Z좌표를 설정한다.
        GameObject.Find(tag.name).transform.position = new Vector3(parentPosition.x, parentPosition.y+tagOffset, -3f);

        TextMesh text = GameObject.Find(tagName).GetComponent<TextMesh>();

        /* Blink */
	    if(Blink) {
            float r = text.color.r + (blinkMode ? Time.deltaTime : -Time.deltaTime) / 2f;
            
            if(r >= .75f)
                blinkMode = false;
            else if(r <= .2f)
                blinkMode = true;
            
            text.color = new Color(r, text.color.g, text.color.b);
        } else {
            // 네임태그의 색상을 초기 상태로 복원
            text.color = originalColor;
        }

        /* 텍스트 설정 */
        if(!textChanged)
            SetNametagTextToDefault();
	}

    public void SetNametagTextToDefault() {
        TextMesh text = GameObject.Find(tag.name).GetComponent<TextMesh>();

        text.text = Blink ? "클릭하세요" : tagText;
        text.GetComponent<BoxCollider2D>().enabled = text.text.Length != 0;
        textChanged = false;
    }

    public void SetNametagText(string text) {
        GameObject.Find(tag.name).GetComponent<TextMesh>().text = text;
        GameObject.Find(tag.name).GetComponent<BoxCollider2D>().enabled = text.Length != 0;
        textChanged = true;
        CancelInvoke("SetNametagTextToDefault");
        Invoke("SetNametagTextToDefault", .75f);
    }
}
