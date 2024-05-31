using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



/// <summary>
/// Ce scripte a pour but de recevoir et géré les message du jeux a afficher au joueur.
/// Il y a deux fonctionnalité : 
///       1) Si un object veux afficher une phrase au joueur quand ce dernier le regarde, son scripte est un StartMessage est il est envoyer 
///          au MessageManager pour être afficher.
///       2) Si un autre scripte (ex : OpenDoor) veut afficher plusieur message en fonction de parametre interne, alors la fonctionnalité 1 
///          s'arrête et la deuxième fonctionnalité s'active. 
///          
/// Fonctionnalité Special = > 2) 
///  
/// </summary>

[System.Serializable]
public class MessageManager : MonoBehaviour
{
    public TextMeshProUGUI MessageTexte;
    public GameObject UIPhrase;

    private List<StartMessage> messagesEnCours = new List<StartMessage>(); // Liste des StartMessage actuellement regardés
    private bool fonctionSpecialeActive = false;  // Nouvelle variable pour indiquer si la fonction spéciale est activée

    public static MessageManager instance;

    private void Awake()
    {
        instance = this;
        UIPhrase.SetActive(false);
    }

    private void Update()
    {
        if (!fonctionSpecialeActive)
        {
            // Vérifie si au moins un StartMessage est regardé
            bool auMoinsUnRegarde = false;

            foreach (var message in messagesEnCours)
            {
                if (message.EstRegarde())
                {
                    auMoinsUnRegarde = true;
                    break;
                }
            }

            if (auMoinsUnRegarde)
            {
                // Afficher l'UI et mettre à jour le texte
                StartMessage premierMessageRegarde = messagesEnCours[0];
                MessageTexte.text = premierMessageRegarde.message;
                UIPhrase.SetActive(true);
            }
            else
            {
                // Cacher l'UI s'il n'y a aucun StartMessage regardé par le joueur
                UIPhrase.SetActive(false);
            }
        }
    }

    // Ajouter le StartMessage à la liste des messages en cours
    public void AjouterMessageEnCours(StartMessage message)
    {

        if (!messagesEnCours.Contains(message))
        {
            messagesEnCours.Add(message);
        }
    }

    // Retirer le StartMessage de la liste des messages en cours
    public void RetirerMessageEnCours(StartMessage message)
    {
        messagesEnCours.Remove(message);
    }

    // Fonction pour activer la fonction spéciale, quand le joueur regarde un object qui n'a pas de startMessage 
    public void ActiverFonctionSpeciale(string message)
    {
        fonctionSpecialeActive = true;
        MessageTexte.text = message;
        UIPhrase.SetActive(true);
    }

    //Fonction pour désactiver la fonction spéciale quand le joueur ne regarde plus l'object qui a activé cette fonctionnalité spécial
    public void DesactiverFonctionSpeciale()
    {
        fonctionSpecialeActive = false;
        UIPhrase.SetActive(false);
    }
}
