using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOverUI : MonoBehaviour
{
    public Button restartBtn;

    public Button mainMenuBtn;

    public void OnEnable(){
        restartBtn.onClick.AddListener(()=>{Restart();});
        mainMenuBtn.onClick.AddListener(()=>{MainMenu();});
    }

    public void Restart(){
        SceneManager.LoadScene("Level_1");
    }

    public void MainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}
