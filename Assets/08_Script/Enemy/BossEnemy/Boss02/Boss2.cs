//======================================================================
// Boss2.cs
//======================================================================
// �J������
//
// 2022/04/15 author�F���쏫�V �{�X2(�o�C�o�C��)�����J�n
// 2022/04/26 author�F���쏫�V �A�j���[�V�����ǉ�(�ړ��E�U��)
// 2022/05/02 author�F���쏫�V �G���G���{�X�̎��肩��~��ɐ��������悤��
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss2 : MonoBehaviour
{
    // �v���C���[���
    [SerializeField] private GameObject player;

    [SerializeField] private EnemyData enemyData;

    // ���􂷂�G���Ie
    public GameObject DivisionEnemy;

    private Animator animation;

    // �{�X�̊��N���X
    private BossBase BossBase;

    private int nMaxHp;

    private bool bHit = false;

    // ���a
    public float distance = 5.0f;

    private float fTime = 2.0f;

    void Start()
    {
        player = GameObject.FindWithTag("Player").gameObject;

        animation = GetComponent<Animator>();

        BossBase = GetComponent<BossBase>();

        nMaxHp = BossBase.enemyData.BossHp;
    }

    void Update()
    {
        //Move(myAgent, Player);
        //CreateDivision();

        if(Input.GetKey(KeyCode.O))
        {
            animation.SetTrigger("attack");
        }

        if(bHit)
        {
            fTime -= Time.deltaTime;

            if(fTime <= 0.0f)
            {
                fTime = 2.0f;
                bHit = false;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && !bHit)
        {
            bHit = true;

            // �{�X��HP���擾
            int nDivisionCnt = OnDamegeHp();

            CreateDivision(DivisionEnemy,    // ��������I�u�W�F�N�g
                           nDivisionCnt * 2, // �������鐔
                           this.gameObject,  // ���S�̃I�u�W�F�N�g
                           distance,         // �e�I�u�W�F�N�g�̋���
                           true);            // �����Ɍ����邩�ǂ���

        }
    }

    // HP�ɂ�镪����
    private int OnDamegeHp()
    {
        // �{�X�̌��݂�HP���擾
        int CurrentHP = BossBase.Death();
        Debug.Log(CurrentHP);

        int nDivisionCnt = 0;

        // HP�������ȏ�Ȃ�
        if (CurrentHP > nMaxHp / 2)
        {
            nDivisionCnt = 1;
        }
        // HP�������ȉ��Ȃ�
        else if (CurrentHP <= nMaxHp / 2 && CurrentHP > nMaxHp / 4)
        {
            nDivisionCnt = 2;
        }
        // HP��1/4�ȉ��Ȃ�
        else if (CurrentHP <= nMaxHp / 4)
        {
            nDivisionCnt = 4;
        }

        return nDivisionCnt;
    }

    // ���􂷂�G�̐���
    public void CreateDivision(GameObject prefab, int count, GameObject center, float distance, bool isLookAtCenter)
    {
        for (int i = 0; i < count; i++)
        {
            // �~��ɃI�u�W�F�N�g�𐶐�
            var position = center.transform.position + (Quaternion.Euler(0f, 360f / count * i, 0f) * center.transform.forward * distance);
            var obj = Instantiate(prefab, position, Quaternion.identity);

            // �����Ɍ����邩�ǂ���
            if (isLookAtCenter)
            {
                obj.transform.LookAt(center.transform);
            }
        }
    }
}
