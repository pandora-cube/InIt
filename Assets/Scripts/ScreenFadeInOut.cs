using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScreenFadeInOut : MonoBehaviour {
	public float fadeSpeed;             // 페이드 속도
    public float endOpacity;            // 페이드가 종료될 투명도값

	void Update() {
        // 페이드가 적용될 스프라이트
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        // 페이드 속도의 절대값
        float deltaSpeed = Mathf.Abs(fadeSpeed) * Time.deltaTime;
        // 페이드 진행 시간에 따른 투명도 값 계산
        float opacity = sprite.color.a + (sprite.color.a < endOpacity ? deltaSpeed : -deltaSpeed);
        
        // 페이드 종료 콜백 호출
        if(sprite.color.a >= endOpacity-.05f && sprite.color.a <= endOpacity+.05f) {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, endOpacity);
            foreach(Transform obj in GameObject.FindObjectsOfType<Transform>())
                obj.SendMessage("FadeEnded", SendMessageOptions.DontRequireReceiver);
        } else {
            // 스프라이트에 투명도 적용
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, opacity);
        }
	}
}
