using UnityEngine;

public enum MapCellType
{
    Battle,
    Rest,
    Event
}

public class MapGenerator : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    private MapCellType[,] map;

    public GameObject battleCellPrefab;
    public GameObject restCellPrefab;
    public GameObject eventCellPrefab;

    void Start()
    {
        map = new MapCellType[width, height];
        GenerateMap();
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
                Instantiate(cellPrefab, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }
}