using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustEffect : MonoBehaviour
{
    private SpriteRenderer spriteFlip;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        //Сдвигаем вниз
        Vector3 position = transform.position;
        position.y -=0.1f;
        transform.position = position;

        bool var1 = Random.value > 0.5f;
        spriteFlip = GetComponent<SpriteRenderer>();
        spriteFlip.flipX = var1;
        anim = GetComponent<Animator>();
        anim.Play("Dust"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
