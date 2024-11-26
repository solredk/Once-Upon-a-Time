using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFader : MonoBehaviour
{
    [SerializeField] float fadeSpeed = 0.1f; // Snelheid van de fade-overgang
    [SerializeField] float fadeAmount = 0.1f; // De dofheid/sterkte van de transparantie
    float originalAlpha; // De originele alpha waarde van het materiaal

    Renderer renderer;
    Material material;
    [SerializeField] Material transparentMaterial; // Transparant materiaal
    [SerializeField] Material originalMaterial; // Oorspronkelijk materiaal

    Color currentColor;
    Color smoothColor;

    [SerializeField] bool doFade;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        material = renderer.material; // Gebruik het huidige materiaal van de renderer
        originalAlpha = material.color.a; // Sla de originele alpha waarde op
    }

    // Update is called once per frame
    void Update()
    {
        if (doFade)
        {
            DoFade();
        }
        else
        {
            ResetFade();
        }
    }

    void DoFade()
    {
        // Zorg ervoor dat het materiaal transparant is als fade aan is
        if (material != transparentMaterial)
        {
            material = new Material(transparentMaterial);
            renderer.material = material;
        }

        currentColor = material.color;
        smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, fadeAmount, fadeSpeed));
        material.color = smoothColor;
    }

    void ResetFade()
    {
        // Zet het materiaal terug naar het originele materiaal
        if (material != originalMaterial)
        {
            material = new Material(originalMaterial);
            renderer.material = material;
        }

        currentColor = material.color;
        smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, originalAlpha, fadeSpeed));
        material.color = smoothColor;
    }
}
