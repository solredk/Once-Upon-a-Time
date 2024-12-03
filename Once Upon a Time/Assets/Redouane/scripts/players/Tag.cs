using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tag : MonoBehaviour
{
    public enum PlayerState 
    {
        tikker,
        verstopper,
        invincible
    }
    public string username;

    [Header("UI")]
    [SerializeField] UIManager uIManager;



    [SerializeField] PlayerState state;
    public PlayerState State { get { return state; } set { state = value; } }
    [SerializeField] GameObject closestPlayer;
    
    [SerializeField] bool inTagRadius;
    bool IsPaused;

    [SerializeField]float SurviveTime;
    [SerializeField] bool tagged;
    float counter;

    private void Start()
    {
        uIManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<UIManager>();
    }

    private void Update()
    {

        if (state == PlayerState.verstopper && HasPlayerInTikkerState())
        {
            SurviveTime += Time.deltaTime;
        }
        if (state == PlayerState.invincible)
        {
            counter += Time.deltaTime;
            if (counter > 2)
            {
                counter = 0;
                state = PlayerState.verstopper;
            }
        }
        if (tagged) 
        {
            Getaged();
        }
    }

    private bool HasPlayerInTikkerState()
    {
        // Assuming you have a way to get all players with the Tag script
        Tag[] players = FindObjectsOfType<Tag>();
        return players.Any(p => p.state == PlayerState.tikker);
    }
    public void Getaged()
    {
        if (state == PlayerState.verstopper)
        {
            if (TagGameManager.Instance != null)
            {
                Tag currentTagger = TagGameManager.instance.currentTagger;
                if (currentTagger != null && currentTagger != this)
                {
                    currentTagger.State = PlayerState.verstopper; 
                }
            }
            state = PlayerState.tikker;
            TagGameManager.instance.currentTagger = this; 
            tagged = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inTagRadius = true;
            closestPlayer = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inTagRadius = false;
        }
    }
    public void DoTaggen(InputAction.CallbackContext context)
    {
        if (state == PlayerState.tikker && inTagRadius)
        {
            state = PlayerState.invincible;
            if (closestPlayer != null && closestPlayer != this.gameObject)
            {
                closestPlayer.GetComponent<Tag>().tagged = true;
            }
        }
    }

    public float ReturnSurviveTime()
    {
        return SurviveTime;
    }
    public void Dopauze(InputAction.CallbackContext context)
    {
        uIManager.DoPauze();
    }
}
