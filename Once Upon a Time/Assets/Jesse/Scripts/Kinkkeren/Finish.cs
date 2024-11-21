using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public Transform spawnLocation;
    public Transform endLocation;
    public Transform endlocation2;
    public GameObject player;

    private float speed = 5f;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        if (other.gameObject.CompareTag("Player1"))
        {
            player.transform.position = Vector3.MoveTowards(endLocation.transform.position, endlocation2.transform.position, speed);
        }
    }
}
