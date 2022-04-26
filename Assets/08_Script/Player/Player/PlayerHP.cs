//======================================================================
// PlayerHP.cs
//======================================================================
// �J������
//
// 2022/03/25 author�F�c���q�� �쐬�J�n
// 2022/03/27 author�F�c���q�� hard���[�h�Ȃ疳��������@�\�ǉ�
// 2022/03/28 author�F�|���@���} �G�t�F�N�g�����g�ݍ���
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    PlayerStatus status;
    PlayerState state;

    //*���}* �G�t�F�N�g�X�N���v�g
    [SerializeField] AID_PlayerEffect effect;

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
        if (state.IsDie) return;
        if (status.bArmor) return;
        // �n�[�h���[�h�Ȃ疳��
        if (state.IsHard)
        {
            //*���}*
            effect.StartEffect(6, this.gameObject, 0.5f);

            // damage���X�g�b�N����
            this.GetComponent<GuardMode>().AddStockExplode(status.BurstStock);
            return;
        }

        //*���}*
        else
        {
            effect.StartEffect(8, this.gameObject, 0.5f);
        }
        //******

        // �_���[�W��^����
        status.HP -= damage;
        if (status.HP <= 0)
        {
            state.GotoDieState();
        }
    }
}
