//======================================================================
// PlayerHP.cs
//======================================================================
// �J������
//
// 2022/03/25 author�F�c���q�� �쐬�J�n
// 2022/03/27 author�F�c���q�� hard���[�h�Ȃ疳��������@�\�ǉ�
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    PlayerStatus status;
    PlayerState state;

    // Start is called before the first frame update
    void Start()
    {
        status = GetComponent<PlayerStatus>();
        state = GetComponent<PlayerState>();
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
        // �n�[�h���[�h�Ȃ疳��
        if (state.IsHard)
        {
            // damage���X�g�b�N����
            this.GetComponent<GuardMode>().AddStockExplode(damage);
            return;
        }

        // �_���[�W��^����
        status.HP -= damage;
    }
}
