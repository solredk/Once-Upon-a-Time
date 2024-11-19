using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TagGameManager : GameManager
{
    private bool taggerChosen=false;
    private Tag tagScript;

    protected override void PrepareLevel()
    {
        base.PrepareLevel();
        if (playerInputManager.playerCount >= 2)
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

    protected override void StartGame()
    {
        if (taggerChosen == false)
        {
            currentPlayers[randomTaggerIndex].GetComponent<Tag>().Getaged();
            taggerChosen = true;
        }
        base.StartGame();
    }

    protected override void CollectData()
    {
        foreach (GameObject player in currentPlayers)
        {
            tagScript = player.GetComponent<Tag>();
            PlayerPrefs.SetFloat(tagScript.username, tagScript.ReturnSurviveTime());
            float playerScore = PlayerPrefs.GetFloat(tagScript.username);
            Debug.Log(tagScript.username + playerScore);
        }
        base.CollectData();
    }
}
