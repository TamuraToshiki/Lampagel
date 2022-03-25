//======================================================================
// PlayerStatus.cs
//======================================================================
// �J������
//
// 2022/03/22 author�F�c���q�� ����J�n statuscomponent�ƕς��\������
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    // ��{�X�e�[�^�X(�ő�)
    private int nMaxLevel = 1;
    private int nMaxHp = 20;
    private int nMaxStamina = 300;
    private int nMaxAttack = 10;
    private int nMaxExp = 10;

    // ��{�X�e�[�^�X(����)
    private int nLevel = 1;
    private int nHp = 20;
    private int nStamina = 300;
    private int nAttack = 10;
    private float fSpeed = 1.0f;
    private int nExp = 0;

    // ���x���A�b�v���̃X�e�[�^�X�㏸�l
    private int nUpHP = 1;
    private int nUpExp = 20;
    private int nUpAttack = 1;

    // �ő�X�e�[�^�X���Q��
    public int MaxLevel { get { return nMaxLevel; } set { nMaxLevel = value; } }
    public int MaxHP { get { return nMaxHp; } set { nMaxHp = value; } }
    public int MaxStamina { get { return nMaxStamina; } set { nMaxStamina = value; } }
    public int MaxAttack { get { return nMaxAttack; } set { nMaxAttack = value; } }
    public int MaxExp { get { return nMaxExp; } set { nMaxExp = value; } }

    // ���݃X�e�[�^�X���Q��
    public int Level { get { return nLevel; } set { nLevel = value; } }
    public int HP { get { return nHp; } set { nHp = value; } }
    public int Stamina { get { return nStamina; } set { nStamina = value; } }
    public int Attack { get { return nAttack; } set { nAttack = value; } }
    public float Speed { get { return fSpeed; } set { fSpeed = value; } }
    public int Exp { get { return nExp; } set { nExp = value; } }

    // ���x���A�b�v�X�e�[�^�X���Q��
    public int UpHP { get { return nUpHP; } set { nUpHP = value; } }
    public int UpExp { get { return nUpExp; } set { nExp = value; } }
    public int UpAttack { get { return nUpAttack; } set { nUpAttack = value; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
