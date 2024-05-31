using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstreChangeRoom : MonoBehaviour
{
    public GameObject monstre;
    public float deplacementZ = 10f;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entree dans le triggers");
        if (other.CompareTag("Player"))
        {
            Debug.Log("triple monstre");

            monstre.transform.position += new Vector3(0, 0, deplacementZ);

        }
    }
}
