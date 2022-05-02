//======================================================================
// Fire.cs
//======================================================================
// �J������
//
// 2022/03/21 author�F�����x ����J�n�@�Β�����
// 2022/03/28 author�F�|���@���}�@�v���C���[�ւ̃_���[�W����
// 2022/04/21 author�F�����@�G�̍U���͂�EnemyData����Q�Ƃ���悤�ɕύX
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
    
    // �Β����o�Ă��Ă��邩
    bool bAttackStart = false;

    // �G�t�F�N�g�֘A
    EnemyEffect enemyEffect;
    GameObject ObjEffect;

    public GameObject player { get; set; }
    public GameObject enemy { get; set; }
    public EnemyEffect effect { get; set; }

    GameObject AttackCircle,TimeCircle;

    float fScale;

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

        // �T�[�N���̑傫���g��
        fScale = AttackCircle.transform.localScale.x / (fAttackStart * 50.0f);

        // �U���T�[�N������
        AttackCircle = Instantiate(AttackCircle, new Vector3(player.transform.position.x, 0.1f, player.transform.position.z), AttackCircle.transform.rotation);
        
        // �T�[�N���̓����x��������
        AttackCircle.GetComponent<SpriteRenderer>().color -= new Color32(0, 0, 0, 125);
    }

    //----------------------------------
    // �X�V
    //----------------------------------
    void Update()
    {
       // �T�[�N���������� & �܂��Β����o�Ă��Ȃ�
        if(AttackCircle == null && !bAttackStart)
        {
            bAttackStart = true;

            // �G�t�F�N�g����
            ObjEffect = effect.CreateEffect(EnemyEffect.eEffect.eFirePiller, gameObject, fLifeTime - fAttackStart);
        }

        Destroy(gameObject, fLifeTime);

        // ��莞�Ԍ�A�\���T�[�N������
        Destroy(AttackCircle, fAttackStart);
        Destroy(TimeCircle, fAttackStart);
    }

    private void FixedUpdate()
    {
        // �T�[�N���T�C�Y�g��
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
            player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<EnemyBase>().GetEnemyData.nAttack);
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
                player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<EnemyBase>().GetEnemyData.nAttack / 10);

                fTime = fInterval;
            }

        }
    }
}
