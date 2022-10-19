using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject Player;
    public GameObject pauseMenu;

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
