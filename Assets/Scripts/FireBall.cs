using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour {
	public float lifeTime = 1.0f;
    public GameObject explosion;
	public GameObject Worminator;
    private bool foundTarget = false;

    AudioSource AudioPlayer;
    public AudioClip explosionSound;

    void Start( ) {
        AudioPlayer = GetComponent<AudioSource>( );
    }
	void Awake() {
        StartCoroutine(Die(lifeTime));
	}


    void OnCollisionEnter(Collision hit) {
        if ( foundTarget ) {
            return;
        }

        foundTarget = true;

        if (hit.gameObject.tag == "worminator" && gameObject.tag == "turretProiectille") {
			hit.gameObject.transform.root.gameObject.GetComponent<Worminator>().Hit();
        }

        if (hit.gameObject.tag == "turret" && gameObject.tag == "worminatorProiectille") {
            hit.gameObject.GetComponent<TurretControl>( ).Hit( );
        }

        Explode( );

    }

    IEnumerator Die(float lifeTime) {
        yield return new WaitForSeconds(lifeTime);
        Explode( );
        Destroy(gameObject);
    }

    private void Explode( ) {
        AudioPlayer.PlayOneShot(explosionSound, 1.0f);
        Instantiate(explosion, transform.position, Quaternion.identity);
    }

}

