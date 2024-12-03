using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Hinkel : MonoBehaviour
{
    [SerializeField] private List<NieuweHinkel> Nieuwehinkel;

    public float randomTime1;
    public float randomTime2;

    void Start()
    {
        randomTime1 = Random.Range(2f, 4f);
        randomTime2 = Random.Range(0.5f, 3f);

        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < Nieuwehinkel.Count; i++)
        {
            Nieuwehinkel[i].StartMove();
        }
    }    
}
