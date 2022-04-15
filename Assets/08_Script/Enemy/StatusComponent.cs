//======================================================================
// StatusComponent.cs
//======================================================================
// �J������
//
// 2022/03/05 author�F�����x ����J�n�@�X�e�[�^�X����
// 2022/03/15 author�F�����x �X�e�[�^�X�����ύX
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class StatusComponent : MonoBehaviour
{
    public int Hp { get; set; } = 1;
    // ��{�X�e�[�^�X
    [SerializeField] private int nLevel    = 1;
    [SerializeField] private int nHp       = 20;
    [SerializeField] private int nAttack   = 10;
    [SerializeField] private float fSpeed  = 1.0f;

    // ���x���A�b�v���̃X�e�[�^�X�㏸�l
    [SerializeField] private int nUpHP     = 1;
    [SerializeField] private int nUpAttack = 1;

    public int Level { get { return nLevel; } set{ nLevel = value; } }
    public int HP { get { return nHp; } set { nHp = value; } }
    public int Attack { get { return nAttack; } set { nAttack = value; } }
    public float Speed { get { return fSpeed; } set { fSpeed = value; } }
    public int UpHP { get { return nUpHP; } set { nUpHP = value; } }
    public int UpAttack { get { return nUpAttack; } set { nUpAttack = value; } }


}
