using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderManager : MonoBehaviour
{
    [SerializeField] int MinQuesvalue = 5, MaxQuesvalue = 10;
    [SerializeField] int MinGuessvalue = 5, MaxGuessvalue = 10;
    [SerializeField] int MinCluevalue = 5, MaxCluevalue = 10;
    public Slider QuesSlider, GuessSlider, ClueSlider;
    public TextMeshProUGUI[] _sliderTxt;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _sliderTxt[0].text = "Questions: " + QuesSlider.value.ToString();
        _sliderTxt[1].text = "Guesses: " + GuessSlider.value.ToString();
        _sliderTxt[2].text = "Clues: " + ClueSlider.value.ToString();

    }
}
