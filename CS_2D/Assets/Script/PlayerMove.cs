using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    public float speed;
    public JoyStick JoyStick;
    public float h;
    public float v;
    bool isHorizonMove;
    Animator anim;
    SpriteRenderer spriter;
    Rigidbody2D rigid;
    GameObject scanObject;
    Vector3 dirVec; 

    void Awake() //컴포넌트 초기화
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //상하좌우 변수
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");   

        //4방향 고정 보조 변수
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");
        
        //현재 방향 체크
        if (h == 1)   
            dirVec = Vector3.right;
       
        else if (h == -1)
            dirVec = Vector3.left;
       
        else if (v == 1)
            dirVec = Vector3.up;
       
        else if (v == -1)
            dirVec = Vector3.down;

        //Check Horizontal Move
        if(hDown || vUp)
        {
            isHorizonMove = true;
        }
        else if(vDown || hUp)
        {
            isHorizonMove = false;
        }

        //조사 
        if(Input.GetButtonDown("Jump") && scanObject != null)
        {
            Debug.Log(scanObject.name);
        }
        //애니메이션
        if (anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else    
            anim.SetBool("isChange", false);

        //위치 이동
        Vector2 moveVec = isHorizonMove ? new Vector2(h,0) : new Vector2(0,v);
        rigid.velocity = moveVec * speed;
    }

    void FixedUpdate() 
    {
        //DrawRay 생성
        Debug.DrawRay(rigid.position, dirVec * 2f, new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec , 2f, LayerMask.GetMask("Border"));
        
        if(rayHit.collider != null)
        {
            scanObject  = rayHit.collider.gameObject;
        }
        else 
        {
            scanObject = null;
        }
    }

}

