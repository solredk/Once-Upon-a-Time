using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameChooser : MonoBehaviour
{
    [SerializeField] int sceneIndex;

    private void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene(sceneIndex);
    }

}
