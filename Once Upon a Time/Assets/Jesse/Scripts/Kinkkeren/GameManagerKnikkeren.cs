using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerKnikkeren : MonoBehaviour
{
    public GameObject marblePrefab;
    public Transform spawnPoint;
    public int numberOfPlayers;

    public Color[] playerColors;

    public Slider chargeSlider;
    public TextMeshProUGUI currentPlayerText;

    [HideInInspector] public int currentPlayerIndex;
    private GameObject currentMarble;
    private bool isWaitingForNextPlayer = false;

    [HideInInspector] public List<GameObject> spawnedMarbles = new List<GameObject>();

    [HideInInspector] public PointManager pointManager;

    private bool isCharging = false;
    private float chargeTime = 0f;
    private float maxChargeTime = 4f;

    private Scene currentScene;
    private string sceneName;
    private int sceneIndex;

    void Start()
    {
        if (playerColors.Length < numberOfPlayers)
        {
            Debug.LogError("Zorg ervoor dat er genoeg kleuren zijn ingesteld voor alle spelers!");
            return;
        }

        if (chargeSlider != null)
        {
            chargeSlider.gameObject.SetActive(false);
            chargeSlider.maxValue = maxChargeTime;
            chargeSlider.value = 0;
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

            HandleCharging(controller);
        }
    }

    void HandleCharging(PlayerController controller)
    {
        if (Input.GetKeyDown(KeyCode.Space) && !controller.isShoot)
        {
            isCharging = true;
            chargeTime = 0f;

            if (chargeSlider != null)
            {
                chargeSlider.gameObject.SetActive(true);
                chargeSlider.value = 0;
            }
        }

        if (isCharging)
        {
            chargeTime += Time.deltaTime;
            chargeTime = Mathf.Clamp(chargeTime, 0, maxChargeTime);

            if (chargeSlider != null)
            {
                chargeSlider.value = chargeTime;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && isCharging)
        {
            isCharging = false;

            if (chargeSlider != null)
            {
                chargeSlider.gameObject.SetActive(false);
            }

            controller.Shoot(chargeTime);
        }
    }

    public void SpawnMarbleForPlayer()
    {
        if (currentPlayerIndex >= numberOfPlayers)
        {
            Debug.Log("Het spel is voorbij! Alle spelers hebben geschoten.");
            pointManager.DetermineClosestMarble();

            StartCoroutine(SceneSwitch(1.3f));
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
        currentPlayerText.text = ($"Speler {currentPlayerIndex + 1} turn");

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
        yield return new WaitForSeconds(5f);

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

    private IEnumerator SceneSwitch(float delay)
    {
        yield return new WaitForSeconds(delay);
        currentScene = SceneManager.GetActiveScene();

        sceneName = currentScene.name;
        sceneIndex = currentScene.buildIndex;

        if (sceneName == "Knikkeren lvl 1 Kim")
        {
            SceneManager.LoadScene("Knikkeren lvl 2 Kim");
        }

        if (sceneName == "Knikkeren lvl 2 Kim")
        {
            SceneManager.LoadScene("Knikkeren lvl 3 Kim");
        }
           
        if(sceneName == "Knikkeren lvl 3 Kim")
        {
            SceneManager.LoadScene("lobby");
        }
    }
}
