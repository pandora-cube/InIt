using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Ending : MonoBehaviour {
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
                transform.localScale = new Vector3(1f, 1f, 1f);
                Fade("Background", fadeSpeed/3f);
                break;
            case 2:
                Fade("1", fadeSpeed);
                break;
            case 3:
                SetDelay();
                break;
            case 4:
                Fade("1", fadeSpeed, false);
                break;
            case 5:
                Fade("2", fadeSpeed);
                break;
            case 6:
                SetDelay();
                break;
            case 7:
                Fade("2", fadeSpeed, false);
                break;
            case 8:
                Fade("3", fadeSpeed);
                break;
            case 9:
                canvas.FindChild("3").GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                canvas.FindChild("4").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                SetDelay(.5f);
                break;
            case 10:
                canvas.FindChild("4").GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                canvas.FindChild("5").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                SetDelay(.5f);
                break;
            case 11:
                canvas.FindChild("5").GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                canvas.FindChild("6").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                SetDelay(.5f);
                break;
            case 12:
                Fade("6", fadeSpeed, false);
                break;
            case 13:
                Fade("7", fadeSpeed);
                SetDelay(1f);
                break;
            case 14:
                Fade("8", fadeSpeed);
                break;
            case 15:
                canvas.FindChild("7").GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                SetDelay();
                break;
            case 16:
                Fade("8", fadeSpeed, false);
                break;
            case 17:
                SetDelay();
                break;
            case 18:
                SceneManager.LoadScene("Scenes/Intro");
                break;
        }
    }

    void Fade(string imgName, float speed, bool upper=true) {
        Image img = canvas.FindChild(imgName).GetComponent<Image>();
        img.color = new Color(1f, 1f, 1f, img.color.a+Time.deltaTime*speed*(upper ? 1 : -1));
        if((upper && img.color.a >= 1f) || (!upper && img.color.a <= 0.01f))
            Step++;
    }

    void SetDelay(float time=3f) {
        if(!delayToggle) {
            delayToggle = true;
            Invoke("Delay", time);
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
