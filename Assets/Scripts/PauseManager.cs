using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private static bool gameIsPaused; 
    public static bool GameIsPaused { get { return gameIsPaused; } }

    private static PauseManager instance = null;
    public static PauseManager Instance { get { return instance; } }
    void Update() 
    { 
        if (Input.GetKeyDown(KeyCode.P)) 
        { 
            gameIsPaused = !gameIsPaused;
            PauseGame();
        } 
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    public void PauseGame() 
    { 
        if (gameIsPaused) 
            Time.timeScale = 0f; 
        else Time.timeScale = 1;
    }

    public void PauseForMenu()
    {
        gameIsPaused = !gameIsPaused;
        if (gameIsPaused)
            Time.timeScale = 0f;
        else Time.timeScale = 1;
    }
}
