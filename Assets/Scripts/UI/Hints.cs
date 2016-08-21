using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hints : MonoBehaviour {
    Transform icons;
    Transform view;
    string[] arrNPC = new string[] {
        string.Empty,
        "Kichoon_Park",
        "Keonwoo_Ahn",
        "Seongbum_Seo",
        "Seohyeon_Han",
        "Hyojeong_Yun",
        "Film_Artist",
        "Foreigner"
    };
    string[] arrHints = new string[] {
        string.Empty,
        "사물함들 중에서 첫번째 포스터를 봤던 것 같아.",
        "음료수 사먹다가 두번째 포스터를 봤던 것 같아.",
        "EPP 수업 듣는 세종관 2층에 있는 강의실 책상 서랍에서 세번째 포스터를 봤던 것 같아.",
        "2층에서 3층 올라가는 벽면에서 네번째 포스터를 봤던 것 같아.",
        "판도라 큐브 동방이 있는 층의 여자 화장실에서 다섯번째 포스터를 봤던 것 같아.",
        "높은 곳에 왔으니 한번 둘러봐~",
        "smoking terrace eseo seventh poster bwatdun gut gatayo."
    };

	void Start() {
	    icons = transform.FindChild("Canvas").FindChild("Icons").transform;
        view = transform.FindChild("Canvas").FindChild("View").FindChild("Text").transform;
        Hide();
	}
	
	public void Open() {
        for(int i = 1; i <= 7; i++) {
            Transform icon = icons.FindChild(i.ToString());
            try {
                NPC npc = GameObject.Find(arrNPC[i]).GetComponent<NPC>();
                icon.GetComponent<Image>().sprite = npc.GetComponent<SpriteRenderer>().sprite;
                if(PlayerData.Player.Level >= npc.Stage)
                    EnableNPCIcon(icon);
                else
                    DisableNPCIcon(icon);
            } catch {
                DisableNPCIcon(icon);
            }
        }
        
        view.GetComponent<Text>().text = string.Empty;

        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void Hide() {
        transform.localScale = new Vector3(0f, 0f, 0f);
    }

    public void OnClickIcon(int id) {
        string name = GameObject.Find(arrNPC[id]).GetComponent<NPC>().Name;
        view.GetComponent<Text>().text = string.Format("< {0} >\n{1}", name, arrHints[id]);
    }

    void EnableNPCIcon(Transform icon) {
        icon.GetComponent<Button>().enabled = true;
    }

    void DisableNPCIcon(Transform icon) {
        icon.GetComponent<Image>().sprite = icons.FindChild("Blank").GetComponent<Image>().sprite;
        icon.GetComponent<Button>().enabled = false;
    }
}
