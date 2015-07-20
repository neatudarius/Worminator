using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{
    public float rotateSpeed;

	void Update ()
    {
        transform.eulerAngles += Vector3.up * rotateSpeed * Time.deltaTime;
	}
}
