using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;

    //public gameObject TextToucheUI; //pour effacer le texte apuy sur f
    private Queue<string> sentences;
    //public Animator animator;
    public static DialogueManager instance;


    private void Awake()
    {

        if (instance != null)
        {
            Debug.LogWarning("Attention plusieur instance de DialogueManager");
            return;
        }
        instance = this;

        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Cursor.visible = true;
        sentences.Clear();
        Time.timeScale = 0; //On arrête de temps pour que le joueur ne puisse pas bouger
        dialogueBox.SetActive(true);

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        //StopAllCoroutines();//empecher affichage de plusieur phrase si appuy sur suivant avaant la fin de la premiere coroutine
        //StartCoroutine(LettreParLettre(sentence));
    }

    IEnumerator LettreParLettre(string sentence)
    {
        dialogueText.text = "";
        foreach (char lettre in sentence.ToCharArray())
        {
            dialogueText.text += lettre;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void EndDialogue()
    {
        Cursor.visible = false;
        dialogueBox.SetActive(false);
        Time.timeScale = 1; //On remet le temps à la normale
    }
}
