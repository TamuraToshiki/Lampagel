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
    [SerializeField] private ItemManager itemManager;

    // ��{�X�e�[�^�X(�ő�)
    private int nMaxLevel = 1;
    private int nMaxHp = 100;
    private int nMaxStamina = 450;
    private int nMaxAttack = 2200;
    private int nMaxExp = 100;
    private float fMaxBurstPower = 3000;
    private float fMaxBurstRadius = 5;

    // ��{�X�e�[�^�X(����)
    private int nLevel = 1;
    private int nHp = 100;
    private int nStamina = 450;
    private int nAttack = 2200;
    private float fSpeed = 100.0f;
    private int nExp = 0;
    private float fBurstStock = 5.0f;
    private float fBurstDamage = 1.0f;
    private float fBurstPower = 5;
    private float fBurstRadius = 1;

    // �ǉ�
    private int nStaminaRecovery { get; set; } = 2;
    private int nStaminaCost { get; set; } = 1;

    // �K�[�h�y�i���e�B����
    public float fGuardPenalty { get; set; } = 1.0f;

    // �A�C�e���̃X�e�[�^�X�㏸�l
    private int nUpHP = 1;
    private int nUpStamina = 10;
    private float fUpSpeed = 5.0f;
    private int nUpExp = 50;
    private int nHeal = 1;
    private int nUpAttack = 6;

    // �ړ��Ɋւ���f�[�^
    public float fBreakTime { get; set; } = 0.0f;

    // ���G�t���O
    public bool bArmor { get; set; } = false;
    public float fArmorTime { get; set; } = 0.0f;

    // �ő�X�e�[�^�X���Q��
    public int MaxLevel { get { return nMaxLevel; } set { nMaxLevel = value; } }
    // �ő�̗͑��₷
    public int MaxHP { get { return nMaxHp + (itemManager.CountList[(int)ItemManager.eItem.eHp] * nUpHP); } set { nMaxHp = value; } }
    // �ő�X�^�~�i���₷
    public int MaxStamina { get { return nMaxStamina + (itemManager.CountList[(int)ItemManager.eItem.eStamina] * nUpStamina); } set { nMaxStamina = value; } }
    public int MaxAttack { get { return nMaxAttack; } set { nMaxAttack = value; } }
    public int MaxExp { get { return nMaxExp; } set { nMaxExp = value; } }
    public float MaxBurstPower { get { return fMaxBurstPower; } set { fMaxBurstPower = value; } }
    public float MaxBurstRadisu { get { return fMaxBurstRadius; } set { fMaxBurstRadius = value; } }

    // ���݃X�e�[�^�X���Q��
    public int Level { get { return nLevel; } set { nLevel = value; } }
    public int HP { get { return nHp; } set { nHp = value; } }
    public int Stamina { get { return nStamina; } set { nStamina = value; } }
    // �U���͂𑝂₷
    public int Attack { get { return nAttack + (itemManager.CountList[(int)ItemManager.eItem.eAttack] * nUpAttack); } set { nAttack = value; } }
    // �X�s�[�h�𑬂�����
    public float Speed { get { return fSpeed + (itemManager.CountList[(int)ItemManager.eItem.eSpeed] * fUpSpeed); } set { fSpeed = value; } }
    public int Exp { get { return nExp; } set { nExp = value; } }
    public float BurstStock { get { return fBurstStock; } set { fBurstStock = value; } }
    // �o�[�X�g�_���[�W�𑝂₷
    public float BurstDamage { get { return fBurstDamage + (itemManager.CountList[(int)ItemManager.eItem.eBurstDamage] * fBurstDamage); } set { fBurstDamage = value; } }
    public float BurstPower { get { return fBurstPower; } set { fBurstPower = value; } }
    // �o�[�X�g�͈͂𑝂₷
    public float BurstRadisu { get { return fBurstRadius + (itemManager.CountList[(int)ItemManager.eItem.eBurstRange] * fBurstRadius); } set { fBurstRadius = value; } }
    public int StaminaRecovery { get { return nStaminaRecovery + (itemManager.CountList[(int)ItemManager.eItem.eStaminaHeal] * nStaminaRecovery); } }
    public int StaminaCost { get { return nStaminaCost; } }

    // ���x���A�b�v�X�e�[�^�X���Q��
    public int UpHP { get { return nUpHP + (nUpHP * itemManager.CountList[(int)ItemManager.eItem.eAutoHeal]); } set { nUpHP = value; } }
    public int UpExp { get { return nUpExp; } set { nExp = value; } }
    public int UpAttack { get { return nUpAttack; } set { nUpAttack = value; } }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            // 1�ȏ�Ȃ玝���Ă���
            if(itemManager.CountList[(int)ItemManager.eItem.eFire] >= 1)
            {
                for(int i = 0;i <= itemManager.CountList[(int)ItemManager.eItem.eFire]; i++)
                {
                    other.gameObject.AddComponent<BurningAttribute>();                    
                }
                for(int i = 0; i <= itemManager.CountList[(int)ItemManager.eItem.eDrain]; i++)
                {
                    Debug.Log("aaa");
                    HP += 2;
                }
            }
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        // ���݃V�[��
        //Scene scene = SceneManager.GetSceneByName("Stage1-1");
        //SceneManager.SetActiveScene(scene);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
