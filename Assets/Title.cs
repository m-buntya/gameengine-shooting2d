using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    private void Start()
    {

    }

    private void Update()
    {

    }

    public void SwitchScene()
    {
        SceneManager.LoadScene("MainScenes", LoadSceneMode.Single);
    }



}