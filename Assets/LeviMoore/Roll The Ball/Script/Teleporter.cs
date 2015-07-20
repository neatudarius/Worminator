using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour
{
    public Vector3 outPosition;

	void OnTriggerEnter(Collider _hit)
    {
        if (_hit.tag == "Player")
        {
            _hit.transform.position = outPosition;
        }
    }
}
