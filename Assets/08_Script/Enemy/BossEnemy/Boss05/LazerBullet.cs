
//======================================================================
// Bullet.cs
//======================================================================
// �J������
//
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBullet : MonoBehaviour
{
    public float Speed { get; set;  }

    GameObject player;
    GameObject enemy;
    GameObject effect;

    public void SetPlayer(GameObject obj) { player = obj; }
    public void SetEnemy(GameObject obj) { enemy = obj; }

    public void SetEffect(GameObject obj) { effect = obj; }

    private int nSetAttack;


    //---------------------------
    // ������
    //---------------------------
    private void Start()
    {
        // �G�t�F�N�g��180����]������
        transform.Rotate(transform.rotation.x, transform.rotation.y + 180.0f, transform.rotation.z);
        effect.transform.rotation = transform.rotation;

        // ���˂����{�l�����Ȃ��Ă��U���͂��킩��悤��
        nSetAttack = enemy.GetComponent<EnemyBase>().GetEnemyData.nAttack;
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
            player.GetComponent<PlayerHP>().OnDamage(nSetAttack);

            Destroy(effect);
            Destroy(gameObject);
        }
    }

}
