using UnityEngine;
using TMPro;


/// <summary>
/// Ce scripte doit �tre mit au object qui doivent donner un message au joueur quand ce dernier les regards.
/// </summary>

[System.Serializable]
public class StartMessage : MonoBehaviour
{


    [TextArea(3, 10)]
    public string message;
    public string targetTag = "Player";

    GameObject target;
    PickUpItem pickupItem;


    private void Start()
    {
        MessageManager.instance.RetirerMessageEnCours(this);
        target = GameObject.Find (targetTag);
        if (target != null)
            pickupItem = target.GetComponent<PickUpItem> ();
    }

    private void Update()
    {
        if (CheckRaycast())
        {
            MessageManager.instance.AjouterMessageEnCours(this);
        }
        else
        {
            MessageManager.instance.RetirerMessageEnCours(this);
        }
    }

    public bool EstRegarde()
    {
        // V�rifie si le joueur regarde cet objet
        return CheckRaycast();
    }

    private bool CheckRaycast()
    {
        if (pickupItem == null) return false;

        if (pickupItem.HitInteract.collider != null)
        {
            // V�rifie si le collider touch� est celui de cet objet.
            return pickupItem.HitInteract.collider == GetComponent<Collider>();
        }

        return false;
    }

    public void ChangeMessage(string mes)
    {
        message = mes;
    }
}
