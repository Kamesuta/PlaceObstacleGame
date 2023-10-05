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
        // ���쒆�̃I�u�W�F�N�g���Ȃ��ꍇ�͏���
        if (currentSelectedObstacle == null)
        {
            currentSelectedObstacle = Instantiate(obstaclePrefab, transform);

            // ���C���[
            currentSelectedObstacle.layer = placingLayer;
        }

        // �}�E�X�̈ʒu���擾
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        // �}�E�X�̈ʒu���O���b�h�ɃX�i�b�v
        Vector3Int cellPosition = grid.WorldToCell(mousePos);
        Vector3 cellCenterPosition = grid.GetCellCenterWorld(cellPosition);
        cellCenterPosition.z = 0;

        // ���쒆�̃I�u�W�F�N�g���O���b�h�ɃX�i�b�v
        currentSelectedObstacle.transform.position = cellCenterPosition;

        // �N���b�N�ŃI�u�W�F�N�g��z�u
        if (Input.GetMouseButtonDown(1))
        {
            // ���C���[
            currentSelectedObstacle.layer = obstacleLayer;

            currentSelectedObstacle = null;
        }
    }
}
