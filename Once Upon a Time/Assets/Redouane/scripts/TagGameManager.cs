using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TagGameManager : GameManager
{
    bool taggerChosen=false;

    Tag tagScript;

    protected override void PrepareLevel()
    {
        base.PrepareLevel();
        if (playerInputManager.playerCount >= 2)
        {
            textMeshProUGUI[0].text = playerInputManager.playerCount + "/4 players game starting in :";
            textMeshProUGUI[1].text = counter.ToString("N0");
            counter -= Time.deltaTime;
            if (counter <= 0)
            {
                textMeshProUGUI[0].text = "";
                textMeshProUGUI[1].text = "";
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
    private void CheckTikkers()
    {
        foreach (GameObject player in currentPlayers)
        {
            tagScript = player.GetComponent<Tag>();
            if (tagScript.State == Tag.PlayerState.tikker)
            { 
                textMeshProUGUI[2].text = "Tikker: " + tagScript.username;
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
        CheckTikkers();
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
    public override void DoJoin()
    {
        base.DoJoin();

    }
}
