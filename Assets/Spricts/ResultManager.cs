using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultManager : Token
{
    private AudioSource audioSource;

    Rigidbody2D rb;

    Vector2 _pos;

    Vector2 _force;
    [SerializeField] float _spd;
    [SerializeField] float _posY_low;
    [SerializeField] int _jumpCnt;
    [SerializeField] float _gra;
    [SerializeField] int _junp;
    [SerializeField] GameObject _textMeido;

    Result textList;

    //各種スプライト
    [SerializeField] Sprite[] _Sprites;

    [SerializeField] AudioClip _pushAudio;

    [SerializeField] float _playTimer;
    float _playTime;

    bool _isPlay;

    enum eState
    {
        Tuujou,
        Kusukusu,
        Kantann,
        Komaru,
        Egao,
        Sagesumi,

    }
    // Start is called before the first frame update
    void Start()
    {
        _isPlay = false;
        _playTime = _playTimer;
        StartCoroutine("ViewRanking");
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = Grobal.SE;
        textList = Resources.Load("Result") as Result;
        rb = this.gameObject.GetComponent<Rigidbody2D>();

        if (Grobal.Solve == 0)
        {
            MeidoAction(eState.Sagesumi);
        }
        else if (Grobal.Solve < 6 && Grobal.Solve > 0)
        {
            MeidoAction(eState.Komaru);
        }
        else if (Grobal.Solve < 18 && Grobal.Solve >= 6)
        {
            MeidoAction(eState.Tuujou);
        }
        else if (Grobal.Solve >= 18 && Grobal.Solve < 22)
        {
            MeidoAction(eState.Kusukusu);
        }
        else if (Grobal.Solve >= 22 && Grobal.Score < 30000)
        {
            MeidoAction(eState.Egao);
        }
        else if (Grobal.Solve >= 22 && Grobal.Score >= 30000)
        {
            MeidoAction(eState.Kantann);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _playTime -= Time.deltaTime;
        if (_playTime < 0)
        {
            _isPlay = true;
        }
        else
        {
            _isPlay = false;
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

    public void OnRetryButton()
    {
        if (_isPlay)
        {
            audioSource.PlayOneShot(_pushAudio);
            StartCoroutine("Stage1");
        }
    }

    public void OnTitleButton()
    {
        if (_isPlay)
        {
            Grobal.SetPlay(1);
            audioSource.PlayOneShot(_pushAudio);
            StartCoroutine("Title");
        }
    }

    void MeidoAction(eState state)
    {
        switch (state)
        {
            case eState.Tuujou:
                SetSprite(_Sprites[0]);
                _textMeido.GetComponent<Text>().text = textList.param[Random.Range(0, 3)].tuujou;
                StartCoroutine(SetMeidoMove(40f, 1, 0.6f));
                break;
            case eState.Kusukusu:
                SetSprite(_Sprites[1]);
                _textMeido.GetComponent<Text>().text = textList.param[Random.Range(0, 3)].kusukusu;
                StartCoroutine(SetMeidoMove(100f, 2, 1.6f));
                break;
            case eState.Kantann:
                SetSprite(_Sprites[2]);
                _textMeido.GetComponent<Text>().text = textList.param[Random.Range(0, 3)].kantan;
                StartCoroutine(SetMeidoMove(40f, 1, 0.3f));
                break;
            case eState.Komaru:
                SetSprite(_Sprites[3]);
                _textMeido.GetComponent<Text>().text = textList.param[Random.Range(0, 3)].komaru;
                StartCoroutine(SetMeidoMove(40f, 1, 0.3f));
                break;
            case eState.Egao:
                SetSprite(_Sprites[4]);
                _textMeido.GetComponent<Text>().text = textList.param[Random.Range(0, 3)].ureshii;
                StartCoroutine(SetMeidoMove(40f, 1, 0.3f));
                break;
            case eState.Sagesumi:
                SetSprite(_Sprites[5]);
                _textMeido.GetComponent<Text>().text = textList.param[Random.Range(0, 3)].sagesumi;
                StartCoroutine(SetMeidoMove(40f, 1, 0.3f));
                break;
        }

        IEnumerator SetMeidoMove(float spd, int cnt, float gravity)
        {
            yield return new WaitForSeconds(0.1f);
            this.transform.position = new Vector2(-5.6f, -0.9f);
            _spd = spd;
            _jumpCnt = cnt;
            _force = new Vector2(0f, _spd);
            rb.AddForce(_force);
            rb.gravityScale = gravity;
        }
    }

    IEnumerator ViewRanking()
    {
        yield return new WaitForSeconds(1.0f);
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(Grobal.Score);

    }

    IEnumerator Title()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Title");
    }

    IEnumerator Stage1()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Stage1");
    }

}
