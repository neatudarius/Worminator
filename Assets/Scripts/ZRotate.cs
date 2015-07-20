using UnityEngine;
using System.Collections;

public class ZRotate : MonoBehaviour {

    public float rotateSpeed = 60.0f;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward, Time.deltaTime * rotateSpeed);
	}
}
