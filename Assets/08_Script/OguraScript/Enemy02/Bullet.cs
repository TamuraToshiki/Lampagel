
//======================================================================
// Bullet.cs
//======================================================================
// �J������
//
// 2022/03/05 author�F�����x ����J�n�@�G�̉������U������
// 2022/03/28 author�F�|���@���}�@�v���C���[�ւ̃_���[�W����
// 2022/03/30 author�F�����@�G�t�F�N�g�����̒ǉ�
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

<<<<<<< HEAD
<<<<<<< HEAD:Assets/Script/OguraScript/Enemy02/Bullet.cs
    // Update is called once per frame
=======

>>>>>>> d2f65eada7be6604d61b693afd0e28d3b8accd2c:Assets/08_Script/OguraScript/Enemy02/Bullet.cs
=======
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
>>>>>>> e2853f8ad6986fc67b6af3dfd7a583e04154f030
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
            player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<StatusComponent>().Attack);

            Destroy(effect);
            Destroy(gameObject);
        }
    }

}
