//======================================================================
// Enemy_03.cs
//======================================================================
// �J������
//
// 2022/03/05 author�F�����x ����J�n�@
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_03 : MonoBehaviour
{
    // �����蔻��p�L���[�u
    GameObject cube;

    EnemyBase enemyBase;

    //-----------------------
    // ������
    //-----------------------
    void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
    }

    //-----------------------
    // �X�V
    //-----------------------
    private void HurtleAttack()
    {
        // �����蔻��p�L���[�u����
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // �L���[�u��\��
        cube.GetComponent<MeshRenderer>().enabled = false;

        // �T�C�Y�A���W�A�p�x�ݒ�
        cube.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        cube.transform.rotation = this.transform.rotation;
        cube.transform.position = transform.position;

        // �R���|�[�l���g����
        cube.AddComponent<Rush>();
        cube.GetComponent<Rush>().SetPlayer(enemyBase.GetComponent<EnemyBase>().GetPlayer);
        cube.GetComponent<Rush>().SetEnemy(this.gameObject);
        cube.GetComponent<BoxCollider>().isTrigger = true;
    }
}
