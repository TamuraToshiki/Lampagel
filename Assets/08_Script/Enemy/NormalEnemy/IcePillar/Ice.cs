//======================================================================
// Ice.cs
//======================================================================
// �J������
//
// 2022/04/21 author�F�����x ����J�n�@�X������
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    // �X���̎���(IcePillar.cs�Őݒ�)
    public float fLifeTime { get; set; }

    // �U���T�[�N�����o�Ă���X�����o�鎞��
    float fAttackStart = 1.0f;

    // �X�����o�Ă��Ă��邩
    bool bAttackStart = false;

    // �G�t�F�N�g�֘A
    EnemyEffect enemyEffect;
    GameObject ObjEffect;

    public GameObject player { get; set; }
    public GameObject enemy { get; set; }
    GameObject AttackCircle, TimeCircle;

    // �T�[�N���g���
    float fScale;

    public void SetCircle(GameObject obj) { AttackCircle = obj; }


    //----------------------------------
    // ������
    //----------------------------------
    private void Start()
    {
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

        // �U�R�p
        enemyEffect = enemy.GetComponent<EnemyBase>().GetEffect;
    }

    //----------------------------------
    // �X�V
    //----------------------------------
    void Update()
    {
        // �T�[�N���������� & �܂��X�����o�Ă��Ȃ�
        if (AttackCircle == null && !bAttackStart)
        {
            bAttackStart = true;

            gameObject.GetComponent<CapsuleCollider>().isTrigger = false;
            gameObject.GetComponent<MeshRenderer>().enabled = true;

            // �G�t�F�N�g����
            //ObjEffect = enemyEffect.CreateEffect(EnemyEffect.eEffect.eFirePiller, gameObject, fLifeTime - fAttackStart);
        }

        Destroy(gameObject, fLifeTime);

        // ��莞�Ԍ�A�\���T�[�N������
        Destroy(AttackCircle, fAttackStart);
        Destroy(TimeCircle, fAttackStart);
    }

    private void FixedUpdate()
    {
        // �T�[�N���T�C�Y�g��
        if (TimeCircle != null)
            TimeCircle.transform.localScale = new Vector3(TimeCircle.transform.localScale.x + fScale, TimeCircle.transform.localScale.y + fScale, 1.0f);
    }




    //----------------------------------
    // �����蔻��
    //----------------------------------
    // �X���ɓ������u�ԃ_���[�W
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player" && bAttackStart)
        {
            // �_���[�W����
            //player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<EnemyBase>().GetEnemyData.nAttack);
        }
    }
}
