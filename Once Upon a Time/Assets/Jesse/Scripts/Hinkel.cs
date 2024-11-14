using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hinkel : MonoBehaviour
{
    [SerializeField] private Transform playerLocation;
    [SerializeField] private List<Transform> nextLocations;

    void Start()
    {

    }

    void Update()
    {
        playerLocation.position = transform.position;

        if(Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("Pressed");
            switch(nextLocations.Count)
            {
                case 0:
                   //playerLocation = nextLocation[i]
                    break;
            }
        }
    }
}
