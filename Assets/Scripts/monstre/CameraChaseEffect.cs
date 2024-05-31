using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChaseEffect : MonoBehaviour
{

    private Camera mainCamera;


    void Awake () { mainCamera = Camera.main; }

    public void BroadcastMonsterChase() {
        
        /* adding intense black fog to global environment (lighting settings) */
        RenderSettings.fog = true;
        RenderSettings.fogColor = Color.black;
        RenderSettings.fogDensity = 0.2f;        
    }

}
