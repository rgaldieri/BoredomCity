using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    
    public Button startGameButton;

    public Button exitGame;

    void Start()
    {
        startGameButton.onClick.AddListener(()=>{StartGame();});
        exitGame.onClick.AddListener(()=>{ExitGame();});
    }

    void Update(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;
    }

    public void StartGame(){
        SceneManager.LoadScene("Level_1");
    }

    public void ExitGame(){
        Application.Quit();
    }
}
