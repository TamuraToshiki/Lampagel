//======================================================================
// BossBase.cs
//======================================================================
// �J������
//
// 2022/03/27 author�F�����x ����J�n�@�{�X�x�[�X����
// 2022/03/28 author�F�|���@���}�@�|�[�^���o���A���X�g�����@�\�R�����g�A�E�g
// 2022/04/16 author�F���쏫�V �݌v�ύX �R���|�[�l���g���ו���
// 2022/05/09 author�F�|���@�{���̃��[�������֕ύX
// 2022/05/11 author�F�|���@���[�������i�]�T����Ί◎�Ƃ������j
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]

public class Boss01 : MonoBehaviour
{
    // �{�X�̍U�����
    enum eAttackType
    { 
        eFire = 0,
        eFlame,
        
        eAttackMax
    }

    // �{�X�̍U��
    //private BossAttack bossAttack;
    // �v���C���[
    private GameObject player;
    // �{�X�̊��N���X
    private EnemyBase BossBase;
    // �O�t���[���̍��W
    private Vector3 vOldPos;

    // �U���֘A
    //[Header("�U�����J�n���鋗��")]
    //[SerializeField, Range(0.0f, 50.0f)] private float fAttackDis = 5.0f;

    [Header("�U���p�x")]
    [SerializeField, Range(0.0f, 10.0f)] private float fAttackTime = 10.0f;

    private float fAttackCount;

    //int nAttackType;

    // �G�t�F�N�g
    [Header("�G�t�F�N�g�V�X�e��")] [SerializeField] EnemyEffect effect;

    // �{�X�̑�
    [Header("��")] [SerializeField] GameObject body;
    [Header("��")] [SerializeField] GameObject tail;

    public EnemyEffect GetEffect { get { return effect; } }

    // �{�X�̑O�ʂɓ����蔻��p��
    GameObject FrontCube;


    // �����]���ƈړ����x
    public float fspeed = 10.0f;
    public float frotSpeed = 1;

    bool bIsAttack = false;
    Rigidbody PlayerRB;
    //


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        BossBase = GetComponent<EnemyBase>();

        // �U���֘A
        //bossAttack = GetComponent<BossAttack>();
        //bossAttack.SetPlayer(player);
        fAttackCount = fAttackTime;
        PlayerRB = player.GetComponent<Rigidbody>();

        // �{�X�̑́A���𐶐�
        body = Instantiate(body, transform.position, transform.rotation);
        tail = Instantiate(tail, transform.position, transform.rotation);

        // �T�C�Y��S�ē����ɂ���
        body.transform.localScale = tail.transform.localScale = transform.localScale;

        // �̂ɃX�N���v�g��ǉ�����
        body.AddComponent<Boss01_body>();
        tail.AddComponent<Boss01_body>();

        // �����Z�b�g
        body.GetComponent<Boss01_body>().SetBossFront(gameObject);
        body.GetComponent<Boss01_body>().SetBossHead(gameObject);
        tail.GetComponent<Boss01_body>().SetBossFront(body);
        tail.GetComponent<Boss01_body>().SetBossHead(gameObject);

        

    }

    void Update()
    {
        // �U��
        Attack();
        Aim_at_Player();

        // �{�X�����񂾂�q�I�u�W�F�N�g���j��
        if (BossBase.nHp <= 0)
            DestroyObject();
    }

    // �q�I�u�W�F�N�g�̔j��
    public void DestroyObject()
    {
        Destroy(tail);
        Destroy(body);
    }

    // // Player��_���A���������� *****************************
    void Aim_at_Player() 
    {
        //�Ǐ]����悤�ɑ���
        Quaternion lookatWP = Quaternion.LookRotation(player.transform.position - this.transform.position);

        this.transform.rotation = Quaternion.Slerp(transform.rotation, lookatWP, frotSpeed * Time.deltaTime);

        this.transform.Translate(0, 0, fspeed * Time.deltaTime);
    }
    //**********************************************************

    // �U��
    void Attack()
    {
        //��莞�Ԗ��ɍU��
        if(bIsAttack == false)
        {
            fAttackCount -= Time.deltaTime;
        }
        
        if (fAttackCount < 0.0f)
        {
            bIsAttack = true;
            frotSpeed = 5;
            
        }
        else
        {
            frotSpeed = 1;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[�Ƃ̏Փˎ��_���[�W
        if (other.CompareTag("Player"))
        {
            fAttackCount = fAttackTime;
        }

        if (other.CompareTag("Player") && bIsAttack == true)
        {
            // �_���[�W����
            player.GetComponent<PlayerHP>().OnDamage(BossBase.GetComponent<EnemyBase>().nAttack);

            PlayerRB.AddForce(this.transform.forward * 500);

            bIsAttack = false;
        }
    }

    
}
