//======================================================================
// ItemManager.cs
//======================================================================
// �J������
//
// 2022/03/09 author�F���R��P �����J�n(���,�����̉�����)
// 2022/03/15                  �A�C�e���ǉ����̎���(����𑝂₷����)
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // �A�C�e���̎��
    enum eItem
    {
        eHp = 0,
        eStamina,
        eAttack,
        eSpeed,
        eTelescopic,
        eBurst,
        eGuard,
        eHighBound,
        eFire,
        eWater,
        eThunder,
        eDivision,
        eAutoHeal,
        eStaminaHeal,
        eCombo,
        eDrain,
        eReborn,
        eWall,
        eAura,
        eSonicBoom,

        eMax
    }

    [Header("�A�C�e���̏o�����W�͈�")] [SerializeField, Range(1.0f, 100.0f)] float InstantiateX = 5.0f;
    [SerializeField, Range(1.0f, 100.0f)] float InstantiateZ = 5.0f;

    public List<GameObject> ItemObjectList;
    public List<int> CountList;
    public int nStatus;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < (int)eItem.eMax; i++)
        {
            CountList.Add(0);
        }

        // ��
        nStatus = Random.Range(0, (int)eItem.eMax - 1);
        CreateItem();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    public int nItemCount()
    {
        CountList[nStatus]++;
        Debug.Log(nStatus + "�ԃA�C�e���F" + CountList[nStatus] + "��");

        return CountList[nStatus];
    }
}
