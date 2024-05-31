using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    /*
    Pour que ce script fonctionne il faut attribuer le layer "Pickable" aux objets à ramasser (si le layer n'existe pas, le créer),
    si l'objet a un mesh collider -> cocher la case "Convex"
    Attention : pour fonctionner les objets récupérables doivent avoir un collider et un rigidbody

    Il faut aussi changer dans Project Settings > Input Manager > Décocher "Use Physical Keys" (permet d'utiliser AZERTY alors qu'unity fonctionne en QWERTY)
    */
    public float DistPickUp;
    public float referenceSize = 1.0f;
    public float powerThrow = 2.0f;

    bool APressed = false;
    bool EPressed = false;


    public RaycastHit HitInteract;

    Transform mainGauche;
    Transform mainDroite;
    Transform camera;

    //Accès plus rapide aux transforms dans le TakeItem()
    Transform TransfMain;
    Transform TransfObjet;

    //On stocke le scale original de l'objet lorsqu'il est pris et il est réattribué à l'objet lorsqu'on le lache
    Vector3 originalScaleObjetGauche;
    Vector3 originalScaleObjetDroite;

    private void Start()
    {
        mainGauche = transform.GetChild(0);
        mainDroite = transform.GetChild(1);
        camera = transform.GetChild(2);
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
            APressed = true;

        if (Input.GetKeyDown(KeyCode.E))
            EPressed = true;

        //On regarde en continue si on vise un objet intéragible
        Physics.Raycast(camera.position, camera.forward, out HitInteract, DistPickUp, LayerMask.GetMask("Pickable"));

        if (HitInteract.collider != null)
        {
            foreach (Outline aimedScript in HitInteract.collider.gameObject.GetComponentsInChildren<Outline>())
                aimedScript.ToggleAimed();
        }
    }

    private void FixedUpdate()
    {
        //main gauche
        if (APressed)
        {
            APressed = false;

            if (mainGauche.childCount == 1)
            {
                DropItem(false);
                return;
            }

            if (HitInteract.collider != null)
            {
                TakeItem(false, HitInteract.collider);
                return;
            }
        }

        //main droite
        if (EPressed)
        {
            EPressed = false;

            if (mainDroite.childCount == 1)
            {
                DropItem(true);
                return;
            }

            if (HitInteract.collider != null)
            {
                TakeItem(true, HitInteract.collider);
                return;
            }
        }
    }

    void TakeItem(bool bMainD, Collider objet)
    {
        /*
        Cette fonction permet de placer l'objet intéragible dans la main du joueur.
        S'il appuie sur E l'objet ira dans la main droite et s'il appuie sur A l'objet ira dans la main gauche
        Il faut donc désactiver la physique de l'objet et le placer dans la main
        */
        TransfObjet = objet.transform;

        if (bMainD)
        {
            TransfMain = mainDroite.transform;
            originalScaleObjetDroite = TransfObjet.localScale;
        }
        else
        {
            TransfMain = mainGauche.transform;
            originalScaleObjetGauche = TransfObjet.localScale;
        }




        TransfObjet.GetComponent<Collider>().enabled = false;
        TransfObjet.GetComponent<Rigidbody>().isKinematic = true;
        TransfObjet.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        TransfObjet.parent = TransfMain;
        TransfObjet.position = TransfMain.position;
        TransfObjet.localEulerAngles = Vector3.zero;


        float scaleFactor = referenceSize / Mathf.Max(TransfObjet.localScale.x, TransfObjet.localScale.y, TransfObjet.localScale.z);
        TransfObjet.localScale *= scaleFactor;
        TransfObjet.gameObject.layer = 0; //Default
    }

    void DropItem(bool bMainD)
    {
        /*
        Cette fonction permet de reposer l'objet intéragible dans la scène du jeu.
        L'objet devrait donc reprendre sa physique et devenir indépendant
        */

        if (bMainD)
            TransfMain = mainDroite.transform;
        else
            TransfMain = mainGauche.transform;

        TransfObjet = TransfMain.GetChild(0);

        TransfObjet.GetComponent<Collider>().enabled = true;
        TransfObjet.GetComponent<Rigidbody>().isKinematic = false;
        TransfObjet.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        TransfObjet.GetComponent<Rigidbody>().velocity = camera.forward * powerThrow;

        TransfObjet.parent = null;
        TransfObjet.localScale = (bMainD ? originalScaleObjetDroite : originalScaleObjetGauche);
        TransfObjet.gameObject.layer = LayerMask.NameToLayer("Pickable");
    }
}


