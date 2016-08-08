using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Option : MonoBehaviour {
    bool alreadyFaded = false;

    void Start() {
        // BGM 불륨
        GameObject.Find("BGM Area").transform.FindChild("Slider").GetComponent<Slider>().value = SingleTone.Instance.volumeBGM;
        // 효과음 불륨
        GameObject.Find("Effects Area").transform.FindChild("Slider").GetComponent<Slider>().value = SingleTone.Instance.volumeEffects;
    }

    public void Open() {
        Transform fader = GameObject.Find("Screen Fader").transform;
        if(fader.localScale == new Vector3(0f, 0f, 0f)) {
            // Fade Screen Fader In
            fader.localScale = new Vector3(2048f, 2048f, 1f);
            fader.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
            fader.GetComponent<ScreenFadeInOut>().endOpacity = .6f;

            alreadyFaded = false;
        } else
            alreadyFaded = true;
        // Hide Menu UI
        GameObject.Find("Menu UI").transform.localScale = new Vector3(0f, 0f, 0f);
        // Show Option UI
        GameObject.Find("Option UI").transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void Close() {
        if(!alreadyFaded) {
            // Fade Screen Fader Out
            GameObject.Find("Screen Fader").GetComponent<ScreenFadeInOut>().endOpacity = 0f;
        }
        // Show Menu UI
        GameObject.Find("Menu UI").transform.localScale = new Vector3(1f, 1f, 1f);
        // Hide Option UI
        GameObject.Find("Option UI").transform.localScale = new Vector3(0f, 0f, 0f);
    }

    public void ClickApplyButton() {
        Close();
    }

    public void ClickCancelButton() {
        Close();
    }
}
