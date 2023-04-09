using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    [SerializeField] RoboCenturion parentScript;
    private Transform tr;

    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = transform.localPosition;
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (parentScript.isDown == true)
        {   
            pos.y = 1.86f;
            transform.localPosition = pos;
        }
        
        if (parentScript.isDown == false)
        {
            pos.y = 1.915f;
            transform.localPosition = pos;
        }
    }

    void FuxedUpdate()
    {

    }
}
