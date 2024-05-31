using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueTrigger : MonoBehaviour
{
    public GameObject TextToucheUI; //Pour que le joueur puisse savoir qu'il peut intéragir avec l'objet avec la touche F

    public void FixedUpdate()
    {
        if (TextToucheUI != null)
            TextToucheUI.SetActive(false);
    }

    public void TriggerInteract()
    {
        if (TextToucheUI != null)
            TextToucheUI.SetActive(true);

        if (Input.GetKeyDown(KeyCode.F))
        {
            Dialogue dialogue = GetComponent<Dialogue>();
            DialogueManager.instance.StartDialogue(dialogue);
            Destroy(TextToucheUI);
        }
    }
}


