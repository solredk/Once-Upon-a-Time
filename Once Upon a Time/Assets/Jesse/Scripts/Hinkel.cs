using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Hinkel : MonoBehaviour
{
    private NieuweHinkel NieuweHinkel;

    public List<GameObject> players; 
    public List<Image> keyImages;
    private List<Image> randomImage1;
    private List<Image> randomImage2;

    private float randomTime1;
    private float randomTime2;

    void Start()
    {
        randomTime1 = Random.Range(2f, 4f);
        randomTime2 = Random.Range(0.5f, 3f);

        foreach (Image image in keyImages)
        {
            image.enabled = false;
        }

        //StartCoroutine(StartGame());
    }

    List<Image> GetRandomImages(List<Image> inputList, int count)
    {
        List<Image> outputList = new List<Image>();

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, inputList.Count);
            outputList.Add(inputList[index]);
        }
        return outputList;
    }

    public IEnumerator ShowImage1()
    {
        yield return new WaitForSeconds(randomTime1);
        randomImage1 = GetRandomImages(keyImages, 1);
        randomImage1[0].enabled = true;

        while (randomImage1[0].enabled)
        {
            CheckKeyPress1();
            yield return null;
        }
    }

    public IEnumerator ShowImage2()
    {
        yield return new WaitForSeconds(randomTime2);
        randomImage2 = GetRandomImages(keyImages, 2);
        randomImage2[0].enabled = true;
        randomImage2[1].enabled = true;

        while (randomImage2[0].enabled && randomImage2[1].enabled)
        {    
            CheckKeyPress2();
            yield return null;
        }
    }

    //private IEnumerator StartGame()
    //{
    //    yield return new WaitForSeconds(1f);
    //    NieuweHinkel.StartMove();
    //}

    private void CheckKeyPress1()
    {
        Debug.Log("CheckingkeyPress1");
        foreach(var keyImages in NieuweHinkel.keyMapping1)
        {
            if(Input.GetKeyDown(keyImages.Key) && keyImages.Value.enabled)
            {
                keyImages.Value.enabled = false;
                NieuweHinkel.MoveToNextLocation();
                break;
            }
        }
    }

    private void CheckKeyPress2()
    {
        var keysPressed = NieuweHinkel.keyMapping1
            .Where(kv => Input.GetKey(kv.Key) && kv.Value.enabled)
            .ToList();

        if (keysPressed.Count == 2)
        {
            foreach (var keyImage in keysPressed)
            {
                keyImage.Value.enabled = false;
            }

            NieuweHinkel.MoveToNextLocation();
        }
    }
}
