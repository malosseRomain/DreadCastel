using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePauseManager : MonoBehaviour
{
    public static bool IsPaused = false;
    public GameObject pauseMenu;
    public GameObject setting;
    public string MenuPrincipal;

    //On pourra acc�d� � �a n'import o� avec la var static
    public static GamePauseManager instance;
    private void Awake()
    {
        instance = this;
        //ICI v�rification que le menu de Pause est bien cacher et que le temps s'�coule bien
        pauseMenu.SetActive(false);
        setting.SetActive(false);
        Time.timeScale = 1;

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (IsPaused)
                BoutonReprendre();
            else
                Pause();
        }
    }

    private void Pause()
    {
        Cursor.visible = true; //LE cursor est visible quand on est sur une UI
        //On met le jeux en pause.
        pauseMenu.SetActive(true);
        // Arr�te les animations 
        Time.timeScale = 0;
        // Changer IsPaused � true;
        IsPaused = true;

    }

    public void BoutonReprendre()
    {
        Cursor.visible = false; //LE cursor est invisible sinon
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        IsPaused = false;

    }

    public void BoutonOuvrirSettings()
    {
        setting.SetActive(true);
    }

    public void BoutonFermerSettings()
    {
        setting.SetActive(false);
    }

    public void BoutonMettreMainMenu()
    {
        SceneManager.LoadScene(MenuPrincipal);
    }

    public void QuitButton()
    {
        //On quite le jeux
        Application.Quit();
    }

    public void SetFullScreen(bool choix)
    {
        Screen.fullScreen = choix;
    }


}
