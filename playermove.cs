using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class playermove : MonoBehaviour
{
  [SerializeField] float runspeed = 1f;
  [SerializeField] float jumpspeed = 5f;
  [SerializeField] float climbspeed = 2f;
  [SerializeField] float gravity = 4f;
  [SerializeField] Color32 deathcolor = new Color32(1,1,1,1);
  [SerializeField] Vector2 deatheffect = new Vector2(10f,10f);
  [SerializeField] GameObject bullet;
  [SerializeField] Transform gun;
  SpriteRenderer sprite;
  Vector2 moveinput;
  Rigidbody2D rd2b;
  Animator animator;
  BoxCollider2D myplayer;
  PolygonCollider2D playerfeet;
  [SerializeField] AudioClip jummp;
  [SerializeField] AudioClip deathsound;
  [SerializeField] float loaddelay = 2f;
  bool isalive = true;

    void Start()
    {
        rd2b = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        myplayer = GetComponent<BoxCollider2D>();
        playerfeet = GetComponent<PolygonCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

  
    void Update()
    {
        if(!isalive){ return;}
        run();
        flipsprite();
        climbladder();
        die();

    }
    void OnFire(InputValue value)
    {
        if(!isalive){ return;}
        Instantiate(bullet,gun.transform.position,bullet.transform.rotation);
        animator.SetTrigger("fire");
    }
    void OnMove(InputValue value)
    { 
        if(!isalive){ return;}
        moveinput = value.Get<Vector2>();
    }
    
    void OnJump(InputValue value)
    { 
        if(!isalive){ return;}
       if(playerfeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
       {
          rd2b.velocity += new Vector2(0f, jumpspeed); 
          GetComponent<AudioSource>().PlayOneShot(jummp);
          
       }
       
    }
    void run()
    {
        Vector2 playervelocity = new Vector2(moveinput.x * runspeed,rd2b.velocity.y);
        rd2b.velocity = playervelocity;
        bool hashorizontalmove = Mathf.Abs(rd2b.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isrunning",hashorizontalmove);
        
    }
    void flipsprite()
    {
        bool hashorizontalmove = Mathf.Abs(rd2b.velocity.x) > Mathf.Epsilon;
        if (hashorizontalmove)
        {
            transform.localScale = new Vector2(Mathf.Sign(rd2b.velocity.x),1f);
        }
        ;
    } 
    void climbladder()
    {
        if(playerfeet.IsTouchingLayers(LayerMask.GetMask("ladder")))
        {
            Vector2 climbvelocity = new Vector2(rd2b.velocity.x,moveinput.y*climbspeed);
            rd2b.velocity = climbvelocity;
            bool hasverticalmove = Mathf.Abs(rd2b.velocity.y)>Mathf.Epsilon;
            animator.SetBool("isclimbing",hasverticalmove);
            rd2b.gravityScale = 0f;
            
        }
        else
        {
            rd2b.gravityScale = gravity;
            animator.SetBool("isclimbing",false);
        }

    }

    void die()
    {
        if(rd2b.IsTouchingLayers(LayerMask.GetMask("enemies")))
        {
            GetComponent<AudioSource>().PlayOneShot(deathsound);
            isalive=false;
            animator.SetTrigger("death");
            rd2b.velocity = deatheffect;
            sprite.color = deathcolor;
            StartCoroutine(dietriggers());
            
        }
    }
    IEnumerator dietriggers()
    {
            yield return new WaitForSecondsRealtime(loaddelay);
            FindObjectOfType<gamesession>().playerdeathprocess();          
    }
}
