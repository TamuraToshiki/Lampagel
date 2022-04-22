//======================================================================
// Hurtle.cs
//======================================================================
// �J������
//
// 2022/03/05 author�F�����x ����J�n�@
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtle : MonoBehaviour
{
    GameObject spher;
    EnemyBase enemyBase;

    void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
    }

    private void HurtleAttack()
    {
        // �����蔻��p�X�t�B�A����
        spher = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // �T�C�Y�A���W�A�p�x�ݒ�
        spher.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        spher.transform.rotation = this.transform.rotation;
        spher.transform.position = transform.position;

        // �R���|�[�l���g����
        spher.AddComponent<Rush>();
        spher.GetComponent<Rush>().SetPlayer(enemyBase.GetComponent<EnemyBase>().player);
        spher.GetComponent<Rush>().SetEnemy(this.gameObject);
        spher.GetComponent<BoxCollider>().isTrigger = true;
            
        spher.AddComponent<Rigidbody>();
        spher.GetComponent<Rigidbody>().useGravity = false;
        spher.GetComponent<Rigidbody>().isKinematic = true;

        spher.GetComponent<MeshRenderer>().enabled = false;
    }
}