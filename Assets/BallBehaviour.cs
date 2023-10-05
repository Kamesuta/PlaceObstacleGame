using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    // 矢印オブジェクト
    public GameObject arrow;

    // 打つ強さ
    public float power;
    // 矢印の長さ
    public float arrowLengthScale;

    // ボールの物理
    private Rigidbody2D rb;

    private Vector3 startPos;
    private float dragLength;
    private float dragAngle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 矢印を非表示
        arrow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 矢印を表示
            arrow.SetActive(true);

            // 長さと角度を初期化
            dragLength = 0;
            dragAngle = 0;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // 矢印を非表示
            arrow.SetActive(false);

            // ボールに力を加える
            float force = -dragLength * power;
            rb.AddForce(new Vector2(force * Mathf.Cos(dragAngle), force * Mathf.Sin(dragAngle)));
        }
        else if (Input.GetMouseButton(0))
        {
            // マウスの位置を取得
            Vector3 mousePos = Input.mousePosition;

            // マウスの位置を取得
            var pos = arrow.transform.position;
            pos.z = 0;
            startPos = Camera.main.WorldToScreenPoint(pos);

            // マウスの位置と初期位置の差を取得
            Vector3 diff = mousePos - startPos;

            // 矢印の長さを更新
            dragLength = diff.magnitude;
            // 矢印の角度を更新
            dragAngle = Mathf.Atan2(diff.y, diff.x);

            // 矢印の長さを更新
            arrow.transform.localScale = new Vector3(1, dragLength * arrowLengthScale, 1);
            // 矢印の角度を更新
            arrow.transform.rotation = Quaternion.Euler(0, 0, dragAngle * Mathf.Rad2Deg + 90);
        }
    }
}
