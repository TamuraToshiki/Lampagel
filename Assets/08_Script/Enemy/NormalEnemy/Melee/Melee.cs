
//======================================================================
// Melee.cs
//======================================================================
// �J������
//
// 2022/03/05 author�F�����x ����J�n�@�G�̂Ђ������U������
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class Melee : MonoBehaviour
{
    // �����蔻��L���[�u
    GameObject cube;

    EnemyBase enemyBase;
    BossBase bossBase;

    // �G�t�F�N�g�֘A
    EnemyEffect enemyEffect;
    GameObject objEffect;

    // �����蔻��T�C�Y
    private float fHitSize;

    // �����̓{�X�H
    public bool bImBoss = false;

    //------------------------
    // ������
    //------------------------
    private void Start()
    {
        // �G�t�F�N�g�擾
        enemyEffect = GetComponent<EnemyEffectBase>().GetEffect;

        
        enemyBase = this.GetComponent<EnemyBase>();

        // �����蔻��̃T�C�Y��G�̑傫���̔����ɂ���
        fHitSize = gameObject.transform.localScale.x / 2;
    }

    //----------------------------------------------
    // �Ђ���������(�A�j���[�V�����ɍ��킹�ČĂяo��)
    //----------------------------------------------
    private void MeleeAttack()
    {
        // �����蔻��p�L���[�u����
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // �L���[�u���\����
        cube.GetComponent<MeshRenderer>().enabled = false;

        // �L���[�u�̃T�C�Y�A��]�A�ʒu��ݒ�
        cube.transform.localScale = new Vector3(fHitSize, 1.0f, fHitSize);
        cube.transform.rotation = this.transform.rotation;
        cube.transform.position = new Vector3(transform.position.x + transform.forward.x * 1.5f, transform.position.y, transform.position.z + transform.forward.z * 1.5f);
        
        // Scratch�R���|�[�l���g�ǉ�
        cube.AddComponent<Scratch>();

        // �����Z�b�g �i�{�X�ł��邩�ǂ����j
        cube.GetComponent<Scratch>().SetPlayer(enemyBase.player);

        cube.GetComponent<Scratch>().SetEnemy(this.gameObject);

        // ���̑��R���|�[�l���g����
        cube.GetComponent<BoxCollider>().isTrigger = true;

        // �G�t�F�N�g����
        objEffect = enemyEffect.CreateEffect(EnemyEffect.eEffect.eScratch, gameObject,1.5f);
    }
}
