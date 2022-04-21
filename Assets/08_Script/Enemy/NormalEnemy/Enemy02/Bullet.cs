
//======================================================================
// Bullet.cs
//======================================================================
// �J������
//
// 2022/03/05 author�F�����x ����J�n�@�G�̉������U������
// 2022/03/28 author�F�|���@���}�@�v���C���[�ւ̃_���[�W����
// 2022/03/30 author�F�����@�G�t�F�N�g�����̒ǉ�
// 2022/04/21 author�F�����@�G�̍U���͂�EnemyData����Q�Ƃ���悤�ɕύX
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed { get; set;  }

    GameObject player;
    GameObject enemy;
    GameObject effect;

    public void SetPlayer(GameObject obj) { player = obj; }
    public void SetEnemy(GameObject obj) { enemy = obj; }

    public void SetEffect(GameObject obj) { effect = obj; }


    //---------------------------
    // ������
    //---------------------------
    private void Start()
    {
        // �G�t�F�N�g��180����]������
        transform.Rotate(transform.rotation.x, transform.rotation.y + 180.0f, transform.rotation.z);
        effect.transform.rotation = transform.rotation;
    }


    //---------------------------
    // �X�V
    //---------------------------
    void Update()
    {
        // �O���֔�΂�(�G�t�F�N�g����]���������߁A�u-�v��t���Čv�Z����)
        transform.position += -transform.forward * Time.deltaTime * Speed;
        effect.transform.position = transform.position;

        Destroy(effect, 3.0f);
        Destroy(gameObject, 3.0f);
    }


    //--------------------------------
    // �v���C���[�Ƃ̐ڐG���_���[�W
    //--------------------------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            // �_���[�W����
            player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<EnemyBase>().GetEnemyData.nAttack);

            Destroy(effect);
            Destroy(gameObject);
        }
    }

}
