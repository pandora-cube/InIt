using UnityEngine;
using System.Collections;

public class BGM : MonoBehaviour {
	void Update () {
	    GetComponent<AudioSource>().volume = SingleTone.Instance.volumeBGM;
	}
}
