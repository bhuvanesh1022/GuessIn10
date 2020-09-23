﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class AppManager : MonoBehaviour
{
    public GameObject Home, AnimalKingdom, Cities;
    public GameObject[] _selectUI,Player1Pos, Player2Pos;
    public GameObject Player1, Player2,Dummyplayer1, Dummyplayer2;
    [SerializeField] GameObject[] Player_Txt;
    [SerializeField] Button[] btnfield;
    [SerializeField] TextMeshProUGUI Displayer_1TXT, Displayer_2TXT;
    public string Strplayer1, Strplayer2; 
    int cnt = 0;
    // Start is called before the first frame update
    void Start()
    {
        Home.SetActive(true);
        AnimalKingdom.SetActive(false);
        Cities.SetActive(false);
    }

    public void OnClosseButtonClicked()
    {
        Home.SetActive(true);
        AnimalKingdom.SetActive(false);
        Cities.SetActive(false);
    }
    
    public void ShowToPanelUI(int n)
    {
        Home.SetActive(false);
        _selectUI[n].SetActive(true);
    }
        public void ClickToPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

   

    public void OnClickBuy()
    {

    }

    public void OnClickTry()
    {

    }

    public void LoadScene(int ID)
    {
        SceneManager.LoadScene(ID);
    }

    public void LogoutUser()
    {
        AuthController.authController.LogoutUser();
    }
    // user profiles arrow
    public void _Player1Profilechange(int n)
    {
        Player1.SetActive(false);

        if (n==0)
        {
            cnt++;
            Player1.MoveToSpring(Player1Pos[0], Player1Pos[1],0.5f);
            StartCoroutine("ShowPlayer", 1f);
        }
        if (n == 1)
        {
            cnt--;
            Player1.MoveToSpring(Player1Pos[2], Player1Pos[1], 0.5f);
            StartCoroutine("ShowPlayer", 1f);
        }
    }
    IEnumerator ShowPlayer()
    {
        Player1.SetActive(false);
        yield return new WaitForSeconds(1f);
        Player1.SetActive(true);
        StopAllCoroutines();
    }

    public void _Player2Profilechange(int n)
    {
        Player2.SetActive(false);

        if (n == 0)
        {
            cnt++;
            Player2.MoveToSpring(Player2Pos[0], Player2Pos[1], 0.5f);
            StartCoroutine("ShowPlayer2", 1f);
        }
        if (n == 1)
        {
            cnt--;
            Player2.MoveToSpring(Player2Pos[2], Player2Pos[1], 0.5f);
            StartCoroutine("ShowPlayer2", 1f);
        }
    }
    IEnumerator ShowPlayer2()
    {
        Player2.SetActive(false);
        yield return new WaitForSeconds(1f);
        Player2.SetActive(true);
        StopAllCoroutines();
    }
    public void ShowInputfield(int n)
    {
        btnfield[n].gameObject.SetActive(false);
        Player_Txt[n].gameObject.SetActive(true);
    }
}
