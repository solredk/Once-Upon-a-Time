using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
    enum PlayerState 
    {
        tikker,
        verstopper,
        invincible
    }

public class Tag : MonoBehaviour
{
    [SerializeField] PlayerState state;

    [SerializeField] GameObject closestPlayer;
    
    [SerializeField] bool inTagRadius;

    float SurviveTime;
    float counter;

    private void Update()
    {
        if (state == PlayerState.verstopper)
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
    }
    public void Getaged()
    {
        if (state == PlayerState.verstopper)
        {
            state = PlayerState.tikker;
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
            closestPlayer.GetComponentInChildren<Tag>().Getaged();
        }
    }

    public float ReturnSurviveTime()
    {
        return SurviveTime;
    }
}
