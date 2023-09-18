using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Báu : MonoBehaviour
{

    public float rotationSpeed = 30.0f; //velocidade de rotação em graus por segundo
    public GameObject question;
    //Atualização a cada quadro
    void Update()
    {

        //Rotaciona o objeto no eixo Y
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {

            rotationSpeed = 0f;

            this.gameObject.SetActive(false);
            question.SetActive(true);
        }
    }
}
