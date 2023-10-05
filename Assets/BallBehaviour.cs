using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    // ���I�u�W�F�N�g
    public GameObject arrow;

    // �ł���
    public float power;
    // ���̒���
    public float arrowLengthScale;

    // �{�[���̕���
    private Rigidbody2D rb;

    private Vector3 startPos;
    private float dragLength;
    private float dragAngle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // �����\��
        arrow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ����\��
            arrow.SetActive(true);

            // �����Ɗp�x��������
            dragLength = 0;
            dragAngle = 0;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // �����\��
            arrow.SetActive(false);

            // �{�[���ɗ͂�������
            float force = -dragLength * power;
            rb.AddForce(new Vector2(force * Mathf.Cos(dragAngle), force * Mathf.Sin(dragAngle)));
        }
        else if (Input.GetMouseButton(0))
        {
            // �}�E�X�̈ʒu���擾
            Vector3 mousePos = Input.mousePosition;

            // �}�E�X�̈ʒu���擾
            var pos = arrow.transform.position;
            pos.z = 0;
            startPos = Camera.main.WorldToScreenPoint(pos);

            // �}�E�X�̈ʒu�Ə����ʒu�̍����擾
            Vector3 diff = mousePos - startPos;

            // ���̒������X�V
            dragLength = diff.magnitude;
            // ���̊p�x���X�V
            dragAngle = Mathf.Atan2(diff.y, diff.x);

            // ���̒������X�V
            arrow.transform.localScale = new Vector3(1, dragLength * arrowLengthScale, 1);
            // ���̊p�x���X�V
            arrow.transform.rotation = Quaternion.Euler(0, 0, dragAngle * Mathf.Rad2Deg + 90);
        }
    }
}
