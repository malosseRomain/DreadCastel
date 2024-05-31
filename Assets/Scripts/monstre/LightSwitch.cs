using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{

    public float updateDelay = .1f;

    private Light[] allLights;
    private Outline[] allOutlines;

    public Color ignoreColor = Color.red;


    public void BroadcastMonsterChase() {
        InvokeRepeating("TurnOffLights", 0, updateDelay);
    }

    void TurnOffLights() {

        allLights = FindObjectsOfType<Light>();
        allOutlines = FindObjectsOfType<Outline>();

        foreach (Light light in allLights) {

            if (light.color != ignoreColor)
                light.enabled = false;
            
            Component halo = light.gameObject.GetComponent("Halo");
            if (halo != null)
                halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
        }

        for (int i = 0; i < allOutlines.Length; i++) {
            allOutlines[i].enabled = false;
        }

    }

}
