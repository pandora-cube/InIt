using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Intro : MonoBehaviour {
    void Start() {
        // 화면 크기 조정 (960 x 540)
        Screen.SetResolution(960, 540, false);
    }

    public void FadeEnded() {
        SceneManager.LoadScene("Scenes/Main");
    }
}
