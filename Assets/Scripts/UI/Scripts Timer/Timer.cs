using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private float startTime;
    public float timerDuration = 600f; // 10 minutes en secondes
    float remainingTime;
    float elapsedTime;

    private bool oneMinuteLeftSoundPlayed = false;
    private bool outOfTimeSoundPlayed = false;

    // R�f�rences aux clips audio
    public AudioClip oneMinuteLeftClip;
    public AudioClip outOfTimeClip;

    private AudioSource audioSource;



    private void Awake()
    {
        startTime = Time.time;

        GameObject audioObject = new GameObject("TimerAudioSource");
        audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.volume = .4f;
    }

    private void Update()
    {
        // Calcule le temps �coul� depuis le d�but du jeu en seconde
        elapsedTime = Time.time - startTime;

        // Calcule le temps restant
        remainingTime = Mathf.Max(timerDuration - elapsedTime, 0f);//Permet d'assurer que le temps n'est pas inf�rieur � 0

        // Mettre � jour l'affichage du temps au format mm:ss
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        GetComponent<Text>().text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // V�rifie si le son "one minute left" doit �tre jou�
        if (!oneMinuteLeftSoundPlayed && remainingTime <= 60f)
        {
            audioSource.PlayOneShot(oneMinuteLeftClip);
            oneMinuteLeftSoundPlayed = true;
        }   

        EndOfTime();
    }

    public void BroadcastEndGame() {
        audioSource.PlayOneShot(outOfTimeClip);
        audioSource.volume = .15f;
        outOfTimeSoundPlayed = true;
    }

    public float GetRemainingTime()
    {
        return Mathf.Max(timerDuration - elapsedTime, 0f);
    }

    void EndOfTime()
    {
        ////Charge menu de lose si temps �coul�
        //if (remainingTime <= 0)
        //{
        //    //SceneManager.LoadScene("MenuLose");
        //    GameOverManager.instance.AfficherMenuDefaite();
        //}

        //Quand le timer arrive � 0 on passe � la camera du monstre.
        //if (remainingTime <= 0)
        //{
        //    CameraManager.instance.SwitchToBossCamera();
        //}
    }
}
