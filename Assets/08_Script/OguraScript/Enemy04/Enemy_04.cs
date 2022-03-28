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
    GameObject cube;
    EnemyBase enemyBase;
    GameObject player;

    [SerializeField] private GameObject Circle;

    private void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
        player = enemyBase.GetPlayer;
    }

    private void AttackEnemy04()
    {
        // �����蔻��p�L���[�u
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // �����蔻��p�L���[�u�𓧖���
        cube.GetComponent<MeshRenderer>().material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse"); ;
        cube.GetComponent<MeshRenderer>().material.color -= new Color32(255,255,255,255);

        // �Β��̃T�C�Y�A���W�A�p�x�ݒ�
        cube.transform.localScale = new Vector3(0.5f, 3.0f, 0.5f);
        cube.transform.rotation = this.transform.rotation;
        cube.transform.position = player.transform.position;

        // �Β��R���|�[�l���g����
        // ���蔲���锻��ɂ���
        cube.GetComponent<BoxCollider>().isTrigger = true;

        // �K�v�ȏ����Z�b�g
        Circle.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        cube.AddComponent<Fire>();
        cube.GetComponent<Fire>().SetCircle(Circle);
        cube.GetComponent<Fire>().SetEnemy(gameObject);
        cube.GetComponent<Fire>().SetPlayer(player);

        //// �e�̃R���|�[�l���g����
        //spher.AddComponent<Bullet>();
        //spher.GetComponent<Bullet>().Speed = 3.0f;
        //spher.GetComponent<Bullet>().SetPlayer(enemyBase.GetComponent<EnemyBase>().GetPlayer);
        //spher.GetComponent<Bullet>().SetEnemy(this.gameObject);
        //spher.AddComponent<Rigidbody>();
        //spher.GetComponent<Rigidbody>().useGravity = false;
        //spher.GetComponent<Rigidbody>().isKinematic = true;
    }
}
