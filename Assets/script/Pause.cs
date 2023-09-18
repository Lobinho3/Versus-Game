using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    public GameObject pausa;
    public static bool Ispause;
    public Button btn_Menu;
    public Sprite play, pause; 
    
    void Start()
    {

        pausa.SetActive(false);
        
    }

    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {

            BtnPausar();
        }
        
    }

    private void Pausar()
    {

        pausa.SetActive(true);
        btn_Menu.image.sprite = play;
        Time.timeScale = 0f;
        Ispause = true;
    }

    public void VoltarGame()
    {

        pausa.SetActive(false);
        btn_Menu.image.sprite = pause;
        Time.timeScale = 1f;
        Ispause = false;
    }

    public void Menu()
    {

        SceneManager.LoadScene("Menu");
    }

    public void BtnPausar()
    {

        if (Ispause)
        {

            VoltarGame();
        }
        else
        {

            Pausar();
        }
    }
}
