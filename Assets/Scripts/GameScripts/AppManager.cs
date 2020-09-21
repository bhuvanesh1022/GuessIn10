﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppManager : MonoBehaviour
{
    public GameObject Home, AnimalKingdom, Cities;
    public GameObject[] _selectUI;
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
    //public void OnClickAnimalKingdom()
    //{
    //    Home.SetActive(false);
    //    AnimalKingdom.SetActive(true);
    //}
    //public void OnClickCities()
    //{
    //    Home.SetActive(false);
    //    Cities.SetActive(true);
    //}
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
}
