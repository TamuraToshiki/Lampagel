//======================================================================
// BossBase.cs
//======================================================================
// �J������
//
// 2022/04/15 author�F���쏫�V �{�X�̊��N���X����
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBase : MonoBehaviour
{
    //private BossAttack bossAttack;
    private StatusComponent status;
    private GameObject player;
    private NavMeshAgent myAgent;
    private Animator animator;

    //*���}*
    [SerializeField] GameObject Portals;
    bool bPortal = false;

    // �O�t���[���̍��W
    private Vector3 vOldPos;

    // �_���[�WUI
    [SerializeField] private GameObject DamageObj;

    // ���ʉ�
    [Header("���S�����ʉ�")]
    [SerializeField] private AudioClip DeathSE;

    // �U���֘A
    [Header("�U�����J�n���鋗��")]
    [SerializeField, Range(0.0f, 50.0f)] private float fAttackDis = 5.0f;

    [Header("�U���p�x")]
    [SerializeField, Range(0.0f, 10.0f)] private float fAttackTime = 3.0f;

    private float fAttackCount;
    int nAttackType;

    // �G�t�F�N�g
    [Header("�G�t�F�N�g�V�X�e��")] [SerializeField] EnemyEffect effect;

    public EnemyEffect GetEffect { get { return effect; } }

    // �{�X�̑O�ʂɓ����蔻��p��
    GameObject FrontCube;

    // �ːi����
    bool bRush = false;
    float fRushTime = 1.0f;
    float fRushCount = 0.0f;

    bool bVisible = false;

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
        //bossAttack = GetComponent<BossAttack>();
        //bossAttack.SetPlayer(player);
        fAttackCount = fAttackTime;

        // �_���[�W����
        BossRush rush = gameObject.GetComponentInChildren<BossRush>();
        rush.SetPlayer(player);
        rush.SetEnemy(gameObject);

    }

    //-------------------------
    // �X�V
    //-------------------------
    void Update()
    {
        Move();
        //Attack();
        Death();

    }

    //----------------------------
    // ���S
    //----------------------------
    public bool Death()
    {
        // HP0�ȉ��ŏ���
        if (status.HP <= 0)
        {
            //*���}*
            if (bPortal == false)
            {
                // ���ʉ��Đ�
                AudioSource.PlayClipAtPoint(DeathSE, transform.position);

                Instantiate(Portals, this.gameObject.transform.position, Quaternion.identity);
                bPortal = true;
            }

            // �S�ď���
            Destroy(this.gameObject);

            return true;
        }
        return false;
    }

    //----------------------------
    // �ړ�
    //----------------------------
    public void Move()
    {
        // ���̏ꏊ���v�Z
        Vector3 nextPoint = myAgent.steeringTarget;
        Vector3 targetDir = nextPoint - transform.position;

        // ��]
        Quaternion targetRotation = Quaternion.LookRotation(targetDir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 120f * Time.deltaTime);


        // �v���C���[��ǂ�������i���b�����ƂɓG�߂����Ĉړ��j
        if (!bRush)
        {
            // ���݂̃v���C���[�̈ʒu��ڎw��
            myAgent.SetDestination(player.transform.position);

            // ������
            bRush = true;
            fRushCount = fRushTime;
        }

        // �ړ��̃J�E���g����
        fRushCount -= Time.deltaTime;
        if (fRushCount < 0.0f)
        {
            bRush = false;
        }

        // �ߋ����W���X�V
        vOldPos = gameObject.transform.position;
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
            Damege();
        }
    }

    //------------------------------------------------------
    // �_���[�W�̏���(�{�X�̑́A���ł��g����悤��public)
    //------------------------------------------------------
    public void Damege()
    {
        // �_���[�W����
        status.HP -= player.GetComponent<PlayerStatus>().Attack;

        // �_���[�W�\�L
        ViewDamage(player.GetComponent<PlayerStatus>().Attack);
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
