
//======================================================================
// Enemy_05.cs
//======================================================================
// �J������
//
// 2022/03/28 author�F�|���@���} �G�t�F�N�g�����g�ݍ���
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

    // �G�t�F�N�g�֘A
    EnemyEffect enemyEffect;
    GameObject objEffect;

    [Header("�Ή����˂̋���")][SerializeField]float fDistance = 3.0f;

    //------------------------
    // ������
    //------------------------
    private void Start()
    {
        enemyBase = GetComponent<EnemyBase>();

        // �G�t�F�N�g�擾�iEnemyBase.cs���j
        enemyEffect = enemyBase.GetEffect;
    }

    //----------------------------------------------
    // �Ή����ˏ���(�A�j���[�V�����ɍ��킹�ČĂяo��)
    //----------------------------------------------
    private void AttackEnemy05()
    {
        // �����蔻��p�̃L���[�u����
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // �T�C�Y�A���W�A�p�x�ݒ�
        cube.transform.localScale = new Vector3(1.0f, 1.0f, 5.0f);
        cube.transform.rotation = this.transform.rotation;
        cube.transform.position = new Vector3(transform.position.x + transform.forward.x * fDistance, transform.position.y, transform.position.z + transform.forward.z * fDistance);

        // �Ή����˂̃R���|�[�l���g��ǉ�
        cube.AddComponent<Flamethrower>();

        // �Ή����˃R���|�[�l���g�ɏ��Z�b�g
        cube.GetComponent<Flamethrower>().SetEnemy(gameObject);
        cube.GetComponent<Flamethrower>().SetPlayer(enemyBase.GetPlayer);
        cube.GetComponent<Flamethrower>().SetDiss(fDistance);

        // �G�t�F�N�g����
        objEffect = enemyEffect.CreateEffect(EnemyEffect.eEffect.eFlame, gameObject);
        cube.GetComponent<Flamethrower>().SetEffect(objEffect);

        cube.GetComponent<BoxCollider>().isTrigger = true;
        enemyBase.SetAttack(true);

        // �����蔻��p�L���[�u�𓧖���(�f�o�b�O�p)
        cube.GetComponent<MeshRenderer>().enabled = false; // 3/28 MeshRenderer���I�t
        //cube.GetComponent<MeshRenderer>().material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
        //cube.GetComponent<MeshRenderer>().material.color -= new Color32(255, 255, 255, 255);
    }
}
