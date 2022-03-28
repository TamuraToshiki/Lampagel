//======================================================================
// PlayerExp.cs
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

public class PlayerExp : MonoBehaviour
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

    // �o���l�㏸
    public void AddExp(int exp)
    {
        status.Exp += exp;

        if(status.MaxExp <= status.Exp)
        {
            LevelUp();
        }
    }

    // ���x���A�b�v
    public void LevelUp()
    {
        status.Level++;

        // ����Exp��0�ɂ���
        status.Exp = 0;
        // ���̃��x���A�b�v�܂ł̌o���l�ʂ𑝂₷
        status.MaxExp += status.UpExp;
        status.Attack += status.UpAttack;
    }
}
