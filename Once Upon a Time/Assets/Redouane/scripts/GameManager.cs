using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public abstract class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public enum GameState
    {
        preparelevel,
        StartGame,
        CollectData,
        Endgame,
        PauzeGame
    }

    [SerializeField] protected PlayerInputManager playerInputManager;
    [SerializeField] protected List<TextMeshProUGUI> textMeshProUGUI;
    
    [SerializeField] protected List<GameObject> Players; [Tooltip("de default player prefaps (max 4)")]
    [SerializeField] protected List<GameObject> currentPlayers; [Tooltip("de prefaps in de scene (max 4)")]

    [Header("de state waar de game in zit")]
    [SerializeField] protected GameState state;
    [SerializeField] public GameState State { get { return state; } set { state = value; } }

    [Header("de counters")]
    [SerializeField] protected float Gameduration;
    [SerializeField] protected float counter = 10f; [Tooltip("de timer voor de start van de game en de game zelf")]

    [Header("de player nummers")]
    [SerializeField] protected int randomTaggerIndex; [Tooltip("de random nummer die wordt gegenereerd aan de start van de game om de player voor de stellen")]
    [SerializeField] protected int playerInt;
    [SerializeField] protected int playercount;

    // Start is called before the first frame update
    void Start()
    {
        playerInputManager.playerPrefab = Players[0];
        if (playerInputManager != null)
            playerInputManager = GetComponent<PlayerInputManager>();
        textMeshProUGUI[0].text = "no players in lobby";
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case GameState.preparelevel:
                PrepareLevel();
                break;
            case GameState.StartGame:
                StartGame();
                break;
            case GameState.CollectData:
                CollectData();
                break;
            case GameState.Endgame:
                PlayerPrefs.SetInt("game", 1);
                GetComponent<UIManager>().GoToLeaderboard();
                break;
                
        }

    }
    protected virtual void PrepareLevel()
    {
        playerInputManager.EnableJoining();
    }

    protected virtual void StartGame()
    {
        Gameduration -= Time.deltaTime;
        textMeshProUGUI[0].text = Gameduration.ToString("N0");
        textMeshProUGUI[1].text = "";
        if (Gameduration <= 0)
        {
            state = GameState.CollectData;
        }
    }

    protected virtual void CollectData()
    {
        state = GameState.Endgame;
    }

    public virtual void DoJoin()
    {
        playerInputManager.playerPrefab = Players[playerInputManager.playerCount];
        textMeshProUGUI[0].text = playerInputManager.playerCount + "/4 players";

    }
}
