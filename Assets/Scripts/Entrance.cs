using UnityEngine;
using System.Collections;

public class Entrance : MonoBehaviour {
    public bool Locked = false;

    public void OnEnter() {
        // 캐릭터
        Transform character = GameObject.Find("Character").transform;

        // 잠긴 경우
        if(Locked) {
            GameObject.Find("Dialogue UI").GetComponent<Dialogue>().ShowDialogue("문", "굳게 잠겨 있다.");
            character.position = new Vector3(transform.position.x, transform.position.y, character.position.z);

            return;
        }

        // 출입구 오브젝트의 이름을 Split하여 정보를 얻음
        // [0] 출입구 이름
        // [1] 충돌한 출입구의 방향 (Outside or Inside)
        string[] objInfo = name.Split('/');
        // 반대편 출입구 이름
        string entrance = objInfo[0] + "/" + ((objInfo[1] == "Outside") ? "Inside" : "Outside");
        // 반대편 출입구
        Transform other = GameObject.Find(entrance).transform;
            
        // 캐릭터를 반대편 출입구로 이동
        character.position = new Vector3(other.position.x, other.position.y, character.position.z);
    }
}
