﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Option : MonoBehaviour {
    bool alreadyFaded = false;

    void Start() {
        // BGM 불륨
        GameObject.Find("BGM Area").transform.FindChild("Slider").GetComponent<Slider>().value = PlayerData.Option.volumeBGM;
        // 효과음 불륨
        GameObject.Find("Effects Area").transform.FindChild("Slider").GetComponent<Slider>().value = PlayerData.Option.volumeEffects;
    }

    public void Open() {
        Transform faderui = GameObject.Find("Screen Fader UI").transform;
        Transform fader = GameObject.Find("Screen Fader").transform;
        if(faderui.localScale == new Vector3(0f, 0f, 0f)) {
            // Fade Screen Fader In
            faderui.localScale = new Vector3(1f, 1f, 1f);
            fader.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
            fader.GetComponent<ScreenFader>().endOpacity = .6f;

            alreadyFaded = false;
        } else
            alreadyFaded = true;
        // Show Option UI
        GameObject.Find("Option UI").transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void Close() {
        if(!alreadyFaded) {
            // Fade Screen Fader Out
            GameObject.Find("Screen Fader").GetComponent<ScreenFader>().endOpacity = 0f;
        }
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