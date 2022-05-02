//======================================================================
// SkullDragon.cs
//======================================================================
// �J������
//
// 2022/03/05 author�F�����x ����J�n�@�G�̉������U������
// 2022/03/30 author�F�����x �G�t�F�N�g�����ǉ�
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class SkullHead : MonoBehaviour
{
    // �����蔻��p�X�t�B�A
    GameObject spher;

    EnemyBase enemyBase;

    // �G�t�F�N�g�֘A
    EnemyEffect enemyEffect;
    GameObject objEffect;

    // �΋��̑��x
    [Header("�΋����x")]
    [SerializeField] float fSpeed = 5.0f;

    //------------------------
    // ������
    //------------------------
    private void Start()
    {
        // �G�t�F�N�g�擾
        enemyEffect = GetComponent<EnemyEffectBase>().GetEffect;

        // �G�l�~�[�x�[�X���擾
        enemyBase = this.GetComponent<EnemyBase>();
    }

    //----------------------------------------------
    // �΋�����(�A�j���[�V�����ɍ��킹�ČĂяo��)
    //----------------------------------------------
    private void SkullHeadAttack()
    {
        // �e�𐶐�
        spher = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        // �e�̃T�C�Y�A���W�A�p�x�ݒ�
        spher.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        spher.transform.rotation = this.transform.rotation;
        spher.transform.position = new Vector3(transform.position.x + transform.forward.x, transform.position.y, transform.position.z + transform.forward.z);

        // �e�ɃR���|�[�l���g�ǉ�
        spher.AddComponent<Bullet>();

        // �e�̃R���|�[�l���g�ɏ����Z�b�g
        Bullet bullet = spher.GetComponent<Bullet>();
        bullet.Speed = fSpeed;
        bullet.SetPlayer(enemyBase.GetComponent<EnemyBase>().player);
        bullet.SetEnemy(gameObject);

        // �G�t�F�N�g����
        objEffect = enemyEffect.CreateEffect(EnemyEffect.eEffect.eFireBall, gameObject);
        bullet.SetEffect(objEffect);

        // ���蔲����悤��
        spher.GetComponent<SphereCollider>().isTrigger = true;
    }
}
