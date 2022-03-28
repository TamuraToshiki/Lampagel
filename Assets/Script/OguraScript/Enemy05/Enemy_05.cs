
//======================================================================
// Flamethrower.cs
//======================================================================
// �J������
//
// 2022/03/21 author�F�����x ����J�n�@�G�̉Ή�����
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_05 : MonoBehaviour
{
    GameObject cube;
    EnemyBase enemyBase;

    // �Ή����ˋ���
    float fDistance = 3.0f;

    private void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
    }


    private void AttackEnemy05()
    {
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);


        // �e�̃T�C�Y�A���W�A�p�x�ݒ�
        cube.transform.localScale = new Vector3(1.0f, 1.0f, 5.0f);
        cube.transform.rotation = this.transform.rotation;
        cube.transform.position = new Vector3(transform.position.x + transform.forward.x * fDistance, transform.position.y, transform.position.z + transform.forward.z * fDistance);

        cube.AddComponent<Flamethrower>();
        cube.GetComponent<Flamethrower>().SetEnemy(gameObject);
        cube.GetComponent<Flamethrower>().SetDiss(fDistance);
        cube.GetComponent<BoxCollider>().isTrigger = true;
        enemyBase.SetAttack(true);

        // �����蔻��p�L���[�u�𓧖���(�f�o�b�O�p)
        cube.GetComponent<MeshRenderer>().material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse"); ;
        cube.GetComponent<MeshRenderer>().material.color -= new Color32(255, 255, 255, 255);
    }
}
