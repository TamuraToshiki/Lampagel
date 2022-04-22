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

    [SerializeField] private GameObject Circle;

    // �T�[�N���T�C�Y
    [Header("�\���T�[�N���̃T�C�Y")] [SerializeField] private Vector3 vCircleSize = new Vector3(0.5f, 0.5f, 0.5f);

    // �X���T�C�Y
    [Header("�X���T�C�Y")] [SerializeField] private Vector3 vFireSize = new Vector3(0.5f, 3.0f, 0.5f);

    private void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
        player = enemyBase.player;
    }

    private void FirePillarAttack()
    {
        // �����蔻��p�L���[�u
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // �����蔻��p�L���[�u���\��
        cube.GetComponent<MeshRenderer>().enabled = false;

        // �X���̃T�C�Y�A���W�A�p�x�ݒ�
        cube.transform.localScale = vFireSize;
        transform.Rotate(-90.0f, 0.0f, 0.0f);
        cube.transform.position = player.transform.position;

        // �X���R���|�[�l���g����

        // �U���T�[�N���̑傫���ύX
        Circle.transform.localScale = vCircleSize;

        // Fire.cs�ǉ�
        cube.AddComponent<Ice>();

        // �U���T�[�N���Z�b�g
        cube.GetComponent<Ice>().SetCircle(Circle);

        // �G���Z�b�g
        cube.GetComponent<Ice>().enemy = gameObject;

        // �v���C���[���Z�b
        cube.GetComponent<Fire>().player = player;
    }
}

