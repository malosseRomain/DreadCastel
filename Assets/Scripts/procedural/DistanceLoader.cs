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

/*  Sould be the child of an loadable object.
    Requires a trigger box and the target tag to be set (defaults to Player) */

public class DistanceLoader : MonoBehaviour {

    public string targetTag = "Player";
    private GameObject parent;


    void Awake () {
        parent = transform.parent.gameObject;
        transform.SetParent (null);

        parent.SetActive (false);
    }

    void OnTriggerExit (Collider other) {
        if (other.gameObject.tag == targetTag)
            parent.SetActive (false);
    }

    void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == targetTag)
            parent.SetActive (true);
    }

    public void BroadcastEndGame() {
        parent.SetActive (false);
    }
}
