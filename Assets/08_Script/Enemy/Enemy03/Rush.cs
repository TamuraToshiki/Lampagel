//======================================================================
// Rush.cs
//======================================================================
// �J������
//
// 2022/03/05 author�F�����x ����J�n�@
// 2022/03/28 author�F�|���@���}�@�v���C���[�ւ̃_���[�W����
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rush : MonoBehaviour
{
    GameObject player;
    GameObject enemy;
    public void SetPlayer(GameObject obj) { player = obj; }
    public void SetEnemy(GameObject obj) { enemy = obj; }

    void Update()
    {
        // �G���S���A�����蔻��p�̃L���[�u��������
        if (enemy == null)
        {
            Destroy(gameObject);
            return;
        }

        // �ːi����
        enemy.transform.position += enemy.transform.forward * (Time.deltaTime * 5.0f);

        // �����蔻��L���[�u���G�ƈꏏ�ɓ���
        transform.position = new Vector3(enemy.transform.position.x + enemy.transform.forward.x,
                                         enemy.transform.position.y,
                                         enemy.transform.position.z + enemy.transform.forward.z);

        // ��b�ŏ���
        Destroy(gameObject, 1.0f);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            // �_���[�W����
            player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<StatusComponent>().Attack);

            Destroy(gameObject);
        }
    }


}
