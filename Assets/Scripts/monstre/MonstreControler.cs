using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class MonstreControler : MonoBehaviour 
{
    public Transform Target;
 
    public float attackRange = 2.2f;
 
    // Cooldown des attaques
    public float attackRepeatTime = 1;
    private float attackTime;
 
    // Montant des dégâts infligés
    public float TheDammage;
 
    // Agent de navigation
    private UnityEngine.AI.NavMeshAgent agent;

    //pou avoir une ref vers sa camera
    public Camera MonstreCamera;
 
    // Animations de l'ennemi
    //private Animation animations;

    public static MonstreControler instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Attention plusieur instance de MonstreControler");
            return;
        }
        instance = this;
    }   
 
    void Start () 
    {
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        //animations = gameObject.GetComponent<Animation>();

        /* Broadcast to the whole scene that we are in the chase sequence */
        BroadcastAll("BroadcastMonsterChase", SendMessageOptions.DontRequireReceiver);

    }
  
    void Update () 
    {
        
        // On cherche le joueur en permanence

        GameObject player = GameObject.Find("Player");
        if (!player) return;

        Target = player.transform;
        agent.destination = Target.position;
        
        // On calcule la distance entre le joueur et l'ennemi, en fonction de cette distance on effectue diverses actions
        float Distance = Vector3.Distance(Target.position, transform.position);

        // Quand l'ennemi est proche mais pas assez pour attaquer
        if (Distance <= attackRange) {
            EndGame();
        }
    }
 

    public static void BroadcastAll(string fun, System.Object msg) {
        GameObject[] gos = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in gos) {
            if (go && go.transform.parent == null) {
                go.gameObject.BroadcastMessage(fun, msg, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
 
    //Cette fonction permet de stopé le monstre et d'attendre un peu avant de mettre les menus
    public void EndGame()
    {

        agent.destination = transform.position;
        MonstreCamera.enabled = true;
        
        /*  Stop the monster's animator, located in a child, 
            set the monster's rotation to the player (target at the time),
            broatcast to the whole scene that we are in the end game
            */
        gameObject.GetComponentInChildren<Animator>().enabled = false;
        transform.LookAt(Target);
        BroadcastAll("BroadcastEndGame", SendMessageOptions.DontRequireReceiver);
        

        //ICI on appel une coroutine qui va attendre 5 secondes avant de mettre le menu de fin de partie
        StartCoroutine(EndGameCoroutine());
    }

    //Coroutine qui attend 5 secondes avant de mettre le menu de fin de partie
    IEnumerator EndGameCoroutine()
    {
        //On attend 5 secondes
        yield return new WaitForSeconds(5);
        //On met le menu de fin de partie
        GameOverManager.instance.AfficherMenuDefaite();
    }



}