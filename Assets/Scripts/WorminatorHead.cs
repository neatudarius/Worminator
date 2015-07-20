using UnityEngine;
using System.Collections;

public class WorminatorHead : MonoBehaviour {
	// Global 
	public GameObject Camera;
	
	// Moving
	public float speed = 5.0f;
	public float rotateSpeed = 10.0f;

	// Shooting
	public GameObject FireBall;


    //jumping
    public float jumpSpeed = 20.0F;
    public float gravity = 20.0F;

   

    void Update( ) {
        // Getcontroller
		CharacterController controller = GetComponent<CharacterController>();

        if ( controller.isGrounded ) {

            if ( Input.GetButton("Jump") ) {
                // TO DO
            }
               

        }
    }
    
    
    void FixedUpdate() {
        // Getcontroller
		CharacterController controller = GetComponent<CharacterController>();
		
        //Get the rotate -> Oy
		transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

        Vector3 moveDirection = transform.TransformDirection(Vector3.forward);
		float currentSpeed = speed * Input.GetAxis("Vertical");

        controller.SimpleMove(moveDirection * currentSpeed);

	}
    
	// Move head &&  Camera at the start poin.
	public void Move(float x = 0, float y = 4, float z = 0) {
		transform.position = new Vector3(x, y, z);
		Camera.transform.position = new Vector3(x, y, z-10);
	}

    public void Translate(float x = 0, float y = 0, float z = 0) {
        transform.position = transform.position + new Vector3(x, y, z);
        Camera.transform.position = Camera.transform.position + new Vector3(x, y, z - 10);
    }
    public void Jump( ) {

    }
	
	// Shoot a fireball
	public void Shoot(float bulletSpeed) {
		GameObject bullet = (GameObject) Instantiate(FireBall, transform.Find("spawnPointWorminator").transform.position, Quaternion.identity);
        bullet.tag = "worminatorProiectille";
        bullet.GetComponent<Rigidbody>( ).AddForce(transform.forward * bulletSpeed);
	}


}

