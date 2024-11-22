using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerKnikkeren : MonoBehaviour
{
    public Transform objectA;
    public Transform objectB;

    public float distanceThreshold;

    void Update()
    {
        if (objectA != null && objectB != null)
        {
            float distance = Vector3.Distance(objectA.position, objectB.position);

            if (distance < distanceThreshold)
            {
                Debug.Log("ja");
            }
            else
            {
                Debug.Log("nee");
            }
        }
    }
}
