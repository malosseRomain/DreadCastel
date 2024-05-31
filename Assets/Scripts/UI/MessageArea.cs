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



/* This script manages the interactions with the in-game doors.
   Each door normally blocks the exit of a room and needs a key to be opened. */

public class MessageArea : MonoBehaviour
{
    /* Variables used by the script */
    public string displayText;

    public string targetTag = "Player",
                  UIManagerTag  = "GameManager";

    public MessageManager userUI;



    /* Utility function, may be called with the Unity Messaging system */
    public void ShowUserUI (string text) { 
        if (userUI == null || text == null) return;
        userUI.ActiverFonctionSpeciale (text); 
    }

    public void HideUserUI () { 
        if (userUI == null) return;
        userUI.DesactiverFonctionSpeciale ();
    }



    /* Main unity event functions */
    void Awake () {
        GameObject UIManager = GameObject.Find (UIManagerTag);
        if (!UIManager) return;
        userUI = UIManager.GetComponentInChildren<MessageManager> ();
    }

    void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag != targetTag)
            return;

        ShowUserUI (displayText);
    }

    void OnTriggerExit (Collider other) {
        if (other.gameObject.tag != targetTag)
            return;

        HideUserUI ();
    }
}

