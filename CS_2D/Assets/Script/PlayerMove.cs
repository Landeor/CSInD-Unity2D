using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public float h;
    public float v;

    void Start()
    {
        h = 0;
        v = 0;
    }

    void Update()
    {
        v = Input.GetAxisRaw("Vertical");
        h = Input.GetAxisRaw("Horizontal");

        Vector3 vec = new Vector3(speed * Time.deltaTime * h, speed * Time.deltaTime * v, 0); //이동속도 제한
        transform.Translate(vec);
    }
}
