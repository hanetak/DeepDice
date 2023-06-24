using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    public float countdownMinutes;
    private float countdownSeconds;
    private Text timeText;

    [SerializeField] GameObject _PlayMeido;
    PlayMeido playMeido;

    public GameObject gameObj;
    GameManager gameManager;

    bool _isActive;

    private void Start()
    {
        playMeido = _PlayMeido.GetComponent<PlayMeido>();
        gameManager = gameObj.GetComponent<GameManager>();
        timeText = GetComponent<Text>();
        countdownSeconds = countdownMinutes * 60;
        _isActive = true;
    }

    void Update()
    {
        if (_isActive)
        {
            countdownSeconds -= Time.deltaTime;
            var span = new TimeSpan(0, 0, (int)countdownSeconds);
            timeText.text = span.ToString(@"mm\:ss");


            if((int)countdownSeconds == 30){
                playMeido.MeidoAction(6,1);
            }
            if (countdownSeconds <= 0)
            {
                countdownSeconds = 0;
                gameManager.GameOver();
                _isActive = false;
                // 0秒になったときの処理
            }
        }

    }
}
