using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
