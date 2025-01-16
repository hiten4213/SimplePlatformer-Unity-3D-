using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelExit : MonoBehaviour
{
    [SerializeField] float loaddelay = 1f;
    [SerializeField] AudioClip levelend;
    AudioSource player;
    private void Start()
     {
         player = GetComponent<AudioSource>();
     }
     void OnTriggerEnter2D(Collider2D other)
     {
        if(other.tag == "Player")
        {
            GetComponent<AudioSource>().PlayOneShot(levelend);
            StartCoroutine(loadnextlevel());
        }
     }

     IEnumerator loadnextlevel()
     {
          yield return new WaitForSecondsRealtime(loaddelay);
          int currentsceneindex = SceneManager.GetActiveScene().buildIndex;
          int newsceneindex = currentsceneindex+1;
           if (newsceneindex == SceneManager.sceneCountInBuildSettings)
           {
             newsceneindex = 0;
           }
           FindObjectOfType<scenepersist>().resetscenepersist();
           SceneManager.LoadScene(newsceneindex);
     }
}
