using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboCenturion : MonoBehaviour
{
    
    public bool isDown = false;
    //На чём может прыгать
    [SerializeField] private LayerMask jumpableGround;

    //Чтоб не прыгать дважды в воздухе
    private BoxCollider2D coll;
    private Animator animator;
    private string currentAnimation;

    // Бег + Прыжки
    private Rigidbody2D rb; 

    private string currentAnimaton;
    
    // Переключает анимацию с-на бег
    private float dirX = 0f; 

    [SerializeField] private float accelerationSpeed = 0.02f;
    [SerializeField] private float moveSpeed = 6;
    private float absMoveSpeed;
    private float jumpForce = 14f;

    //Animation States
    const string ROBO_IDLE = "RoboCenturion_Idle";
    const string ROBO_RUN = "RoboCenturion_Run";

    //const string PLAYER_JUMP = "Player_jump";
    //const string PLAYER_ATTACK = "Player_attack";
   // const string PLAYER_AIR_ATTACK = "Player_air_attack";


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Присваиваем переменной значение полученного компонента.
        animator=GetComponent<Animator>();
    }
    private bool IsGrounded()
    {
        //Создаём коробку как колайдер бокс игрока, смещаем ее чуть ниже и чекаем, касается ли джампблГраунд
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
    
    // Update is called once per frame
    void Update()
    {
        
        
        //Движение влево, вправо + ускорение
        absMoveSpeed = Mathf.Abs(rb.velocity.x);

        dirX = Input.GetAxisRaw("Horizontal");

        //rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y); 
       
        if (absMoveSpeed <= moveSpeed)
        {
            rb.velocity += new Vector2(dirX * accelerationSpeed, rb.velocity.y); 
        }
        
        
            
        //Прыжок
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce);
            //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }    

    }

     private void FixedUpdate()
    {
        
        /*
        // Движение с ускорением
        dirX = Input.GetAxisRaw("Horizontal");

        if (dirX > 0)
        {
        rb.AddForce(acceleration * rb.mass);
        }

        if (dirX < 0)
        {
        rb.AddForce(-acceleration * rb.mass);
        }
        */



        // Анимация
        if (dirX != 0)
        {
            ChangeAnimationState(ROBO_RUN);
        }
        else
        {
            ChangeAnimationState(ROBO_IDLE);
        }

         //Возврат Body на место, когда не движемся
        if ((absMoveSpeed < 0.05f) || (absMoveSpeed > -0.05f) && (dirX == 0))
        {
            isDown = false;
        }

        //Debug.Log("moveSpeed = " + moveSpeed); //Debug         
        //Debug.Log("absMoveSpeed = " + absMoveSpeed); //Debug   
   
    }

    void ChangeAnimationState(string newAnimation)
    {
        //Останавливает анимацию от прерывания собой же, чтоб игралась всегда 
        if (currentAnimaton == newAnimation) return;

        animator.Play(newAnimation);

        currentAnimaton = newAnimation;
    }

    //Прыгание Body при ходьбе
    void GetDown()
    {
        isDown = true;
    }

    void GetUp()
    {
        isDown = false;
    }

} 
