//======================================================================
// EnemyBase.cs
//======================================================================
// �J������
//
// 2022/03/05 ����J�n �G�̃x�[�X�����i�U���A�ړ��A���S�j
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]

public class EnemyBase : MonoBehaviour
{

    // �A�j���[�V�����̎��
    enum eAnimetion
    {
        eWait = 0,
        eMove,
        eAttack,
    }

    private StatusComponent status;
    private GameObject player;
    private EnemyManager manager;
    private NavMeshAgent myAgent;
    private SphereCollider SpherCol;
    private Animator animator;


    private bool bFind = false;
    private bool bAttack = false;
    private float nMoveTime = 2.0f; // ��
    private Vector3 vOldPos;


    // ��{�X�e�[�^�X
    [SerializeField] private int HP = 20;
    [SerializeField] private int Attack = 10;
    [SerializeField] private int Speed = 1;

    [Header("���x���A�b�v���̏オ�蕝")] [SerializeField, Range(1.0f, 10.0f)] private int nUpHP = 1;
    [SerializeField, Range(1.0f, 10.0f)] private int nUpAttack = 1;

    [Header("�^�[�Q�b�g�������鋗��")] [SerializeField, Range(1.0f, 50.0f)] private float fRadius = 5.0f;
    [Header("�^�[�Q�b�g������������")] [SerializeField, Range(1.0f, 50.0f)] private float fMissDis = 8.0f;
    [Header("�����_���ɓ�������")] [SerializeField, Range(1.0f, 100.0f)] private float fRandMove = 10.0f;
    [Header("�U�����J�n���鋗��")] [SerializeField, Range(0.0f, 50.0f)] private float fAttackDis = 3.0f;
    [Header("�U���p�x")] [SerializeField, Range(0.0f, 10.0f)] private float fAttackTime = 3.0f;
    private float fAttackCount;

    private Rigidbody rb;
    private float rbTime = 2.0f;

    public void SetManager(EnemyManager obj) { manager = obj; }
    public void SetPlayer(GameObject obj) { player = obj; }

    public GameObject GetPlayer { get { return player; } }

    void Start()
    {
        // �X�e�[�^�X������
        status = GetComponent<StatusComponent>();
        status.Level = 1;   // TODO:��XManager�Őݒ肷��
        status.HP = HP + (status.Level * nUpHP);
        status.Attack = Attack + (status.Level * nUpAttack);
        status.Speed = Speed;

        // �i�r���b�V���ݒ�
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.speed = status.Speed;        

        // SpherCollider�ǉ��i�v���C���[�T���p�j
        SpherCol = gameObject.AddComponent<SphereCollider>();
        SpherCol.isTrigger = true;
        SpherCol.radius = fRadius;

        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody>();

        bFind = false;
        fAttackCount = fAttackTime;
    }


    void Update()
    {
        Move();
        Death();
        Burst();
    }

    // ���S����
    private void Death()
    {
        if (status.HP <= 0)
        {
            manager.NowEnemyList.Remove(gameObject);
            Destroy(this.gameObject);
        }
    }




    //-------------------
    // �U���J�n
    //-------------------
    private void EnemyAttack()
    {
        myAgent.speed = 0.0f;   

        // �U���J�n(��)
        bAttack = StartAttack();
        if (bAttack)
        {
            // �U�����[�V����
            animator.SetInteger("Parameter", (int)eAnimetion.eAttack);  
        }
    }

    //-------------------
    // �ړ�
    //-------------------
    private void Move()
    {
        // �����Ă��邩
        if (vOldPos.x == transform.position.x || vOldPos.z == transform.position.z)
        {
            // �ҋ@���[�V����
            animator.SetInteger("Parameter", (int)eAnimetion.eWait);
        }
        else
        {
            // �ړ����[�V����
            animator.SetInteger("Parameter", (int)eAnimetion.eMove);
        }

        // ���̏ꏊ���v�Z
        Vector3 nextPoint = myAgent.steeringTarget;
        Vector3 targetDir = nextPoint - transform.position;

        // ��]
        Quaternion targetRotation = Quaternion.LookRotation(targetDir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 120f * Time.deltaTime);

        // ���̋������ꂽ�ꍇ�A������
        Vector3 vDiffPos = this.transform.position - player.transform.position;
        if (vDiffPos.x > fMissDis || vDiffPos.z > fMissDis)
            bFind = false;

        // �͈͓��Ƀv���C���[��������ǂ�������
        if (bFind)
        {
            myAgent.SetDestination(player.transform.position);

            // �G�Ƃ̋��������ȉ��Ȃ�U������
            if ((vDiffPos.x <= fAttackDis && vDiffPos.x >= -fAttackDis) && (vDiffPos.z <= fAttackDis && vDiffPos.z >= -fAttackDis))
            {
                // �U���J�n
                EnemyAttack();
            }
            else if (myAgent.speed == 0.0f)
            {
                // �X�s�[�h�̍Đݒ�
                myAgent.speed = status.Speed;
            }
        }
        //�@�ݒ�t���[�����ɁA�ړI�n�ύX
        else if (!bFind)
        {
            nMoveTime -= Time.deltaTime;
            if (nMoveTime < 0)
            {
                // �����_���ړ�
                myAgent.SetDestination(new Vector3(Random.Range(-fRandMove, fRandMove), 0, Random.Range(-fRandMove, fRandMove)));
                nMoveTime = 2.0f;�@// TODO:�G�̐i�H�I���J�E���g�i���j
            }
        }

        vOldPos = this.gameObject.transform.position;
    }

    //-------------------
    // �U��
    //-------------------
    private bool StartAttack()
    {
        fAttackCount -= Time.deltaTime;

        // �U���J�n
        if (fAttackCount < 0.0f)
        {
            fAttackCount = fAttackTime;
            return true;
        }
        return false;
    }

    //------------------------------
    // �͈͂Ƀv���C���[����������
    //-----------------------------
    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[���͈͂ɓ�������ǂ�
        if (other.CompareTag("Player"))
        {
            bFind = true;
        }
    }

    //------------------------------
    // �v���C���[�ɓ���������
    //-----------------------------
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            // �v���C���[�̐��ʂɉ���
            Vector3 vPush = player.transform.forward;
            transform.position += vPush;

            // �_���[�W����
            status.HP -= 10;     // TODO:�����Ƀv���C���[�̍U���͂�����,���L�̒ʂ�ɂȂ�͂��i�����j
            //status.HP -= player.GetComponent<StatusComponent>().Attack;
        }
    }

    //------------------------------
    // �o�[�X�g���ꂽ�Ƃ�
    //-----------------------------
    private void Burst()
    {
        // �������ZON�̎��i�o�[�X�g���A�v���C���[�̃X�N���v�g�ɂĕ������Z��ON�ɂ����j
        if (!rb.isKinematic)
        {
            // 2�b��A�������Z��OFF�ɂ���
            rbTime -= Time.deltaTime;
            if (rbTime < 0.0f)
            {
                rbTime = 2.0f;
                rb.isKinematic = true;
            }
        }


    }
}
