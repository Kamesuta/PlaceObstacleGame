using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePlacer : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject currentSelectedObstacle;

    private Grid grid;
    private int placingLayer;
    private int obstacleLayer;

    // Start is called before the first frame update
    void Start()
    {
        grid = GetComponent<Grid>();
        placingLayer = LayerMask.NameToLayer("Placing");
        obstacleLayer = LayerMask.NameToLayer("Obstacle");
    }

    // Update is called once per frame
    void Update()
    {
        // 操作中のオブジェクトがない場合は召喚
        if (currentSelectedObstacle == null)
        {
            currentSelectedObstacle = Instantiate(obstaclePrefab, transform);

            // レイヤー
            currentSelectedObstacle.layer = placingLayer;
        }

        // マウスの位置を取得
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        // マウスの位置をグリッドにスナップ
        Vector3Int cellPosition = grid.WorldToCell(mousePos);
        Vector3 cellCenterPosition = grid.GetCellCenterWorld(cellPosition);
        cellCenterPosition.z = 0;

        // 操作中のオブジェクトをグリッドにスナップ
        currentSelectedObstacle.transform.position = cellCenterPosition;

        // クリックでオブジェクトを配置
        if (Input.GetMouseButtonDown(1))
        {
            // レイヤー
            currentSelectedObstacle.layer = obstacleLayer;

            currentSelectedObstacle = null;
        }
    }
}
