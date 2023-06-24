using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grobal
{
    //初期化


    //スコア
    static int _score;
    public static int Score
    {
        get { return _score; }
    }

    public static void SetScore(int score)
    {
        _score = score;
    }

    //といた数
    static int _solveNum;
    public static int Solve
    {
        get { return _solveNum; }
    }

    public static void SetSolve(int solveNum)
    {
        _solveNum = solveNum;
    }

    static int _playCount;
    public static int PlayCount
    {
        get { return _playCount; }
    }

    public static void SetPlay(int PlayCount)
    {
        _playCount = PlayCount;
    }

    static float _bgm;
    public static float Bgm
    {
        get { return _bgm; }
    }

    public static void SetBGM(float bgm)
    {
        _bgm = bgm;
    }

    static float _se;
    public static float SE
    {
        get { return _se; }
    }

    public static void SetSE(float se)
    {
        _se = se;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
