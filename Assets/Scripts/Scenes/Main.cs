using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Main : MonoBehaviour {
    Slider solSlider;

    void Start() {
        solSlider = GameObject.Find("SaveOrLoad UI").transform.FindChild("Canvas").FindChild("Slider").GetComponent<Slider>();
        GameObject.Find("SaveOrLoad UI").transform.localScale = new Vector3(0f, 0f, 0f);
    }

	public void ClickPlayButton() {
        Transform faderui = GameObject.Find("Screen Fader UI").transform;
        Transform fader = GameObject.Find("Screen Fader").transform;
        Transform solui = GameObject.Find("SaveOrLoad UI").transform;

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

    public void SaveOrLoadUI_ValueChanged() {
        // Slider 핸들의 텍스트 설정
        solSlider.transform.FindChild("Handle Slide Area").FindChild("Handle").FindChild("Text").GetComponent<Text>().text = solSlider.value == 0 ? "새 게임" : "불러오기";
    }

    public void SaveOrLoadUI_Hide() {
        // SaveOrLoad UI 비활성화
        GameObject.Find("SaveOrLoad UI").transform.localScale = new Vector3(0f, 0f, 0f);
        // Screen Fader 페이드 아웃
        GameObject.Find("Screen Fader").GetComponent<ScreenFader>().endOpacity = 0f;
    }

    public void SaveOrLoadUI_Play() {
        PlayerData.flagLoadPlayerData = solSlider.value == 1;
        SceneManager.LoadScene("Scenes/FirstFloor");
    }
}
