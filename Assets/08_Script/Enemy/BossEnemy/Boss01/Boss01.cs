//======================================================================
// BossBase.cs
//======================================================================
// �J������
//
// 2022/03/27 author�F�����x ����J�n�@�{�X�x�[�X����
// 2022/03/28 author�F�|���@���}�@�|�[�^���o���A���X�g�����@�\�R�����g�A�E�g
// 2022/04/16 author�F���쏫�V �݌v�ύX �R���|�[�l���g���ו���
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
    private BossAttack bossAttack;
    // �v���C���[
    private GameObject player;
    // �{�X�̊��N���X
    private BossBase BossBase;
    // �O�t���[���̍��W
    private Vector3 vOldPos;

    // �U���֘A
    //[Header("�U�����J�n���鋗��")]
    //[SerializeField, Range(0.0f, 50.0f)] private float fAttackDis = 5.0f;

    [Header("�U���p�x")]
    [SerializeField, Range(0.0f, 10.0f)] private float fAttackTime = 3.0f;

    private float fAttackCount;

    int nAttackType;

    // �G�t�F�N�g
    [Header("�G�t�F�N�g�V�X�e��")] [SerializeField] EnemyEffect effect;

    // �{�X�̑�
    [Header("��")] [SerializeField] GameObject body;
    [Header("��")] [SerializeField] GameObject tail;

    public EnemyEffect GetEffect { get { return effect; } }

    // �{�X�̑O�ʂɓ����蔻��p��
    GameObject FrontCube;

    void Start()
    {
        player = GameObject.Find("Player");
        BossBase = GetComponent<BossBase>();

        // �U���֘A
        bossAttack = GetComponent<BossAttack>();
        bossAttack.SetPlayer(player);
        fAttackCount = fAttackTime;

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
        //Attack();

        // �{�X�����񂾂�q�I�u�W�F�N�g���j��
        if(BossBase.Death())
            DestroyObject();
    }

    // �q�I�u�W�F�N�g�̔j��
    public void DestroyObject()
    {
        Destroy(tail);
        Destroy(body);
    }

    // �U��
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
}
