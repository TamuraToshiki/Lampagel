//======================================================================
// Fire.cs
//======================================================================
// �J������
//
// 2022/03/21 author�F�����x ����J�n�@�Β�����
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // �Β��̎���
    float fLifeTime = 3.0f;

    // �_���[�W��^����Ԋu
    float fInterval = 0.5f;
    float fTime;

    // �U���T�[�N�����o�Ă���Β����o�鎞��
    float fAttackStart = 1.0f;

    bool bAttackStart = false;

    GameObject player;
    GameObject enemy;
    GameObject AttackCircle,TimeCircle;

    float fScale;

    public void SetPlayer(GameObject obj) { player = obj; }
    public void SetEnemy(GameObject obj) { enemy = obj; }
    public void SetCircle(GameObject obj) { AttackCircle = obj; }


    //----------------------------------
    // ������
    //----------------------------------
    private void Start()
    {
        // �C���^�[�o���Z�b�g
        fTime = fInterval;

        // �L����T�[�N������
        TimeCircle = Instantiate(AttackCircle, new Vector3(player.transform.position.x, 0.1f, player.transform.position.z), AttackCircle.transform.rotation);
        // �傫���̓[��
        TimeCircle.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);


        // �U���T�[�N������
        AttackCircle = Instantiate(AttackCircle, new Vector3(player.transform.position.x, 0.1f, player.transform.position.z), AttackCircle.transform.rotation);
        // �T�[�N���̓����x��������
        AttackCircle.GetComponent<SpriteRenderer>().color -= new Color32(0, 0, 0, 125);

        // �T�[�N���̑傫���g��
        fScale = AttackCircle.transform.localScale.x / (fAttackStart * 50.0f);
    }

    //----------------------------------
    // �X�V
    //----------------------------------
    void Update()
    {
        Destroy(gameObject, fLifeTime);
        Destroy(AttackCircle, fAttackStart);
        Destroy(TimeCircle, fAttackStart);

       // �����蔻��p�L���[�u��s������(�f�o�b�O�p)
        fAttackStart -= Time.deltaTime;
        if(fAttackStart < 0.0f && !bAttackStart)
        {
            gameObject.GetComponent<MeshRenderer>().material.color += new Color32(255, 255, 255, 122);
            bAttackStart = true;
        }
    }

    private void FixedUpdate()
    {
        if(TimeCircle != null)
            TimeCircle.transform.localScale = new Vector3(TimeCircle.transform.localScale.x + fScale, TimeCircle.transform.localScale.y + fScale, 1.0f);
    }

    //----------------------------------
    // �����蔻��
    //----------------------------------
    // �Β��ɓ������u�ԃ_���[�W
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player" && bAttackStart)
        {
             // �_���[�W����
             //player.GetComponent<StatusComponent>().HP -= enemy.GetComponent<StatusComponent>().Attack;

             Debug.Log("�_���[�W");

        }
    }

     // �Β��ɓ����葱���Ă���Ƃ�
    private void OnTriggerStay(Collider other)
    {
        // �v���C���[�ɓ��������� & �Β����o�Ă���Ƃ�
        if (other.tag == "Player" && bAttackStart)
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
