//======================================================================
// EnemyBase.cs
//======================================================================
// �J������
//
// 2022/03/05 author�F�����x ����J�n�@�G�̃x�[�X�����ǉ�
// 2022/03/11 author�F�����x �o�[�X�g�����ǉ�
// 2022/03/15 author�F�����x �X�e�[�^�X�����ύX
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

    // �v���C���[�������Ă��邩
    //private bool bFind = false;

    // �����_���ɓ�������
    private float nMoveTime = 2.0f; // ��
    private Vector3 vOldPos;

   // ������΂���Ă��瓮���o���b��
    private float fBurstTime = 2.0f;
    private Rigidbody rb;

    [SerializeField] private GameObject DamageObj;
    [Header("�^�[�Q�b�g�������鋗��")] [SerializeField, Range(1.0f, 50.0f)] private float fRadius = 5.0f;
    [Header("�^�[�Q�b�g������������")] [SerializeField, Range(1.0f, 50.0f)] private float fMissDis = 8.0f;
    [Header("�����_���ɓ�������")] [SerializeField, Range(1.0f, 100.0f)] private float fRandMove = 10.0f;
    [Header("�U�����J�n���鋗��")] [SerializeField, Range(0.0f, 50.0f)] private float fAttackDis = 3.0f;
    [Header("�U���p�x")] [SerializeField, Range(0.0f, 10.0f)] private float fAttackTime = 3.0f;
    private float fAttackCount;

    public void SetManager(EnemyManager obj) { manager = obj; }
    public void SetPlayer(GameObject obj) { player = obj; }
    public GameObject GetPlayer { get { return player; } }

    //----------------------------
    // ������
    //----------------------------
    void Start()
    {
        // �X�e�[�^�X������
        status = GetComponent<StatusComponent>();
        status.Level = 0;   // TODO:��XManager�Őݒ肷��??
        status.HP = status.HP + (status.Level * status.UpHP);
        status.Attack = status.Attack + (status.Level * status.UpAttack);
        status.Speed = status.Speed;

        // �i�r���b�V���ݒ�
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.speed = status.Speed;        

        // SpherCollider�ǉ��i�v���C���[�T���p�j
        //SpherCol = gameObject.AddComponent<SphereCollider>();
        //SpherCol.isTrigger = true;
        //SpherCol.radius = fRadius;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        //bFind = false;
        fAttackCount = fAttackTime;
    }

    //----------------------------
    // �X�V
    //----------------------------
    void Update()
    {
        Burst();
        Move();
        Death();
    }

    //----------------------------
    // ���S
    //----------------------------
    private void Death()
    {
        // HP0�ȉ��ŏ���
        if (status.HP <= 0)
        {
            // ���X�g����폜
            manager.NowEnemyList.Remove(gameObject);
            Destroy(this.gameObject);
        }
    }



    //----------------------------
    // �U��
    //----------------------------
    private void EnemyAttack()
    {
        // �������~�߂�
        myAgent.speed = 0.0f;   

        // �U���J�n������(��)
        if (IsAttack())
        {
            // �U�����[�V����
            animator.SetInteger("Parameter", (int)eAnimetion.eAttack);

            Debug.Log("�U��");
        }
    }

    //----------------------------
    // �U���J�n��
    //----------------------------
    private bool IsAttack()
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

    //----------------------------
    // �ړ�
    //----------------------------
    private void Move()
    {
        // �����Ă��邩
        if ((vOldPos.x == transform.position.x || vOldPos.z == transform.position.z))
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

        
        Vector3 vDiffPos = this.transform.position - player.transform.position;
        // ���̋������ꂽ�ꍇ�A������
        //if (vDiffPos.x > fMissDis || vDiffPos.z > fMissDis)
        //    bFind = false;

        // �v���C���[��ǂ�������
        myAgent.SetDestination(player.transform.position);

        // �G�Ƃ̋��������ȉ��Ȃ�U������
        if ((vDiffPos.x <= fAttackDis && vDiffPos.x >= -fAttackDis) && (vDiffPos.z <= fAttackDis && vDiffPos.z >= -fAttackDis))
        {
            EnemyAttack();
        }
        // �U���I���������o��
        else if (myAgent.speed == 0.0f)
        {
            // �X�s�[�h�̍Đݒ�
            myAgent.speed = status.Speed;
        }

        //if (bFind)
        //{
        //    myAgent.SetDestination(player.transform.position);

        //    // �G�Ƃ̋��������ȉ��Ȃ�U������
        //    if ((vDiffPos.x <= fAttackDis && vDiffPos.x >= -fAttackDis) && (vDiffPos.z <= fAttackDis && vDiffPos.z >= -fAttackDis))
        //    {
        //        EnemyAttack();
        //    }
        //    else if (myAgent.speed == 0.0f)
        //    {
        //        // �X�s�[�h�̍Đݒ�
        //        myAgent.speed = status.Speed;    
        //    }
        //}
        ////�@�ݒ�t���[�����ɁA�ړI�n�ύX
        //else if (!bFind)
        //{
        //    nMoveTime -= Time.deltaTime;
        //    if (nMoveTime < 0)
        //    {
        //        // �����_���ړ�
        //        myAgent.SetDestination(new Vector3(Random.Range(-fRandMove, fRandMove), 0, Random.Range(-fRandMove, fRandMove)));
        //        nMoveTime = 2.0f;�@// ��
        //    }
        //}

        vOldPos = this.gameObject.transform.position;
    }

    //----------------------------
    // �v���C���[�ǐ�
    //----------------------------
    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[���͈͂ɓ�������ǂ�
        // �v���C���[�Ƃ̏Փˎ��_���[�W
        if (other.CompareTag("Player"))
        {
            //bFind = true;

            // �_���[�W����
            status.HP -= 10;     // TODO:�����Ƀv���C���[�̍U���͂�����

            // �_���[�W�\�L
            ViewDamage(10);      // TODO:�����Ƀv���C���[�̍U���͂�����
        }
    }

    //----------------------------
    // �v���C���[�Ƃ̏Փˎ�
    //----------------------------
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            // �v���C���[�̐��ʂɉ���
            Vector3 vPush = player.transform.forward;
            transform.position += vPush;

            // �_���[�W����
            status.HP -= 10;     // TODO:�����Ƀv���C���[�̍U���͂�����

            // �_���[�W�\�L
            ViewDamage(10);      // TODO:�����Ƀv���C���[�̍U���͂�����
        }
    }

    //----------------------------
    // �o�[�X�g����������Ƃ�
    //----------------------------
    private void Burst()
    {
        // �������Z��ON�̎��i�o�[�X�g���ɕ������Z��ON�ɂȂ�j
        if(!rb.isKinematic)
        {
            // 2�b��ɁA�������ZOFF�ɂ���(��)
            fBurstTime -= Time.deltaTime;
            if(fBurstTime < 0.0)
            {
                fBurstTime = 2.0f;
                rb.isKinematic = true;
            }
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
        text.transform.position = new Vector3(transform.position.x,transform.position.y, transform.position.z + 1.0f) ;
    }
}
