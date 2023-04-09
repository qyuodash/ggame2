// Вопросы 1. Бага с ружьём в прыжке 2. Как коллизить с роботом 3. Чё там с пулями, нужен файрпоинт?

// при входе, убирать is trigger, передавать робота чёрчл камере и диактивировать чувака.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
   
    //Передаём позицию мышки рукам с оружием
    private WeaponParent weaponParentAim;

    //Чтоб не прыгать дважды в воздухе
    private BoxCollider2D coll;
    
    //Смена анимаций
    private enum movementState { idle, run, jump, fall};
    
    //Бежит ли задом
    //private bool runBackwards;

    //Для вычисления позиции мышки для флипа спрайтов
    private float mouseFlip;

    // Бег + Прыжки
    private Rigidbody2D rb; 

    // Переключает анимацию с-на бег
    private Animator anim; 

    // Переключает анимацию с-на бег
    private float dirX = 0f; 

    // Поворачивает спрайт по иксу
    private SpriteRenderer spriteFlip;

    //На чём может прыгать
    [SerializeField] private LayerMask jumpableGround;

    // Параметры игрока
    private float moveSpeed = 3f;
    [SerializeField] private float moveFSpeed = 3f;
    [SerializeField] private float moveBSpeed = 2f;
    [SerializeField] private float jumpForce = 14f;
    
    // Чекаем позицию мыши для конвертации в ворлд позишн
    private Vector3 mouseScreenPosition;
    private Vector3 mouseWorldPosition;

    //Не забыть назначить в редакторе камеру
    [SerializeField] private Camera mainCamera; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Присваиваем переменной значение полученного компонента.
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>(); //Будем чекать, бежит или нет.
        spriteFlip = GetComponent<SpriteRenderer>();
        weaponParentAim = GetComponentInChildren<WeaponParent>(); //Передаём позицию мышки рукам с оружием
        
        //Получить номер маски для игнора
        //Debug.Log(LayerMask.GetMask("Vehicles"));

    }

    private void Update()
    {
            //Считаем позицию мышки, чтоб затем флипать спрайт игрока
            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0f;

            //Передаём позицию мышки рукам с оружием
            weaponParentAim.PointerPosition = mouseWorldPosition;

            //Движение влево, вправо
            dirX = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y); 
            
            //Прыжок
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce);
                //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }    
            
            //Смена анимации
            UpdateAnimationState();

            //Поворот спрайта относительно курсора 
            mouseFlip = rb.position.x - mouseWorldPosition.x;

            if (mouseFlip > 0) //Смотрит в лево
            {
                spriteFlip.flipX = true;
            }

            else //Смотрит в право
            {
                spriteFlip.flipX = false;
            }

            //Вычисляем, как бежит: задом или передом и меняем скорость и реверсим анимацию
            if ((dirX > 0 && mouseFlip < 0) || (dirX < 0 && mouseFlip > 0)) //Передом
            {
                //runBackwards = false;
                moveSpeed = moveFSpeed;
                anim.SetFloat ("speed", 1.0f);

            }
            else if ((dirX > 0 && mouseFlip > 0) || (dirX < 0 && mouseFlip < 0)) //Задом
            {
                //runBackwards = true;
                moveSpeed = moveBSpeed;
                anim.SetFloat ("speed", -0.8f);
            }
            else//стоит
            {
                 anim.SetFloat ("speed", 1.0f);
            }

            // Игнорируем коллизию с транспортом, чтоб не врезался
            Physics2D.IgnoreLayerCollision(7, 8);

            //Проверяем коллизию c роботом Raycast
            RaycastHit2D hitdown = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 0.2f, 128);

            if(hitdown) 
            {
                Debug.Log("we hit" + hitdown.collider.name);   
            }
                       
           // Debug.Log("rb.velocity.y = " +  rb.velocity.y); //Debug
           // Debug.Log("dirX = " + dirX); //Debug
           // Debug.Log("mouseFlip = " + mouseFlip); //Debug         
            
    }
    private void UpdateAnimationState()
    {
            movementState state;

            //Далее переключаем переменную running в Аниматоре, в зависимости от скорости
            if (dirX > 0f) //не юзаем велосити.х, т.к. она может менятсья не только пользаком, но и от взрыва отбросить может.
            {
                state = movementState.run;
            }
            else if (dirX < 0f)       
            {
                state = movementState.run;
            }
            else
            {
                 state = movementState.idle;
            }

            if (rb.velocity.y > .1f)
            {
                state = movementState.jump;
            }
            else if (rb.velocity.y < -.1f)
            {
                state = movementState.fall;
            }

            anim.SetInteger("state",(int)state);//т.к. у нас массив, а в аниматоре инт, то переводим в инт.
    }
    private bool IsGrounded()
    {
        //Создаём коробку как колайдер бокс игрока, смещаем ее чуть ниже и чекаем, касается ли джампблГраунд
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
    
    //Коллизия c роботом
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "RoboCenturion")
            {
                Debug.Log("Есть контакт"); //Debug
            }    
    }

    private void OnTriggerEnter (Collider other)
    {
         if (other.gameObject.name == "RoboCenturion")
            {
                Debug.Log("Есть контакт Triggered"); //Debug
            }           
    }

}

