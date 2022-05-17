using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstCircle : MonoBehaviour
{
    GameObject player;
    GameObject enemy;

    public void SetPlayer(GameObject obj) { player = obj; }
    public void SetEnemy(GameObject obj) { enemy = obj; }

    private int nSetAttack;

    void Start()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = true;

        // ���˂����{�l�����Ȃ��Ă��U���͂��킩��悤��
        nSetAttack = enemy.GetComponent<EnemyBase>().GetEnemyData.nAttack;
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            // �_���[�W����
            player.GetComponent<PlayerHP>().OnDamage(nSetAttack);

            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            
        }
    }
}
