using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Instruction : MonoBehaviour {
    void Start() {
        HideMessage();
    }

    void Update() {
        bool findingPoster = false;

        foreach(NPC npc in GameObject.FindObjectsOfType<NPC>())
            if(npc.Stage == PlayerData.Player.Level+1 && npc.name.StartsWith("Poster")) {
                findingPoster = true;
                ShowMessage(string.Format("{0}번째 포스터를 찾으세요!", npc.name.Substring(npc.name.Length-1)));
            }
        if(!findingPoster)
            HideMessage();
    }
    
	public void ShowMessage(string message, float time=0f) {
        transform.FindChild("Canvas").FindChild("Text").GetComponent<Text>().text = message;
        transform.localScale = new Vector3(1f, 1f, 1f);
        if(time > 0f) {
            CancelInvoke("HideMessage");
            Invoke("HideMessage", time);
        }
    }

	public void HideMessage() {
        transform.FindChild("Canvas").FindChild("Text").GetComponent<Text>().text = string.Empty;
        transform.localScale = new Vector3(0f, 0f, 0f);
    }
}