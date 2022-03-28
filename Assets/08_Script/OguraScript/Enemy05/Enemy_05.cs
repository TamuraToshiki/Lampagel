<<<<<<< HEAD:Assets/08_Script/OguraScript/Enemy05/Enemy_05.cs
//======================================================================
// Enemy_05.cs
//======================================================================
// �J������
//
// 2022/03/28 author�F�|���@���} �G�t�F�N�g�����g�ݍ���
=======

//======================================================================
// Flamethrower.cs
//======================================================================
// �J������
//
// 2022/03/21 author�F�����x ����J�n�@�G�̉Ή�����
>>>>>>> 8709684d4e54354a91684949987394adf606b0ff:Assets/Script/OguraScript/Enemy05/Enemy_05.cs
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_05 : MonoBehaviour
{
    GameObject cube;
    EnemyBase enemyBase;

<<<<<<< HEAD:Assets/08_Script/OguraScript/Enemy05/Enemy_05.cs
    //*���}* �G�t�F�N�g�X�N���v�g
    [SerializeField] AID_PlayerEffect effect;

=======
    // �Ή����ˋ���
>>>>>>> 8709684d4e54354a91684949987394adf606b0ff:Assets/Script/OguraScript/Enemy05/Enemy_05.cs
    float fDistance = 3.0f;

    private void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
        effect = GameObject.FindWithTag("AID_Effect").GetComponent<AID_PlayerEffect>();
    }


    private void AttackEnemy05()
    {
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        //*���}*
        effect.StartEffect(5, this.gameObject, 2.0f);


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
        cube.GetComponent<MeshRenderer>().enabled = false; // 3/28 MeshRenderer���I�t
        cube.GetComponent<MeshRenderer>().material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
        cube.GetComponent<MeshRenderer>().material.color -= new Color32(255, 255, 255, 255);
    }
}
