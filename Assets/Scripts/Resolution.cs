using UnityEngine;
using System.Collections;

public class Resolution : MonoBehaviour {
	void Update() {
        // 화면 크기가 16:9가 아닌 경우 세로 크기를 조정하여 비율을 맞춤
        if(Screen.width/16 != Screen.height/9)
            Screen.SetResolution(Screen.width, Screen.width/16*9, Screen.fullScreen);
	}
}
