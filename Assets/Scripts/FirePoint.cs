using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{   
    //Курсор
    [SerializeField] private Camera mainCamera;

    //[SerializeField] private Vector3 firePointVector3;   

    public Transform firePointTransform;
    public GameObject bullet;
    
    private Vector3 line1;
    
    void Start()
    {
   
    }

    void Update()
    {

         //Мышка
        line1 = new Vector3(transform.position.x, transform.position.y);
        
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        mouseWorldPosition.z = 0f;
             
        Vector3 targetPosition = mouseWorldPosition;
        Debug.DrawLine(line1, mouseWorldPosition, Color.green);

        //Выстрел
        if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Мышь нажата");
                Instantiate(bullet, firePointTransform.position, firePointTransform.rotation);
                 
            } 
        //Debug.Log("trans pos z" + transform.position.z);
    }
}
