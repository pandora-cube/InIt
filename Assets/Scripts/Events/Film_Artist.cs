using UnityEngine;
using System.Collections;

public class Film_Artist : MonoBehaviour {
    Vector3 originalPos;

	void Start () {
        originalPos = transform.position;

        if(PlayerData.Player.Level >= GetComponent<NPC>().Stage-1) {
            GameObject.Find("3F-Rooftop/Outside").GetComponent<Entrance>().Locked = false;
            GameObject.Find("3F-Rooftop/Inside").GetComponent<Entrance>().Locked = false;
        }

	    GetComponent<NPC>().dontChat = true;
	}
	
	void OnNPCCommandsEnd() {
        if(PlayerData.Player.Location != "Background_세종관 옥상") {
            transform.position = originalPos;
            PlayerData.Player.Level = GameObject.Find("Mingyeong_Kim").GetComponent<NPC>().Stage-1;
            GameObject.Find("Dialogue UI").GetComponent<Dialogue>().ShowDialogue("실패", "미스터리 영예과가 옥상 문을 잠궜습니다.");
        } else if(PlayerData.Player.Level == GetComponent<NPC>().Stage-2) {
            GameObject.Find("3F-Rooftop/Outside").GetComponent<Entrance>().Locked = false;
            GameObject.Find("3F-Rooftop/Inside").GetComponent<Entrance>().Locked = false;

            PlayerData.Player.Level = GetComponent<NPC>().Stage-1;

            GetComponent<NPC>().dontChat = false;
        }
    }
}
