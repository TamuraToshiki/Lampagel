//======================================================================
// Scratch.cs
//======================================================================
// �J������
//
// 2022/03/05 author�F�����x ����J�n�@�G�̂Ђ������U������
// 2022/03/28 author�F�|���@���}�@�v���C���[�ւ̃_���[�W����
// 2022/04/21 author�F�����@�G�̍U���͂�EnemyData����Q�Ƃ���悤�ɕύX
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scratch : MonoBehaviour
{
    GameObject player;
    GameObject enemy;

    public void SetPlayer (GameObject obj) { player = obj; }
    public void SetEnemy(GameObject obj) { enemy = obj; }

    public bool bImBoss = false;


    
    void Update()
    {
        Destroy(gameObject, 0.5f);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")

        {
            // �_���[�W����
            if (bImBoss == true)
            {
                player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<BossBase>().GetEnemyData.nAttack);
            }
            else if(bImBoss == false)
            {
                player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<EnemyBase>().GetEnemyData.nAttack);
            }
            

            Destroy(gameObject);

        }
    }
}
