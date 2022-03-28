//======================================================================
// BossBase.cs
//======================================================================
// �J������
//
// 2022/03/27 author�F�����x ����J�n�@�{�X�x�[�X����
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]

public class Boss01 : MonoBehaviour
{
    enum eAttackType
    { 
        eFire = 0,
        eFlame,
        
        eAttackMax
    }

    private BossAttack bossAttack;
    private StatusComponent status;
    private GameObject player;
    private EnemyManager manager;
    private NavMeshAgent myAgent;
    private Animator animator;
    private Rigidbody rb;


    // �O�t���[���̍��W
    private Vector3 vOldPos;

    // �U������
    private bool bAttack = false;

    // �U���͈͂ɓ����Ă���A��x�ڂ̍U����
    private bool bFirstAttack = false;

    // �_���[�WUI
    [SerializeField] private GameObject DamageObj;

    // ���ʉ�
    [Header("���S�����ʉ�")] [SerializeField] private AudioClip DeathSE;

    // �U���֘A
    [Header("�U�����J�n���鋗��")] [SerializeField, Range(0.0f, 50.0f)] private float fAttackDis = 3.0f;
    [Header("�U���p�x")] [SerializeField, Range(0.0f, 10.0f)] private float fAttackTime = 3.0f;
    private float fAttackCount;
    int nAttackType;

    // �����_���œ�������
    [SerializeField] private float fRangeDiff = 10.0f;

    // �ړI�n�ύX���鎞��
    [SerializeField] private float fRangeTime = 2.0f;
    private float fRangeCount;

    // ���̖ړI�n�p
    Vector3 vRandomPos;


    public void SetAttack(bool flag) { bAttack = flag; }


    //-------------------------
    // ������
    //-------------------------
    void Start()
    {
        player = GameObject.Find("Player");

        // �X�e�[�^�X������
        status = GetComponent<StatusComponent>();
        status.Level = 0;
        status.HP = status.HP + (status.Level * status.UpHP);
        status.Attack = status.Attack + (status.Level * status.UpAttack);
        status.Speed = status.Speed;

        // �i�r���b�V���������i�X�e�[�^�X����X�s�[�h���擾�j
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.speed = status.Speed;

        // ���̑�������
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        bossAttack = GetComponent<BossAttack>();
        bossAttack.SetPlayer(player);

        fAttackCount = fAttackTime;
        fRangeCount = fRangeTime;
    }

    //-------------------------
    // �X�V
    //-------------------------
    void Update()
    {
        Death();
        Move();
        Attack();
    }

    //----------------------------
    // ���S
    //----------------------------
    private void Death()
    {
        // HP0�ȉ��ŏ���
        if (status.HP <= 0)
        {
            // ���ʉ��Đ�
            AudioSource.PlayClipAtPoint(DeathSE, transform.position);

            // ���X�g����폜
            manager.NowEnemyList.Remove(gameObject);
            Destroy(this.gameObject);
        }
    }

    //----------------------------
    // �ړ�
    //----------------------------
    void Move()
    {
        fRangeCount -= Time.deltaTime;
        if(fRangeCount < 0.0f)
        {
            // ���̖ړI�n���v�Z
            vRandomPos = new Vector3(Random.Range(-fRangeDiff, fRangeDiff), 0, Random.Range(-fRangeDiff, fRangeDiff));
            fRangeCount = fRangeTime;
        }


        if(!bAttack)
        {
            // �ړI�n�ֈړ�
            myAgent.SetDestination(vRandomPos);
        }
        else if(bAttack)
        {
            myAgent.speed = 0;
            myAgent.velocity = Vector3.zero;
        }


        //// �����Ă��邩
        //if ((vOldPos.x == transform.position.x || vOldPos.z == transform.position.z))
        //{
        //    // �ҋ@���[�V����
        //    //animator.SetInteger("Parameter", (int)eAnimetion.eWait);
        //}
        //else
        //{
        //    // �ړ����[�V����
        //    //animator.SetInteger("Parameter", (int)eAnimetion.eMove);
        //    bFirstAttack = false;
        //}

        vOldPos = gameObject.transform.position;
    }

    //----------------------------
    // �U��
    //----------------------------
    void Attack()
    {
        // ��莞�Ԗ��ɍU��
        fAttackCount -= Time.deltaTime;
        if(fAttackCount < 0.0f)
        {
            // �U����ނ������_����
            nAttackType = Random.Range(0, (int)eAttackType.eAttackMax) % (int)eAttackType.eAttackMax;

            // �U���̕���
            switch (nAttackType)
            {
                // �Β�����
                case (int)eAttackType.eFire:
                    bossAttack.CreateFire(player.transform.position);
                    break;

                // �Ή����ː���
                case (int)eAttackType.eFlame:
                    bossAttack.CreateFlame(player.transform.position);
                    break;

                default:
                    break;
            }

            // �^�C�}�[������
            fAttackCount = fAttackTime;
        }
    }

    //----------------------------
    // �v���C���[�Ƃ̐ڐG��
    //----------------------------
    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[�Ƃ̏Փˎ��_���[�W
        if (other.CompareTag("Player"))
        {
            // �_���[�W����
            status.HP -= 10;     // TODO:�����Ƀv���C���[�̍U���͂�����

            // �_���[�W�\�L
            ViewDamage(10);      // TODO:�����Ƀv���C���[�̍U���͂�����
        }
    }

    //----------------------------
    // �_���[�W�\�L
    //----------------------------
    private void ViewDamage(int damage)
    {
        // �e�L�X�g�̐���
        GameObject text = Instantiate(DamageObj);
        text.GetComponent<TextMesh>().text = damage.ToString();

        // �������炵���ʒu�ɐ���(z + 1.0f)
        text.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.0f);
    }
}
