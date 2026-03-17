using System;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    // NOT IMPLEMENTED
    
    public static PauseManager instance;
    public static bool gameIsPaused;

    private void Awake()
    {
        instance = this;
        gameIsPaused = false;
    }

    public void OnPause()
    {
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        gameIsPaused = false;
    }
}
