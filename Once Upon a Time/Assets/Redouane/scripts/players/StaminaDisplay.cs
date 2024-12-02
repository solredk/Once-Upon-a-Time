using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class StaminaDisplay : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] PlayerMovment PlayerMovment;
    [SerializeField] TextMeshPro textMeshPro;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        transform.position = transform.parent.position + Vector3.up * 2f;         
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
        if (PlayerMovment.tired)
        {
            textMeshPro.text = "tired";
        }
        else
        {
            
        textMeshPro.text = PlayerMovment.stamina.ToString();
        }
    }
}
