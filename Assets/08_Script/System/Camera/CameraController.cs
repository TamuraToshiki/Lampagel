//======================================================================
// CameraController.cs
//======================================================================
// �J������
//
// 2022/03/14 author�F���쏫�V �J�����̃v���C���[�Ǐ]�@�\����
//                             �t���[�J�����@�\����
// 2022/04/25 author�F�|���W�j�Y�@�J�������o�p�̃X�C�b�`���ǉ�
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //�J�����̈ړ���
    [SerializeField, Range(0.1f, 60.0f)]
    private float PositionStep = 90.0f;

    //�}�E�X���x
    [SerializeField, Range(30.0f, 300.0f)]
    private float MouseSensitive = 90.0f;

    //�J������transform  
    private Transform CameraTransform;

    //�}�E�X�̎n�_ 
    private Vector3 StartMousePos;

    //�J������]�̎n�_���
    private Vector3 PresentCamRotation;
    private Vector3 PresentCamPos;

    //�v���C���[���i�[�p
    private GameObject Player;

    // ���΋����擾�p
    private Vector3 Offset;

    // 0425 �J�������o�I���I�t
    public bool bOnDirection = false;

    // �J�����̏��
    // private bool bCameraMode = false;

    void Start()
    {
        CameraTransform = this.gameObject.transform;

        //�@Player�̏����擾
        this.Player = GameObject.Find("Player");

        // �J������Player�Ƃ̑��΋��������߂�
        Offset = transform.position - Player.transform.position;
    }

    void Update()
    {
        // �v���C���[�Ǐ]
        if(bOnDirection == false)
        transform.position = Player.transform.position + Offset;

        //�J�����̉�]
        // CameraRotationMouseControl();

        //�J�����̃��[�J���ړ�
        // CameraPositionKeyControl();  
    }


    //�J�����̉�]
    private void CameraRotationMouseControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartMousePos = Input.mousePosition;
            PresentCamRotation.x = CameraTransform.transform.eulerAngles.x;
            PresentCamRotation.y = CameraTransform.transform.eulerAngles.y;
        }

        if (Input.GetMouseButton(0))
        {
            //(�ړ��J�n���W - �}�E�X�̌��ݍ��W) / �𑜓x �Ő��K��
            float x = (StartMousePos.x - Input.mousePosition.x) / Screen.width;
            float y = (StartMousePos.y - Input.mousePosition.y) / Screen.height;

            //��]�J�n�p�x �{ �}�E�X�̕ω��� * �}�E�X���x
            float eulerX = PresentCamRotation.x + y * MouseSensitive;
            float eulerY = PresentCamRotation.y + x * MouseSensitive;

            CameraTransform.rotation = Quaternion.Euler(eulerX, eulerY, 0);
        }
    }

    //�J�����̃��[�J���ړ� �L�[
    private void CameraPositionKeyControl()
    {
        Vector3 campos = CameraTransform.position;

        // �E
        if (Input.GetKey(KeyCode.J)) { campos += CameraTransform.right * Time.deltaTime * PositionStep; }
        // ��
        if (Input.GetKey(KeyCode.L)) { campos -= CameraTransform.right * Time.deltaTime * PositionStep; }
        // ��
        if (Input.GetKey(KeyCode.I)) { campos += CameraTransform.up * Time.deltaTime * PositionStep; }
        // ��
        if (Input.GetKey(KeyCode.K)) { campos -= CameraTransform.up * Time.deltaTime * PositionStep; }
        // �Y�[���C��
        if (Input.GetKey(KeyCode.Space)) { campos += CameraTransform.forward * Time.deltaTime * PositionStep; }
        // �Y�[���A�E�g
        if (Input.GetKey(KeyCode.LeftShift)) { campos -= CameraTransform.forward * Time.deltaTime * PositionStep; }

        CameraTransform.position = campos;
    }
}
