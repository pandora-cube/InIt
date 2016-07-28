using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScreenFadeInOut : MonoBehaviour {
	public float fadeSpeed;
    public float endOpacity;
	SpriteRenderer[] spriteFadeList;

	void Start() {
		spriteFadeList = GetComponentsInChildren<SpriteRenderer>();
	}

	void Update() {
		foreach(SpriteRenderer sprite in spriteFadeList) {
            float opacity = sprite.color.a + fadeSpeed * Time.deltaTime;

            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, opacity);
            if((fadeSpeed < 0f && opacity < endOpacity) || (fadeSpeed >= 0f && opacity > endOpacity))
                SceneManager.LoadScene("Scenes/Main");
        }
	}

    void FadeStart() {
        GameObject goEmpty = new GameObject("FadeObject");
        goEmpty.AddComponent<ScreenFadeInOut>();

        SpriteRenderer[] spriteList = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in spriteList) {
            sprite.tag = "";
            Collider2D[] col2DList = GetComponentsInChildren<Collider2D>();
            foreach (Collider2D col2D in col2DList) {
                col2D.enabled = false;
            }
        }
        goEmpty.transform.position = transform.position;
    }
}
