using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;
    public Camera MonstreCamera;

    public static CameraManager instance;
    public float screenShakeDuration = 1f;
    public float screenShakeMagnitude = 1f;
    private Vector3 originalCameraPosition;
    public bool isShaking = false;



    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Attention plusieur instance de CameraManager");
            return;
        }
        instance = this;
    }

    void Start()
    {
        mainCamera.enabled = true;
        MonstreCamera.enabled = false;
    }

    // Fonction pour activer la cam�ra du monstre
    public void SwitchToBossCamera()
    {
        mainCamera.enabled = false;
        MonstreCamera.enabled = true;
        //StartCoroutine(ScreenShake()); // Appel de la coroutine pour le tremblement d'�cran
        MonstreControler.instance.EndGame(); //Pour pr�parer la fin du jeu
    }


    public IEnumerator ScreenShake()
    {
        // Sauvegardez la position de la cam�ra originale
        originalCameraPosition = Camera.main.transform.position;

        isShaking = true;
        float elapsedTime = 0f;

        while (elapsedTime < screenShakeDuration)
        {
            float xOffset = Random.Range(-screenShakeMagnitude, screenShakeMagnitude);
            float yOffset = Random.Range(-screenShakeMagnitude, screenShakeMagnitude);

            // D�placez la cam�ra de mani�re al�atoire pour simuler la secousse
            Camera.main.transform.position = originalCameraPosition + new Vector3(xOffset, yOffset, originalCameraPosition.z);

            // Incr�mentez le temps �coul�
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        // R�initialisez la position de la cam�ra � sa position d'origine
        Camera.main.transform.position = originalCameraPosition;
        isShaking = false;
    }
}