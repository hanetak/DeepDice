using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : Token
{
    Rigidbody2D rb;

    Vector2 _pos;

    Vector2 _force;
    [SerializeField] float _spd;
    [SerializeField] float _posY_low;
    [SerializeField] int _jumpCnt;
    [SerializeField] float _gra;
    [SerializeField] int _junp;
    [SerializeField] GameObject _textMeido;
    Title textList;

    [SerializeField] float _lockTimer;
    float _lockTime;
    [SerializeField] float _faceTimer;
    float _faceTime;
    bool _isLock;

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
    [SerializeField] Sprite[] _Sprites;//通常

    // Start is called before the first frame update
    void Start()
    {
        _isLock = false;
        textList = Resources.Load("Title") as Title;

        rb = this.gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine("StartDelay");
        _faceTime = _faceTimer + 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        _lockTime -= Time.deltaTime;
        if (_lockTime < 0)
        {
            _isLock = false;
        }

        _faceTime -= Time.deltaTime;
        if (_faceTime < 0)
        {
            int ransu = Random.Range(0, 5);
            if (ransu == 0 || ransu == 4 || ransu == 2)
            {
                MeidoAction(eState.Tuujou);
            }
            if (ransu == 1)
            {
                MeidoAction(eState.Kusukusu);
            }
            if (ransu == 3)
            {
                MeidoAction(eState.Kantann);
            }
            _faceTime = _faceTimer;
        }

        _pos = this.gameObject.transform.position;
        if (_pos.y <= _posY_low && rb.velocity.y < 0 && _jumpCnt > 0)
        {
            _jumpCnt--;
            rb.velocity = Vector2.zero;
            if (_jumpCnt == 0)
            {
                rb.gravityScale = 0;
            }
            else
            {
                rb.AddForce(_force);
            }
        }
    }

    int _clickNum;
    public void OnClickMeido()
    {
        if (!_isLock)
        {
            _faceTime = 10.0f;
            _clickNum++;
            if (_clickNum == 20)
            {
                MeidoAction(eState.Komaru);
                _clickNum = 0;
            }
            else
            {
                int ransu = Random.Range(0, 5);
                if (ransu == 0 || ransu == 4)
                {
                    MeidoAction(eState.Tuujou);
                }
                if (ransu == 1)
                {
                    MeidoAction(eState.Kusukusu);
                }
                if (ransu == 2)
                {
                    MeidoAction(eState.Egao);
                }
                if (ransu == 3)
                {
                    MeidoAction(eState.Kantann);
                }

            }
        }

    }

    void MeidoAction(eState state)
    {
        switch (state)
        {
            case eState.Tuujou:
                SetSprite(_Sprites[0]);
                _textMeido.GetComponent<Text>().text = textList.param[Random.Range(0, 6)].tuujou;
                SetMeidoMove(40f, 1, 0.6f);
                break;
            case eState.Kusukusu:
                SetSprite(_Sprites[1]);
                _textMeido.GetComponent<Text>().text = textList.param[Random.Range(0, 3)].kusukusu;
                SetMeidoMove(100f, 2, 1.6f);
                break;
            case eState.Kantann:
                SetSprite(_Sprites[2]);
                _textMeido.GetComponent<Text>().text = textList.param[Random.Range(0, 3)].kantan;
                SetMeidoMove(40f, 1, 0.3f);
                break;
            case eState.Komaru:
                SetSprite(_Sprites[3]);
                _textMeido.GetComponent<Text>().text = textList.param[Random.Range(0, 3)].komaru;
                SetMeidoMove(40f, 1, 0.3f);
                break;
            case eState.Egao:
                SetSprite(_Sprites[4]);
                _textMeido.GetComponent<Text>().text = textList.param[Random.Range(0, 3)].ureshii;
                SetMeidoMove(40f, 1, 0.3f);
                break;
            case eState.Sagesumi:
                SetSprite(_Sprites[5]);
                SetMeidoMove(40f, 1, 0.3f);
                break;
        }

        void SetMeidoMove(float spd, int cnt, float gravity)
        {
            this.transform.position = new Vector2(-5.6f, -0.9f);
            _spd = spd;
            _jumpCnt = cnt;
            rb.velocity = Vector2.zero;
            _force = new Vector2(0f, _spd);
            rb.AddForce(_force);
            rb.gravityScale = gravity;
        }
        _isLock = true;
        _lockTime = _lockTimer;
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(3.0f);
        MeidoAction(eState.Tuujou);
    }
}
