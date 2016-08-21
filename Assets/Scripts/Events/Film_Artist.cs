using UnityEngine;
using System.Collections;

public class Film_Artist : MonoBehaviour {
    Vector3 originalPos;

	void Start () {
        originalPos = transform.position;
        
        if(PlayerData.Player.Level == GetComponent<NPC>().Stage-2)
            PlayerData.Player.Level = GetComponent<NPC>().Stage-3;
        else if(PlayerData.Player.Level >= GetComponent<NPC>().Stage-1) {
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
            
            /*while(GetComponents<BoxCollider2D>().Length > 0) {
                Debug.Log("1");
                Destroy(GetComponent<BoxCollider2D>());
            }*/
            BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
            BoxCollider2D trigger = gameObject.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(1.15f, 3.5f);
            trigger.size = new Vector2(1.4f, 3.75f);
            trigger.isTrigger = true;

            PlayerData.Player.Level = GetComponent<NPC>().Stage-1;

            GetComponent<NPC>().dontChat = false;
        }
    }
}
