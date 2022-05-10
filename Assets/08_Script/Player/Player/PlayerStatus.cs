//======================================================================
// PlayerStatus.cs
//======================================================================
// �J������
//
// 2022/03/22 author�F�c���q�� ����J�n statuscomponent�ƕς��\������
// 2022/03/28 suthor�F�|���@���}�@nMaxStamina fMaxBurstPower fMaxBurstRadius nStamina fBurstPower fBurstRadius �l�ύX
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    // ��{�X�e�[�^�X(�ő�)
    private int nMaxLevel = 1;
    private int nMaxHp = 40;
    private int nMaxStamina = 450;
    private int nMaxAttack = 22;
    private int nMaxExp = 10;
    private float fMaxBurstPower = 5;
    private float fMaxBurstRadius = 1;

    // ��{�X�e�[�^�X(����)
    private int nLevel = 1;
    private int nHp = 40;
    private int nStamina = 450;
    private int nAttack = 22;
    private float fSpeed = 1.0f;
    private int nExp = 0;
    private float fBurstStock = 5.0f;
    private float fBurstDamage = 1.0f;
    private float fBurstPower = 5;
    private float fBurstRadius = 1;

    // �K�[�h�y�i���e�B����
    public float fGuardPenalty { get; set; } = 1.0f;

    // ���x���A�b�v���̃X�e�[�^�X�㏸�l
    private int nUpHP = 10;
    private int nUpStamina = 10;
    private int nUpExp = 20;
    private int nUpAttack = 6;

    // �ړ��Ɋւ���f�[�^
    public float fBreakTime { get; set; } = 0.0f;

    // ���G�t���O
    public bool bArmor { get; set; } = false;
    public float fArmorTime { get; set; } = 0.0f;

    // �ő�X�e�[�^�X���Q��
    public int MaxLevel { get { return nMaxLevel; } set { nMaxLevel = value; } }
    public int MaxHP { get { return nMaxHp; } set { nMaxHp = value; } }
    public int MaxStamina { get { return nMaxStamina; } set { nMaxStamina = value; } }
    public int MaxAttack { get { return nMaxAttack; } set { nMaxAttack = value; } }
    public int MaxExp { get { return nMaxExp; } set { nMaxExp = value; } }
    public float MaxBurstPower { get { return fMaxBurstPower; } set { fMaxBurstPower = value; } }
    public float MaxBurstRadisu { get { return fMaxBurstRadius; } set { fMaxBurstRadius = value; } }

    // ���݃X�e�[�^�X���Q��
    public int Level { get { return nLevel; } set { nLevel = value; } }
    public int HP { get { return nHp; } set { nHp = value; } }
    public int Stamina { get { return nStamina; } set { nStamina = value; } }
    public int Attack { get { return nAttack; } set { nAttack = value; } }
    public float Speed { get { return fSpeed; } set { fSpeed = value; } }
    public int Exp { get { return nExp; } set { nExp = value; } }
    public float BurstStock { get { return fBurstStock; } set { fBurstStock = value; } }
    public float BurstDamage { get { return fBurstDamage; } set { fBurstDamage = value; } }
    public float BurstPower { get { return fBurstPower; } set { fBurstPower = value; } }
    public float BurstRadisu { get { return fBurstRadius; } set { fBurstRadius = value; } }

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
