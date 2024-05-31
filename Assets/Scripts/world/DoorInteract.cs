using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/* This script manages the interactions with the in-game doors.
   Each door normally blocks the exit of a room and needs a key to be opened. */
public class DoorInteract : MonoBehaviour
{
    /* Variables used by the script */
    public string targetTag = "Player",
                  keyTag = "KeyDoor";

    public GameObject manager;

    bool unlocked = false;
    Animator doorAnimator;
    GameObject alertZone;



    /* Utility functions (may be invoked extrenally, using Unity Messaging System) */
    public void OpenDoor(GameObject target)
    {
        if (doorAnimator == null) return;
        doorAnimator.SetBool("open", true);
    }

    public void CloseDoor(GameObject target)
    {
        if (doorAnimator == null) return;
        doorAnimator.SetBool("open", false);
    }

    public void LockDoor(GameObject key)
    {
        unlocked = false;
        if (key != null) Destroy(key);
        if (alertZone != null) alertZone.SetActive(true);
    }

    public void UnlockDoor(GameObject key)
    {
        unlocked = true;
        if (key != null) Destroy(key);
        if (alertZone != null)
        {
            alertZone.SendMessage("HideUserUI");
            alertZone.SetActive(false);

            GameWinManager.instance.NouvelleSalleGagner();
        }
    }



    /* Other utility function */
    (GameObject target, GameObject key) GetObjectByTag(GameObject other)
    {
        GameObject target = null,
                   key = null;

        if (other.tag == targetTag)
            target = other;

        if (other.tag == keyTag)
            key = other;

        return (target, key);
    }



    /* Main unity event functions */
    void Awake()
    {
        doorAnimator = GetComponentInChildren<Animator>();

        MessageArea messageArea = GetComponentInChildren<MessageArea>();
        if (!messageArea) return;
        alertZone = messageArea.gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        (GameObject target, GameObject key) = GetObjectByTag(other.gameObject);
        if (target == null && key == null)
            return;

        if (unlocked && target != null)
            OpenDoor(target);

        if (key != null)
        {
            UnlockDoor(key);
            OpenDoor(target);
        }
    }

    void OnTriggerExit(Collider other)
    {
        (GameObject target, GameObject key) = GetObjectByTag(other.gameObject);
        if (target == null && key == null)
            return;

        if (unlocked && target != null)
            CloseDoor(target);
    }
}
