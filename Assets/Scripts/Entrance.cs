using UnityEngine;
using System.Collections;

public class Entrance : MonoBehaviour {
    public bool Locked = false;

    public void OnEnter(Transform dest_=null) {
        Transform dest = dest_ == null ? GameObject.Find("Character").transform : dest_;

        // 잠긴 경우
        if(Locked) {
            if(dest_ == null && dest.GetComponents<Nametag>().Length > 0)
                dest.GetComponent<Nametag>().SetNametagText("잠겨있다");
            //dest.position = new Vector3(transform.position.x, transform.position.y, dest.position.z);

            return;
        }

        // 출입구 오브젝트의 이름을 Split하여 정보를 얻음
        // [0] 출입구 이름
        // [1] 충돌한 출입구의 방향 (Outside or Inside)
        string[] objInfo = name.Split('/');
        // 반대편 출입구 이름
        string entrance = objInfo[0] + "/" + (objInfo[1] == "Outside" ? "Inside" : "Outside");
        // 반대편 출입구
        Transform other = GameObject.Find(entrance).transform;
            
        // 캐릭터를 반대편 출입구로 이동
        dest.position = new Vector3(other.position.x, other.position.y, dest.position.z);

        // 입퇴장 완료 콜백 호출
        dest.SendMessage("OnEntranceEnter", SendMessageOptions.DontRequireReceiver);
    }
}
