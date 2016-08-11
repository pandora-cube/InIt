using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Main : MonoBehaviour {
	public void ClickPlayButton() {
		SceneManager.LoadScene("Scenes/FirstFloor");
	}

    public void ClickQuitButton() {
        Application.Quit();
    }
}
