//======================================================================
// ItemManager.cs
//======================================================================
// �J������
//
// 2022/03/09 author�F���R��P �����J�n(���,�����̉�����)
// 2022/03/15                  �A�C�e���ǉ����̎���(����𑝂₷����)
// 2022/04/21 author�F�|���W�j�Y nItemCount�C���A�����p�ɒ���
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    // �A�C�e���̎��
    enum eItem
    {
        eHp = 0,     // nHp
        eStamina,    // nStamina
        eAttack,     // nAttack
        eSpeed,      // fSpeed
        eBurstRange, // �o�[�X�g�͈�
        eBurstDamage,// �o�[�X�g�_���[�W
        //eGuard,      // �����g�p
        //eHighBound,  // �����g�p
        eFire,       // �Α���
        eWater,      // ������
        eThunder,    // ������
        eDivision,   // ����
        eAutoHeal,   // �I�[�g��
        eStaminaHeal,// �X�^�~�i�񕜑��x
        //eCombo,      // �����g�p
        eDrain,      // HP�z��
        eReborn,     // �c�@
        //eWall,       // �����g�p
        //eAura,       // �����g�p
        eSonicBoom,  // ���G����

        eMax
    }

    [Header("�A�C�e���̏o�����W�͈�")] [SerializeField, Range(1.0f, 100.0f)] float InstantiateX = 5.0f;
    [SerializeField, Range(1.0f, 100.0f)] float InstantiateZ = 5.0f;

    public List<GameObject> ItemObjectList;
    public List<int> CountList;
    public List<int> DrawItemList;
    public int nStatus;

    // �A�C�e���摜�f�[�^�i�|�j
    public ItemImage itemImage;
    public GameObject imageObject;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < (int)eItem.eMax + 1; i++)
        {
            CountList.Add(0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            nStatus = Random.Range(0, (int)eItem.eMax - 1);
            nItemCount(nStatus);
        }
    }

    // �V�K�I�u�W�F�N�g�̐����֐�
    public void CreateItem()
    {
        Vector3 vPos;

        vPos = new Vector3(Random.Range(-InstantiateX, InstantiateX), transform.position.y, Random.Range(-InstantiateZ, InstantiateZ));
        nStatus = Random.Range(0, (int)eItem.eMax - 1);
        Instantiate(ItemObjectList[nStatus], vPos, Quaternion.identity);
    }

    // �I�u�W�F�N�g�̎擾�����J�E���g
    public int nItemCount(int itemID)
    {
        CountList[itemID]++;
        //Debug.Log(itemID + "�ԃA�C�e���F" + CountList[itemID] + "��");

        if (CountList[itemID] == 1)
        {
            DrawItemList.Add(itemID);
            imageObject.GetComponent<Image>().sprite = itemImage.ItemImageList[itemID];
            GetComponent<ItemUI>().CreateItemUI(DrawItemList.Count, imageObject);
        }
        //Debug.Log(CountList[itemID]);
        return CountList[itemID];
    }

    /* �ȉ��A���x���A�b�v�����p�֐� */

    // �I�u�W�F�N�g�̃A�C�e���ԍ��n��
    public int nStatusNum()
    {
        // �����_���Ȕԍ��𐶐����A�����Ԃ�
        int num = Random.Range(0, (int)eItem.eMax - 1);
        return num;
    }

    // �I�u�W�F�N�g�̎擾���̃J�E���g�A�b�v
    // �����F�擾�����A�C�e���̃A�C�e���ԍ�
    public void nCountup(int nItemStatus)
    {
        CountList[nItemStatus]++;
    }

    // �A�C�e�����I�i�|���j
    public int nItemGacha()
    {
        int nItemID = 0;
        nItemID = Random.Range(0, (int)eItem.eMax - 1);
        return nItemID;
    }

    // �A�C�e���摜�n���i�|�j
    public Sprite setItemIcon(int itemID)
    {
        Sprite image;
        image = itemImage.ItemImageList[itemID];
        return image;
    }
}
