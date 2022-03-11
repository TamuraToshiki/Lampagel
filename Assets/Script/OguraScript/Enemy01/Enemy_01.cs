//======================================================================
// Enemy_01.cs
//======================================================================
// �J������
//
// 2022/03/05 ����J�n �G�̂Ђ������U��
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class Enemy_01 : MonoBehaviour
{
    GameObject cube;
    EnemyBase enemyBase;

    private void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
    }

    private void AttackEnemy01()
    {
        // �����蔻��p�L���[�u����
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        cube.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);  // �U���͈�
        cube.transform.rotation = this.transform.rotation;
        cube.transform.position = new Vector3(transform.position.x + transform.forward.x * 1.5f, transform.position.y, transform.position.z + transform.forward.z * 1.5f);
        cube.AddComponent<Scratch>();
        cube.GetComponent<Scratch>().SetPlayer(enemyBase.GetComponent<EnemyBase>().GetPlayer);
        cube.GetComponent<Scratch>().SetEnemy(this.gameObject);
        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Rigidbody>().useGravity = false;
        cube.GetComponent<Rigidbody>().isKinematic = true;
    }
}
