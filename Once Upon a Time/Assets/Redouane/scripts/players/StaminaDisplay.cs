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
        // Zoek de hoofdcamera

        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {

        transform.position = transform.parent.position + Vector3.up * 2f; 
        
        // Laat het object altijd naar de camera wijzen
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
