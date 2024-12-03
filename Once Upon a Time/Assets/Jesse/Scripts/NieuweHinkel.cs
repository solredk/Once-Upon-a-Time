using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NieuweHinkel : MonoBehaviour
{
    private Hinkel hinkel;

    [SerializeField] public Transform playerPos;

    [SerializeField] public List<Transform> nextLocations;
    [SerializeField] public List<Image> keyImages;
    [SerializeField] public List<KeyCode> keysCodes;
    private List<Image> randomImage1;
    private List<Image> randomImage2;

    public Dictionary<KeyCode, Image> keyMapping1;

    public float speed;

    private void Start()
    {
        hinkel = FindAnyObjectByType<Hinkel>();

        ResetImages();

        keyMapping1 = new Dictionary<KeyCode, Image>
        {
            { KeyCode.Q, keyImages[0] },
            { KeyCode.W, keyImages[1] },
            { KeyCode.E, keyImages[2] },
            { KeyCode.R, keyImages[3] },
            { KeyCode.T, keyImages[4] },
            { KeyCode.Y, keyImages[5] }
        };
    }

        public void StartMove()
    {
        playerPos.transform.position = Vector3.MoveTowards(playerPos.transform.position, nextLocations[1].position, speed);
        nextLocations.Remove(nextLocations[0]);
    }

    public void MoveToNextLocation()
    {
        if (nextLocations.Count > 1)
        {
            playerPos.transform.position = Vector3.MoveTowards(playerPos.transform.position, nextLocations[1].position, speed);
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

    public IEnumerator ShowImage1()
    {
        yield return new WaitForSeconds(hinkel.randomTime1);
        randomImage1 = GetRandomImages(keyImages, 1);
        randomImage1[0].enabled = true;

        while (randomImage1[0].enabled)
        {
            CheckKeyPress1();
            yield return null;
        }
    }

    public IEnumerator ShowImage2()
    {
        yield return new WaitForSeconds(hinkel.randomTime2);
        randomImage2 = GetRandomImages(keyImages, 2);
        randomImage2[0].enabled = true;
        randomImage2[1].enabled = true;

        while (randomImage2[0].enabled && randomImage2[1].enabled)
        {
            CheckKeyPress2();
            yield return null;
        }
    }

    private void CheckKeyPress1()
    {
        Debug.Log("CheckingkeyPress1");
        for (int i = 0; i < keyImages.Count; i++)
        {
            if (Input.GetKeyDown(keysCodes[i]) && keyImages[i].enabled)
            {
                ResetImages();
                MoveToNextLocation();
                break;
            }
        }
    }

    private void CheckKeyPress2()
    {
        for (int i = 0; i < keyImages.Count; i++)
        {
            for (int j = 0; j < keyImages.Count; j++)
            {
                if (Input.GetKeyDown(keysCodes[i]) && keyImages[i].enabled && Input.GetKeyDown(keysCodes[j]) && keyImages[j].enabled)
                {
                    ResetImages();
                    MoveToNextLocation();
                    break;
                }
            }
        }
    }
    void ResetImages()
    {
        for (int i = 0; i < keyImages.Count; i++)
        {
            keyImages[i].enabled = false;
        }
    }
}
