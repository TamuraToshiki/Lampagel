
//======================================================================
// Flamethrower.cs
//======================================================================
// �J������
//
// 2022/03/21 author�F�����x ����J�n�@�G�̉Ή�����
// 2022/03/28 author�F�|���@���}�@�v���C���[�ւ̃_���[�W����i�G���[�j
// 2022/04/21 author�F�����@�G�̍U���͂�EnemyData����Q�Ƃ���悤�ɕύX
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    // �Ή����˂̎���
    float fLifeTime = 3.0f;

    // �Ή����˂̒���
    float fDis;

    // �_���[�W��^����Ԋu
    float fInterval = 0.5f;
    float fTime;

    GameObject player;
    GameObject enemy;
    GameObject effect;

    public void SetPlayer(GameObject obj) { player = obj; }
    public void SetEnemy(GameObject obj) { enemy = obj; }

    public void SetDiss(float dis) { fDis = dis; }

    public void SetEffect(GameObject obj) { effect = obj; }

    //---------------------------
    // �X�V
    //---------------------------
    void Update()
    {

        // �ݒ莞�Ԍ�A�U���I��
        fLifeTime -= Time.deltaTime;
        if (fLifeTime <= 0.0f)
        {
            // �G�̍U���t���O�����낷�i�ړ��\��Ԃցj
            if (enemy.GetComponent<EnemyBase>() != null)
            {
                enemy.GetComponent<EnemyBase>().bAttack = false;
            }
            Destroy(effect);
            Destroy(gameObject);
        }

        // �G�����S�����Ƃ��A�ꏏ�ɏ�����
        if (enemy == null)
        {
            Destroy(effect);
            Destroy(gameObject);
        }

        // �G�������Ă���Ƃ�
        if (enemy != null)
        {
            // �p�x�A���W���Ǐ]����悤��
            transform.rotation = enemy.transform.rotation;
            transform.position = new Vector3(enemy.transform.position.x + transform.forward.x * fDis, transform.position.y, enemy.transform.position.z + transform.forward.z * fDis);

            // �G�t�F�N�g���Ǐ]����悤��
            effect.transform.rotation = transform.rotation;
            effect.transform.position = enemy.transform.position;
        }

    }

    //----------------------------------
    // �����蔻��
    //----------------------------------
    // �Ή����˂ɓ������u�ԃ_���[�W
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            // �_���[�W����
            player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<EnemyBase>().GetEnemyData.nAttack);
        }
    }

    // �Ή����˂ɓ����葱���Ă���Ƃ�
    private void OnTriggerStay(Collider other)
    {
        // �v���C���[�ɓ��������� & �Ή����˂��o�Ă���Ƃ�
        if (other.tag == "Player")
        {
            fTime -= Time.deltaTime;

            // �ݒ�C���^�[�o�����Ƀ_���[�W��^����(0.5�b)
            if (fTime < 0.0f)
            {
                // �_���[�W�����i�_���[�W�����Ȃ��j
                player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<EnemyBase>().GetEnemyData.nAttack / 10);

                fTime = fInterval;
            }

        }
    }
}
