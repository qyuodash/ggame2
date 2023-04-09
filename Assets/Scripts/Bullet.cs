using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject dustEffect;
    [SerializeField] private float spread;
    //РБ Пули
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;


    // Start is called before the first frame update
    void Start()
    {   
        rb.rotation += Random.Range(-spread, spread);
        Destroy(gameObject, 5f);
    }
   
    void Update()
    {
        
        rb.velocity = transform.right * speed;
        
        /*
        RaycastHit2D hitdown = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.zero), 1f, 128);

        if(hitdown) 
            {
                //Debug.Log("we hit" + hitdown.collider.name);               
                Destroy(gameObject);
                Instantiate (dustEffect, transform.position, Quaternion.identity);
            }
        */
        

    }

    
   private void OnCollisionEnter2D(Collision2D other)
   {        
        //Debug.Log ($"столкнулись с {other.gameObject.name}");
        if (other.gameObject.tag == "Terrain")   
        {
            Destroy(gameObject);
            Instantiate (dustEffect, transform.position, Quaternion.identity);
        }
    }
    
}
