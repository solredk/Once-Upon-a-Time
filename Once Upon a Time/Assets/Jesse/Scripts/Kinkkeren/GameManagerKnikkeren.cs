using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerKnikkeren : MonoBehaviour
{
    public List<GameObject> playerPrefabs;

    public Transform objectA;
    public Transform objectB;
    public Transform beginLocation;

    private PlayerController playerController;

    public float distanceThreshold;

    void Update()
    {
        if (objectA != null && objectB != null)
        {
            float distance = Vector3.Distance(objectA.position, objectB.position);

            if (distance < distanceThreshold)
            {
                Debug.Log("ja");
                Instantiate(playerPrefabs[1], beginLocation.transform.position, Quaternion.identity);
                playerPrefabs.Remove(playerPrefabs[0]);
            }
            else
            {
                Debug.Log("nee");
            }
        }
    }
}
