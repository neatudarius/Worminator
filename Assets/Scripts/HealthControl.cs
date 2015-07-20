using UnityEngine;
using System.Collections;

public class HealthControl : MonoBehaviour {
	public GameObject Heart;

    private GameObject[] hearts;
    private int noOfHearts = 5;
    private int MAX_HEARTS = 10;


    public void AddHeart( ) {
        hearts[noOfHearts] = (GameObject) Instantiate(Heart, transform.position, Quaternion.identity);
        hearts[noOfHearts].transform.SetParent(transform);
        
        if ( noOfHearts > 0 ) {
            hearts[noOfHearts].transform.position = hearts[noOfHearts - 1].transform.position + new Vector3(+50, 0, 0);
        }
        noOfHearts++;
    }
    
    public void RemoveHeart( ) {
        noOfHearts--;
        Destroy(hearts[noOfHearts]);
    }
    

    public void SetMaxLives(int maxLives) {
        MAX_HEARTS = maxLives;
        hearts = new GameObject[MAX_HEARTS];
        noOfHearts = 0;
    }
}
