using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] EventSystem EventSystem;

    [SerializeField] List<GameObject> buttons = new List<GameObject>();
    [SerializeField] int index = 0;

    [SerializeField] bool IsPaused;


    private void Start()
    {
        if (EventSystem == null)
        {
            EventSystem = GetComponent<EventSystem>();  
        }
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
            Debug.Log(EventSystem.currentSelectedGameObject);
        }

    }


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

    public void GoToHinkelen()
    {
        SceneManager.LoadScene("Hinkelen");
    }

    public void DoPauze(InputAction.CallbackContext context)
    {
        if (!IsPaused)// hiermee kan je de game pauzeren
        {
            IsPaused = true;
            Time.timeScale = 0f;

        }
        else 
        { 
        IsPaused =false;
        Time.timeScale = 1f; 
        }
    }

}
