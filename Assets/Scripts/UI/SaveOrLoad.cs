using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SaveOrLoad : MonoBehaviour {
    Transform solUI;
    Transform solCanvas;
    Slider solSlider;
    Image solSummary;
    float BackgroundOH;         // Background의 원래 Height
    float TopBarOP;             // TopBar의 원래 Y좌표
    float SliderOP;             // Slider의 원래 Y좌표
    float PlayButtonOP;         // Play Button의 원래 Y좌표
    float TopBarMP = 120f;      // TopBar의 이동될 Y좌표
    float SliderMP = 40f;       // Slider의 이동될 Y좌표
    float PlayButtonMP = -110f; // Play Button의 이동될 Y좌표

    void Start() {
        solUI = GameObject.Find("SaveOrLoad UI").transform;
        solCanvas = solUI.FindChild("Canvas");
        solSlider = solCanvas.FindChild("Slider").GetComponent<Slider>();
        solSummary = solCanvas.FindChild("Summary").GetComponent<Image>();

        BackgroundOH = solCanvas.FindChild("Background").GetComponent<RectTransform>().sizeDelta.y;
        TopBarOP = solCanvas.FindChild("TopBar").position.y;
        SliderOP = solCanvas.FindChild("Slider").position.y;
        PlayButtonOP = solCanvas.FindChild("Play Button").position.y;

        solUI.localScale = new Vector3(0f, 0f, 0f);
    }

    void Update() {
        if(solSlider.value == 0) {
            if(solSummary.color.a != 0f) {
                float a = solSummary.color.a - Time.deltaTime;
                Text text = solSummary.transform.FindChild("Text").GetComponent<Text>();
                
                a = a <= 0f ? 0f : a;
                solSummary.color = new Color(solSummary.color.r, solSummary.color.g, solSummary.color.b, a);
                text.color = new Color(text.color.r, text.color.g, text.color.b, a);
            } else {
                bool flag = true;
                RectTransform bg = solCanvas.FindChild("Background").GetComponent<RectTransform>();
                Transform topbar = solCanvas.FindChild("TopBar");
                Transform slider = solCanvas.FindChild("Slider");
                Transform playbutton = solCanvas.FindChild("Play Button");
                float speed = 50f * Time.deltaTime;
                
                if(bg.sizeDelta.y > BackgroundOH) {
                    flag = false;
                    if(bg.sizeDelta.y-speed <= BackgroundOH)
                        bg.sizeDelta = new Vector2(bg.sizeDelta.x, BackgroundOH);
                    else
                        bg.sizeDelta = new Vector2(bg.sizeDelta.x, bg.sizeDelta.y-speed);
                }
                if(topbar.position.y > TopBarOP) {
                    flag = false;
                    if(topbar.position.y-speed <= TopBarOP)
                        topbar.position = new Vector3(0f, TopBarOP, 0f);
                    else
                        topbar.position = new Vector3(0f, topbar.position.y-speed, 0f);
                }
                if(slider.position.y > SliderOP) {
                    flag = false;
                    if(slider.position.y-speed <= SliderOP)
                        slider.position = new Vector3(0f, SliderOP, 0f);
                    else
                        slider.position = new Vector3(0f, slider.position.y-speed, 0f);
                }
                if(playbutton.position.y < PlayButtonOP) {
                    flag = false;
                    if(playbutton.position.y+speed >= PlayButtonOP)
                        playbutton.position = new Vector3(0f, PlayButtonOP, 0f);
                    else
                        playbutton.position = new Vector3(0f, playbutton.position.y+speed, 0f);
                }

                if(flag) {
                }
            }
        }
    }

    public void OnValueChanged() {
        // Slider 핸들의 텍스트 설정
        solSlider.transform.FindChild("Handle Slide Area").FindChild("Handle").FindChild("Text").GetComponent<Text>().text = solSlider.value == 0 ? "새 게임" : "불러오기";

        // 불러오기를 선택하였을 때
        if(solSlider.value == 1) {
            // 데이터를 임시로 불러옴
            PlayerData.PLAYER temp = PlayerData.ReadPlayerData();
            int ptime = (int)temp.PlayTime;
            string ptimetext = string.Empty;
            Transform summary = solCanvas.FindChild("Summary");
            
            if(ptime/3600 > 0)
                ptimetext += string.Format(" {0}시간", ptime/3600);
            if((ptime%3600)/60 > 0)
                ptimetext += string.Format(" {0}분", (ptime%3600)/60);
            if(ptime%60 > 0)
                ptimetext += string.Format(" {0}초", ptime%60);

            summary.FindChild("Text").GetComponent<Text>().text = string.Format("위치: {0}\n시작한 시각: {1}\n플레이 시간:{2}", temp.Location.Split('_')[1], temp.CreatedTime, ptimetext);
            summary.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void Hide() {
        // SaveOrLoad UI 비활성화
        solUI.localScale = new Vector3(0f, 0f, 0f);
        // Screen Fader 페이드 아웃
        GameObject.Find("Screen Fader").GetComponent<ScreenFader>().endOpacity = 0f;
    }

    public void Play() {
        PlayerData.flagLoadPlayerData = solSlider.value == 1;
        SceneManager.LoadScene("Scenes/FirstFloor");
    }
}
