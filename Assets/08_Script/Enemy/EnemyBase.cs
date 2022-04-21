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
// 2022/03/31 author�F�����x ��苗�������ƓG�����ł���悤��
// 2022/04/04 author�F�����x ���{�X�p�ɏ�������
// 2022/04/15 author�F���쏫�V �}�X�^�[�f�[�^����X�e�[�^�X���擾
// 2022/04/21 author�F�����x GetEnemyData���쐬(EnamyManager.cs�ɂĎg�p)
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

    // �G�̃}�X�^�[�f�[�^
    [SerializeField] private EnemyData enemyData;
    public EnemyData GetEnemyData { get { return enemyData; } }

    public GameObject player{ get; set; }
    public EnemyManager manager { get; set; }
    private NavMeshAgent myAgent;
    private Animator animator;
    private Rigidbody rb;

    // HP�ƍU����
    private float nHp;
    private float nAttack;

    // �O�t���[���̍��W
    private Vector3 vOldPos;

    // ������΂���Ă��瓮���o���b��
    private float fBurstTime = 2.0f;

    // �U������
    public bool bAttack { get; set; }

    // �U���͈͂ɓ����Ă���A��x�ڂ̍U����
    private bool bFirstAttack = false;

    // �_���[�WUI
    [SerializeField] private GameObject DamageObj;


    [Header("���S�����ʉ�")]
    [SerializeField] private AudioClip DeathSE;

    [Header("�U�����J�n���鋗��")]
    [SerializeField, Range(0.0f, 50.0f)] private float fAttackDis = 3.0f;

    [Header("�U���p�x")]
    [SerializeField, Range(0.0f, 10.0f)] private float fAttackTime = 3.0f;
    private float fAttackCount;

    [Header("�G�t�F�N�g�V�X�e��")]
    [SerializeField] EnemyEffect effect;
    public EnemyEffect GetEffect { get { return effect; }}

    // ���ŋ���
    float fDistance = 20.0f;

    //------------------------
    // �Q�b�^�[�A�Z�b�^�[
    //------------------------

    //----------------------------
    // ������
    //----------------------------
    void Start()
    {
        // �X�e�[�^�X������
        nHp = enemyData.nHp + (enemyData.nLevel * enemyData.nUpHP);
        nAttack = enemyData.nAttack + (enemyData.nLevel * enemyData.nUpAttack);

        // �i�r���b�V���ݒ�
        myAgent = GetComponent<NavMeshAgent>();
   
        // �X�s�[�h�ݒ�
        myAgent.speed = enemyData.fSpeed;
    
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

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
        DistanceDeth();
    }

    //----------------------------
    // ���S
    //----------------------------
    private void Death()
    {
        // HP0�ȉ��ŏ���
        if (nHp <= 0)
        {
            // �o���l����
            player.GetComponent<PlayerExp>().AddExp(10);

            // ���ʉ��Đ�
            AudioSource.PlayClipAtPoint(DeathSE, transform.position);

            // ���X�g����폜(���{�X��EnemyManager�̃��X�g�ɓ����ĂȂ����ߏ������Ȃ�)
            if(manager != null) manager.NowEnemyList.Remove(gameObject);
            Destroy(this.gameObject);
        }
    }

    //----------------------------
    //  ��苗�����ꂽ�����
    //----------------------------
    private void DistanceDeth()
    {
        // ���{�X�͗���Ă����ł��Ȃ����ߏ������Ȃ�
        if (manager == null) return;

        // �v���C���[�Ƃ̍����v�Z
        Vector2 vdistance = new Vector2(transform.position.x - player.transform.position.x, transform.position.z - player.transform.position.z);

        // ���ŏ���
        if (vdistance.x > fDistance || vdistance.x < -fDistance ||
           vdistance.y > fDistance || vdistance.y < -fDistance)
        {
            Debug.Log("����");

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
        myAgent.velocity = Vector3.zero;

        // �U���J�n������ or �U���͈͂ɓ����Ă���A�ŏ��̍U���̎�
        if (IsAttack() || !bFirstAttack)
        {
            // �U�����[�V����
            animator.SetInteger("Parameter", (int)eAnimetion.eAttack);

            bFirstAttack = true;
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
            bFirstAttack = false;
        }

        // �U�����łȂ��Ƃ�
        if (!bAttack)
        {
           // ���̏ꏊ���v�Z
           Vector3 nextPoint = myAgent.steeringTarget;
            Vector3 targetDir = nextPoint - transform.position;

           // ��]
           Quaternion targetRotation = Quaternion.LookRotation(targetDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 120f * Time.deltaTime);

            // �v���C���[��ǂ�������
            myAgent.SetDestination(player.transform.position);
        }

        // �v���C���[�Ƃ̋����v�Z
        Vector3 vDiffPos = this.transform.position - player.transform.position;

        // �G�Ƃ̋��������ȉ��Ȃ�U������
        if ((vDiffPos.x <= fAttackDis && vDiffPos.x >= -fAttackDis) && (vDiffPos.z <= fAttackDis && vDiffPos.z >= -fAttackDis))
        {
            EnemyAttack();
        }
        // �U���I���������o��
        else if (myAgent.speed == 0.0f && !bAttack)
        {
            // �X�s�[�h�̍Đݒ�
            myAgent.speed = enemyData.fSpeed;
        }

        vOldPos = this.gameObject.transform.position;
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
            nHp -= player.GetComponent<PlayerStatus>().Attack;     // TODO:�����Ƀv���C���[�̍U���͂�����

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
        if (!rb.isKinematic)
        {
            // 2�b��ɁA�������ZOFF�ɂ���(��)
            fBurstTime -= Time.deltaTime;
            if (fBurstTime < 0.0)
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
        text.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.0f);
    }
}
