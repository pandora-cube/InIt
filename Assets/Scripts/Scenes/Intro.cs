using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Intro : MonoBehaviour {
    public float fadeSpeed = .5f;
    int Step = 0;
    bool delayToggle = false;

    void Start() {
        // 화면 크기 조정 (960 x 540)
        Screen.SetResolution(960, 540, false);
    }

    void Update() {
        switch(Step) {
            case 0:
                Fade(1);
                break;
            case 1:
                SetDelay(1f);
                break;
            case 2:
                Hide(1);
                Show(2);
                SetDelay(.5f);
                break;
            case 3:
                Hide(2);
                Show(3);
                SetDelay(3f);
                break;
            case 4:
                Fade(3, false);
                break;
            case 5:
                SceneManager.LoadScene("Scenes/Main");
                break;
        }
    }
    
    void Show(int id) {
        GameObject.Find(id.ToString()).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
    }

    void Hide(int id) {
        GameObject.Find(id.ToString()).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
    }

    void Fade(int id, bool upper=true) {
        Image img = GameObject.Find(id.ToString()).GetComponent<Image>();
        img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a+Time.deltaTime*fadeSpeed*(upper ? 1 : -1));
        if((upper && img.color.a >= 1f) || (!upper && img.color.a <= 0.01f))
            Step++;
    }

    void SetDelay(float time) {
        if(!delayToggle) {
            delayToggle = true;
            Invoke("Delay", time);
        }
    }

    void Delay() {
        delayToggle = false;
        Step++;
    }
}
