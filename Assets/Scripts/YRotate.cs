using UnityEngine;
using System.Collections;

public class YRotate : MonoBehaviour {

    public float rotateSpeed = 60.0f;

    // Update is called once per frame
    void Update( ) {
        transform.Rotate(Vector3.up, Time.deltaTime * rotateSpeed);
    }
}
