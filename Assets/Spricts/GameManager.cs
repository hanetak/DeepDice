
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private AudioSource audioSource;
    //効果音
    [SerializeField] AudioClip _pushAudio;
    [SerializeField] AudioClip _nextAudio;
    [SerializeField] AudioClip _resetAudio;
    [SerializeField] AudioClip _timeUpAudio;


    [SerializeField] GameObject _textMondai;
    [SerializeField] GameObject[] _textNumbers;
    [SerializeField] GameObject _textScore;
    [SerializeField] GameObject _PlayMeido;
    PlayMeido playMeido;

    [SerializeField] float _solveTimer;
    [SerializeField] float _faceTimer;

    [SerializeField] float _lockTimer;
    float _lockTime;

    float _solveTime;

    private int _mainNum;
    private int _bfNum;
    private int[] _nums;
    private int _score;
    private int _stage;

    public int _solveNum;

    private bool _isActive;
    // Start is called before the first frame update
    void Start()
    {
        _faceTimer = 10f;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = Grobal.SE;

        playMeido = _PlayMeido.GetComponent<PlayMeido>();

        _stage = 1;
        BoxTextView(1);
        StartCoroutine("FirstQuestion");
    }

    // Update is called once per frame
    void Update()
    {
        _lockTime -= Time.deltaTime;
        if (_lockTime < 0)
        {
            _isActive = true;
        }

        _solveTime += Time.deltaTime;
        if (_solveTime > _faceTimer)
        {
            _faceTimer = 9999;
            playMeido.MeidoAction(3, _stage);
        }
    }

    public void OnClickNum0()
    {
        if (_isActive)
        {
            Culclate(0);
            Check();
        }
    }

    public void OnClickNum1()
    {
        if (_isActive)
        {
            Culclate(1);
            Check();
        }
    }

    public void OnClickNum2()
    {
        if (_isActive)
        {
            Culclate(2);
            Check();
        }
    }

    public void OnClickNum3()
    {
        if (_isActive)
        {
            Culclate(3);
            Check();
        }
    }

    void Culclate(int i)
    {
        audioSource.PlayOneShot(_pushAudio);
        string tmp = _textNumbers[i].GetComponent<Text>().text[0].ToString();
        string index = _textNumbers[i].GetComponent<Text>().text.Remove(0, 1);
        if (tmp == "+")
        {
            _mainNum += int.Parse(index);
            _textMondai.GetComponent<Text>().text = _mainNum.ToString();
        }
        if (tmp == "-")
        {
            _mainNum -= int.Parse(index);
            _textMondai.GetComponent<Text>().text = _mainNum.ToString();
        }
        if (tmp == "×")
        {
            _mainNum *= int.Parse(index);
            _textMondai.GetComponent<Text>().text = _mainNum.ToString();
        }
        if (tmp == "÷")
        {
            _mainNum /= int.Parse(index);
            _textMondai.GetComponent<Text>().text = _mainNum.ToString();
        }
    }

    //ゾロ目かチェック
    void Check()
    {
        _isActive = false;
        _lockTime = _lockTimer;
        bool _isZorome = true;
        int loop = _mainNum.ToString().Length;
        int check = _mainNum;
        int bf = check % 10;
        check /= 10;
        for (int i = 0; i < loop - 1; i++)
        {
            int af = check % 10;
            check /= 10;
            if (af != bf)
            {
                _isZorome = false;
            }
            bf = af;
        }

        if (_mainNum < 0 || _mainNum > 99999)
        {
            _isActive = false;
            BoxTextView(4);
        }

        //ゾロ目の場合次の問題へ-------------------------------------------------------------------
        if (_isZorome && loop != 1 || _mainNum == 1)
        {

            _solveNum++;
            audioSource.PlayOneShot(_nextAudio);
            if (_mainNum == 1)
            {
                _score += _stage * _stage * 1000;
            }
            else
            {
                if (_stage == 1)
                {
                    _score += Mathf.Max(loop - 1, 1) * Mathf.Max(loop - 1, 1) * 100;
                }
                else if (_stage == 2)
                {
                    _score += Mathf.Max(loop - 2, 1) * Mathf.Max(loop - 2, 1) * 1000;
                }
                else if (_stage == 3)
                {
                    _score += Mathf.Max(loop - 3, 1) * 5000;
                }
            }
            _textScore.GetComponent<Text>().text = _score.ToString();
            BoxTextView(loop - 1 + 10);//箱の表示文字
            if (_mainNum == 1)
            {
                BoxTextView(1001);
            }
            else if (_solveNum % 10 == 1 && _solveNum != 1)
            {
                BoxTextView(101);
            }


            if (_solveNum <= 10)
            {
                if (_mainNum == 1)
                {
                    playMeido.MeidoAction(1, _stage);
                }
                else
                {
                    if (_solveTime < 1)
                    {
                        playMeido.MeidoAction(2, _stage);
                    }
                    else if (_solveTime < 3 && _solveTime >= 1)
                    {
                        playMeido.MeidoAction(4, _stage);
                    }
                    else
                    {
                        playMeido.MeidoAction(0, _stage);
                    }
                }

                StartCoroutine("keta2Question");
                _faceTimer = 10;
            }
            else if (_solveNum <= 20 && _solveNum > 10)
            {
                if (_mainNum == 1)
                {
                    playMeido.MeidoAction(1, _stage);
                }
                else
                {
                    if (_solveTime < 5)
                    {
                        playMeido.MeidoAction(2, _stage);
                    }
                    else if (_solveTime < 10 && _solveTime >= 5)
                    {
                        playMeido.MeidoAction(4, _stage);
                    }
                    else
                    {
                        playMeido.MeidoAction(0, _stage);
                    }
                }
                _stage = 2;

                StartCoroutine("keta3Question");
                _faceTimer = 20;
            }
            else
            {
                if (_mainNum == 1)
                {
                    playMeido.MeidoAction(1, _stage);
                }
                else
                {
                    if (_solveTime < 8)
                    {
                        playMeido.MeidoAction(2, _stage);
                    }
                    else if (_solveTime < 16 && _solveTime >= 8)
                    {
                        playMeido.MeidoAction(4, _stage);
                    }
                    else
                    {
                        playMeido.MeidoAction(0, _stage);
                    }
                }
                _stage = 3;
                StartCoroutine("keta4Question");
                _faceTimer = 30;
            }
            _lockTime = 0.5f;
            _solveTime = 0;
        }

    }

    public void OnClickReset()
    {
        if (_isActive)
        {
            audioSource.PlayOneShot(_resetAudio);
            BoxTextView(2);
            StartCoroutine("ResetNum");
        }
    }

    IEnumerator ResetNum()
    {
        yield return new WaitForSeconds(0.2f);

        List<int> numbers = new List<int>();
        int numbermax = 10 * _stage;
        List<int> ransus = new List<int>();

        for (int i = 1; i <= numbermax; i++)
        {
            numbers.Add(i);
        }

        int count = 4;
        while (count-- > 0)
        {

            int index = Random.Range(0, numbers.Count);
            int ransu = numbers[index];
            numbers.RemoveAt(index);
            ransus.Add(ransu);
        }

        for (int i = 0; i < 4; i++)
        {
            if (i < 2)
            {
                _textNumbers[i].GetComponent<Text>().text = "+" + ransus[i].ToString();
            }
            else
            {
                _textNumbers[i].GetComponent<Text>().text = "-" + ransus[i].ToString();
            }
            /*
            if(tmp == 5)
            {
                int index = Random.Range(2,5);
                _textNumbers[i].GetComponent<Text>().text = "×" + index.ToString();
            }
            if(tmp == 6)
            {
                int index = Random.Range(2,5);
                _textNumbers[i].GetComponent<Text>().text = "÷" + index.ToString();
            }*/
        }
        _mainNum = _bfNum;
        _textMondai.GetComponent<Text>().text = _mainNum.ToString();
        _isActive = true;
    }

    IEnumerator FirstQuestion()
    {
        _isActive = false;
        yield return new WaitForSeconds(1.0f);
        _mainNum = Random.Range(11, 91);
        if (_mainNum / 10 == _mainNum % 10)
        {
            _mainNum += Random.Range(1, 10);
        }
        _bfNum = _mainNum;
        _textMondai.GetComponent<Text>().text = _mainNum.ToString();

        //かぶらない
        List<int> numbers = new List<int>();
        List<int> ransus = new List<int>();

        for (int i = 1; i <= 10; i++)
        {
            numbers.Add(i);
        }

        int count = 4;
        while (count-- > 0)
        {

            int index = Random.Range(0, numbers.Count);
            int ransu = numbers[index];
            numbers.RemoveAt(index);
            ransus.Add(ransu);
        }

        for (int i = 0; i < 4; i++)
        {
            if (i < 2)
            {
                _textNumbers[i].GetComponent<Text>().text = "+" + ransus[i].ToString();
            }
            else
            {
                _textNumbers[i].GetComponent<Text>().text = "-" + ransus[i].ToString();
            }
            /*
            if(tmp == 5)
            {
                int index = Random.Range(2,5);
                _textNumbers[i].GetComponent<Text>().text = "×" + index.ToString();
            }
            if(tmp == 6)
            {
                int index = Random.Range(2,5);
                _textNumbers[i].GetComponent<Text>().text = "÷" + index.ToString();
            }*/
            _isActive = true;
        }
    }

    void NextQuestion()
    {

    }

    void BoxTextView(int state)
    {
        if (state == 1)
        {
            _textMondai.GetComponent<Text>().text = "START";
        }
        if (state == 2)
        {
            _textMondai.GetComponent<Text>().text = "RESET";
        }
        if (state == 3)
        {
            _textMondai.GetComponent<Text>().text = "FIN";
        }
        if (state == 4)
        {
            _textMondai.GetComponent<Text>().text = "ERROR";
        }
        if (state == 11)
        {
            _textMondai.GetComponent<Text>().text = "GOOD";
        }
        if (state == 12)
        {
            _textMondai.GetComponent<Text>().text = "NICE";
        }
        if (state == 13)
        {
            _textMondai.GetComponent<Text>().text = "WOW!";
        }
        if (state == 14)
        {
            _textMondai.GetComponent<Text>().text = "OMG!";
        }
        if (state == 101)
        {
            _textMondai.GetComponent<Text>().text = "LvUP";
        }
        if (state == 1001)
        {
            _textMondai.GetComponent<Text>().text = "Solo1";
        }

    }

    public void GameOver()
    {
        _lockTime = 3.0f;
        _isActive = false;
        audioSource.PlayOneShot(_timeUpAudio);
        Grobal.SetScore(_score);
        Grobal.SetSolve(_solveNum);
        BoxTextView(3);
        StartCoroutine("GoResult");
    }

    IEnumerator GoResult()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Result");
    }

    IEnumerator keta2Question()
    {
        _isActive = false;
        yield return new WaitForSeconds(0.3f);
        _mainNum = Random.Range(11, 91);
        if (_mainNum / 10 == _mainNum % 10)
        {
            _mainNum += Random.Range(1, 10);
        }
        _bfNum = _mainNum;
        _textMondai.GetComponent<Text>().text = _mainNum.ToString();

        //かぶらない
        List<int> numbers = new List<int>();
        List<int> ransus = new List<int>();
        for (int i = 1; i <= 10; i++)
        {
            numbers.Add(i);
        }

        int count = 4;
        while (count-- > 0)
        {

            int index = Random.Range(0, numbers.Count);
            int ransu = numbers[index];
            numbers.RemoveAt(index);
            ransus.Add(ransu);
        }

        for (int i = 0; i < 4; i++)
        {
            if (i < 2)
            {
                _textNumbers[i].GetComponent<Text>().text = "+" + ransus[i].ToString();
            }
            else
            {
                _textNumbers[i].GetComponent<Text>().text = "-" + ransus[i].ToString();
            }
            /*
            if(tmp == 5)
            {
                int index = Random.Range(2,5);
                _textNumbers[i].GetComponent<Text>().text = "×" + index.ToString();
            }
            if(tmp == 6)
            {
                int index = Random.Range(2,5);
                _textNumbers[i].GetComponent<Text>().text = "÷" + index.ToString();
            }*/
            _isActive = true;
        }
    }

    IEnumerator keta3Question()
    {
        _isActive = false;
        yield return new WaitForSeconds(0.3f);
        _mainNum = Random.Range(101, 990);
        bool _isZorome = true;
        int loop = _mainNum.ToString().Length;
        int check = _mainNum;
        int bf = check % 10;
        check /= 10;
        for (int i = 0; i < loop - 1; i++)
        {
            int af = check % 10;
            check /= 10;
            if (af != bf)
            {
                _isZorome = false;
            }
            bf = af;
        }
        //ゾロ目の場合次の問題へ
        if (_isZorome && loop != 1)
        {
            _mainNum += Random.Range(1, 10);
        }
        _bfNum = _mainNum;
        _textMondai.GetComponent<Text>().text = _mainNum.ToString();

        //かぶらない
        List<int> numbers = new List<int>();
        List<int> ransus = new List<int>();
        for (int i = 1; i <= 20; i++)
        {
            numbers.Add(i);
        }

        int count = 4;
        while (count-- > 0)
        {
            int index = Random.Range(0, numbers.Count);
            int ransu = numbers[index];
            numbers.RemoveAt(index);
            ransus.Add(ransu);
        }

        for (int j = 0; j < 4; j++)
        {
            if (j < 2)
            {
                _textNumbers[j].GetComponent<Text>().text = "+" + ransus[j].ToString();
            }
            else
            {
                _textNumbers[j].GetComponent<Text>().text = "-" + ransus[j].ToString();
            }
            /*
            if(tmp == 5)
            {
                int index = Random.Range(2,5);
                _textNumbers[i].GetComponent<Text>().text = "×" + index.ToString();
            }
            if(tmp == 6)
            {
                int index = Random.Range(2,5);
                _textNumbers[i].GetComponent<Text>().text = "÷" + index.ToString();
            }*/
            _isActive = true;
        }
    }

    IEnumerator keta4Question()
    {
        _isActive = false;
        yield return new WaitForSeconds(0.3f);
        _mainNum = Random.Range(1001, 9990);
        bool _isZorome = true;
        int loop = _mainNum.ToString().Length;
        int check = _mainNum;
        int bf = check % 10;
        check /= 10;
        for (int i = 0; i < loop - 1; i++)
        {
            int af = check % 10;
            check /= 10;
            if (af != bf)
            {
                _isZorome = false;
            }
            bf = af;
        }
        //ゾロ目の場合次の問題へ
        if (_isZorome && loop != 1)
        {
            _mainNum += Random.Range(1, 10);
        }
        _bfNum = _mainNum;
        _textMondai.GetComponent<Text>().text = _mainNum.ToString();

        //かぶらない
        List<int> numbers = new List<int>();
        List<int> ransus = new List<int>();
        for (int i = 1; i <= 30; i++)
        {
            numbers.Add(i);
        }

        int count = 4;
        while (count-- > 0)
        {
            int index = Random.Range(0, numbers.Count);
            int ransu = numbers[index];
            numbers.RemoveAt(index);
            ransus.Add(ransu);
        }

        for (int j = 0; j < 4; j++)
        {
            if (j < 2)
            {
                _textNumbers[j].GetComponent<Text>().text = "+" + ransus[j].ToString();
            }
            else
            {
                _textNumbers[j].GetComponent<Text>().text = "-" + ransus[j].ToString();
            }
            /*
            if(tmp == 5)
            {
                int index = Random.Range(2,5);
                _textNumbers[i].GetComponent<Text>().text = "×" + index.ToString();
            }
            if(tmp == 6)
            {
                int index = Random.Range(2,5);
                _textNumbers[i].GetComponent<Text>().text = "÷" + index.ToString();
            }*/
            _isActive = true;
        }
    }
}
