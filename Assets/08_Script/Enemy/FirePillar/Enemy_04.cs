//======================================================================
// Enemy_04.cs
//======================================================================
// �J������
//
// 2022/03/21 author�F�����x ����J�n�@�G�̉Β��U��
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class Enemy_04 : MonoBehaviour
{
    // �����蔻��p�L���[�u
    GameObject cube;

    EnemyBase enemyBase;
    GameObject player;

    [SerializeField] private GameObject Circle;

    // �T�[�N���T�C�Y
    [Header("�\���T�[�N���̃T�C�Y")] [SerializeField] private Vector3 vCircleSize = new Vector3(0.5f, 0.5f, 0.5f);

    // �Β��T�C�Y
    [Header("�Β��T�C�Y")] [SerializeField] private Vector3 vFireSize = new Vector3(0.5f, 3.0f, 0.5f);

    private void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
        player = enemyBase.GetPlayer;
    }

    private void FirePillarAttack()
    {
        // �����蔻��p�L���[�u
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // �����蔻��p�L���[�u���\��
        cube.GetComponent<MeshRenderer>().enabled = false;

        // �Β��̃T�C�Y�A���W�A�p�x�ݒ�
        cube.transform.localScale = vFireSize;
        transform.Rotate(-90.0f,0.0f,0.0f);
        cube.transform.position = player.transform.position;

        // �Β��R���|�[�l���g����
        // ���蔲���锻��ɂ���
        cube.GetComponent<BoxCollider>().isTrigger = true;

        // �K�v�ȏ����Z�b�g
        Circle.transform.localScale = vCircleSize;
        cube.AddComponent<Fire>();
        cube.GetComponent<Fire>().SetCircle(Circle);
        cube.GetComponent<Fire>().SetEnemy(gameObject);
        cube.GetComponent<Fire>().SetPlayer(player);
    }
}
