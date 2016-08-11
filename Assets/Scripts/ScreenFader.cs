using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour {
    public float fadeSpeed = 1f;    // 페이드 속도
    public float endOpacity = 0.6f; // 페이드가 종료될 투명도값

	void Start() {
        GameObject.Find("Screen Fader UI").transform.localScale = new Vector3(0f, 0f, 0f);
	}

    void Update() {
        // 페이드가 적용될 스프라이트
        Image fader = GetComponent<Image>();
        // 페이드 속도의 절대값
        float deltaSpeed = Mathf.Abs(fadeSpeed) * Time.deltaTime;
        // 페이드 진행 시간에 따른 투명도 값 계산
        float opacity = fader.color.a + (fader.color.a < endOpacity ? deltaSpeed : -deltaSpeed);
        
        // 페이드 종료
        if(fader.color.a >= endOpacity-.05f && fader.color.a <= endOpacity+.05f) {
            fader.color = new Color(fader.color.r, fader.color.g, fader.color.b, endOpacity);
            // 투명도가 0인 경우 비활성화
            if(fader.color.a == 0f)
                GameObject.Find("Screen Fader UI").transform.localScale = new Vector3(0f, 0f, 0f);
        } else {
            // 투명도 적용
            fader.color = new Color(fader.color.r, fader.color.g, fader.color.b, opacity);
        }
    }
}
