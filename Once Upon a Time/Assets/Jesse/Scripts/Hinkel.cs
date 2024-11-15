using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hinkel : MonoBehaviour
{
    [SerializeField] private Transform playerLocation;
    [SerializeField] private List<Transform> nextLocations;
    public float speed;

    void Start()
    {

    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("Pressed");
            playerLocation.transform.position = Vector3.MoveTowards(playerLocation.transform.position, nextLocations[1].position, speed);
            nextLocations.Remove(nextLocations[0]);
        }
    }
}
