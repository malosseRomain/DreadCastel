using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Scritpe pour géré la défaite du jeu. Quand le timer tombe a 0, le menu de défaite (Lose) s'afficher et bloque le temps
/// </summary>


public class GameOverManager : MonoBehaviour
{
    public GameObject gameOvertUI;
    public string MenuPrincipal;
    public TextMeshProUGUI NbSalleFaite;




    //On pourra accédé à ça n'import où avec la var static
    public static GameOverManager instance;
    private void Awake()
    {
        instance = this;
        //ICI vérification que le menu de Lose est bien cacher et que le temps s'écoule bien
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
        //ICI il faudrait mettre une animation de fondu au noir pour evvité d'avoir des truc bizarres à l'écran
        //TODO
        //ICI on va a la scene du menu de départ
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
