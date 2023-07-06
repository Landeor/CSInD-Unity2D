using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    CircleCollider2D Cir;
    Vector3 dirVec;
    public bool isTouchUp;
    public bool isTouchDown;
    public bool isTouchRight;
    public bool isTouchLeft;
    public int h;
    public int v;
    float HP;
    public static System.Action enemy;
    float nextMoveTime;
    public GameObject[] coin;
    
    void Awake()
    {
        enemy = () => { OnHit(); };
        HP = 5;
        Cir = GetComponent<CircleCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();   
        Think(); 
    }

    void Update()
    {
        if((isTouchRight && h == 1) || (isTouchLeft && h == -1))
            h = 0;

        
        if((isTouchUp && v == 1) || (isTouchDown && v == -1))
            v = 0;
    
        if(h == 1)   //방향 전환
            dirVec = Vector3.right;
            
        else if (h == -1)
            dirVec = Vector3.left;
       
        else if (v == 1)
            dirVec = Vector3.up;
       
        else if (v == -1)
            dirVec = Vector3.down;
    }

    void FixedUpdate()
    {
        Vector3 vec= new Vector3(h * Time.deltaTime, v * Time.deltaTime);
        transform.Translate(vec);
    }

    public void OnHit()
    {
        if (HP > 0)
        {
            anim.SetTrigger("isHurt");
            HP -= 1;
            if (HP <= 0)
            {
                Dead();
            }
            else
            {
                Debug.Log("남은 체력 : " + HP);
            }
        }    

    }
    void Dead()
    {
        
        anim.SetTrigger("isDead");
        CancelInvoke();
        Invoke("DeadExe", 0.6f);
        
    }
    void DeadExe()
    {   
        int ran = Random.Range(0,10);
        if(ran < 4)
        {
            Debug.Log("None");
        }
        else if (4 <= ran && ran < 7)
        {
            Instantiate(coin[0], transform.position, transform.rotation );
        }
        else if (7 <= ran && ran < 9)
        {
            Instantiate(coin[1], transform.position, transform.rotation );

        }
        else if (9 <= ran && ran <= 10)
        {
            Instantiate(coin[2], transform.position, transform.rotation );

        }
        Destroy(gameObject);
        Debug.Log(gameObject + "가 처치되었습니다. ");
        

    }
    void Think() 
    {
        h = Random.Range(-1, 2);
        v = Random.Range(-1, 2);
        if (h == 0 && v == 0)
        {
            CancelInvoke();
            Rest();
        }
        else if(h != 0)
        {
            anim.SetInteger("WalkSpeed", h); 
            spriteRenderer.flipX = h == -1;
            CancelInvoke();
            Invoke("Rest",1);
        }
        else if(h == 0 && v != 0)
        {
            anim.SetInteger("WalkSpeed", v); 
            CancelInvoke();
            Invoke("Rest",1);
        }
        
        
        
    }
    void Rest()
    {
        anim.SetInteger("WalkSpeed", 0); 
        h = 0;
        v = 0;
        nextMoveTime = Random.Range(0.5f,2);
        CancelInvoke();
        Invoke("Think", nextMoveTime); 
    }

    
   
    void OnTriggerEnter2D(Collider2D collision) //벽에 닿을 때
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

   void OnTriggerExit2D(Collider2D collision) //벽에서 나올 때
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

