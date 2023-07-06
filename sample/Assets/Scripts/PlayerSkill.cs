using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public float speed;
    Rigidbody2D rigid;
    float h;
    float v;
    private float defaultSpeed;
    private bool isdash;
    public float dashSpeed;
    public float defaultTime;
    private float dashTime;
    public static bool isAttack;

    void Start()
    {
        defaultSpeed = speed;
        rigid = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        h = PlayerMove.h;
        v = PlayerMove.v;
       
        if (Input.GetKeyDown(KeyCode.A))
        {
            isdash = true;
            isAttack = true;
        }
        if(dashTime <=0)
        {
            defaultSpeed = speed;
            if(isdash)
                dashTime = defaultTime;
        }
        else
        {
            dashTime -= Time.deltaTime;
            defaultSpeed = dashSpeed;
            
        }
        Vector3 vec = new Vector3(defaultSpeed * Time.deltaTime * h, defaultSpeed * Time.deltaTime * v, 0); //이동속도 제한
        transform.Translate(vec);
        isdash = false;
        isAttack = false;
    }
}
