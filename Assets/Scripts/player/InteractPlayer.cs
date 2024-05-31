using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPlayer : MonoBehaviour
{
    float DistInteract;
    RaycastHit HitInteract;

    Transform camera;


    void Start()
    {
        DistInteract = gameObject.GetComponent<PickUpItem>().DistPickUp;
        camera = transform.GetChild(2);
    }

    void Update()
    {
        Physics.Raycast(camera.position, camera.forward, out HitInteract, DistInteract, LayerMask.GetMask("Interactable"));
        if (HitInteract.collider != null)
        {
            foreach (Outline aimedScript in HitInteract.collider.gameObject.GetComponentsInChildren<Outline>())
                aimedScript.ToggleAimed();

            DialogueTrigger trigger = HitInteract.collider.gameObject.GetComponent<DialogueTrigger>();
            if (trigger) trigger.TriggerInteract();
        }
    }
}
