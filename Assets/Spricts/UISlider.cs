using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlider : MonoBehaviour
{
    [SerializeField] GameObject sliderBGM;
    [SerializeField] GameObject sliderSE;
    Slider _sliderBGM;
    Slider _sliderSE;

    // Start is called before the first frame update
    void Start()
    {
        _sliderBGM = sliderBGM.GetComponent<Slider>();
        _sliderSE = sliderSE.GetComponent<Slider>();
        if (Grobal.PlayCount == 1)
        {
            _sliderBGM.value = Grobal.Bgm;
            _sliderSE.value = Grobal.SE;
        }
    }


    // Update is called once per frame
    void Update()
    {
        float bgm = _sliderBGM.value;
        Grobal.SetBGM(bgm);
        float se = _sliderSE.value;
        Grobal.SetSE(se);
    }
}
