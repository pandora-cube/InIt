using UnityEngine;
using System.Collections;

public class Layer : MonoBehaviour {
	void Update() {
        Vector3 charPosition = GameObject.Find("Character").transform.position;
        Vector3 myPosition = transform.localPosition;
        
        transform.localPosition = new Vector3(myPosition.x, myPosition.y, myPosition.y > charPosition.y ? -1 : -2);
	}
}
