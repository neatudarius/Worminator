using UnityEngine;
using System.Collections;

public class Worminator : MonoBehaviour {
    // Game MenuController
    public GameObject MenuController;
    

	// Global
	public bool worldIsFreeze = false;

    // Worminator components 
	public GameObject WorminatorHead, BodyPart;
	private GameObject[] bodyParts;
	
    // Health and Superpowers
    public GameObject HealthControl;
	private int lives; // curent number of life
    public int LIVES = 5; // initial lives
    public int MAXLIVES = 5;

    //Dying
	private bool dead = false;

	//Getting hit
	private bool isSpinning = false;
	
	//Spin around && jump
	public float tumbleSpeed = 800f;
	public float decreseTime = 0.01f;
	public float decayTime = 0.01f;
    private float jumpTime = 5.0f;
    private float jumpPeriod = 1.0f;

	//Shooting
	public float bulletSpeed = 2000f;
    public float shootPeriod = 1.0f;  
	private float shootTime = 0f; 
	
	//Backup
	private float[] spinBackup = new float[3];


	void Start() {
		// Start the gamme	
		shootTime = (float) Time.time;
		
        //Build Worminator
		Build();

        TakeCareOfWorld( );

		// spinBackup some variables
		Backup();
	}
	
	void Update() {

        if ( IsGameOver( ) ) {
            GameOver( );
            return;
        }
		

		// Shoot a fireball
		if (CanShoot()) {
            shootTime += shootPeriod;
            WorminatorHead.GetComponent<WorminatorHead>().Shoot(bulletSpeed);
        }

        if ( CanJump( ) ) {
            jumpTime += jumpPeriod;
            WorminatorHead.GetComponent<WorminatorHead>( ).Jump( );
        }
	}
	
	void LateUpdate() {
      
        if ( IsGameOver( ) ) {
            GameOver( );
            return;
        }

		if (isSpinning) {
			if (tumbleSpeed < 1) {
				//we're not hit anymore.. reset & get back in the game
                ResetSpinVariables( );
                isSpinning = false;
			} else {
				// we're hit! Spin out character around
				WorminatorHead.transform.Rotate(0, tumbleSpeed * Time.deltaTime, 0, Space.World);
				tumbleSpeed -= decreseTime;
				decreseTime += decayTime;
			}
		}
	}
	
	void DeleteBodyPart() {
        // Lost a live
        HealthControl.GetComponent<HealthControl>( ).RemoveHeart( );
        
        // Destroy last BodyPart
        
		Destroy(bodyParts[lives]);
        lives--;

	}

    void AddBodyPart( ) {
        if (CanGetLive() == false) {
            return;
        }

        // Get a new live
        lives++;
        HealthControl.GetComponent<HealthControl>( ).AddHeart( );

        // Create a badypart
        bodyParts[lives] = (GameObject) Instantiate(BodyPart, transform.position, Quaternion.identity);

        // Link it 
        bodyParts[lives].GetComponent<SmoothFollow>( ).target = bodyParts[lives-1].transform;
        bodyParts[lives].GetComponent<SmoothFollow>( ).distance = ( lives != 1 ? 1.2f : 1.6f );
        bodyParts[lives].transform.parent = transform;
        bodyParts[lives].gameObject.tag = "worminator";

    }

	public void Hit(uint powerShoot = 1) {
        if ( CanBeHit( ) == false ) {
            return;
        }

        worldIsFreeze = true;

		while (powerShoot > 0) {
			DeleteBodyPart();
			powerShoot--;
		}

        while (isSpinning);
        worldIsFreeze = false;
	}
	
	public void Kill() {
		while (lives > 0) {
            DeleteBodyPart();
		}
	}

	private void Build() {
        // Display live status
        HealthControl.GetComponent<HealthControl>( ).SetMaxLives(MAXLIVES);
        
        // Build the Worminator
		bodyParts = new GameObject[MAXLIVES+1];
		bodyParts[0] = WorminatorHead; // Head
        bodyParts[0].gameObject.tag = "worminator";
        
        // Get number of bodyParts === lives
        lives = 0;
        
        //Body
		for (int i = 1; i <= LIVES; i++) {
            AddBodyPart( );
		}

	}

    public bool IsGameOver( ) {
        if ( dead || lives == 0 || InPlayField( ) == false ) {
            return true;
        }
        return false;
    }

    public void GameOver( ) {
        dead = true;
        isSpinning = false;
        worldIsFreeze = true;

        
        MenuController.gameObject.GetComponent<MenuController>( ).shouldShowMenu = true;
        MenuController.gameObject.GetComponent<MenuController>( ).menuName = "Game Over Menu";
        
    }


    // If Worminator fall from terrain
    void OnTriggerEnter(Collider hit) {
        if ( hit.gameObject.tag == "fallout" ) {
            transform.GetComponent<Worminator>( ).Kill( );
        }
    }

    private void ResetSpinVariables( ) {
        tumbleSpeed = spinBackup[0];
        decreseTime = spinBackup[1];
        decayTime = spinBackup[2];
    }

	private void ResetVariables() {
        ResetSpinVariables( );
        worldIsFreeze = false;
        isSpinning = false;
        dead = false;
        Kill( );
	}
	
	private void Backup() {
		spinBackup[0] = tumbleSpeed;
		spinBackup[1] = decreseTime;
		spinBackup[2] = decayTime;
	}
	
	private bool InPlayField() {
		if (WorminatorHead.transform.position.y > -20f) {
			return true;
		}
		return false;
	}

    private bool CanShoot( ) {
        return Input.GetButtonDown("Shoot") && dead == false && isSpinning == false && Time.time > shootTime && worldIsFreeze == false;
    }

    public void PlaySound(AudioClip sound) {
        //SoundPlayer.PlayOneShot(sound, 0.7f);
    }

    public bool CanBeHit( ) {
        return lives > 0 && worldIsFreeze == false;
    }

    public bool CanGetLive( ) {
        return lives < MAXLIVES;
    }

    public bool CanJump( ) {
        return Input.GetButtonDown("Jump") && dead == false && isSpinning == false && Time.time > jumpTime && worldIsFreeze == false;
    }

    private void TakeCareOfWorld( ) {
        worldIsFreeze = false;


        MenuController.gameObject.GetComponent<MenuController>( ).Reset( );
        MenuController.gameObject.GetComponent<MenuController>( ).menuName = "Pause Menu";
    }
}
