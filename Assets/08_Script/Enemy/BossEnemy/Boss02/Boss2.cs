// Boss2.cs
//======================================================================
// �J������
//
// 2022/04/15 author�F���쏫�V �{�X2(�o�C�o�C��)�����J�n
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss2 : MonoBehaviour
{
    // �G�̃f�[�^
    public EnemyData EnemyData;

    // AI
    private NavMeshAgent myAgent;

    // �v���C���[���
    [SerializeField] private GameObject Player;

    // �O�t���[���̍��W
    private Vector3 vOldPos;

    //public void SetPlayer(GameObject obj) { Player = obj; }
    // �ːi����
    bool bRush = false;
    float fRushTime = 1.0f;
    float fRushCount = 0.0f;

    bool bVisible = false;

    void Start()
    {
        // �i�r���b�V���������i�X�e�[�^�X����X�s�[�h���擾�j
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.speed = EnemyData.fSpeed;
    }

    void Update()
    {
        //Move(myAgent, Player);
        //CreateDivision();
    }

    //protected override void Move(NavMeshAgent nav, GameObject obj)
    //{

    //    // �v���C���[��ǂ�������i���b�����ƂɓG�߂����Ĉړ��j
    //    if (!bRush)
    //    {
    //        // ���݂̃v���C���[�̈ʒu��ڎw��
    //        myAgent.SetDestination(Player.transform.position);

    //        // ������
    //        bRush = true;
    //        fRushCount = fRushTime;
    //    }

    //    // �ړ��̃J�E���g����
    //    fRushCount -= Time.deltaTime;
    //    if (fRushCount < 0.0f)
    //    {
    //        bRush = false;
    //    }


    //    vOldPos = gameObject.transform.position;
    //}


    //public void Move()
    //{
    //    // ���̏ꏊ���v�Z
    //    Vector3 nextPoint = myAgent.steeringTarget;
    //    Vector3 targetDir = nextPoint - transform.position;

    //    // ��]
    //    Quaternion targetRotation = Quaternion.LookRotation(targetDir);
    //    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 120f * Time.deltaTime);
    //    myAgent.SetDestination(Player.transform.position);

    //   // vOldPos = gameObject.transform.position;
    //}

    // �{�X�̕�������
    void CreateDivision()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[�Ƃ̏Փˎ��_���[�W
        if (other.CompareTag("Player"))
        {
            // �_���[�W����
            //Damege();
            CreateDivision();
            Debug.Log("�o�C�o�C��");
        }
    }

}
