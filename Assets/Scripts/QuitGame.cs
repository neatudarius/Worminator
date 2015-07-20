using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour {

    void OnMouseEnter( ) {
        GetComponent<Renderer>( ).material.color = Color.red;
    }

    void OnMouseExit( ) {
        GetComponent<Renderer>( ).material.color = Color.white;
    }

    void OnMouseUp( ) {
        // load level
        Application.Quit();
    }
}

