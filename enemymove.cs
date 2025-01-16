using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymove : MonoBehaviour
{
    [SerializeField] float movespeed = 1f;
    Rigidbody2D rd2b;
    
    void Start()
    {
        rd2b = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rd2b.velocity = new Vector2(movespeed,0f);
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        movespeed = -movespeed;
        transform.localScale = new Vector2(Mathf.Sign(rd2b.velocity.x),1f);
    }
}
