using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Option : MonoBehaviour {
    GameObject[] arrObject;
    float[] arrOpacity;

    void Start() {
        /*
         *  Option.Start()
         *      초기화
         *      설정 값 동기화
         */

        /* 초기화 */
        arrObject = GameObject.FindGameObjectsWithTag("Background");
        arrOpacity = new float[arrObject.Length];
        // 초기 상태의 투명도 값
        for(int i = 0; i < arrObject.Length; i++) {
            SpriteRenderer spr = arrObject[i].GetComponent<SpriteRenderer>();
            arrOpacity[i] = spr.color.a;
        }

        /* 설정 값 동기화 */
        // BGM 불륨
        GameObject.Find("BGM Area").transform.FindChild("Slider").GetComponent<Slider>().value = SingleTone.Instance.volumeBGM;
        // 효과음 불륨
        GameObject.Find("Effects Area").transform.FindChild("Slider").GetComponent<Slider>().value = SingleTone.Instance.volumeEffects;
    }

    public void Open() {
        // 투명도 값을 초기 상태의 반으로 설정
        for(int i = 0; i < arrObject.Length; i++) {
            SpriteRenderer spr = arrObject[i].GetComponent<SpriteRenderer>();
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, arrOpacity[i]/2f);
        }
        // Hide Menu UI
        GameObject.Find("Menu UI").transform.localScale = new Vector3(0f, 0f, 0f);
        // Show Option UI
        GameObject.Find("Option UI").transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void Close() {
        // 투명도 값 복구
        for(int i = 0; i < arrObject.Length; i++) {
            SpriteRenderer spr = arrObject[i].GetComponent<SpriteRenderer>();
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, arrOpacity[i]);
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
