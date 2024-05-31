/* Copyright 2023 Infinity Entertainment
   Permission is hereby granted, free of charge, to any person obtaining a copy of this software 
   and associated documentation files (the �Software�), to deal in the Software without 
   restriction, including without limitation the rights to use, copy, modify, merge, publish, 
   distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the 
   Software is furnished to do so, subject to the following conditions:
 
   The above copyright notice and this permission notice shall be included in all copies or 
   substantial portions of the Software.
 
   THE SOFTWARE IS PROVIDED �AS IS�, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING 
   BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
   NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, 
   DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/* track_t datastrucrtues. Contains all necessary data related to this script. */
[System.Serializable]
public struct track_t {
    public string name;
    public AudioClip clip;
    public AudioSource source;
}



/* Script that manages everything about sountracks in-game */
public class MusicManager : MonoBehaviour {

    public float transitionSpeed = .002f,
                 maxVolume = .1f;

    public track_t[] tracks;
    int current,
        next;
    


    AudioSource InitializeSource (AudioClip clip) {
        AudioSource source = gameObject.AddComponent<AudioSource> ();
        source.volume = 0;
        source.loop = true;
        source.clip = clip;
        source.Play ();
        return source;
    }

    void InitializeTracks (track_t[] tracks) {
        for (int i = 0; i < tracks.Length; ++i)
            tracks[i].source = InitializeSource (tracks[i].clip);
    }

    int FindTrackByName(track_t[] tracks, string name) {
        for (int i = 0; i < tracks.Length; ++i)
            if (tracks[i].name == name)
                return i;

        return -1;
    }

    void PlayTrack (string name) {
        int id = FindTrackByName(tracks, name);
        if (id < 0 || id == current) return;

        next = id;
    }


    public void BroadcastMonsterChase() { gameObject.SetActive(false); }


    void Awake () {  InitializeTracks (tracks); }

    void LateUpdate () {
        if (tracks[next].name != tracks[current].name) {
            if (tracks[current].source.volume == 0) {
                current = next;
                return;
            }

            tracks[current].source.volume -= transitionSpeed;
     
        } else {
            if (tracks[current].source.volume < maxVolume)
                tracks[current].source.volume += transitionSpeed;
        }
    }
}