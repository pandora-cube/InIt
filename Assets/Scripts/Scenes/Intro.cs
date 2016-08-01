using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Intro : MonoBehaviour {
    public void FadeEnded() {
        SceneManager.LoadScene("Scenes/Main");
    }
}
