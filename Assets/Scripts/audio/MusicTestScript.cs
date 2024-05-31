using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTestScript : MonoBehaviour
{

    public GameObject MusicManager;
    public string MusicName;
    public bool SendMusic;

    void Awake () {
        MusicManager = GameObject.Find ("MusicManager");
    }

    void LateUpdate () {
        if (SendMusic) {
            SendMusic = false;
            MusicManager.SendMessage ("PlayTrack", MusicName);
        }

    }
}
