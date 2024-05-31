using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Scritpe pour g�r� la d�faite du jeu. Quand le timer tombe a 0, le menu de d�faite (Lose) s'afficher et bloque le temps
/// </summary>


public class GameOverManager : MonoBehaviour
{
    public GameObject gameOvertUI;
    public string MenuPrincipal;
    public TextMeshProUGUI NbSalleFaite;




    //On pourra acc�d� � �a n'import o� avec la var static
    public static GameOverManager instance;
    private void Awake()
    {
        instance = this;
        //ICI v�rification que le menu de Lose est bien cacher et que le temps s'�coule bien
        gameOvertUI.SetActive(false);
        Time.timeScale = 1;

    }
    public void AfficherMenuDefaite()
    {
        Cursor.visible = true; //LE cursor est visible quand on est sur une UI
        NbSalleFaite.text = GameWinManager.instance.getNombreDeSalleFaite().ToString(); //Pour avoir le nombre de salle faite
        //ICI on affiche le menu game over
        gameOvertUI.SetActive(true);
        //ICI on bloque le temps
        Time.timeScale = 0;
    }

    public void RetryButton()
    {
        //ICI il faudrait mettre une animation de fondu au noir pour evvit� d'avoir des truc bizarres � l'�cran
        //TODO
        //ICI on va a la scene du menu de d�part
        SceneManager.LoadScene(MenuPrincipal);
        //ICI on cache le menu game over
        gameOvertUI.SetActive(false);
    }

    public void QuitButton()
    {
        //On quite le jeux
        Application.Quit();
    }



}
