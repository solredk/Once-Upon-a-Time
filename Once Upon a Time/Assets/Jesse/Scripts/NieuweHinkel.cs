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

    public Dictionary<KeyCode, Image> keyMapping1;

    public float speed;

    private void Start()
    {
        hinkel = FindAnyObjectByType<Hinkel>();

        keyMapping1 = new Dictionary<KeyCode, Image>
        {
            { KeyCode.Q, keyImages[0] },
            { KeyCode.W, keyImages[1] },
            { KeyCode.E, keyImages[2] },
            { KeyCode.R, keyImages[3] },
            { KeyCode.T, keyImages[4] },
            { KeyCode.Y, keyImages[5] }
        };

        StartCoroutine(StartMove(1f));
    }

    public IEnumerator StartMove(float delay)
    {
        yield return new WaitForSeconds(delay);
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
            StartCoroutine(hinkel.ShowImage1());
        }

        if (other.gameObject.CompareTag("2 vakjes"))
        {
            Debug.Log("in2");
            StartCoroutine(hinkel.ShowImage2());
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Won!");
        }
    }
}
