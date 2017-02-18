using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Prologue : MonoBehaviour {
    public float fadeSpeed = 1f;

    int Step = 0;
    Transform canvas;
    bool delayToggle = false;

    void Start() {
        canvas = transform.FindChild("Canvas");
        transform.localScale = new Vector3(0f, 0f, 0f);
    }

    void Update() {
        switch(Step) {
            case 1:
				GameObject.Find("Character").GetComponent<CharacterMove>().canmove = false;
                transform.localScale = new Vector3(1f, 1f, 1f);
                canvas.FindChild("Background").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                canvas.FindChild("1").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                SetDelay();
                break;
            case 2:
                Fade("1", fadeSpeed, false);
                break;
            case 3:
                Fade("2", fadeSpeed);
                break;
            case 4:
                SetDelay();
                break;
            case 5:
                Fade("2", fadeSpeed, false);
                break;
            case 6:
                Fade("3", fadeSpeed);
                break;
            case 7:
                SetDelay();
                break;
            case 8:
                Fade("3", fadeSpeed, false);
                break;
            case 9:
                Fade("4", fadeSpeed);
                break;
            case 10:
                SetDelay();
                break;
            case 11:
                Fade("4", fadeSpeed, false);
                break;
            case 12:
                Fade("Background", fadeSpeed/3f, false);
                break;
            case 13:
                transform.localScale = new Vector3(0f, 0f, 0f);
                break;
        }
    }

    void Fade(string imgName, float speed, bool upper=true) {
        Image img = canvas.FindChild(imgName).GetComponent<Image>();
        img.color = new Color(1f, 1f, 1f, img.color.a+Time.deltaTime*speed*(upper ? 1 : -1));
        if((upper && img.color.a >= 1f) || (!upper && img.color.a <= 0.01f))
            Step++;
    }

    void SetDelay() {
        if(!delayToggle) {
            delayToggle = true;
            Invoke("Delay", 3f);
        }
    }

    void Delay() {
        delayToggle = false;
        Step++;
    }

    public void Run() {
        Step = 1;
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
