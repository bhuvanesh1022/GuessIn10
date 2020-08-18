using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject currentPackage;
    public Transform holder;

    public GameObject activeGO;
    public List<GameObject> openList = new List<GameObject>();
    public List<GameObject> unopenedList = new List<GameObject>();
    public List<GameObject> myList = new List<GameObject>();
    public List<GameObject> oppenentList = new List<GameObject>();

    public TextMeshProUGUI myScore;
    public TextMeshProUGUI oppScore;
    public TextMeshProUGUI endGame;

    GameObject activePackage;

    // Start is called before the first frame update
    void Start()
    {
        activePackage = Instantiate(currentPackage, holder);
        InitiateCard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitiateCard()
    {
        for (int i = 0; i < currentPackage.transform.childCount; i++)
        {
            activePackage.transform.GetChild(i).gameObject.SetActive(false);
            unopenedList.Add(activePackage.transform.GetChild(i).gameObject);
        }

        StartCoroutine("LoadCard");
    }

    IEnumerator LoadCard()
    {
        if (activeGO) activeGO.SetActive(false);
        yield return new WaitForSeconds(0.01f);

        if (unopenedList.Count > 0)
        {
            int r = Random.Range(0, unopenedList.Count);

            activeGO = unopenedList[r];
            unopenedList.RemoveAt(r);
            activeGO.SetActive(true);
        }
        else
        {
            endGame.gameObject.SetActive(true);
        }
    }

    public void AddToOpenList()
    {
        openList.Add(activeGO);
        StartCoroutine("LoadCard");
    }

    public void AddToMyList()
    {
        myList.Add(activeGO);
        myScore.text = myList.Count.ToString();
        StartCoroutine("LoadCard");
    }

    public void AddToOpponentList()
    {
        oppenentList.Add(activeGO);
        oppScore.text = oppenentList.Count.ToString();
        StartCoroutine("LoadCard");
    }
}
