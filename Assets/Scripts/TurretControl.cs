using UnityEngine;
using System.Collections;

public class TurretControl : MonoBehaviour {
	// General
	public Transform target;
	public GameObject Worminator;
	public int lives = 3;
	
	// Shooting
	public GameObject FireBall;
	public float rotateSpeed = 1.0f;
	public float bulletSpeed = 5000f;
	public float frequencyShootTime = 1.0f;
	public float fireDistance = 5.0f;
	private float distance = Mathf.Infinity;
    

	void Start () {
		StartCoroutine(Shoot());
	}

	void Update () {
		//Check if target is close and shot it.
		distance = Vector3.Distance(target.position, transform.position);
		if (target && CanShoot()) {
			Quaternion rotate = Quaternion.LookRotation(target.position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * rotateSpeed);
		}
	}

	IEnumerator Shoot() {
		while (true) {
			if (target && CanShoot()) {
				GameObject bullet = (GameObject)Instantiate(FireBall, transform.Find("spawnPointTurret").position, Quaternion.identity);
				bullet.gameObject.tag = "turretProiectille";
				bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
			}
			yield return new WaitForSeconds(frequencyShootTime);
		}
	}

    public void Hit(int powerShoot = 1 ) {
        lives -= powerShoot;
        if (lives <= 0) {
            
            Destroy(transform.parent.gameObject);
		}
    }

    public bool CanShoot( ) {
        return distance < fireDistance && Worminator.gameObject.GetComponent<Worminator>( ).worldIsFreeze == false;
    }
}
