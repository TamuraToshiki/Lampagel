//======================================================================
// DivisionEnemy.cs
//======================================================================
// �J������
//
// 2022/04/26 author�F���쏫�V �����̎����@�Ǐ](�K�o�K�o�Ȃ̂ŗv�C��)
// 2022/05/19 author�F���쏫�V �G���G�̃_���[�W����
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DivisionEnemy : MonoBehaviour
{
    // ����������ł܂ł̎���
    [Header("����������ł܂ł̎���")]
    public float ExtinctTime = 4.0f;
    [Header("�ǔ����鎞��")]
    public float stopTime = 2.0f;
    [Header("AoE(�U���͈͂̕\��)")]
    public GameObject AttackCircle;
    // �G���I�̍U���͈�
    [Header("�U���͈�")]
    public float Radius = 2.0f;

    // �v���C���[
    private GameObject player;
    // �G���G��AI
    private NavMeshAgent myAgent;
    // Update��1�񂾂��֐��ĂԂ��߂̔���
    private bool isCalledOnce = false;

    void Start()
    {
        player = GameObject.Find("Player");
        myAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Move();
        ExtinctTime -= Time.deltaTime;

        // 2�b�o�߂�����
        if (ExtinctTime <= stopTime)
        {
            // AoE����
            if (!isCalledOnce)
            {
                // 1�x����Aoe�𐶐�
                isCalledOnce = true;
                AttackCircle = Instantiate(AttackCircle, new Vector3(this.transform.position.x, 0.1f, this.transform.position.z), AttackCircle.transform.rotation);
                AttackCircle.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);

                // �ǔ����~
                myAgent.velocity = Vector3.zero;
                myAgent.Stop();
            }
        }

        // ����
        if(ExtinctTime <= 0.0f)
        {
            // �_���[�W����
            DivisionEnemyAttack();

            // AoE�ƎG���G���폜
            Destroy(this.AttackCircle);
            Destroy(this.gameObject);
        }
    }

    private void Move()
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
    // �_���[�W����
    void DivisionEnemyAttack()
    {
        // �{�X�̈ʒu
        Vector3 enemypos = this.transform.position;
        // �v���C���[�̈ʒu
        Vector3 playerpos = player.transform.position;

        // �{�X�ƃv���C���[�̈ʒu���������Ă�Ȃ�_���[�W����
        if (InSphere(enemypos, Radius, playerpos))
        {
            player.GetComponent<PlayerHP>().OnDamage(10);
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
