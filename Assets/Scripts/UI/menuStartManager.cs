using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class menuStartManager : MonoBehaviour
{
    public string NomSceneJeux;


    public void LancerLeJeux()
    {
        SceneManager.LoadScene(NomSceneJeux);
    }

    public void Quitter()
    {
        Application.Quit();
    }
}
