using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    [SerializeField] RoboCenturion parentScript;
    private Transform tr;
    private Vector3 pos;

    private Animator animator;
    private string currentAnimation;
    private string currentAnimaton;
    //Animation States
    const string BODY_IDLE = "Body_Idle";
    const string BODY_JUMP = "Body_Jump";
    void Start()
    {
        Vector3 pos = transform.localPosition;
        tr = GetComponent<Transform>();

        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (parentScript.isDown == true)
        {   
            pos.y = 1.86f;
            transform.localPosition = pos;
            ChangeAnimationState(BODY_JUMP);
        }
        
        if (parentScript.isDown == false)
        {
            pos.y = 1.915f;
            transform.localPosition = pos;
            ChangeAnimationState(BODY_IDLE);
        }
    }

    void FuxedUpdate()
    {

    }

        void ChangeAnimationState(string newAnimation)
    {
        //Останавливает анимацию от прерывания собой же, чтоб игралась всегда 
        if (currentAnimaton == newAnimation) return;

        animator.Play(newAnimation);

        currentAnimaton = newAnimation;
    }

}
