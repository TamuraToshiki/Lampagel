//======================================================================
// Enemy_06.cs
//======================================================================
// �J������
//
// 2022/04/15 author�F�����x �G�ǉ�
//
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_06 : MonoBehaviour
{
    GameObject cube;
    EnemyBase enemyBase;
    GameObject player;

    [SerializeField] private GameObject Circle;

    // �T�[�N���T�C�Y
    [Header("�\���T�[�N���̃T�C�Y")][SerializeField] private Vector3 vCircleSize = new Vector3(0.5f, 0.5f, 0.5f);

    // �X���T�C�Y
    [Header("�X���T�C�Y")][SerializeField] private Vector3 vIceSize = new Vector3(0.5f, 3.0f, 0.5f);


    private void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
        player = enemyBase.GetPlayer;
    }

    private void IcePillarAttack()
    {
        // �����蔻��p�L���[�u
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // �����蔻��p�L���[�u�̐F
        cube.GetComponent<MeshRenderer>().material.color = new Color32(255, 255, 255, 255);
        cube.GetComponent<MeshRenderer>().enabled = false;

        // �X���̃T�C�Y�A���W�A�p�x�ݒ�
        cube.transform.localScale = vIceSize;
        cube.transform.rotation = this.transform.rotation;
        cube.transform.position = player.transform.position;

        // ���蔲���锻��ɂ���
        cube.GetComponent<BoxCollider>().isTrigger = true;

        // �K�v�ȏ����Z�b�g
        // �\���T�[�N���̃T�C�Y�ύX
        Circle.transform.localScale = vCircleSize;
        cube.AddComponent<Ice>();
        cube.GetComponent<Ice>().SetCircle(Circle);
        cube.GetComponent<Ice>().SetEnemy(gameObject);
        cube.GetComponent<Ice>().SetPlayer(player);
    }
}
