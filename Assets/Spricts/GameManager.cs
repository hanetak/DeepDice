using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum MapCellType
{
    Battle,
    Rest,
    Event
}

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

    private int _DiceNum;
    private int _MoveNum;
    public GameObject _textDice;
    public GameObject _textMove;
    public GameObject _camera;
    private Vector3 lastMoveDirection;

    public int width = 10;
    public int height = 10;
    private MapCellType[,] map;

    public GameObject battleCellPrefab;
    public GameObject restCellPrefab;
    public GameObject eventCellPrefab;

    public float moveDistance = 4.0f; // カメラの移動距離

    // Start is called before the first frame update
    void Start()
    {
        map = new MapCellType[width, height];
        GenerateMap();
        _MoveNum = -1;
        _DiceNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _textDice.GetComponent<Text>().text = _DiceNum.ToString();
        _textMove.GetComponent<Text>().text = _MoveNum.ToString();

        if (_MoveNum > 0)
        {
            // 上矢印キーが押されたら
            if (Input.GetKeyDown(KeyCode.UpArrow) && lastMoveDirection != Vector3.down)
            {
                // カメラの位置を上に移動する
                _camera.transform.position += Vector3.up * moveDistance;
                _MoveNum -= 1;
                lastMoveDirection = Vector3.up;
            }

            // 下矢印キーが押されたら
            if (Input.GetKeyDown(KeyCode.DownArrow) && lastMoveDirection != Vector3.up)
            {
                // カメラの位置を下に移動する
                _camera.transform.position += Vector3.down * moveDistance;
                _MoveNum -= 1;
                lastMoveDirection = Vector3.down;
            }

            // 左矢印キーが押されたら
            if (Input.GetKeyDown(KeyCode.LeftArrow) && lastMoveDirection != Vector3.right)
            {
                // カメラの位置を左に移動する
                _camera.transform.position += Vector3.left * moveDistance;
                _MoveNum -= 1;
                lastMoveDirection = Vector3.left;
            }

            // 右矢印キーが押されたら
            if (Input.GetKeyDown(KeyCode.RightArrow) && lastMoveDirection != Vector3.left)
            {
                // カメラの位置を右に移動する
                _camera.transform.position += Vector3.right * moveDistance;
                _MoveNum -= 1;
                lastMoveDirection = Vector3.right;
            }
        }
        // プレイヤーがマスに止まったとき
        if (_MoveNum == 0)
        {
            // プレイヤーが止まったマスの座標を計算
            int x = (int)(_camera.transform.position.x / moveDistance) + width / 2;
            int y = (int)(_camera.transform.position.y / moveDistance) + height / 2;

            // マスの種類に基づいてイベント画面を表示
            switch (map[x, y])
            {
                case MapCellType.Battle:
                    // 戦闘イベント画面を表示
                    _MoveNum = -1;
                    break;
                case MapCellType.Rest:
                    // 休憩イベント画面を表示
                    _MoveNum = -1;
                    break;
                case MapCellType.Event:
                    // イベント画面を表示
                    _MoveNum = -1;
                    break;
            }
        }
    }

    public int RollDice()
    {
        int num = Random.Range(1, 7);
        return num;
    }

    public void OnClickDice()
    {
        _DiceNum = RollDice();
        _MoveNum = _DiceNum;
    }

    void GenerateMap()
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                GameObject cellPrefab;

                // ランダムな値に基づいてセルのタイプを設定します
                int randomValue = Random.Range(0, 3);
                if(randomValue == 0)
                {
                    map[x, y] = MapCellType.Battle;
                    cellPrefab = battleCellPrefab;
                }
                else if(randomValue == 1)
                {
                    map[x, y] = MapCellType.Rest;
                    cellPrefab = restCellPrefab;
                }
                else
                {
                    map[x, y] = MapCellType.Event;
                    cellPrefab = eventCellPrefab;
                }

                // プレハブをインスタンス化します
                int pointX = x -  width/2;
                int pointY = y -  height/2;
                pointX =  pointX * 4;
                pointY = pointY * 4;
                Instantiate(cellPrefab, new Vector3(pointX,  pointY, 0), Quaternion.identity);
            }
        }
    }
}
