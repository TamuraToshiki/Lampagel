//======================================================================
// EnemyBase.cs
//======================================================================
// �J������
//
// 2022/03/05 author�F�����x ����J�n�@�G�̃x�[�X�����ǉ�
// 2022/03/11 author�F�����x �o�[�X�g�����ǉ�
// 2022/03/15 author�F�����x �X�e�[�^�X�����ύX
<<<<<<< HEAD:Assets/08_Script/OguraScript/EnemyBase.cs
// 2022/03/28 auther�F�|���@���}�@�o���l�@�\�ǉ�
=======
// 2022/03/24 author�F�����x ���ʉ������̒ǉ�
>>>>>>> 8709684d4e54354a91684949987394adf606b0ff:Assets/Script/OguraScript/EnemyBase.cs
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
    private Animator animator;
    private Rigidbody rb;

    // �O�t���[���̍��W
    private Vector3 vOldPos;

   // ������΂���Ă��瓮���o���b��
    private float fBurstTime = 2.0f;

    // �U������
    private bool bAttack = false;

    // �U���͈͂ɓ����Ă���A��x�ڂ̍U����
    private bool bFirstAttack = false;
   
    // �_���[�WUI
    [SerializeField] private GameObject DamageObj;

    // ���ʉ�
    [Header("���S�����ʉ�")] [SerializeField] private AudioClip DeathSE;

    [Header("�U�����J�n���鋗��")] [SerializeField, Range(0.0f, 50.0f)] private float fAttackDis = 3.0f;
    [Header("�U���p�x")] [SerializeField, Range(0.0f, 10.0f)] private float fAttackTime = 3.0f;
    private float fAttackCount;  


    // �Q�b�^�[�A�Z�b�^�[
    public void SetManager(EnemyManager obj) { manager = obj; }
    public void SetPlayer(GameObject obj) { player = obj; }
    public GameObject GetPlayer { get { return player; } }

    public void SetAttack(bool flag) { bAttack = flag; }

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
    }

    //----------------------------
    // ���S
    //----------------------------
    private void Death()
    {
        // HP0�ȉ��ŏ���
        if (status.HP <= 0)
        {
<<<<<<< HEAD:Assets/08_Script/OguraScript/EnemyBase.cs
            //*���}*
            player.GetComponent<PlayerExp>().AddExp(10);
            
=======
            // ���ʉ��Đ�
            AudioSource.PlayClipAtPoint(DeathSE,transform.position);
>>>>>>> 8709684d4e54354a91684949987394adf606b0ff:Assets/Script/OguraScript/EnemyBase.cs

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
        if(!bAttack)
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
            myAgent.speed = status.Speed;
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
