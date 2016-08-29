using UnityEngine;
using System.Collections;

public class Layer : MonoBehaviour {
	void Update() {
        Transform character = GameObject.Find("Character").transform;
        Vector3 charPosition = character.position;
        Vector3 myPosition = transform.position;
        float charFoot = charPosition.y - character.GetComponent<SpriteRenderer>().bounds.size.y / 2f;
        float myFoot = myPosition.y - GetComponent<SpriteRenderer>().bounds.size.y / 2f;
        
        transform.position = new Vector3(myPosition.x, myPosition.y, myFoot > charFoot ? -1 : -3);
	}
}
