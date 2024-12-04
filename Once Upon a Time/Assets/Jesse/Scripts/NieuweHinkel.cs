using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    private HashSet<int> pressedKeys = new HashSet<int>();
    private float keyPressTimeout = 1.0f;
    private float lastKeyPressTime = 0f;

    public float speed;

    private void Start()
    {
        hinkel = FindAnyObjectByType<Hinkel>();

        ResetImages();
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
            SceneManager.LoadScene("lobby");
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
        Debug.Log("Spawning 1 Image...");

        yield return new WaitForSeconds(hinkel.randomTime1);

        randomImage1 = GetRandomImages(keyImages, 1);

        yield return new WaitForSeconds(0.1f);

        randomImage1[0].enabled = true;

        Debug.Log("Spawning 1 Image done....");

        while (randomImage1[0].enabled)
        {
            CheckKeyPress1();
            yield return null;
        }
    }

    public IEnumerator ShowImage2()
    {
        Debug.Log("Spawning 2 Images....");

        yield return new WaitForSeconds(hinkel.randomTime2);

        randomImage2 = GetRandomImages(keyImages, 2);

        if (randomImage2 == null || randomImage2.Count < 2)
        {
            Debug.LogError("Error: GetRandomImages returned less than 2 images.");
            yield break;
        }

        yield return new WaitForSeconds(0.1f);

        if (randomImage2.Count == 2) 
        {  
            randomImage2[0].enabled = true;
            randomImage2[1].enabled = true;
        }

        Debug.Log("Spawning 2 Images done.....");

        while (randomImage2[0].enabled && randomImage2[1].enabled)
        {
            CheckKeyPress2();
            yield return null;
        }
    }

    private void CheckKeyPress1()
    {
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
            if (Input.GetKeyDown(keysCodes[i]) && keyImages[i].enabled)
            {
                pressedKeys.Add(i);
                lastKeyPressTime = Time.time;
            }
        }

        if (Time.time - lastKeyPressTime > keyPressTimeout)
        {
            pressedKeys.Clear();
        }


        if (pressedKeys.Count == 2)
        {
            ResetImages();
            MoveToNextLocation();
            pressedKeys.Clear();
        }
    }
    void ResetImages()
    {
        for (int i = 0; i < keyImages.Count; i++)
        {
            for(int j = 0;j < keyImages.Count; j++)
            {
                keyImages[i].enabled = false;
                keyImages[j].enabled = false;
            }
        }
    }
}
