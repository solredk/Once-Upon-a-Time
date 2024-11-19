using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Hinkel : MonoBehaviour
{
    [SerializeField] private Transform playerLocation;
    [SerializeField] private List<Transform> nextLocations;
    public float speed;

    public List<Image> keyImages;

    [SerializeField] private Image Qkey;
    [SerializeField] private Image Wkey;
    [SerializeField] private Image Ekey;
    [SerializeField] private Image Rkey;
    [SerializeField] private Image Tkey;
    [SerializeField] private Image Ykey;

    private float randomTime;

    void Start()
    {
        randomTime = Random.Range(2f, 8f);

        Qkey.enabled = false;
        Wkey.enabled = false;
        Ekey.enabled = false;
        Rkey.enabled = false;
        Tkey.enabled = false;
        Ykey.enabled = false;

        StartCoroutine(StartGame());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Qkey.enabled == true)
        {
            Qkey.enabled = false;
            playerLocation.transform.position = Vector3.MoveTowards(playerLocation.transform.position, nextLocations[1].position, speed);
            nextLocations.Remove(nextLocations[0]);
        }

        if (Input.GetKeyDown(KeyCode.W) && Wkey.enabled == true)
        {
            Wkey.enabled = false;
            playerLocation.transform.position = Vector3.MoveTowards(playerLocation.transform.position, nextLocations[1].position, speed);
            nextLocations.Remove(nextLocations[0]);
        }

        if (Input.GetKeyDown(KeyCode.E) && Ekey.enabled == true)
        {
            Ekey.enabled = false;
            playerLocation.transform.position = Vector3.MoveTowards(playerLocation.transform.position, nextLocations[1].position, speed);
            nextLocations.Remove(nextLocations[0]);
        }

        if (Input.GetKeyDown(KeyCode.R) && Rkey.enabled == true)
        {
            Rkey.enabled = false;
            playerLocation.transform.position = Vector3.MoveTowards(playerLocation.transform.position, nextLocations[1].position, speed);
            nextLocations.Remove(nextLocations[0]);
        }

        if (Input.GetKeyDown(KeyCode.T) && Tkey.enabled == true)
        {
            Tkey.enabled = false;
            playerLocation.transform.position = Vector3.MoveTowards(playerLocation.transform.position, nextLocations[1].position, speed);
            nextLocations.Remove(nextLocations[0]);
        }

        if (Input.GetKeyDown(KeyCode.Y) && Ykey.enabled == true)
        {
            Ykey.enabled = false;
            playerLocation.transform.position = Vector3.MoveTowards(playerLocation.transform.position, nextLocations[1].position, speed);
            nextLocations.Remove(nextLocations[0]);
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

    private IEnumerator showImage1()
    {
        yield return new WaitForSeconds(randomTime);
        Debug.Log("in1");
        List<Image> randomImage1 = GetRandomImages(keyImages, 1);
        randomImage1[0].enabled = true;
        StopCoroutine(showImage1());
    }

    private IEnumerator showImage2()
    {
        yield return new WaitForSeconds(randomTime);
        List<Image> randomImage1 = GetRandomImages(keyImages, 2);
        randomImage1[0].enabled = true;
        randomImage1[1].enabled = true;
        StopCoroutine(showImage2());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1);
        playerLocation.transform.position = Vector3.MoveTowards(playerLocation.transform.position, nextLocations[1].position, speed);
        nextLocations.Remove(nextLocations[0]);
        StopCoroutine(StartGame());
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("1 vakje"))
        {
            Debug.Log("in1");
            StartCoroutine(showImage1());
        }

        if (other.gameObject.CompareTag("2 vakjes"))
        {
            Debug.Log("in2");
            StartCoroutine(showImage2());
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Won!");
        }
    }
}
