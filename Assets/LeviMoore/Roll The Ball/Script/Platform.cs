using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour
{
    public float speed;
    public Vector3 targetPosition;
    public float waitTime;
    private Vector3 defaultPosition;
    private bool goBack;
    private bool wait;

	void Start ()
    {
        defaultPosition = transform.position;
	}
	
	void Update ()
    {
        if (wait)
        {
            return;
        }
        Vector3 _target = targetPosition;
        if (goBack)
        {
            _target = defaultPosition;
        }

        transform.position += ((_target - transform.position).normalized * Time.deltaTime * speed);
        if (Vector3.Distance(transform.position, _target) < 0.05f)
        {
            transform.position = _target;
            goBack = !goBack;
            StartCoroutine("Wait");
        }
	}

    IEnumerator Wait()
    {
        wait = true;
        yield return new WaitForSeconds(waitTime);
        wait = false;
    }
}
