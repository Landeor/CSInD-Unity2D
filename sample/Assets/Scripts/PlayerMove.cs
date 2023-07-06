using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMove : MonoBehaviour
{
    public float speed;
    Animator anim;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    CircleCollider2D Cir;
    public bool isTouchUp;
    public bool isTouchDown;
    public bool isTouchRight;
    public bool isTouchLeft;
    public static Vector3 dirVec; 
    public static float h;
    public static float v;
    GameObject scanObject; 

    //모바일 키 변수
    int up_Value;
    int down_Value;
    int left_Value;
    int right_Value;

 
    void Awake() //시작할 때 변수 초기화
    {
        h = 0;
        v = 0;
        Cir = GetComponent<CircleCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();   
    }
 
    void Update()
    {
        
        //방향키를 눌렀을 때
        v =  Input.GetAxisRaw("Vertical") + up_Value + down_Value;
        h =  Input.GetAxisRaw("Horizontal") + right_Value + left_Value;
        if (h > 1)
        {
            h = 1;
        }
        else if (h < -1)
        {
            h = -1;
        }
        else if (v < -1)
        {
            v = -1;
        }
        else if (v < -1)
        {
            v = -1;
        }
    
        if (Input.GetKeyDown(KeyCode.S))
        {
            speed = 5;
        }
        if(Input.GetKeyUp(KeyCode.S))
        {
            speed = 2;
        }
        

        if (Input.GetButton("Horizontal") || left_Value == -1 || right_Value == 1)//좌우 방향키 모션 전환
            spriteRenderer.flipX = h == -1;

        if (Input.GetKey("up")) //윗 방향키
            anim.SetBool("isWalk", true); 

        if (Input.GetKey("down"))  //아래 방향키
            anim.SetBool("isWalk", true); 

        if (Input.GetKey("left"))  //좌측 방향키
            anim.SetBool("isWalk", true); 

        if (Input.GetKey("right"))  //우측 방향키
            anim.SetBool("isWalk", true); 

        if (h == 0 && v == 0)
        {
            anim.SetBool("isWalk", false); 
        }
        else if (h != 0 || v != 0)
        {
            anim.SetBool("isWalk", true); 
        }

        if(Input.GetButtonDown("Jump") && scanObject != null)
        {
            Enemy.enemy();
        }

        //방향키를 뗐을 때
        if(Input.GetButtonUp("Vertical"))
            anim.SetBool("isWalk", false);

        if(Input.GetButtonUp("Horizontal"))
            anim.SetBool("isWalk", false);

        //방향 체크
        if (h == 1)   
            dirVec = Vector3.right;
       
        else if (h == -1)
            dirVec = Vector3.left;
       
        else if (v == 1)
            dirVec = Vector3.up;
       
        else if (v == -1)
            dirVec = Vector3.down;

        //Border line 
        if((isTouchRight && h == 1) || (isTouchLeft && h == -1))
            h = 0;

        
        if((isTouchUp && v == 1) || (isTouchDown && v == -1))
            v = 0;

    
        //이동
        Vector3 vec = new Vector3(speed * Time.deltaTime * h, speed * Time.deltaTime * v, 0); //이동속도 제한
        transform.Translate(vec);
        
    }
    void FixedUpdate()
    {
        Debug.DrawRay(rigid.position, dirVec * 2f, new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec , 2f, LayerMask.GetMask("Enemy"));
        
        if(rayHit.collider != null)
        {
            scanObject  = rayHit.collider.gameObject;
        }
        else 
        {
            scanObject = null;
        }
        
        
    }

    void OnTriggerEnter2D(Collider2D collision) //벽에 닿을 때
    {
        if(collision.gameObject.tag == "Border")
        {
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
        if(collision.gameObject.tag == "Item")
            Destroy(collision.gameObject);

         
    }    

    void OnTriggerExit2D(Collider2D collision) //벽에서 나올 때
    {
        if(collision.gameObject.tag == "Border")
        {
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
    
    public void ButtonDown(string type)
    {
        switch (type)
        {
            case "U":
                up_Value = 1;
                break;
                    
            case "D":
                down_Value = -1;
                break;
                    
            case "L":
                left_Value = -1;
                break;
                    
            case "R":
                right_Value = 1;
                break;
        }
    }
    public void ButtonUp(string type)
    {
        switch (type)
        {
            case "U":
                up_Value = 0;
                break;
                    
            case "D":
                down_Value = 0;
                break;
                    
            case "L":
                left_Value = 0;
                break;
                    
            case "R":
                right_Value = 0;
                break;
        }
    }

}