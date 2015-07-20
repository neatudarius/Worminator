using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
        Screen.lockCursor = false;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadLevel(int _level)
    {
        Application.LoadLevel("Level " + _level.ToString());
    }

    public void Play()
    {
        Application.LoadLevel("Level 1");
    }
}
