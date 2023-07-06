using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public GameManager gameManager;
    Rigidbody2D rigid;
    BoxCollider2D box;
    void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
    }


    void OnTriggerEnter2D(Collider2D collision) //워프 게이트에 닿을 때
    {
        if(collision.gameObject.tag == "Warp")
        {
            switch (collision.gameObject.name)
            {
                case "사냥터 1":
                    gameManager.afterIndex = 1;
                    gameManager.MapTeleport();
                    transform.position = new Vector3(0, 4f,0);
                    Debug.Log("이동 1");
                    break;
                case "광장":
                    gameManager.afterIndex = 0;
                    gameManager.MapTeleport();
                    transform.position = new Vector3(0,- 4f,0);
                    Debug.Log("이동 2");
                    break;   
            }
        }
    } 

}
