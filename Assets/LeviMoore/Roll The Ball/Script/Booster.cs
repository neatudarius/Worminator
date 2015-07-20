using UnityEngine;
using System.Collections;

public class Booster : MonoBehaviour
{
    public float power;
    public Vector3 dir;

    void OnTriggerEnter(Collider _hit)
    {
        if (_hit.tag == "Player")
        {
            _hit.GetComponent<Rigidbody>().velocity = Vector3.zero;
            _hit.GetComponent<Rigidbody>().AddForce(dir * power * 100);
        }
    }
}
