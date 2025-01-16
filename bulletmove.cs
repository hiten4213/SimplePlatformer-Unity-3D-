using UnityEngine;
using UnityEngine.InputSystem;

public class bulletmove : MonoBehaviour
{
    Rigidbody2D rd2b;
    [SerializeField] float bulletspeed = 3f;
     playermove player;
     float xspeed;
     
    void Start()
    {
        rd2b = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<playermove>();
        xspeed = player.transform.localScale.x * bulletspeed;
    }

    
    void Update()
    {
        rd2b.velocity = new Vector2(xspeed,0f);
    }
    void OnTriggerEnter2D(Collider2D other) 
     {
        if(other.tag == "enemy")
        {
            Destroy(other.gameObject); 
            Destroy(gameObject);       
        }
         
     }
    private void OnCollisionEnter2D(Collision2D other)
     {
      {
         Destroy(gameObject);
      }
     }
}
