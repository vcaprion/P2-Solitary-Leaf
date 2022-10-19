using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject Player;
    public GameObject pauseMenu;
    [SerializeField]GameObject ResumeButton;
    [SerializeField]Text menuText;

    void Start()
    {
        menuText.text = "Game is Paused";
    }
    
    void Update()
    {
        if (Player.GetComponent<PlayerMovement>().gameOver == true)
        {
            ResumeButton.SetActive(false);
            menuText.text = "Game Over";
        }
        else
        {
            ResumeButton.SetActive(true);
            menuText.text = "Game is Paused";
        }
    }
    
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Player.GetComponent<PauseControls>().paused = false;
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
