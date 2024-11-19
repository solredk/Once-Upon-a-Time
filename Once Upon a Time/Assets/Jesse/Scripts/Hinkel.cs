using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hinkel : MonoBehaviour
{
    [SerializeField] private Transform playerLocation;
    [SerializeField] private List<Transform> nextLocations;
    public float speed;

    [SerializeField] private Image Qkey;
    [SerializeField] private Image Wkey;
    [SerializeField] private Image Ekey;
    [SerializeField] private Image Rkey;
    [SerializeField] private Image Tkey;
    [SerializeField] private Image Ykey;



    private float randomtime;

    void Start()
    {
        randomtime = Random.Range(1.5f, 5f);

        Qkey.enabled = false;
        Wkey.enabled = false;
        Ekey.enabled = false;
        Rkey.enabled = false;
        Tkey.enabled = false;
        Ykey.enabled = false;

        StartCoroutine(StartGame());
        StartCoroutine(keysSpawn());
    }

    private void Update()
    {
        
    }

    private IEnumerator keysSpawn()
    {
        yield return new WaitForSeconds(2);
        Ykey.enabled = false;
        Qkey.enabled = true;
        yield return new WaitForSeconds(randomtime);
        Qkey.enabled = false;
        Wkey.enabled = true;
        yield return new WaitForSeconds(randomtime);
        Wkey.enabled = false;
        Ekey.enabled = true;
        yield return new WaitForSeconds(randomtime);
        Ekey.enabled = false;
        Rkey.enabled = true;
        yield return new WaitForSeconds(randomtime);
        Rkey.enabled = false;
        Tkey.enabled = true;
        yield return new WaitForSeconds(randomtime);
        Tkey.enabled = false;
        Ykey.enabled = true;
        yield return new WaitForSeconds(randomtime);
        StartCoroutine(keysSpawn());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1);
        playerLocation.transform.position = Vector3.MoveTowards(playerLocation.transform.position, nextLocations[1].position, speed);
        nextLocations.Remove(nextLocations[0]);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(gameObject.CompareTag("1 vakje"))
        {
            Debug.Log("in");

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

        if (other.gameObject.CompareTag("2 vakjes"))
        {
            Debug.Log("in");
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Won!");
        }
    }
}
