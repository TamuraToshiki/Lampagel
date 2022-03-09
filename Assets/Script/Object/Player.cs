//======================================================================
// Player.cs
//======================================================================
// �J������
//
// 2022/03/01 author�F���쏫�V �v���C���[�̈ړ��쐬(�}�E�X)
// 2022/03/05 aythor�F�c���q�� ��ʂ̂ǂ��𑀍삵�Ă������悤�ɑ����
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����R���|�[�l���g�A�^�b�`
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LineRenderer))]

public class Player : PlayerManager
{
    // �����{��
    [SerializeField] private float fInitial = 50.0f;
    // ������
    [SerializeField] private float fLate = 0.995f;

    // ���˕���
    private LineRenderer Direction = null;
    // ���˕����̗�
    private Vector3 vCurrentForce = Vector3.zero;
    // �h���b�O�J�n�_
    private Vector3 vDragStart = Vector3.zero;

    // �~�ώ���
    private float fStockPower = 0;

    private bool bShot = false;

    protected override void Start()
    {
        base.Start();

        Direction = GetComponent<LineRenderer>();
    }

    protected override void Update()
    {
        base.Update();

        if (!IsNormal) return;

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

        if (Mathf.Abs(x) >= 0.01f || Mathf.Abs(y) >= 0.01f)
        {
            bShot = true;
            vCurrentForce = new Vector3(-x * Time.deltaTime, 0, -y * Time.deltaTime);
            fStockPower += Time.deltaTime;

            // ���̈������菈��
            Direction.enabled = true;
            // ���������Ƌt�ɖ�󂪏o��悤��
            Direction.SetPosition(0, rb.position);
            Direction.SetPosition(1, rb.position - vCurrentForce.normalized * 2);

            if (fStockPower < 2)
            {
                fStockPower += Time.deltaTime;
            }
        }
        else if (bShot == true)
        {
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

    private void OnCollisionEnter(Collision collision)
    {
        rb.velocity *= 1.2f;
    }


        //private bool b_shot = false;
        //private Vector2 StartPos;
        //private Vector2 EndPos;
        //private Vector2 Move;

        //// �����{��
        //[SerializeField] private float Initial_Vec = 10.0f;
        //// ������
        //[SerializeField] private float Decelerate = 0.9f;

        //protected override void Start()
        //{
        //    base.Start();
        //}

        //protected override void Update()
        //{
        //    base.Update();

        //    // Update��������
        //    First();

        //    // �L�[�{�[�h�ړ�
        //    if (Input.GetMouseButton(0)) // ���N���b�N
        //    {
        //        // �e�𔭎˂��ĂȂ���
        //        if (!b_shot)
        //        {
        //            // �}�E�X�̏����ʒu���擾
        //            StartPos = Input.mousePosition;
        //        }

        //        b_shot = true;
        //    }
        //    else
        //    {
        //        // �e�𔭎˂�����
        //        if (b_shot)
        //        {
        //            // �}�E�X�̈ړ�������̈ʒu
        //            EndPos = Input.mousePosition;

        //            // �ړ��ʒu�̍������擾
        //            Move = (StartPos - EndPos).normalized;
        //            Debug.Log(Move);
        //            b_shot = false;
        //        }
        //    }

        //    // �p�b�h�ړ�

        //    // �n�[�h���[�h

        //    // Update�ŏI����
        //    Last();
        //}

        //private void First()
        //{
        //    Move = Vector2.zero;
        //}

        //private void Last()
        //{
        //    // 2������3�����ɕϊ�
        //    var move = new Vector3(Move.x, 0.0f, Move.y);
        //    rb.AddForce(move * Initial_Vec, ForceMode.Impulse);
        //    rb.velocity *= Decelerate;
        //}

        //private Rigidbody rb = null;
        //// ���˕���
        //[SerializeField]
        //private LineRenderer Direction = null;
        //// �ő�t�^�͗�
        //private const float fMaxMagnitude = 2.0f;
        //// ���˕����̗�
        //private Vector3 vCurrentForce = Vector3.zero;
        //// ���C���J����
        //private Camera MainCamera = null;
        //// ���C���J�������W
        //private Transform MainCameraTransform = null;
        //// �h���b�O�J�n�_
        //private Vector3 vDragStart = Vector3.zero;

        //// ����������
        //public void Awake()
        //{
        //    rb = GetComponent<Rigidbody>();

        //    // ���C���J�����̏����擾
        //    MainCamera = Camera.main;
        //    MainCameraTransform = MainCamera.transform;
        //}

        //// �}�E�X���W�����[���h���W�ɕϊ����Ď擾
        //private Vector3 GetMousePosition()
        //{
        //    // Z���W����
        //    var position = Input.mousePosition;
        //    position.z = MainCameraTransform.position.z;
        //    position = MainCamera.ScreenToWorldPoint(position);

        //    return position;
        //}

        //// �}�E�X�N���b�N�J�n
        //public void OnMouseDown()
        //{
        //    // �}�E�X�̏����ʒu���擾
        //    vDragStart = GetMousePosition();

        //    // ���̈������菈��
        //    Direction.enabled = true;
        //    Direction.SetPosition(0, rb.position);
        //    Direction.SetPosition(1, rb.position);
        //}

        //// �}�E�X�N���b�N���̏���
        //public void OnMouseDrag()
        //{
        //    // ���������}�E�X���W�̈ʒu���擾
        //    var position = GetMousePosition();

        //    // �}�E�X�̏������W�Ɠ����������W�̍������擾
        //    vCurrentForce = position - vDragStart;

        //    // 2�_�Ԃ̋������r
        //    if (vCurrentForce.magnitude > fMaxMagnitude * fMaxMagnitude)
        //    {
        //        vCurrentForce *= fMaxMagnitude / vCurrentForce.magnitude;
        //    }

        //    // ���������Ƌt�ɖ�󂪏o��悤��
        //    Direction.SetPosition(0, rb.position - vCurrentForce);
        //    Direction.SetPosition(1, rb.position);
        //}

        //// �}�E�X��b�������̏���
        //public void OnMouseUp()
        //{
        //    Direction.enabled = false;

        //    // ����
        //    Flip(vCurrentForce * 3.0f);
        //}

        //// �v���C���[��e��
        //public void Flip(Vector3 force)
        //{
        //    // �u�ԓI�ɗ͂������Ă͂���
        //    rb.AddForce(force, ForceMode.Impulse);
        //}
    }
