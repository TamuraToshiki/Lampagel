using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    PlayerStatus status;

    // Start is called before the first frame update
    void Start()
    {
        status = GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �_���[�W�R�[���o�b�N�֐�
    public void OnDamage(int damage)
    {
        // 0�ȉ��Ȃ玀��ł邽�߃��^�[��
        if (status.HP <= 0) return;

        // �_���[�W��^����
        status.HP -= damage;
    }
}
