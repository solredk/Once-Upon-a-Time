using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public Transform spawnLocation;
    public Transform endLocation;
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        if (other.gameObject.CompareTag("Player1"))
        {
            Destroy(other.gameObject);
            Instantiate(player, spawnLocation.transform.position, Quaternion.identity);
        }
    }
}
