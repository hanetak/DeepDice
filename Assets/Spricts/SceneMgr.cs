using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] AudioClip _pushAudio;

    [SerializeField] float _playTimer;
    float _playTime;

    bool _isSePlay;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _isSePlay = true;
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = Grobal.SE;

        _playTime -= Time.deltaTime;
        if (_playTime < 0)
        {
            _isSePlay = true;
        }
        else
        {
            _isSePlay = false;
        }
    }

    public void PressStart()
    {
        audioSource.PlayOneShot(_pushAudio);
        StartCoroutine("Stage1");
    }

    public void OnClickRetire()
    {
        Grobal.SetPlay(1);
        audioSource.PlayOneShot(_pushAudio);
        StartCoroutine("Title");
    }

    public void ChangeSriderSE()
    {
        if (_isSePlay)
        {
            audioSource.PlayOneShot(_pushAudio);
            _playTime = 0.5f;
        }
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

