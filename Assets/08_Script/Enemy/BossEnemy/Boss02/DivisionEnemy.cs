//======================================================================
// DivisionEnemy.cs
//======================================================================
// 開発履歴
//
// 2022/04/26 author：松野将之 分裂後の実装　追従(ガバガバなので要修正)
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

    private float fTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        // NavMeshAgentを保持しておく
        myAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        fTime -= Time.deltaTime;


        if (fTime <= 0)
        {
            bStop = true;
        }

        Move();
    }

    private void Move()
    {
        if (!bStop)
        {
            // 次の場所を計算
            Vector3 nextPoint = myAgent.steeringTarget;
            Vector3 targetDir = nextPoint - transform.position;

            // 回転
            Quaternion targetRotation = Quaternion.LookRotation(targetDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 120f * Time.deltaTime);

            // プレイヤーを追いかける
            myAgent.SetDestination(player.transform.position);
        }

        //if(myAgent.stoppingDistance <= 4.0f)
        //{
        //    bStop = true;
        //}
    }
}
