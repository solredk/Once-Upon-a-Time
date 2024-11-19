using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tijdelijk : MonoBehaviour
{
    [SerializeField] List<Image> list = new List<Image>();

    List<Image> GetRandomImages(List<Image> inputList, int count)
    {
        List<Image> outputList = new List<Image>();
        for(int i = 0; i < count; i++)
        {
            int index = Random.Range(0, inputList.Count);
            outputList.Add(inputList[index]);
        }
        return outputList;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            var randomImage1 = GetRandomImages(list, 1);
            Debug.Log(randomImage1);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            var randomImage2 = GetRandomImages(list, 2);
            Debug.Log(randomImage2);
        }
    }
}
