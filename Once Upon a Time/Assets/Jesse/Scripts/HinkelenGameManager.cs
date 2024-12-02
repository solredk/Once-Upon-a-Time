using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HinkelenGameManager : MonoBehaviour
{
    [SerializeField] private List<Transform> playerLocations;
    [SerializeField] private List<List<Transform>> nextLocationsPerPlayer;
    [SerializeField] private List<List<Image>> keyImagesPerPlayer;
    [SerializeField] private float speed;

    private List<Dictionary<KeyCode, Image>> keyImageMappings;
    private List<float> randomTime1PerPlayer;
    private List<float> randomTime2PerPlayer;
    private List<bool> playerFinished;
    void Start()
    {
        int playerCount = playerLocations.Count;
        randomTime1PerPlayer = new List<float>();
        randomTime2PerPlayer = new List<float>();
        keyImageMappings = new List<Dictionary<KeyCode, Image>>();
        playerFinished = new List<bool>();

        for (int i = 0; i < playerCount; i++)
        {
            randomTime1PerPlayer.Add(Random.Range(2f, 4f));
            randomTime2PerPlayer.Add(Random.Range(0.5f, 3f));
            playerFinished.Add(false);

            keyImageMappings.Add(new Dictionary<KeyCode, Image>
            {
                { KeyCode.Q, keyImagesPerPlayer[i][0] },
                { KeyCode.W, keyImagesPerPlayer[i][1] },
                { KeyCode.E, keyImagesPerPlayer[i][2] },
                { KeyCode.R, keyImagesPerPlayer[i][3] },
                { KeyCode.T, keyImagesPerPlayer[i][4] },
                { KeyCode.Y, keyImagesPerPlayer[i][5] },
                { KeyCode.U, keyImagesPerPlayer[i][6] },
                { KeyCode.I, keyImagesPerPlayer[i][7] },
                { KeyCode.O, keyImagesPerPlayer[i][8] },
                { KeyCode.P, keyImagesPerPlayer[i][9] }
            });

            foreach (Image image in keyImagesPerPlayer[i])
            {
                image.enabled = false;
            }
        }

        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1);

        for (int i = 0; i < playerLocations.Count; i++)
        {
            MovePlayerToNextLocation(i);
        }
    }

    private void MovePlayerToNextLocation(int playerIndex)
    {
        if (nextLocationsPerPlayer[playerIndex].Count > 1)
        {
            Transform player = playerLocations[playerIndex];
            Transform nextLocation = nextLocationsPerPlayer[playerIndex][1];
            player.position = Vector3.MoveTowards(player.position, nextLocation.position, speed);
            nextLocationsPerPlayer[playerIndex].RemoveAt(0);
        }
        else
        {
            playerFinished[playerIndex] = true;
        }
    }

    private IEnumerator ShowImage1(int playerIndex)
    {
        yield return new WaitForSeconds(randomTime1PerPlayer[playerIndex]);

        var randomImage = GetRandomImages(keyImagesPerPlayer[playerIndex], 1);
        randomImage[0].enabled = true;

        while (randomImage[0].enabled)
        {
            CheckKeyPress1(playerIndex);
            yield return null;
        }
    }

    private IEnumerator ShowImage2(int playerIndex)
    {
        yield return new WaitForSeconds(randomTime2PerPlayer[playerIndex]);

        var randomImages = GetRandomImages(keyImagesPerPlayer[playerIndex], 2);
        randomImages[0].enabled = true;
        randomImages[1].enabled = true;

        while (randomImages[0].enabled && randomImages[1].enabled)
        {
            CheckKeyPress2(playerIndex);
            yield return null;
        }
    }

    private void CheckKeyPress1(int playerIndex)
    {
        foreach (var keyImage in keyImageMappings[playerIndex])
        {
            if (Input.GetKeyDown(keyImage.Key) && keyImage.Value.enabled)
            {
                keyImage.Value.enabled = false;
                MovePlayerToNextLocation(playerIndex);
                break;
            }
        }
    }

    private void CheckKeyPress2(int playerIndex)
    {
        var keysPressed = keyImageMappings[playerIndex]
            .Where(kv => Input.GetKey(kv.Key) && kv.Value.enabled)
            .ToList();

        if (keysPressed.Count == 2)
        {
            foreach (var keyImage in keysPressed)
            {
                keyImage.Value.enabled = false;
            }

            MovePlayerToNextLocation(playerIndex);
        }
    }

    private List<Image> GetRandomImages(List<Image> inputList, int count)
    {
        List<Image> outputList = new List<Image>();

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, inputList.Count);
            outputList.Add(inputList[index]);
        }

        return outputList;
    }

    private void OnTriggerEnter(Collider other, int playerIndex)
    {
        if (other.gameObject.CompareTag("1 vakje"))
        {
            Debug.Log($"Player {playerIndex + 1} in 1 vakje");
            StartCoroutine(ShowImage1(playerIndex));
        }

        if (other.gameObject.CompareTag("2 vakjes"))
        {
            Debug.Log($"Player {playerIndex + 1} in 2 vakjes");
            StartCoroutine(ShowImage2(playerIndex));
        }

        if (other.gameObject.CompareTag("Finish") && !playerFinished[playerIndex])
        {
            Debug.Log($"Player {playerIndex + 1} Won!");
            playerFinished[playerIndex] = true;
        }
    }

    public void OnPlayerTriggerEnter(int playerIndex, string triggerTag)
    {
        switch (triggerTag)
        {
            case "1 vakje":
                StartCoroutine(ShowImage1(playerIndex));
                break;

            case "2 vakjes":
                StartCoroutine(ShowImage2(playerIndex));
                break;

            case "Finish":
                if (!playerFinished[playerIndex])
                {
                    Debug.Log($"Player {playerIndex + 1} won!");
                    playerFinished[playerIndex] = true;
                }
                break;

            default:
                Debug.Log($"Unknown trigger: {triggerTag}");
                break;
        }
    }
}
