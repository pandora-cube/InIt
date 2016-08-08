using UnityEngine;
using System.Collections;

public class ScreenFader : MonoBehaviour {
	void Start() {
        GameObject.Find("Screen Fader").transform.localScale = new Vector3(0f, 0f, 0f);
	}

    void FadeEnded() {
        float opacity = GetComponent<SpriteRenderer>().color.a;
        
        if(opacity == 0f)
            transform.localScale = new Vector3(0f, 0f, 0f);
    }
}
