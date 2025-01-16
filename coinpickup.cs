using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class coinpickup : MonoBehaviour
{
    [SerializeField]  int coinpoints = 100;
   [SerializeField] AudioClip coinsfx;
   bool wascollected;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
       {
          FindObjectOfType<gamesession>().addtoscore(coinpoints);
          AudioSource.PlayClipAtPoint(coinsfx,Camera.main.transform.position);
          Destroy(gameObject);
       }

    }
}
