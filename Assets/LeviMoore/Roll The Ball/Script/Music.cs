using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Application.LoadLevel("Menu");
    }
}
