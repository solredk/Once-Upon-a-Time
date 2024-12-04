using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] PlayerInputManager playerInputManager;
    [SerializeField] TextMeshProUGUI playerCount;

    [SerializeField] List<GameObject> Players; [Tooltip("de default player prefaps (max 4)")]
    [SerializeField] List<GameObject> currentPlayers; [Tooltip("de prefaps in de scene (max 4)")]
    // Start is called before the first frame update
    void Start()
    {
        playerInputManager.playerPrefab = Players[playerInputManager.playerCount];
        if (playerInputManager != null)
        {
            playerInputManager = GetComponent<PlayerInputManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerCount.text = playerInputManager.playerCount + "/4 players";
    }
    public virtual void DoJoin()
    {
        playerInputManager.playerPrefab = Players[playerInputManager.playerCount];
        playerCount.text = playerInputManager.playerCount + "/4 players";

    }


}

