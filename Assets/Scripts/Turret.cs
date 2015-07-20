using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	// Use this for initialization
    public void DestroySelf( ) {
        Destroy(gameObject);
    }
}
