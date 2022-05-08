//======================================================================
// DivisionEnemy.cs
//======================================================================
// �J������
//
// 2022/04/26 author�F���쏫�V �����̎����@�Ǐ](�K�o�K�o�Ȃ̂ŗv�C��)
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DivisionEnemy : MonoBehaviour
{
    private GameObject player;

    private NavMeshAgent myAgent;

    private float fTime = 4.0f;

    bool isCalledOnce = false;
    bool bAttackStart = false;

    public GameObject AttackCircle;

    public GameObject enemy { get; set; }

    void Start()
    {
        player = GameObject.Find("Player");
        // NavMeshAgent��ێ����Ă���
        myAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Move();
        fTime -= Time.deltaTime;

        if (fTime <= 2.0f)
        {
            // AoE����
            if (!isCalledOnce)
            {
                isCalledOnce = true;
                AttackCircle = Instantiate(AttackCircle, new Vector3(this.transform.position.x, 0.1f, this.transform.position.z), AttackCircle.transform.rotation);
                AttackCircle.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);

                myAgent.velocity = Vector3.zero;
                myAgent.Stop();
            }

        }
        if(fTime <= 0.0f)
        {
            bAttackStart = true;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && bAttackStart)
        {
            // �_���[�W����
            player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<EnemyBase>().GetEnemyData.nAttack);
        }
    }
}
