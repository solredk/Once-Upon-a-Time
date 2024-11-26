using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerKnikkeren : MonoBehaviour
{
    public GameObject marblePrefab;
    public Transform spawnPoint;
    public int numberOfPlayers = 4;

    public Color[] playerColors;

    [HideInInspector] public int currentPlayerIndex = 0;
    private GameObject currentMarble;
    private bool isWaitingForNextPlayer = false;

    [HideInInspector] public List<GameObject> spawnedMarbles = new List<GameObject>();

    public PointManager pointManager;

    void Start()
    {
        if (playerColors.Length < numberOfPlayers)
        {
            Debug.LogError("Zorg ervoor dat er genoeg kleuren zijn ingesteld voor alle spelers!");
            return;
        }

        SpawnMarbleForPlayer();
    }

    void Update()
    {
        if (currentMarble != null && !isWaitingForNextPlayer)
        {
            Rigidbody rb = currentMarble.GetComponent<Rigidbody>();
            PlayerController controller = currentMarble.GetComponent<PlayerController>();

            if (rb.velocity.magnitude < 0.01f && controller.isShoot)
            {
                StartCoroutine(WaitAndSpawnNextPlayer());
            }
        }
    }

    public void SpawnMarbleForPlayer()
    {
        if (currentPlayerIndex >= numberOfPlayers)
        {
            Debug.Log("Het spel is voorbij! Alle spelers hebben geschoten.");
            pointManager.DetermineClosestMarble();
            return;
        }

        currentMarble = Instantiate(marblePrefab, spawnPoint.position, Quaternion.identity);
        spawnedMarbles.Add(currentMarble);

        Renderer renderer = currentMarble.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = playerColors[currentPlayerIndex];
        }

        Debug.Log($"Speler {currentPlayerIndex + 1} is nu aan de beurt!");

        PlayerController controller = currentMarble.GetComponent<PlayerController>();
        controller.enabled = true;

        LineRenderer lineRenderer = currentMarble.GetComponent<LineRenderer>();
        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;
        }

        isWaitingForNextPlayer = false;
    }

    IEnumerator WaitAndSpawnNextPlayer()
    {
        if (isWaitingForNextPlayer)
            yield break;

        isWaitingForNextPlayer = true;
        yield return new WaitForSeconds(3f);

        PlayerController controller = currentMarble.GetComponent<PlayerController>();
        controller.enabled = false;

        LineRenderer lineRenderer = currentMarble.GetComponent<LineRenderer>();
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }

        currentPlayerIndex++;
        SpawnMarbleForPlayer();
    }
}
