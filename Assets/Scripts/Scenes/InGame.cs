using UnityEngine;
using System.Collections;

public class InGame : MonoBehaviour {
    bool menuZoomIn = true;

    void Start() {
        GameObject.Find("Screen Fader").transform.localScale = new Vector3(0f, 0f, 0f);
        GameObject.Find("Menu UI").transform.localScale = new Vector3(0f, 0f, 0f);
    }
    
	void Update() {
        Transform fader = GameObject.Find("Screen Fader").transform;
        Transform menu = GameObject.Find("Menu UI").transform;
        
	    if(Input.GetKeyDown(KeyCode.Escape)) {
            // Screen Fader가 비활성화 되어 있는 경우
            if(fader.localScale == new Vector3(0f, 0f, 0f)) {
                // Screen Fader Fade In
                fader.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
                fader.GetComponent<ScreenFadeInOut>().endOpacity = .6f;
                // Screen Fader 활성화
                fader.localScale = new Vector3(2048f, 2048f, 1f);
                
                // Menu UI Zoom In
                menuZoomIn = true;
                menu.localScale = new Vector3(1f, 1f, 1f);
                menu.position = new Vector3(menu.position.x, 500f, menu.position.z);

                // 캐릭터 이동 불가
                GameObject.Find("Character").GetComponent<CharacterMove>().canmove = false;
            } else {
                Resume();
            }
        }
        
        if(menu.position.y > 0f) {
            float moved_y = menu.position.y - Time.deltaTime * 750;

            if(moved_y <= 0f)
                menu.position = new Vector3(menu.position.x, 0f, menu.position.z);
            else
                menu.position = new Vector3(menu.position.x, moved_y, menu.position.z);
        }

        /*if(menu.localScale.x > 0f && menu.localScale.x != 1f) {
            if(menuZoomIn) {
                if(menu.localScale.x+Time.deltaTime >= 1f)
                    menu.localScale = new Vector3(1f, 1f, 1f);
                else
                    menu.localScale = new Vector3(menu.localScale.x+2*Time.deltaTime, menu.localScale.y+2*Time.deltaTime, 1f);
            }
        }*/
	}

    public void Resume() {
        // Screen Fader Fade Out
        GameObject.Find("Screen Fader").GetComponent<ScreenFadeInOut>().endOpacity = 0f;
        // Menu UI 비활성화
        GameObject.Find("Menu UI").transform.localScale = new Vector3(0f, 0f, 0f);

        // 캐릭터 이동 가능
        GameObject.Find("Character").GetComponent<CharacterMove>().canmove = true;
    }

    void FadeEnded() {
        float opacity = GameObject.Find("Screen Fader").GetComponent<SpriteRenderer>().color.a;
        
        if(opacity == 0f)
            GameObject.Find("Screen Fader").transform.localScale = new Vector3(0f, 0f, 0f);
    }
}
