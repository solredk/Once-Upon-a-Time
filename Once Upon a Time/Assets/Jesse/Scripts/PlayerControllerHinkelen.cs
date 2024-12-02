using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerHinkelen : MonoBehaviour
{
    public int playerIndex;
    public Transform playerTransform;
    private HinkelenGameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<HinkelenGameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("1 vakje"))
        {
            Debug.Log($"Player {playerIndex + 1} entered 1 vakje");
            gameManager.OnPlayerTriggerEnter(playerIndex, "1 vakje");
        }

        if (other.gameObject.CompareTag("2 vakjes"))
        {
            Debug.Log($"Player {playerIndex + 1} entered 2 vakjes");
            gameManager.OnPlayerTriggerEnter(playerIndex, "2 vakjes");
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log($"Player {playerIndex + 1} reached Finish");
            gameManager.OnPlayerTriggerEnter(playerIndex, "Finish");
        }
    }
}
