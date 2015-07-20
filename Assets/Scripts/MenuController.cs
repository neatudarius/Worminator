using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {
    public Font menuFont;
    
    public bool shouldShowMenu = false;
    private bool showGraphicsDropDown = false;


    // BOx Title
    private float titleHeight = 50.0f;
    public string menuName = "Worminator";
    private string mainMenuName = "Worminator";

    //Box MenuController
    private float width = 0.0f;
    private float height = 0.0f;
    private Vector2 menuDimensions =  Vector2.zero;
    private Vector2 upLeft =  Vector2.zero;
    //private Vector2 upRight = Vector2.zero;
    //private Vector2 downLeft  =  Vector2.zero;
    //private Vector2 downRight =  Vector2.zero;

    //Buttons
    private int noOfButtons = 5;
    private float buttonWidth = 0.0f;
    private float buttonHeight = 0.0f;
    private Vector2 buttonDimensions = Vector2.zero;
    private Vector2 decayButtonPosition = Vector2.zero;
    private Vector2[] buttonPositions = null;

    void Start( ) {
        Reset( );
        if ( Application.loadedLevel == 0 ) {
            shouldShowMenu = true;
            menuName = mainMenuName;
            AudioListener.volume = 1;
            Cursor.visible = true;
        }
    }

    public void Reset( ) {
        width = Screen.width / 2;
        height = Screen.height / 2 + titleHeight;
        menuDimensions = new Vector2(width, height);
        upLeft = new Vector2(Screen.width / 4, Screen.height / 4);
        if ( Application.loadedLevel == 0 ) {
            upLeft = upLeft - new Vector2(width / 2.1f, height / 2.5f);
        }
        //upRight = upLeft + new Vector2(width, 0);
        //downLeft = upLeft + new Vector2(0, height);
        //downRight = upLeft + new Vector2(width, height);

        buttonHeight = ( height - titleHeight ) / noOfButtons;
        buttonWidth = width;
        buttonDimensions = new Vector2(buttonWidth, buttonHeight);
        decayButtonPosition = new Vector2(0, buttonHeight);

        buttonPositions = new Vector2[noOfButtons];
        buttonPositions[0] = upLeft + decayButtonPosition;
        for ( int i = 1; i < noOfButtons; i++ ) {
            buttonPositions[i] = buttonPositions[i - 1] + decayButtonPosition;
        }

        shouldShowMenu = false;
        Time.timeScale = 1;
        AudioListener.volume = 1;
        Cursor.visible = false;
    }

    void ReturnToGame( ) {
        //unpause the game
        shouldShowMenu = false;
        Time.timeScale = 1;
        AudioListener.volume = 1;
        Cursor.visible = false;
    }

    void Update( ) {
        if ( Application.loadedLevel == 0 ) {
            menuName = mainMenuName;
        }
        if ( shouldShowMenu == false && menuName != mainMenuName ) {
            ReturnToGame( );
        }

        //check if pause button (escape key) is pressed
        if ( Input.GetKeyDown("escape") && menuName != mainMenuName) {

            //check if game is already paused		
            if ( shouldShowMenu == true ) {
                ReturnToGame( );
            } else {
                shouldShowMenu = true;
                //AudioListener.volume = 0;
                Time.timeScale = 0;
                Cursor.visible = true;
            }
        }
    }



    void OnGUI( ) {
        if ( Application.loadedLevel == 0 ) {
            menuName = mainMenuName;
        }
        GUI.skin.box.font = menuFont;
        GUI.skin.button.font = menuFont;

        if ( shouldShowMenu == true || menuName == mainMenuName ) {
            Time.timeScale = 1.0f;

            switch ( menuName ) {
                case "Pause Menu":
                    PauseMenu( );
                    break;
                case "Game Over Menu":
                    GameOverMenu( );
                    break;
                default:
                    MainMenu( );
                    break;
            }

            if ( showGraphicsDropDown == true ) {
                ChangeQualityDropMenu();

                if ( Input.GetKeyDown("escape") && menuName != mainMenuName ) {
                    showGraphicsDropDown = false;
                    UpdateMenuName("Pause Menu");
                }
            }
            Cursor.visible = true;
            Time.timeScale = 0.0f;
            if ( Application.loadedLevel == 0 ) {
                Time.timeScale = 1.0f;
            }
        }

    }

    void PauseMenu( ) {
        MakeMenu(upLeft);

        ResumeButton(buttonPositions[0]);
        ReplayButton(buttonPositions[1]);
        MainMenuButton(buttonPositions[2]);
        ChangeQualityButton(buttonPositions[3]);
        QuitButton(buttonPositions[4]);
    }

    void GameOverMenu( ) {
        MakeMenu(upLeft);
 
        ReplayButton(buttonPositions[1]);
        MainMenuButton(buttonPositions[2]);
        ChangeQualityButton(buttonPositions[3]);
        QuitButton(buttonPositions[4]);
    }

    void MainMenu( ) {
        MakeMenu(upLeft);

        PlayButton(buttonPositions[0]);
        LoadLevelButton(buttonPositions[1]);
        MainMenuButton(buttonPositions[2]);
        ChangeQualityButton(buttonPositions[3]);
        QuitButton(buttonPositions[4]);
    }

    void MakeMenu(Vector2 menuPosition) {
        GUI.Box(new Rect(menuPosition, menuDimensions), menuName);
    }

    void ResumeButton(Vector2 buttonPosition) {
        if ( GUI.Button(new Rect(buttonPosition, buttonDimensions), "Resume") ) {
            ReturnToGame( );
        }
    }

    void PlayLevel(int level = 1) {
        Application.LoadLevel(level);
    }

    void PlayNextLevel( ) {
        int level = Application.loadedLevel;
        if ( level < Application.levelCount ) {
            level = level + 1;
        } else {
            level = 0;
        }
        PlayLevel(level);
    }

    void PlayButton(Vector2 buttonPosition) {
        if ( GUI.Button(new Rect(buttonPosition, buttonDimensions), "Play") ) {
            Application.LoadLevel(1);
        }
    }

    void LoadLevelButton(Vector2 buttonPosition) {
        if ( GUI.Button(new Rect(buttonPosition, buttonDimensions), "Load") ) {
            Application.LoadLevel(1);
        }
    }
    void ReplayButton(Vector2 buttonPosition) {
        if ( GUI.Button(new Rect(buttonPosition, buttonDimensions), "Replay level") ) {
            Application.LoadLevel(Application.loadedLevelName);
        }
    }
    
    void MainMenuButton(Vector2 buttonPosition) {
        if ( GUI.Button(new Rect(buttonPosition, buttonDimensions), "Main Menu") ) {
            Application.LoadLevel("main");
        }
    }

    void ChangeQualityButton(Vector2 buttonPosition) {
        //Make Change Graphics Quality button
        if ( GUI.Button(new Rect(buttonPosition, buttonDimensions), "Change Graphics Quality") ) {

            if ( showGraphicsDropDown == false ) {
                showGraphicsDropDown = true;
            } else {
                showGraphicsDropDown = false;
            }
        }
    }



    void QuitButton(Vector2 buttonPosition) {
        //Make quit game button
        if ( GUI.Button(new Rect(buttonPosition, buttonDimensions), "Quit Game") ) {
            Application.Quit( );
        }
    }

    void ChangeQualityDropMenu( ) {
        if ( GUI.Button(new Rect(Screen.width / 2 + 150, Screen.height / 2, 250, 50), "Fastest") ) {
            QualitySettings.currentLevel = QualityLevel.Fastest;
        }
        if ( GUI.Button(new Rect(Screen.width / 2 + 150, Screen.height / 2 + 50, 250, 50), "Fast") ) {
            QualitySettings.currentLevel = QualityLevel.Fast;
        }
        if ( GUI.Button(new Rect(Screen.width / 2 + 150, Screen.height / 2 + 100, 250, 50), "Simple") ) {
            QualitySettings.currentLevel = QualityLevel.Simple;
        }
        if ( GUI.Button(new Rect(Screen.width / 2 + 150, Screen.height / 2 + 150, 250, 50), "Good") ) {
            QualitySettings.currentLevel = QualityLevel.Good;
        }
        if ( GUI.Button(new Rect(Screen.width / 2 + 150, Screen.height / 2 + 200, 250, 50), "Beautiful") ) {
            QualitySettings.currentLevel = QualityLevel.Beautiful;
        }
        if ( GUI.Button(new Rect(Screen.width / 2 + 150, Screen.height / 2 + 250, 250, 50), "Fantastic") ) {
            QualitySettings.currentLevel = QualityLevel.Fantastic;
        }
    }

    public void UpdateMenuName(string name) {
        menuName = name;    
    }

    public void Show( ) {
        Start( );
    }


}