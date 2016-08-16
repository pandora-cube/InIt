using UnityEngine;
using System.Collections;

public class CloudMove : MonoBehaviour {
    public float scrollSpeed = 0.2f;
    float Offset;

	void Update() {
        Offset += Time.deltaTime * scrollSpeed;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(-Offset, 0);
	}
}
