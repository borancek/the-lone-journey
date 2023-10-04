using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public Transform craftingParent;
    public Transform itemsParent;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
            {
                Resume();
            } else{
                Pause();
            }
        }
        
    }

    public void Resume() 
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Player.instance.isPaused = false;
        itemsParent.gameObject.SetActive(false);
        craftingParent.gameObject.SetActive(false);
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Player.instance.isPaused = true;
        itemsParent.gameObject.SetActive(false);
        craftingParent.gameObject.SetActive(false);
    }

    public void LoadMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame(){
        Application.Quit();
    }
}

