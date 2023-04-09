using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{   
    //Скейлим руки с оружием, в зависимости от положения (принимаем значение от родителя)
    public Vector2 PointerPosition {get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {     
        //Скейлим руки с оружием, в зависимости от положения
        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;

        Vector2 scale = transform.localScale;
        if(direction.x < 0) //Лево
        {
            scale.y = -1;
            transform.localPosition = new Vector2(0.14f, 0.17f);
        }
        else if(direction.x > 0) //Право
        {
            scale.y = 1;
            transform.localPosition = new Vector2(-0.14f, 0.17f);
        }
        transform.localScale = scale;

        //Debug.Log("PointerPosition = " +  PointerPosition);
    }
}
