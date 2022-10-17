using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public GameObject SettingsMenu;
    private bool settings;
    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }
    
    public void PlayGame()
    {
        Debug.Log("Game Started!");
        SceneManager.LoadScene("TestScene");
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        settings = false;
    }
    void Update()
    {
            if (Input.GetButtonDown("Exit") &&settings == true)
            {
            SettingsMenu.SetActive(false);
            }
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Settings()
    {
        if (settings == false)
        {
        SettingsMenu.SetActive(true);
        settings = true;
        }
        else
        {
        SettingsMenu.SetActive(false);
        settings = false;     
        }
    }
    public void SettingsInactive()
    {
        SettingsMenu.SetActive(false);
    }
}
