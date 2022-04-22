//======================================================================
// IcePillar.cs
//======================================================================
// �J������
//
// 2022/04/21 author�F�����x ����J�n
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class IcePillar : MonoBehaviour
{
    // �����蔻��p�L���[�u
    GameObject cube;

    EnemyBase enemyBase;
    GameObject player;

    // �U���\���T�[�N��
    [SerializeField] private GameObject Circle;

    [Header("�\���T�[�N���̃T�C�Y")]
    [SerializeField] private Vector3 vCircleSize = new Vector3(0.5f, 0.5f, 0.5f);

    [Header("�X���T�C�Y")]
    [SerializeField] private Vector3 vFireSize = new Vector3(0.5f, 3.0f, 0.5f);

    [Header("�X������")]
    [SerializeField] private float fIceTime = 6.0f;

    private void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
        player = enemyBase.player;
    }

    private void IcePillarAttack()
    {
        // �����蔻��p�L���[�u
        cube = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

        // �X���̃T�C�Y�A���W�A�p�x�ݒ�
        cube.transform.localScale = vFireSize;
        transform.Rotate(-90.0f, 0.0f, 0.0f);
        cube.transform.position = player.transform.position;

        // �U���T�[�N���̑傫���ύX
        Circle.transform.localScale = vCircleSize;

        // �L���[�u�ɓ�����Ȃ��悤��
        cube.GetComponent<CapsuleCollider>().isTrigger = true;

        // �L���[�u���\��
        cube.GetComponent<MeshRenderer>().enabled = false;

        // Ice.cs�ǉ�
        cube.AddComponent<Ice>();

        // �U���T�[�N���Z�b�g
        cube.GetComponent<Ice>().SetCircle(Circle);

        // �G���Z�b�g
        cube.GetComponent<Ice>().enemy = gameObject;

        // �v���C���[���Z�b�g
        cube.GetComponent<Ice>().player = player;

        // �X���̐������Ԑݒ�
        cube.GetComponent<Ice>().fLifeTime = fIceTime;
    }
}

