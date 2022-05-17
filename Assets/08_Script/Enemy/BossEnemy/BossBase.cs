//======================================================================
// BossBase.cs
//======================================================================
// �J������
//
// 2022/04/15 author�F���쏫�V �{�X�̊��N���X����
// 2022/05/02 author�F���쏫�V ���S�֐�(Death)����HP���擾�\��
// 2022/05/02 author�F�����x   �^�[�Q�b�g�}�[�J�[�����ǉ�
// 2022/05/05 author�F�|���@�v���C���[�̑��x�ɑ΂��ă_���[�W�o����悤��
// 2022/05/06 �@�@�@�@�@�@�@�����A�U���֐���ǉ����A�j���[�V�������N��������
// 2022/05/09               �A�j���[�V�����ƍU�������ǉ�
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBase : MonoBehaviour
{
    //0509 �ǉ� **********************************************
    // �A�j���[�V�����̎��
    enum eAnimetion
    {
        eDefult,
        eMove,
        eAttack,
    }

    // �U������
    public bool bAttack { get; set; }

    // �U���p�x
    private float fAttackCount;

    [Header("�U�����J�n���鋗��")]
    [SerializeField, Range(0.0f, 50.0f)] private float fAttackDis = 3.0f;

    [Header("�U���p�x")]
    [SerializeField, Range(0.0f, 10.0f)] private float fAttackTime = 3.0f;

    // �U���͈͂ɓ����Ă���A��x�ڂ̍U����
    private bool bFirstAttack = false;

    private Rigidbody rb;
    //********************************************************

    [SerializeField] public EnemyData enemyData;
    public EnemyData GetEnemyData { get { return enemyData; } }
    public GameObject player { get; set; }
    private NavMeshAgent myAgent;
    private Animator animator;
    

    // HP
    public int nHp;

    // ���x�ɑ΂���_���[�W�␳
    float fSpeedtoDamage = 0.03f;

    //*���}
    [SerializeField] GameObject Portals;
    bool bPortal = false;

    // �O�t���[���̍��W
    private Vector3 vOldPos;

    // �_���[�WUI
    [SerializeField] private GameObject DamageObj;

    [Header("���S�����ʉ�")]
    [SerializeField] private AudioClip DeathSE;

    [Header("�G�t�F�N�g�V�X�e��")]
    [SerializeField] EnemyEffect effect;

    [Header("�G�}�[�J�[")]
    [SerializeField] Canvas Marker;

    public EnemyEffect GetEffect { get { return effect; } }



    void Start()
    {
        player = GameObject.FindWithTag("Player");

        nHp = enemyData.BossHp + (enemyData.nLevel * enemyData.nUpHP);

        // �i�r���b�V���������i�X�e�[�^�X����X�s�[�h���擾�j
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.speed = enemyData.fSpeed;

        // �A�j���[�^�[������
        animator = GetComponent<Animator>();

        // �G�^�[�Q�b�g�}�[�J�[����
        Marker = Instantiate(Marker, Vector3.zero, Quaternion.identity);

        // �{�X�����Z�b�g
        Marker.GetComponentInChildren<TargetMarker>().target = gameObject.transform;

        //0509 �ǉ� **********************************************
        rb = this.gameObject.GetComponent<Rigidbody>();
        //********************************************************

    }



    void Update()
    {
        Move();
        Death();

        // 0509 �ǉ� **********************************************
        Burst();
        //********************************************************

    }



    // ���S
    public int Death()
    {
        // HP0�ȉ��ŏ���
        if (nHp <= 0)
        {
            //*���}*
            if (bPortal == false)
            {
                // ���ʉ��Đ�
                AudioSource.PlayClipAtPoint(DeathSE, transform.position);

                Instantiate(Portals, this.gameObject.transform.position, Quaternion.identity);
                bPortal = true;
            }

            // �^�[�Q�b�g�}�[�J�[����
            Destroy(Marker);

            // �S�ď���
            Destroy(this.gameObject);

            return 0;
        }
        return nHp;
    }



    // �ړ�
    public void Move()
    {
        // ���̏ꏊ���v�Z
        Vector3 nextPoint = myAgent.steeringTarget;
        Vector3 targetDir = nextPoint - transform.position;

        // ��]
        Quaternion targetRotation = Quaternion.LookRotation(targetDir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 120f * Time.deltaTime);

        // ���݂̃v���C���[�̈ʒu��ڎw��
        myAgent.SetDestination(player.transform.position);

        // �ߋ����W���X�V
        vOldPos = gameObject.transform.position;

        // �v���C���[�Ƃ̋����v�Z
        Vector3 vDiffPos = this.transform.position - player.transform.position;


        // 0509 �ǉ� **********************************************
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
        //********************************************************
    }

    //�v���C���[�Ƃ̐ڐG��(IsTrigger)
    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[�Ƃ̏Փˎ��_���[�W
        if (other.CompareTag("Player"))
        {
            // �_���[�W����
            Damege();
        }
    }


    // 0509 �ǉ� **********************************************
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

    // �_���[�W�̏���
    public void Damege()
    {
        // �_���[�W����
        int n = (int)((player.GetComponent<PlayerStatus>().Attack * (int)player.GetComponent<Rigidbody>().velocity.magnitude) * fSpeedtoDamage);
        nHp -= n;

        // �_���[�W�\�L
        ViewDamage(n);
    }

    // �_���[�W�\�L
    private void ViewDamage(int damage)
    {
        // �e�L�X�g�̐���
        GameObject text = Instantiate(DamageObj);
        text.GetComponent<TextMesh>().text = damage.ToString();

        // �������炵���ʒu�ɐ���(z + 1.0f)
        text.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.0f);
    }
    //********************************************************

    //----------------------------
    // �o�[�X�g����������Ƃ�
    //----------------------------
    private void Burst()
    {
        // �������Z��ON�̎��i�o�[�X�g���ɕ������Z��ON�ɂȂ�j
        if (!rb.isKinematic)
        {

            rb.isKinematic = true;




        }
        
    }
}
