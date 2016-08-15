using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Main : MonoBehaviour {
	public void ClickPlayButton() {
        Transform faderui = GameObject.Find("Screen Fader UI").transform;
        Transform fader = GameObject.Find("Screen Fader").transform;
        Transform solui = GameObject.Find("GameStart UI").transform;

        // Screen Fader 페이드 인
        faderui.localScale = new Vector3(1f, 1f, 1f);
        fader.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
        fader.GetComponent<ScreenFader>().endOpacity = .6f;

        // SaveOrLoad UI 활성화
        solui.localScale = new Vector3(1f, 1f, 1f);
	}

    public void ClickQuitButton() {
        Application.Quit();
    }
}
