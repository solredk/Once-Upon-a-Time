using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
    enum GameState
    {
        preparelevel,
        StartGame,
        CollectData,
        Endgame,
        PauzeGame
    }

public class TagGameManager : MonoBehaviour
{
    [SerializeField] PlayerInputManager playerInputManager;

    [SerializeField] List<GameObject> Players; [Tooltip("de default player prefaps (max 4)")]
    [SerializeField] List<GameObject> currentPlayers; [Tooltip("de prefaps in de scene (max 4)")]

    [Header("de state waar de game in zit")]
    [SerializeField] GameState state;

    [Header ("de counters")]
    [SerializeField]float Gameduration;
    [SerializeField]float counter; [Tooltip("de timer voor de start van de game en de game zelf")]

    [Header("de player nummers")]
    [SerializeField] int randomTaggerIndex; [Tooltip("de random nummer die wordt gegenereerd aan de start van de game om de player voor de stellen")]
    [SerializeField] int playerInt;
    [SerializeField] int playercount;
   
    private void Start()
    {
        if (playerInputManager != null)
            playerInputManager = GetComponent<PlayerInputManager>();
    }

    private void Update()
    {

        switch (state)
        {
            case GameState.preparelevel:
                PrepareLevel();
                break;
            case GameState.StartGame:
                StartGame();
                break;
            case GameState.CollectData:
                CollectData();
                break;
            case GameState.Endgame:
                break;
                
        }
    }
    
    private void PrepareLevel()
    {
        playerInputManager.EnableJoining();
        
        if (playerInputManager.playerCount >= 1)
        {
            counter += Time.deltaTime;
             if (counter > 10)
            {
                state = GameState.StartGame;
                playerInputManager.DisableJoining();

                GameObject[] foundPlayers = GameObject.FindGameObjectsWithTag("Player");
                foreach (GameObject player in foundPlayers)
                {
                    currentPlayers.Add(player);
                }

                randomTaggerIndex = UnityEngine.Random.Range(0, currentPlayers.Count);
                counter = 0;
            }
        }
    }

    private void StartGame()
    {
        currentPlayers[randomTaggerIndex].GetComponent<Tag>().Getaged();

        counter += Time.deltaTime;
        if (counter > Gameduration)
        {
             state = GameState.CollectData;
        }
    }

    private void CollectData()
    {
       foreach (GameObject player in currentPlayers)
             {
               playerInt++;
               PlayerPrefs.SetFloat("player" + playerInt, player.GetComponent<Tag>().ReturnSurviveTime());
               float playerScore = PlayerPrefs.GetFloat("player" + playerInt);
               Debug.Log(playerScore);
               Debug.Log(PlayerPrefs.GetInt("player" + playerInt));
             }
       playerInt = playercount;
       state = GameState.Endgame;
    }

    public void DoPauze(InputAction.CallbackContext context)
    {
        Time.timeScale = 0f;

    }
    public void DoJoin()
    {
        playerInputManager.playerPrefab = Players[playercount];
        playercount++;
    }
}
