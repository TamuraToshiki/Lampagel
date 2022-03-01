using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;

    private bool b_shot = false;
    private Vector2 StartPos;
    private Vector2 EndPos;
    private Vector2 Move;

    [SerializeField] private float Initial_Vec = 10.0f;   // �����{��
    [SerializeField] private float Decelerate = 0.9f;   // ������

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update��������
        First();

        // �L�[�{�[�h�ړ�
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.Space)) // ���N���b�N
        {
            if (!b_shot)
            {
                StartPos = Input.mousePosition;
            }
            b_shot = true;
        }
        else
        {
            if (b_shot)
            {
                EndPos = Input.mousePosition;
                Move = (StartPos - EndPos).normalized;
                b_shot = false;
            }
        }


        // �p�b�h�ړ�

        // �n�[�h���[�h

        // Update�ŏI����
        Last();
    }

    private void First()
    {
        Move = Vector2.zero;
    }

    private void Last()
    {
        // 2������3�����ɕϊ�
        var move = new Vector3(Move.x, 0.0f, Move.y);
        rb.AddForce(move * Initial_Vec, ForceMode.Impulse);
        rb.velocity *= Decelerate;
    }
}
