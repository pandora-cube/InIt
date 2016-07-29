using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Option : MonoBehaviour {
    GameObject[] arrObject;
    float[] arrOpacity;

    void Start() {
        arrObject = GameObject.FindGameObjectsWithTag("Background");
        arrOpacity = new float[arrObject.Length];
        for(int i = 0; i < arrObject.Length; i++) {
            SpriteRenderer spr = arrObject[i].GetComponent<SpriteRenderer>();
            arrOpacity[i] = spr.color.a;
        }
    }

    public void Open() {
        for(int i = 0; i < arrObject.Length; i++) {
            SpriteRenderer spr = arrObject[i].GetComponent<SpriteRenderer>();
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, arrOpacity[i]/2f);
        }
        GameObject.Find("Menu UI").transform.localScale = new Vector3(0f, 0f, 0f);
        GameObject.Find("Option UI").transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void Close() {
        for(int i = 0; i < arrObject.Length; i++) {
            SpriteRenderer spr = arrObject[i].GetComponent<SpriteRenderer>();
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, arrOpacity[i]);
        }
        GameObject.Find("Menu UI").transform.localScale = new Vector3(1f, 1f, 1f);
        GameObject.Find("Option UI").transform.localScale = new Vector3(0f, 0f, 0f);
    }

    public void ClickApplyButton() {
        Close();
    }

    public void ClickCancelButton() {
        Close();
    }
}
