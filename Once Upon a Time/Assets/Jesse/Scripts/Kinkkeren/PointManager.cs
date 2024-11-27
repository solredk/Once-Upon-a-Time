using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public Transform endPoint;
    public GameManagerKnikkeren gameManager;
    public Dictionary<int, int> playerScores = new Dictionary<int, int>();

    void Start()
    {
        for (int i = 0; i < gameManager.numberOfPlayers; i++)
        {
            playerScores[i] = 0;
        }
    }

    public void DetermineClosestMarble()
    {
        if (gameManager.spawnedMarbles.Count == 0)
        {
            Debug.LogWarning("Er zijn geen knikkers in het spel!");
            return;
        }

        float closestDistance = float.MaxValue;
        int closestPlayerIndex = -1;
        GameObject closestMarble = null;

        for (int i = 0; i < gameManager.spawnedMarbles.Count; i++)
        {
            GameObject marble = gameManager.spawnedMarbles[i];
            float distance = Vector3.Distance(marble.transform.position, endPoint.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayerIndex = i;
                closestMarble = marble;
            }
        }

        if (closestPlayerIndex != -1)
        {
            PlayerPrefs.SetFloat("closestPlayerScore" , playerScores[closestPlayerIndex]++);
            Debug.Log($"Speler {closestPlayerIndex + 1} krijgt een punt! Totaal: {playerScores[closestPlayerIndex]} punten.");
        }

        RemoveAllMarbles();


        StartNewRound();
    }

    void RemoveAllMarbles()
    {
        foreach (GameObject marble in gameManager.spawnedMarbles)
        {
            Destroy(marble);
        }

        gameManager.spawnedMarbles.Clear();
    }

    void StartNewRound()
    {
        gameManager.currentPlayerIndex = 0;
        gameManager.SpawnMarbleForPlayer();
    }
}
