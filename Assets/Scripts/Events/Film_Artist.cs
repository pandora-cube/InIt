using UnityEngine;
using System.Collections;

public class Film_Artist : MonoBehaviour {
	void Start () {
	    GetComponent<NPC>().dontChat = true;
	}
	
	void OnNPCCommandsEnd() {
        if(PlayerData.Player.Level == GetComponent<NPC>().Stage-2) {
            PlayerData.Player.Level = GetComponent<NPC>().Stage-1;
            GetComponent<NPC>().dontChat = false;
        }
    }
}
