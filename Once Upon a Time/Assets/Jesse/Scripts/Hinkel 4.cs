using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Hinkel4 : MonoBehaviour
{
    [SerializeField] private Transform playerLocation;

    [SerializeField] private List<Transform> nextLocations;
    public List<Image> keyImages;
    private List<Image> randomImage1;
    private List<Image> randomImage2;

    private Dictionary<KeyCode, Image> keyImageMapping;

    private float randomTime1;
    private float randomTime2;
    public float speed;

    void Start()
    {
        randomTime1 = Random.Range(2f, 4f);
        randomTime2 = Random.Range(0.5f, 3f);

        foreach (Image image in keyImages)
        {
            image.enabled = false;
        }

        keyImageMapping = new Dictionary<KeyCode, Image>
        {
            { KeyCode.Q, keyImages[0] },
            { KeyCode.W, keyImages[1] },
            { KeyCode.E, keyImages[2] },
            { KeyCode.R, keyImages[3] },
            { KeyCode.T, keyImages[4] },
            { KeyCode.Y, keyImages[5] },
            { KeyCode.U, keyImages[6] },
            { KeyCode.I, keyImages[7] },
            { KeyCode.O, keyImages[8] },
            { KeyCode.P, keyImages[9] },

        };

        StartCoroutine(StartGame());
    }

    List<Image> GetRandomImages(List<Image> inputList, int count)
    {
        List<Image> outputList = new List<Image>();

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, inputList.Count);
            outputList.Add(inputList[index]);
        }
        return outputList;
    }

    private IEnumerator ShowImage1()
    {
        yield return new WaitForSeconds(randomTime1);
        randomImage1 = GetRandomImages(keyImages, 1);
        randomImage1[0].enabled = true;

        while (randomImage1[0].enabled)
        {
            CheckKeyPress1();
            yield return null;
        }
    }

    private IEnumerator ShowImage2()
    {
        yield return new WaitForSeconds(randomTime2);
        randomImage2 = GetRandomImages(keyImages, 2);
        randomImage2[0].enabled = true;
        randomImage2[1].enabled = true;

        while (randomImage2[0].enabled && randomImage2[1].enabled)
        {
            CheckKeyPress2();
            yield return null;
        }
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1);
        playerLocation.transform.position = Vector3.MoveTowards(playerLocation.transform.position, nextLocations[1].position, speed);
        nextLocations.Remove(nextLocations[0]);
    }

    private void CheckKeyPress1()
    {
        foreach (var keyImages in keyImageMapping)
        {
            if (Input.GetKeyDown(keyImages.Key) && keyImages.Value.enabled)
            {
                keyImages.Value.enabled = false;
                MoveToNextLocation();
                break;
            }
        }
    }

    private void CheckKeyPress2()
    {
        var keysPressed = keyImageMapping
            .Where(kv => Input.GetKey(kv.Key) && kv.Value.enabled)
            .ToList();

        if (keysPressed.Count == 2)
        {
            foreach (var keyImage in keysPressed)
            {
                keyImage.Value.enabled = false;
            }

            MoveToNextLocation();
        }
    }

    private void MoveToNextLocation()
    {
        if (nextLocations.Count > 1)
        {
            playerLocation.transform.position = Vector3.MoveTowards(playerLocation.transform.position, nextLocations[1].position, speed);
            nextLocations.Remove(nextLocations[0]);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("1 vakje"))
        {
            Debug.Log("in1");
            StartCoroutine(ShowImage1());
        }

        if (other.gameObject.CompareTag("2 vakjes"))
        {
            Debug.Log("in2");
            StartCoroutine(ShowImage2());
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Won!");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("1 vakje"))
        {
            //CheckKeyPress1();
        }

        if (other.gameObject.CompareTag("2 vakjes"))
        {
            //CheckKeyPress2();
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Won!");
        }
    }
}
