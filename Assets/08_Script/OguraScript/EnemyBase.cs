//======================================================================
// EnemyBase.cs
//======================================================================
// �J������
//
// 2022/03/05 author�F�����x ����J�n�@�G�̃x�[�X�����ǉ�
// 2022/03/11 author�F�����x �o�[�X�g�����ǉ�
// 2022/03/15 author�F�����x �X�e�[�^�X�����ύX
// 2022/03/28 auther�F�|���@���}�@�o���l�@�\�ǉ�
// 2022/03/24 author�F�����x ���ʉ������̒ǉ�
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
    private bool bFind = false;

    // �U������
    private bool bAttack = false;

    // �����_���ɓ�������
    private float nMoveTime = 2.0f; // ��
    private Vector3 vOldPos;

   // ������΂���Ă��瓮���o���b��
    private float fBurstTime = 2.0f;
    private Rigidbody rb;

<<<<<<< HEAD:Assets/Script/OguraScript/EnemyBase.cs
    [SerializeField] private GameObject DamageObj;
    [Header("�^�[�Q�b�g�������鋗��")] [SerializeField, Range(1.0f, 50.0f)] private float fRadius = 5.0f;
    [Header("�^�[�Q�b�g������������")] [SerializeField, Range(1.0f, 50.0f)] private float fMissDis = 8.0f;
    [Header("�����_���ɓ�������")] [SerializeField, Range(1.0f, 100.0f)] private float fRandMove = 10.0f;
=======
    // �U������
    private bool bAttack = false;

    // �U���͈͂ɓ����Ă���A��x�ڂ̍U����
    private bool bFirstAttack = false;
   
    // �_���[�WUI
    [SerializeField] private GameObject DamageObj;

    // ���ʉ�
    [Header("���S�����ʉ�")] [SerializeField] private AudioClip DeathSE;

>>>>>>> d2f65eada7be6604d61b693afd0e28d3b8accd2c:Assets/08_Script/OguraScript/EnemyBase.cs
    [Header("�U�����J�n���鋗��")] [SerializeField, Range(0.0f, 50.0f)] private float fAttackDis = 3.0f;
    [Header("�U���p�x")] [SerializeField, Range(0.0f, 10.0f)] private float fAttackTime = 3.0f;
    private float fAttackCount;  


    // �Q�b�^�[�A�Z�b�^�[
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
        status.HP = status.HP + (status.Level * status.UpHP);
        status.Attack = status.Attack + (status.Level * status.UpAttack);
        status.Speed = status.Speed;

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
        if (status.HP <= 0)
        {
<<<<<<< HEAD:Assets/Script/OguraScript/EnemyBase.cs
=======
            //*���}*
            player.GetComponent<PlayerExp>().AddExp(10);
            

            // ���ʉ��Đ�
            AudioSource.PlayClipAtPoint(DeathSE,transform.position);


            // ���X�g����폜
>>>>>>> d2f65eada7be6604d61b693afd0e28d3b8accd2c:Assets/08_Script/OguraScript/EnemyBase.cs
            manager.NowEnemyList.Remove(gameObject);
            Destroy(this.gameObject);
        }
    }

    //----------------------------
    // �U���J�n
    //----------------------------
    private bool StartAttack()
    {
<<<<<<< HEAD:Assets/Script/OguraScript/EnemyBase.cs
        fAttackCount -= Time.deltaTime;

        // �U���J�n
        if (fAttackCount < 0.0f)
        {
            fAttackCount = fAttackTime;
            return true;
=======
        // �������~�߂�
        myAgent.speed = 0.0f;
        myAgent.velocity = Vector3.zero;

        // �U���J�n������ or �U���͈͂ɓ����Ă���A�ŏ��̍U���̎�
        if (IsAttack() || !bFirstAttack)
        {
            // �U�����[�V����
            animator.SetInteger("Parameter", (int)eAnimetion.eAttack);

            bFirstAttack = true;
>>>>>>> d2f65eada7be6604d61b693afd0e28d3b8accd2c:Assets/08_Script/OguraScript/EnemyBase.cs
        }
        return false;
    }

    //----------------------------
    // �U��
    //----------------------------
    private void EnemyAttack()
    {
        myAgent.speed = 0.0f;   

        // �U���J�n(��)
        bAttack = StartAttack();
        if (bAttack)
        {
            // �U�����[�V����
            animator.SetInteger("Parameter", (int)eAnimetion.eAttack);

            Debug.Log("attack");
        }
    }

    //----------------------------
    // �ړ�
    //----------------------------
    private void Move()
    {
        // �����Ă��邩
        if ((vOldPos.x == transform.position.x || vOldPos.z == transform.position.z) && !bAttack)
        {
            // �ҋ@���[�V����
            animator.SetInteger("Parameter", (int)eAnimetion.eWait);
        }
        else
        {
            // �ړ����[�V����
            animator.SetInteger("Parameter", (int)eAnimetion.eMove);
            bFirstAttack = false;
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

<<<<<<< HEAD:Assets/Script/OguraScript/EnemyBase.cs
        // �͈͓��Ƀv���C���[��������ǂ�������
        if (bFind)
=======
        // �G�Ƃ̋��������ȉ��Ȃ�U������
        if ((vDiffPos.x <= fAttackDis && vDiffPos.x >= -fAttackDis) && (vDiffPos.z <= fAttackDis && vDiffPos.z >= -fAttackDis))
>>>>>>> d2f65eada7be6604d61b693afd0e28d3b8accd2c:Assets/08_Script/OguraScript/EnemyBase.cs
        {
            myAgent.SetDestination(player.transform.position);

            // �G�Ƃ̋��������ȉ��Ȃ�U������
            if ((vDiffPos.x <= fAttackDis && vDiffPos.x >= -fAttackDis) && (vDiffPos.z <= fAttackDis && vDiffPos.z >= -fAttackDis))
            {
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
                nMoveTime = 2.0f;�@// ��
            }
        }

        vOldPos = this.gameObject.transform.position;
    }

    //----------------------------
    // �v���C���[�Ƃ̐ڐG��
    //----------------------------
    private void OnTriggerEnter(Collider other)
    {
<<<<<<< HEAD:Assets/Script/OguraScript/EnemyBase.cs
        // �v���C���[���͈͂ɓ�������ǂ�
=======
        // �v���C���[�Ƃ̏Փˎ��_���[�W
>>>>>>> d2f65eada7be6604d61b693afd0e28d3b8accd2c:Assets/08_Script/OguraScript/EnemyBase.cs
        if (other.CompareTag("Player"))
        {
            bFind = true;
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
            status.HP -= player.GetComponent<PlayerStatus>().Attack;     // TODO:�����Ƀv���C���[�̍U���͂�����

            // �_���[�W�\�L
            ViewDamage(player.GetComponent<PlayerStatus>().Attack);      // TODO:�����Ƀv���C���[�̍U���͂�����
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
