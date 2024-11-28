using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFader : MonoBehaviour
{
    [SerializeField] Material baseMaterial;
    [SerializeField] Material fadeMaterial;

    enum MaterialState
    {
        normall,
        shaders
    }


    Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        renderer.material = new Material(fadeMaterial);

    }
    private void OnTriggerStay(Collider other)
    {
        renderer.material = new Material(fadeMaterial);
    }
    private void OnTriggerExit(Collider other)
    {
        renderer.material = new Material(baseMaterial);        
    }
}
