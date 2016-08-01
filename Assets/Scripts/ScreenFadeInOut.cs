using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScreenFadeInOut : MonoBehaviour {
	public float fadeSpeed;             // 페이드 속도
    public float endOpacity;            // 페이드가 종료될 투명도값

	void Update() {
        // 페이드가 적용될 스프라이트
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        // 페이드 진행 시간에 따른 투명도 값 계산
        float opacity = sprite.color.a + fadeSpeed * Time.deltaTime;

        // 스프라이트에 투명도 적용
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, opacity);
        // 페이드 종료 콜백 호출
        if((fadeSpeed < 0f && opacity < endOpacity) || (fadeSpeed >= 0f && opacity > endOpacity))
            Camera.main.GetComponent<Intro>().FadeEnded();
	}
}
