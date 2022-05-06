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

    private bool bStop = false;

    private float fTime = 4.0f;

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
            //myAgent.velocity = Vector3.zero;
            myAgent.Stop();
            //Destroy(this.gameObject);
        }
        if(fTime <= 0.0f)
        {
            Destroy(this.gameObject);
            //fTime = 0.0f;
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
}
