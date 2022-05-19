//======================================================================
// Boss2.cs
//======================================================================
// �J������
//
// 2022/04/15 author�F���쏫�V �{�X2(�o�C�o�C��)�����J�n
// 2022/04/26 author�F���쏫�V �A�j���[�V�����ǉ�(�ړ��E�U��)
// 2022/05/02 author�F���쏫�V �G���G���{�X�̎��肩��~��ɐ��������悤��
// 2022/05/18 author�F���쏫�V �U���͈́E�_���[�W�����ǉ�                           
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss2 : MonoBehaviour
{
    // �{�X�̏��
    [Header("�{�X�̏��")]
    public EnemyData enemyData;
    // ���􂷂�G���G
    [Header("�G���I�I�u�W�F�N�g")]
    public GameObject DivisionEnemy;
    // �U���͈͂̕\���p�I�u�W�F�N�g
    [Header("�U���͈͕\���p�I�u�W�F�N�g")]
    public GameObject AttackField;
    // �{�X�̍U���͈�
    [Header("�U���͈�")]
    public float Radius = 5.0f;

    // �v���C���[���
    private GameObject player;
    // �A�j���[�V����
    private Animator animation;
    // �{�X�̊��N���X
    private EnemyBase BossBase;
    // �{�X�̍ő�HP
    private int nMaxHp;
    // �G���I�����̔��a
    private float distance = 5.0f;
    // �G���G���������������܂ł̎���
    private float fTime = 2.0f;

    void Start()
    {
        player = GameObject.FindWithTag("Player").gameObject;

        animation = GetComponent<Animator>();
        BossBase = GetComponent<EnemyBase>();
        // �{�X�̍ő�HP�ݒ�
        nMaxHp = enemyData.BossHp;
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other) // �|���FCollision����Trigger�։��ύX�A�v���C���[�����Ƃ�������߂�
    {
        if (other.gameObject.tag == "Player")
        {
            //bHit = true;

            // �{�X��HP���擾
            int nDivisionCnt = OnDamegeHp();

            // �G���G�̐���
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
        int CurrentHP = (int)BossBase.nHp;
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

    // �U���͈͕\��
    void CreateAttackField()
    {
        GameObject field = AttackField;

        // �U���͈͐���
        field = Instantiate(field, new Vector3(this.transform.position.x, 0.1f, this.transform.position.z), field.transform.rotation);
        // �U���͈͂̐F��ԐF��
        field.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);

        // �͈͂̍폜
        StartCoroutine(AcidAttackField(field));
    }

    // �U���͈͍폜
    IEnumerator AcidAttackField(GameObject field)
    {
        // �U���͈͂��\�������1.8�b�o�߂�����
        yield return new WaitForSeconds(1.8f);
        // �U���͈͂��폜
        Destroy(field);
    }

    // �_���[�W����
    void Boss2Attack()
    {
        // �{�X�̈ʒu
        Vector3 enemypos = this.transform.position;
        // �v���C���[�̈ʒu
        Vector3 playerpos = player.transform.position;

        // �{�X�ƃv���C���[�̈ʒu���������Ă�Ȃ�_���[�W����
        if (InSphere(enemypos, Radius, playerpos))
        {
            player.GetComponent<PlayerHP>().OnDamage(enemyData.BossAttack);
        }
    }

    // Aoe�������鎞�̔���擾�p
    public static bool InSphere(Vector3 pos, float rad, Vector3 center)
    {
        var sum = 0f;
        for (var i = 0; i < 3; i++)
        {
            sum += Mathf.Pow(pos[i] - center[i], 2);
        }
        return sum <= Mathf.Pow(rad, 2f);
    }
}
