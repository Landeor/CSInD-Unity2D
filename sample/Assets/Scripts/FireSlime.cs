using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSlime : MonoBehaviour
{
   
    Animator anim;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    CircleCollider2D Cir;
    public bool isTouchUp;
    public bool isTouchDown;
    public bool isTouchRight;
    public bool isTouchLeft;
    public float speed;
    float h;
    float v;
    void Awake()
    {
        Cir = GetComponent<CircleCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();   
    }
     void Update()
    {
        h = Input.GetAxisRaw("Horizontal2");
        if((isTouchRight && h == 1) || (isTouchLeft && h == -1))
            h = 0;

        v = Input.GetAxisRaw("Vertical2");
        if((isTouchUp && v == 1) || (isTouchDown && v == -1))
            v = 0;

        //방향키를 눌렀을 때
        if(Input.GetButton("Horizontal2"))//좌우 방향키
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal2") == -1;
        

        
        
        //방향키를 뗐을 때
        if(Input.GetButtonUp("Vertical2"))
            anim.SetBool("isWalk", false);

        if(Input.GetButtonUp("Horizontal2"))
            anim.SetBool("isWalk", false);
    
        
        Vector3 vec =  new Vector3(speed * Time.deltaTime * h, speed * Time.deltaTime * v, 0);
        transform.Translate(vec);

        

    }
    


   void OnTriggerEnter2D(Collider2D collision)
   {
        if(collision.gameObject.tag == "Border")
            switch (collision.gameObject.name)
            {
                case "Borderline Up":
                    isTouchUp = true;
                    break;

                case "Borderline Down":
                    isTouchDown = true;
                    break;

                case "Borderline Right":
                    isTouchRight = true;
                    break;

                case "Borderline Left":
                    isTouchLeft = true;
                    break;
            }
   }    

   void OnTriggerExit2D(Collider2D collision)
   {
     if(collision.gameObject.tag == "Border")
            switch (collision.gameObject.name)
            {
                case "Borderline Up":
                    isTouchUp = false;
                    break;

                case "Borderline Down":
                    isTouchDown = false;
                    break;

                case "Borderline Right":
                    isTouchRight = false;
                    break;

                case "Borderline Left":
                    isTouchLeft = false;
                    break;
            }
        
   }            
   
    


   
}