using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveDistance = 4.0f; // カメラの移動距離

    void Update()
    {
        // 上矢印キーが押されたら
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // カメラの位置を上に移動する
            transform.position += Vector3.up * moveDistance;
        }

        // 下矢印キーが押されたら
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // カメラの位置を下に移動する
            transform.position += Vector3.down * moveDistance;
        }

        // 左矢印キーが押されたら
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // カメラの位置を左に移動する
            transform.position += Vector3.left * moveDistance;
        }

        // 右矢印キーが押されたら
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // カメラの位置を右に移動する
            transform.position += Vector3.right * moveDistance;
        }
    }
}
