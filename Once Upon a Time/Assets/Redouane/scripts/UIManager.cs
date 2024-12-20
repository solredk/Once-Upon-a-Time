using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] EventSystem EventSystem;
    [SerializeField] List<Canvas> PauzeMenus;
    

    [SerializeField] List<GameObject> buttons = new List<GameObject>();
    [SerializeField] int index = 0;

    [SerializeField] bool IsPaused;


    private void Start()
    {
        if (EventSystem == null)
        {
            EventSystem = GetComponent<EventSystem>();  
        }
        if (PauzeMenus.Count >= 1)
        PauzeMenus[1].enabled = false;
    }
    private void Update()
    {
        if (EventSystem != null)
        {
            EventSystem.firstSelectedGameObject = buttons[index];
            if (EventSystem.currentSelectedGameObject == null)
            {
                EventSystem.SetSelectedGameObject(buttons[index]);
            }
        }
    }


    public void GoToHomeScreen()
    {
        SceneManager.LoadScene("Homescreen");
    }
    public  void GoToLobby()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void GoToTag()
    {
        SceneManager.LoadScene("Tag");
    }

    public void GoToLeaderboard()
    {
        SceneManager.LoadScene("leaderboard");
    }

    public void GoToHinkelen()
    {
        SceneManager.LoadScene("Hinkelen");
    }

    public void DoPauze()
    {
        if (PauzeMenus.Count >= 1)
        {
            if (!IsPaused)
            {
                IsPaused = true;
                Time.timeScale = 0f;
                PauzeMenus[0].enabled = false;
                PauzeMenus[1].enabled = true;
            }
            else
            {
                IsPaused = false;
                Time.timeScale = 1f;
                PauzeMenus[1].enabled = false;
                PauzeMenus[0].enabled = true;
            }
        }
    }

}
