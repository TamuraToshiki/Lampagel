//======================================================================
// PlayerMove.cs
//======================================================================
// �J������
//
// 2022/03/01 author�F���쏫�V �v���C���[�̈ړ��쐬(�}�E�X)
// 2022/03/05 author�F�c���q�� ��ʂ̂ǂ��𑀍삵�Ă������悤�ɑ����
// 2022/03/09 author�F�c���q�� �p�b�h�������
// 2022/03/09 author�F�c���q�� �ړ������������悤�ɕύX
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����R���|�[�l���g�A�^�b�`
[RequireComponent(typeof(Rigidbody))]

public class PlayerMove : MonoBehaviour
{
    // �����{��
    [SerializeField] private float fInitial = 50.0f;
    // ������
    [SerializeField] private float fLate = 0.995f;

    private PlayerState state;
    private Rigidbody rb;

    // ���˕���
    [SerializeField] private LineRenderer Direction = null;
    // ���˕����̗�
    private Vector3 vCurrentForce = Vector3.zero;
    // �h���b�O�J�n�_
    private Vector3 vDragStart = Vector3.zero;

    // �~�ώ���
    private float fStockPower = 0;

    private bool bShot = false;

    void Start()
    {
        state = GetComponent<PlayerState>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!state.IsNormal)
        {
            fStockPower = 0;
            Direction.enabled = false;
            return;
        }

        // �����Ă����������
        if (rb.velocity != new Vector3(0, 0, 0))
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }

        PadMove();
        KeyBoardMove();
        // ����
        rb.velocity *= fLate;
    }

    // �L�[�{�[�h����
    private void KeyBoardMove()
    {
        // ���N���b�N����
        if (Input.GetMouseButton(0))
        {
            // �����ꂽ�Ƃ�
            if (Input.GetMouseButtonDown(0))
            {
                // �}�E�X�̏����ʒu���擾
                vDragStart = GetMousePosition();
                fStockPower = 0;
            }
            // ���������}�E�X���W�̈ʒu���擾
            var position = GetMousePosition();

            // �}�E�X�̏������W�Ɠ����������W�̍������擾
            vCurrentForce = vDragStart - position;

            // ��������������
            if (vCurrentForce != new Vector3(0, 0, 0))
            {
                transform.rotation = Quaternion.LookRotation(vCurrentForce);
            }

            // ���̈������菈��
            Direction.enabled = true;
            // ���������Ƌt�ɖ�󂪏o��悤��
            Direction.SetPosition(0, rb.position);
            Direction.SetPosition(1, rb.position - vCurrentForce.normalized * 2);

            // �}�E�X�������Ă�ԁA�З͂����߂�
            if (fStockPower < 2)
            {
                fStockPower += Time.deltaTime;
            }
        }

        // ���N���b�N���ꂽ�Ƃ�
        if (Input.GetMouseButtonUp(0))
        {
            // �u�ԓI�ɗ͂������Ă͂���
            rb.AddForce(vCurrentForce.normalized * fStockPower * fInitial, ForceMode.Impulse);

            // ������
            fStockPower = 0;
            Direction.enabled = false;
        }
    }

    private void PadMove()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        // �X�e�B�b�N��|���Ă�Ȃ�
        if (Mathf.Abs(x) >= 0.01f || Mathf.Abs(y) >= 0.01f)
        {
            // �t���O�𗧂Ă�
            bShot = true;
            // ���͕������t�ɂ��Ď󂯎��
            vCurrentForce = new Vector3(-x * Time.deltaTime, 0, -y * Time.deltaTime);

            // ��������������
            transform.rotation = Quaternion.LookRotation(vCurrentForce);

            // ���̈������菈��
            Direction.enabled = true;
            // ���������Ƌt�ɖ�󂪏o��悤��
            Direction.SetPosition(0, rb.position);
            Direction.SetPosition(1, rb.position - vCurrentForce.normalized * 2);

            fStockPower += Time.deltaTime;
            if (fStockPower < 2)
            {
                fStockPower += Time.deltaTime;
            }
        }
        else if (bShot == true)
        {
            // �t���O������
            bShot = false;
            // �u�ԓI�ɗ͂������Ă͂���
            rb.AddForce(vCurrentForce.normalized * fStockPower * fInitial, ForceMode.Impulse);
            // ������
            fStockPower = 0;
            Direction.enabled = false;
        }
    }

    // �}�E�X���W��3D���W�ɕϊ�
    private Vector3 GetMousePosition()
    {
        return new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
    }
}
