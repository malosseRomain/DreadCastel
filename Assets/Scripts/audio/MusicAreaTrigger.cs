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




/* This script is to be used to generate real time ambiance music per area.
   It is to be attached to a GameObject with trigger collider component. */

public class MusicAreaTrigger : MonoBehaviour {

    /* Variables used in this script */
    public string musicName;

    public string targetTag = "Player",
                  musicManagerTag = "MusicManager";

    GameObject musicManager;
    


    /* Unity event functions */
    void Awake () { musicManager = GameObject.Find (musicManagerTag); }

    void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag != targetTag || musicManager == null)
            return;

        musicManager.SendMessage ("PlayTrack", musicName);
    }
}
