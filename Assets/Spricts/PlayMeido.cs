using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayMeido : Token
{
    [SerializeField] GameObject _textMeido;
    Lv1 lv1List;
    Lv2 lv2List;
    Lv3 lv3List;


    //状態
    enum eState
    {
        Tuujou,
        Kusukusu,
        Kantann,
        Komaru,
        Egao,
        Sagesumi,

    }

    //各種スプライト
    [SerializeField] Sprite[] _Sprites;

    // Start is called before the first frame update
    void Start()
    {
        lv1List = Resources.Load("Lv1") as Lv1;
        lv2List = Resources.Load("Lv2") as Lv2;
        lv3List = Resources.Load("Lv3") as Lv3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MeidoAction(int state,int lv)
    {
        switch (state)
        {
            case 0:
                SetSprite(_Sprites[0]);
                if (lv == 1)
                {
                    _textMeido.GetComponent<Text>().text = lv1List.param[Random.Range(0,5)].tuujou;
                }
                if (lv == 2)
                {
                    _textMeido.GetComponent<Text>().text = lv2List.param[Random.Range(0,4)].tuujou;
                }
                if (lv == 3)
                {
                    _textMeido.GetComponent<Text>().text = lv2List.param[Random.Range(0,3)].tuujou;
                }
                break;
            case 1:
                SetSprite(_Sprites[1]);
                _textMeido.GetComponent<Text>().text = lv1List.param[0].kusukusu;
                break;
            case 2:
                SetSprite(_Sprites[2]);
                _textMeido.GetComponent<Text>().text = lv1List.param[Random.Range(0,3)].kantan;
                break;
            case 3:
                SetSprite(_Sprites[3]);
                _textMeido.GetComponent<Text>().text = lv1List.param[Random.Range(0,3)].komaru;
                break;
            case 4:
                SetSprite(_Sprites[4]);
                if (lv == 1)
                {
                    _textMeido.GetComponent<Text>().text = lv1List.param[Random.Range(0,3)].ureshii;
                }
                if (lv == 2)
                {
                    _textMeido.GetComponent<Text>().text = lv2List.param[Random.Range(0,3)].ureshii;
                }
                if (lv == 3)
                {
                    _textMeido.GetComponent<Text>().text = lv2List.param[Random.Range(0,3)].ureshii;
                }
                break;
            case 5:
                SetSprite(_Sprites[5]);
                break;
            case 6://残り30
                SetSprite(_Sprites[0]);
                _textMeido.GetComponent<Text>().text = lv3List.param[3].tuujou;
                break; 
        }
    }

}
