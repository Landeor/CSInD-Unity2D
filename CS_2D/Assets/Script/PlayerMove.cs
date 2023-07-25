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
    float h_value; //모바일 전용 이동 변수
    float v_value;
    bool isHorizonMove;
    Animator anim;
    SpriteRenderer spriter;
    Rigidbody2D rigid;
    
    void Awake() //컴포넌트 초기화
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //상하좌우 변수
        h_value = JoyStick.Horizontal();
        v_value = JoyStick.Vertical();
        h = Input.GetAxisRaw("Horizontal") + h_value;
        v = Input.GetAxisRaw("Vertical") + v_value;   

        //4방향 이동 변수
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        //Check Horizontal Move
        if(hDown || vUp)
        {
            isHorizonMove = true;
        }
        else if(vDown || hUp)
        {
            isHorizonMove = false;
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
    }
    void FixedUpdate() 
    {
        //위치 이동
        Vector2 moveVec = isHorizonMove ? new Vector2(h,0) : new Vector2(0,v);
        rigid.velocity = moveVec * speed;
    }

}

