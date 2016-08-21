using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class InGame : MonoBehaviour {
    void Start() {
        PlayerData.Player.Level = 0;

        GameObject.Find("Menu UI").transform.localScale = GameObject.Find("SaveMessage UI").transform.localScale = new Vector3(0f, 0f, 0f);
        
        if(PlayerData.flagLoadPlayerData)
            PlayerData.LoadPlayerData();
        else {
            // 현재 시간 등록
            System.DateTime now = System.DateTime.Now;
            PlayerData.Player.CreatedTime = string.Format("{0}년 {1}월 {2}일 {3}시", now.Year, now.Month, now.Day, now.Hour);
            GameObject.Find("Prologue").GetComponent<Prologue>().Run();
        }
    }
    
	void Update() {
        /*
         *  InGame.Update()
         *      메뉴 활성화 작업
         *      플레이시간 측정
         */

        /* 메뉴 활성화 작업 */
        Transform faderui = GameObject.Find("Screen Fader UI").transform;
        Transform fader = GameObject.Find("Screen Fader").transform;
        Transform menu = GameObject.Find("Menu UI").transform;
        
	    if(Input.GetKeyDown(KeyCode.Escape)
            && GameObject.Find("Dialogue UI").transform.localScale == new Vector3(0f, 0f, 0f)
            && GameObject.Find("Option UI").transform.localScale == new Vector3(0f, 0f, 0f)
            && GameObject.Find("Posters UI").transform.localScale == new Vector3(0f, 0f, 0f)) {
            // Screen Fader가 비활성화 되어 있는 경우
            if(faderui.localScale == new Vector3(0f, 0f, 0f)) {
                // Screen Fader Fade In
                fader.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
                fader.GetComponent<ScreenFader>().endOpacity = .6f;
                // Screen Fader 활성화
                faderui.localScale = new Vector3(1f, 1f, 1f);
                
                // Menu UI 활성화
                menu.localScale = new Vector3(1f, 1f, 1f);
                menu.position = new Vector3(menu.position.x, 500f, menu.position.z);

                // 캐릭터 이동 불가
                GameObject.Find("Character").GetComponent<CharacterMove>().canmove = false;
            } else {
                Resume();
            }
        }
        
        // 메뉴 내려오는 애니메이션
        if(menu.position.y > 0f) {
            float moved_y = menu.position.y - Time.deltaTime * 750;

            if(moved_y <= 0f)
                menu.position = new Vector3(menu.position.x, 0f, menu.position.z);
            else
                menu.position = new Vector3(menu.position.x, moved_y, menu.position.z);
        }

        /* 플레이시간 측정 */
        if(menu.localScale == new Vector3(0f, 0f, 0f)) {
            PlayerData.Player.PlayTime += Time.deltaTime;
        }
	}

    public void Resume() {
        // Screen Fader Fade Out
        GameObject.Find("Screen Fader").GetComponent<ScreenFader>().endOpacity = 0f;
        // Menu UI 비활성화
        GameObject.Find("Menu UI").transform.localScale = new Vector3(0f, 0f, 0f);

        // 캐릭터 이동 가능
        GameObject.Find("Character").GetComponent<CharacterMove>().canmove = true;
    }

    public void Save() {
        PlayerData.SavePlayerData();
    }

    public void Quit() {
		SceneManager.LoadScene("Scenes/Main");
    }

    public void SaveMessage_Hide() {
        GameObject.Find("SaveMessage UI").transform.localScale = new Vector3(0f, 0f, 0f);
    }
}
