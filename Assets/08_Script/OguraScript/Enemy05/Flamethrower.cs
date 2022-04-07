
//======================================================================
// Flamethrower.cs
//======================================================================
// �J������
//
// 2022/03/21 author�F�����x ����J�n�@�G�̉Ή�����
// 2022/03/28 author�F�|���@���}�@�v���C���[�ւ̃_���[�W����i�G���[�j
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
        if(fLifeTime <= 0.0f)
        {
<<<<<<< HEAD
<<<<<<< HEAD:Assets/Script/OguraScript/Enemy05/Flamethrower.cs
            //enemy.GetComponent<EnemyBase>().SetAttack(false);
=======
=======
            // �G�̍U���t���O�����낷�i�ړ��\��Ԃցj
>>>>>>> e2853f8ad6986fc67b6af3dfd7a583e04154f030
            if(enemy.GetComponent<EnemyBase>() != null)
            {
                enemy.GetComponent<EnemyBase>().SetAttack(false);
            }
<<<<<<< HEAD
>>>>>>> d2f65eada7be6604d61b693afd0e28d3b8accd2c:Assets/08_Script/OguraScript/Enemy05/Flamethrower.cs
=======
            Destroy(effect);
>>>>>>> e2853f8ad6986fc67b6af3dfd7a583e04154f030
            Destroy(gameObject);        
        }

        // �G�����S�����Ƃ��A�ꏏ�ɏ�����
        if (enemy == null)
        {
            Destroy(effect);
            Destroy(gameObject);
        }

        // �G�������Ă���Ƃ�
        if(enemy != null)
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
            player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<StatusComponent>().Attack);
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
                player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<StatusComponent>().Attack / 5);

                fTime = fInterval;
            }

        }
    }
}
