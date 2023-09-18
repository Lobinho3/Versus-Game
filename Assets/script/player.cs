using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    public DynamicJoystick joydyn;
    public float speed;
    public float speedRotation;
    Animator m_Animator;
    public int life = 100;
    public int moeda = 0;
    
    void Start()
    {

        //m_Animator = GetComponent<Animator>();
        
    }

    
    void Update()
    {

        float moveV = joydyn.Vertical;
        transform.Translate(0, 0, moveV * Time.deltaTime * speed);

        float moveH = joydyn.Horizontal;
        transform.Rotate(0, moveH * Time.deltaTime * speedRotation, 0);

        /*if (moveH > 0 || moveH < 0 || moveV > 0 || moveV < 0)
        {

            m_Animator.SetBool("Walk", true);
        }
        else
        {

            m_Animator.SetBool("walk", false);
        }*/
        
    }

    private void OnTriggerEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {

            life -= 25;

            if (life <= 0)
            {

                life = 100;
                this.transform.position = new Vector3(-305.22522f, -268.498169f, -672.264526f);
                this.transform.eulerAngles = new Vector3(0f, 185.870773f, 0f);

            }
        }

    }
}
