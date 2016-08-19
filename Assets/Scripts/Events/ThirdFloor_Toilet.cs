using UnityEngine;
using System.Collections;

public class ThirdFloor_Toilet : MonoBehaviour {
	void OnCollisionEnter2D(Collision2D col) {
        if(PlayerData.Player.Level >= GameObject.Find("Hyojeong_Yun").GetComponent<NPC>().Stage
            && GetComponents<Entrance>().Length == 0) {
            gameObject.AddComponent<Entrance>();
            GameObject.Find("Dialogue UI").GetComponent<Dialogue>().ShowDialogue("변기", "잔해 사이로 구멍이 보인다.");
        }
    }
}
