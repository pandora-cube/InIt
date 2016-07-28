using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Option : MonoBehaviour {
    public void ClickApplyButton() {
        SceneManager.LoadScene("Scenes/Main");
    }

    public void ClickCancelButton() {
        SceneManager.LoadScene("Scenes/Main");
    }
}
