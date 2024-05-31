using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Ce scripte sert a g�r� la victoire du joueur, lorsqu'il a r�ussi dans les temps a gagner toute les salles
/// </summary>
[System.Serializable]

public class GameWinManager : MonoBehaviour
{
    public GameObject gameWinUI;
    public string MenuPrincipal;

    public int NombreSalleReussi = 0;
    public int nombreSallePourUnePartie;

    public TextMeshProUGUI nombreSalleReussiText;

    //Pour incr�ment� le nombre de salle r�ussi.
    //Quand le player r�ussi une salle, on envoie un message � cette fonction
    //Et la fonction incr�mente le nombre de salle r�ussi
    public void NouvelleSalleGagner()
    {
        NombreSalleReussi += 1;

        if (NombreSalleReussi >= nombreSallePourUnePartie)
            AfficherMenuVictoire();
    }

    //On pourra acc�d� � �a n'import o� avec la var static
    public static GameWinManager instance;
    private void Awake()
    {
        instance = this;
        //ICI v�rification que le menu de Lose est bien cacher et que le temps s'�coule bien
        gameWinUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void AfficherMenuVictoire()
    {
        Cursor.visible = true; //Le cursor est visible quand on est sur une UI
        //ICI on affiche le menu game over
        nombreSalleReussiText.text = NombreSalleReussi.ToString();
        gameWinUI.SetActive(true);
        //ICI on bloque le temps
        Time.timeScale = 0;
    }

    public void Recommencer()
    {
        //ICI on va a la scene du menu de d�part
        SceneManager.LoadScene(MenuPrincipal);
        //ICI on cache le menu game over
        gameWinUI.SetActive(false);
    }

    public void Quitter()
    {
        //On quite le jeux
        Application.Quit();
    }

    public int getNombreDeSalleFaite() { return NombreSalleReussi; }
}
