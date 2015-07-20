using UnityEngine;
using System.Collections;

public class PlayGame : MonoBehaviour {

    void OnMouseEnter( ) {
        GetComponent<Renderer>( ).material.color = Color.red;
    }

    void OnMouseExit( ) {
        GetComponent<Renderer>( ).material.color = Color.white;
    }

    void OnMouseUp( ) {
        // load level
        Time.timeScale = 1.0f;
        Application.LoadLevel(1);
    }
}

