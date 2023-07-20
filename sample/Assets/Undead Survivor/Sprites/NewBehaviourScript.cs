using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    float h;
    float v;
    bool isHorizonMove;

    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        h = Input.GetAxisRaw("Vertical");
        v = Input.GetAxisRaw("Horizontal");

        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        if(hDown)
        {
            isHorizonMove = true;
        }
        else if(vDown)
        {
            isHorizonMove = false;
        }
    }
    void FixedUpdate() {
        //위치 이동
        Vector2 moveVec = isHorizonMove ? new Vector2(h,0) : new Vector2(0,v);
        rigid.velocity = moveVec * speed;
    }
}