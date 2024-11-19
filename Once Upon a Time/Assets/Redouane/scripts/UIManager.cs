using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public void GoToHomeScreen()
    {
        SceneManager.LoadScene("Homescreen");
    }

    public void GoToTag()
    {
        SceneManager.LoadScene("Tag");
    }

    public void GoToLeaderboard()
    {
        SceneManager.LoadScene("leaderboard");
    }


    public void DoPauze(InputAction.CallbackContext context)
    {
        Time.timeScale = 0f;

    }
}
