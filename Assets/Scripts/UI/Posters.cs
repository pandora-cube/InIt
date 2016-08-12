using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Posters : MonoBehaviour {
    Transform icons;
    Transform view;
    Color viewBackground;

	void Start() {
	    icons = transform.FindChild("Canvas").FindChild("Icons").transform;
        view = transform.FindChild("Canvas").FindChild("View").transform;
        viewBackground = view.GetComponent<Image>().color;
        Hide();
	}

    public void Open() {
        for(int i = 1; i <= 7; i++) {
            // i번째 포스터의 UI 내 아이콘
            Transform icon = icons.FindChild(i.ToString());
            try {
                /* 게임에 등록된 포스터 */
                // i번째 포스터의 NPC 객체
                NPC posterNPC = GameObject.Find(string.Format("Poster{0}", i)).GetComponent<NPC>();
                // 아이콘의 이미지 설정
                icon.GetComponent<Image>().sprite = GameObject.Find(string.Format("Poster{0}", i)).GetComponent<NPC>().Poster;
                // 플레이어의 i번째 포스터 획득 여부에 따라 아이콘 활성화/비활성화
                if(SingleTone.Instance.Level >= posterNPC.Stage)
                    EnablePosterIcon(icon);
                else
                    DisablePosterIcon(icon);
            } catch {
                /* 게임에 등록되지 않은 포스터 */
                // 아이콘 비활성화
                DisablePosterIcon(icon);
            }
        }

        // View 초기화
        view.GetComponent<Image>().sprite = null;
        view.GetComponent<Image>().color = viewBackground;

        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void Hide() {
        transform.localScale = new Vector3(0f, 0f, 0f);
    }
	
	public void OnClickIcon(int id) {
        Transform icon = icons.FindChild(id.ToString());

        // View의 이미지를 클릭한 아이콘의 이미지로 동기화
        view.GetComponent<Image>().sprite = icon.GetComponent<Image>().sprite;
        // 이미지 진하게 설정
        view.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
    }

    void EnablePosterIcon(Transform icon) {
        icon.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        icon.GetComponent<Button>().enabled = true;
    }

    void DisablePosterIcon(Transform icon) {
        icon.GetComponent<Image>().sprite = null;
        icon.GetComponent<Image>().color = new Color(.5f, .5f, .5f, 1f);
        icon.GetComponent<Button>().enabled = false;
    }
}
