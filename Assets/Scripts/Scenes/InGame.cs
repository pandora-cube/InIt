using UnityEngine;
using System.Collections;

public class InGame : MonoBehaviour {
    void Start() {
        GameObject.Find("Screen Fader").transform.localScale = new Vector3(0f, 0f, 0f);
        GameObject.Find("Menu UI").transform.localScale = new Vector3(0f, 0f, 0f);
    }
    
	void Update() {
        Transform fader = GameObject.Find("Screen Fader").transform;
        
	    if(Input.GetKeyDown(KeyCode.Escape)) {
            Debug.Log("AAAA");
            // Screen Fader가 비활성화 되어 있는 경우
            if(fader.localScale == new Vector3(0f, 0f, 0f)) {
                // Screen Fader를 투명하게 함
                // (ScreenFadeInOut 컴포넌트에 의해 Fade In)
                fader.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
                // Screen Fader 활성화
                fader.localScale = new Vector3(2048f, 2048f, 1f);
                
                // Menu UI 활성화
                GameObject.Find("Menu UI").transform.localScale = new Vector3(1f, 1f, 1f);

                // 캐릭터 이동 불가
                GameObject.Find("Character").GetComponent<CharacterMove>().canmove = false;
            } else {
                Resume();
            }
        }
	}

    public void Resume() {
        // Screen Fader, Menu UI 비활성화
        GameObject.Find("Screen Fader").transform.localScale = GameObject.Find("Menu UI").transform.localScale = new Vector3(0f, 0f, 0f);

        // 캐릭터 이동 가능
        GameObject.Find("Character").GetComponent<CharacterMove>().canmove = true;
    }
}
