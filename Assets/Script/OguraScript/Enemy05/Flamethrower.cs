
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

public class Flamethrower : MonoBehaviour
{
    // �Β��̎���
    float fLifeTime = 2.0f;

    // �_���[�W��^����Ԋu
    float fInterval = 0.5f;
    float fTime;

    GameObject player;
    GameObject enemy;

    public void SetPlayer(GameObject obj) { player = obj; }
    public void SetEnemy(GameObject obj) { enemy = obj; }


    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material.color += new Color32(255, 0, 0, 122);

 

        
    }


    void Update()
    {
        // �ݒ莞�Ԍ�A�U���I��
        fLifeTime -= Time.deltaTime;
        if(fLifeTime <= 0.0f)
        {
            enemy.GetComponent<EnemyBase>().SetAttack(false);
            Destroy(gameObject);        
        }

        // �G�����S�����Ƃ��A�ꏏ�ɏ�����
        if (enemy == null)
        {
            Destroy(gameObject);
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
            //player.GetComponent<StatusComponent>().HP -= enemy.GetComponent<StatusComponent>().Attack;

            Debug.Log("�_���[�W");

        }
    }

    // �Ή����˂ɓ����葱���Ă���Ƃ�
    private void OnTriggerStay(Collider other)
    {
        // �v���C���[�ɓ��������� & �Β����o�Ă���Ƃ�
        if (other.tag == "Player")
        {
            fTime -= Time.deltaTime;

            // �ݒ�C���^�[�o�����Ƀ_���[�W��^����(0.5�b)
            if (fTime < 0.0f)
            {
                // �_���[�W����
                //player.GetComponent<StatusComponent>().HP -= enemy.GetComponent<StatusComponent>().Attack;

                Debug.Log("�_���[�W");
                fTime = fInterval;
            }

        }
    }
}
