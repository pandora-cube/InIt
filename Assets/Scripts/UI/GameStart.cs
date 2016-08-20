using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameStart : MonoBehaviour {
    Transform gsUI;
    Transform gsCanvas;
    Slider gsSlider;
    Image gsSummary;

    float BgTopOP = 70f;            // Background_Top의 Y좌표 (새 게임 선택시)
    float BgBotOP = -70f;           // Background_Bottom의 Y좌표 (새 게임 선택시)
    float SliderOP = 0f;            // Slider의 Y좌표 (새 게임 선택시)
    float PlayButtonOP = -60f;      // Play Button의 Y좌표 (새 게임 선택시)

    float BgTopMP = 120f;           // Background_Top의 Y좌표 (불러오기 선택시)
    float BgBotMP = -120f;          // Background_Bottom의 Y좌표 (불러오기 선택시)
    float SliderMP = 50f;           // Slider의 Y좌표 (불러오기 선택시)
    float PlayButtonMP = -110f;     // Play Button의 Y좌표 (불러오기 선택시)

    void Start() {
        gsUI = GameObject.Find("GameStart UI").transform;
        gsCanvas = gsUI.FindChild("Canvas");
        gsSlider = gsCanvas.FindChild("Slider").GetComponent<Slider>();
        gsSummary = gsCanvas.FindChild("Summary").GetComponent<Image>();

        gsSlider.value = 0f;
        gsCanvas.FindChild("Background_Top").localPosition = new Vector3(0f, BgTopOP, 0f);
        gsCanvas.FindChild("Background_Bottom").localPosition = new Vector3(0f, BgBotOP, 0f);
        gsCanvas.FindChild("Slider").localPosition = new Vector3(0f, SliderOP, 0f);
        gsCanvas.FindChild("Play Button").localPosition = new Vector3(0f, PlayButtonOP, 0f);
        
        gsUI.localScale = gsSummary.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    // Summary와 Text의 투명도 설정
    bool SetSummaryOpacity() {
        bool flag = true;               // 투명도가 변경되지 않은 경우 true
        float a = gsSummary.color.a;    // Summary와 하위 Text의 투명도
        Text text = gsSummary.transform.FindChild("Text").GetComponent<Text>();
        Text del = gsSummary.transform.FindChild("Delete Button").FindChild("Text").GetComponent<Text>();
        
        if(gsSlider.value == 0f) {
            if(a != 0f)
                flag = false;
            else
                gsSummary.transform.localScale = new Vector3(0f, 0f, 0f);

            // 새 게임이 선택되어 있을 땐 투명도를 0f까지 내린다.
            a -= Time.deltaTime;
            a = a <= 0f ? 0f : a;
        } else {
            if(a != 1f)
                flag = true;
            gsSummary.transform.localScale = new Vector3(1f, 1f, 1f);

            // 불러오기가 선택되어 있을 땐 투명도를 1f까지 올린다.
            a += Time.deltaTime;
            a = a >= 1f ? 1f : a;
        }
        
        // Summary와 하위 Text의 투명도를 설정한다.
        gsSummary.color = new Color(gsSummary.color.r, gsSummary.color.g, gsSummary.color.b, a);
        text.color = new Color(text.color.r, text.color.g, text.color.b, a);
        del.color = new Color(del.color.r, del.color.g, del.color.b, a);
        
        return flag;
    }

    // UI 구성 오브젝트들의 위치 설정
    bool SetObjectsPosition() {
        bool flag = true; // 위치가 변경되지 않은 경우 true
        
        Transform bgtop = gsCanvas.FindChild("Background_Top");
        Transform bgbot = gsCanvas.FindChild("Background_Bottom");
        Transform slider = gsCanvas.FindChild("Slider");
        Transform playbutton = gsCanvas.FindChild("Play Button");

        // 오브젝트들의 이동 속도
        float speed = 50f * Time.deltaTime;
        // 오브젝트들의 이동될 Y좌표
        float bgtopTo = bgtop.localPosition.y;
        float bgbotTo = bgbot.localPosition.y;
        float sliderTo = slider.localPosition.y;
        float playbuttonTo = playbutton.localPosition.y;

        if(gsSlider.value == 0f) {
            if(bgtopTo != BgTopOP || bgbotTo != BgBotOP || sliderTo != SliderOP || playbuttonTo != PlayButtonOP)
                flag = false;

            bgtopTo -= speed;
            bgbotTo += speed;
            sliderTo -= speed;
            playbuttonTo  += speed;

            bgtopTo = bgtopTo <= BgTopOP ? BgTopOP : bgtopTo;
            bgbotTo = bgbotTo >= BgBotOP ? BgBotOP : bgbotTo;
            sliderTo = sliderTo <= SliderOP ? SliderOP : sliderTo;
            playbuttonTo = playbuttonTo >= PlayButtonOP ? PlayButtonOP : playbuttonTo;
        } else {
            if(bgtopTo != BgTopOP || bgbotTo != BgBotOP || sliderTo != SliderOP || playbuttonTo != PlayButtonOP)
                flag = false;
            
            bgtopTo += speed;
            bgbotTo -= speed;
            sliderTo += speed;
            playbuttonTo -= speed;

            bgtopTo = bgtopTo >= BgTopMP ? BgTopMP : bgtopTo;
            bgbotTo = bgbotTo <= BgBotMP ? BgBotMP : bgbotTo;
            sliderTo = sliderTo >= SliderMP ? SliderMP : sliderTo;
            playbuttonTo = playbuttonTo <= PlayButtonMP ? PlayButtonMP : playbuttonTo;
        }
        
        bgtop.localPosition = new Vector3(0f, bgtopTo, 0f);
        bgbot.localPosition = new Vector3(0f, bgbotTo, 0f);
        slider.localPosition = new Vector3(0f, sliderTo, 0f);
        playbutton.localPosition = new Vector3(0f, playbuttonTo, 0f);

        return flag;
    }

    void Update() {
        SetSummaryOpacity();
        SetObjectsPosition();
    }

    public void OnValueChanged() {
        // Slider 핸들의 텍스트 설정
        gsSlider.transform.FindChild("Handle Slide Area").FindChild("Handle").FindChild("Text").GetComponent<Text>().text = gsSlider.value == 0 ? "새 게임" : "불러오기";

        // 불러오기를 선택하였을 때
        if(gsSlider.value == 1) {
            // 데이터를 임시로 불러옴
            PlayerData.PLAYER temp = PlayerData.ReadPlayerData();
            Transform summary = gsCanvas.FindChild("Summary");
            string summarytext;

            if(temp == null) {
                summarytext = "불러올 데이터가 없습니다!\n선택 시 새 게임을 시작합니다.";
                GameObject.Find("Delete Button").transform.localScale = new Vector3(0f, 0f, 0f);
            }
            else {
                int ptime = (int)temp.PlayTime;
                string ptimetext = string.Empty;
            
                if(ptime/3600 > 0)
                    ptimetext += string.Format(" {0}시간", ptime/3600);
                if((ptime%3600)/60 > 0)
                    ptimetext += string.Format(" {0}분", (ptime%3600)/60);
                if(ptime%60 > 0)
                    ptimetext += string.Format(" {0}초", ptime%60);

                summarytext = string.Format("위치: {0}\n시작한 시각: {1}\n플레이 시간:{2}", temp.Location.Split('_')[1], temp.CreatedTime, ptimetext);
            }
            summary.FindChild("Text").GetComponent<Text>().text = summarytext;
        }
    }

    public void Hide() {
        // GameStart UI 비활성화
        gsUI.localScale = new Vector3(0f, 0f, 0f);
        // Screen Fader 페이드 아웃
        GameObject.Find("Screen Fader").GetComponent<ScreenFader>().endOpacity = 0f;
    }

    public void DeletePlayerData() {
        PlayerData.DeletePlayerData();
        OnValueChanged();
    }

    public void Play() {
        PlayerData.flagLoadPlayerData = gsSlider.value == 1 && PlayerData.ReadPlayerData() != null;
        SceneManager.LoadScene("Scenes/FirstFloor");
    }
}
