using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMove : MonoBehaviour
{
    public float speed;
    float h;
    float v;
    bool isHorizonMove;
    Animator anim;
    SpriteRenderer spriter;
    Rigidbody2D rigid;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //Move Value
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        //Check Vutton Down & Up
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
        //Animation
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
