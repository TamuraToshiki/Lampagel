//======================================================================
// Enemy_02.cs
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

public class Enemy_02 : MonoBehaviour
{
    GameObject spher;
    EnemyBase enemyBase;

    // �G�t�F�N�g�֘A
    EnemyEffect enemyEffect;
    GameObject objEffect;

    [Header("�΋����x")][SerializeField] float fSpeed = 5.0f;

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
    // �΋�����(�A�j���[�V�����ɍ��킹�ČĂяo��)
    //----------------------------------------------
    private void AttackEnemy02()
    {
        // �e�𐶐�
        spher = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        // �e�̃T�C�Y�A���W�A�p�x�ݒ�
        spher.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        spher.transform.rotation = this.transform.rotation;
        spher.transform.position = new Vector3(transform.position.x + transform.forward.x * 0.5f, transform.position.y, transform.position.z + transform.forward.z * 0.5f);

        // �e�ɃR���|�[�l���g�ǉ�
        spher.AddComponent<Bullet>();

        // �e�̃R���|�[�l���g�ɏ����Z�b�g
        Bullet bullet = spher.GetComponent<Bullet>();
        bullet.Speed = fSpeed;
        bullet.SetPlayer(enemyBase.GetComponent<EnemyBase>().GetPlayer);
        bullet.SetEnemy(gameObject);

        // �G�t�F�N�g����
        objEffect = enemyEffect.CreateEffect(EnemyEffect.eEffect.eFireBall, gameObject);
        bullet.SetEffect(objEffect);

        // ���̑��R���|�[�l���g����
        spher.AddComponent<Rigidbody>();
        spher.GetComponent<Rigidbody>().useGravity = false;
        spher.GetComponent<Rigidbody>().isKinematic = true;
        spher.GetComponent<SphereCollider>().isTrigger = true;
    }
}