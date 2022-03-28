//======================================================================
// Item.cs
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

public class Item : MonoBehaviour
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
        // ��
        nStatus = Random.Range(0, (int)eItem.eMax - 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            switch(nStatus)
            {
                // TODO:�v���C���[�̃X�e�[�^�X�㏸�l�A�m�肵����
                case (int)eItem.eHp:
                    Debug.Log("HP�A�C�e���擾");
                    break;

                case (int)eItem.eStamina:
                    Debug.Log("�X�^�~�i�A�C�e���擾");
                    break;

                case (int)eItem.eAttack:
                    Debug.Log("�U���A�C�e���擾");
                    break;

                case (int)eItem.eSpeed:
                    Debug.Log("�X�s�[�h�A�C�e���擾");
                    break;

                case (int)eItem.eTelescopic:
                    Debug.Log("�L�k�A�C�e���擾");
                    break;

                case (int)eItem.eBurst:
                    Debug.Log("�o�[�X�g�A�C�e���擾");
                    break;

                case (int)eItem.eGuard:
                    Debug.Log("�h��A�C�e���擾");
                    break;

                case (int)eItem.eHighBound:
                    Debug.Log("�n�C�o�E���h�A�C�e���擾");
                    break;

                case (int)eItem.eFire:
                    Debug.Log("�΃A�C�e���擾");
                    break;

                case (int)eItem.eWater:
                    Debug.Log("���A�C�e���擾");
                    break;

                case (int)eItem.eThunder:
                    Debug.Log("���A�C�e���擾");
                    break;

                case (int)eItem.eDivision:
                    Debug.Log("����A�C�e���擾");
                    break;

                case (int)eItem.eAutoHeal:
                    Debug.Log("�I�[�g�񕜃A�C�e���擾");
                    break;

                case (int)eItem.eStaminaHeal:
                    Debug.Log("�X�^�~�i�񕜃A�C�e���擾");
                    break;

                case (int)eItem.eCombo:
                    Debug.Log("�R���{�A�C�e���擾");
                    break;

                case (int)eItem.eDrain:
                    Debug.Log("�h���C���A�C�e���擾");
                    break;

                case (int)eItem.eReborn:
                    Debug.Log("���{�[���A�C�e���擾");
                    break;

                case (int)eItem.eWall:
                    Debug.Log("�ǃA�C�e���擾");
                    break;

                case (int)eItem.eAura:
                    Debug.Log("�o�[�X�g�I�[���A�C�e���擾");
                    break;

                case (int)eItem.eSonicBoom:
                    Debug.Log("�\�j�b�N�u�[���A�C�e���擾");
                    break;

                default:
                    Debug.Log("�s���A�C�e��");
                    break;
            }
            CountList[nStatus]++;
            Debug.Log(nStatus + "�Ԗڂ̃I�u�W�F�N�g��" + CountList[nStatus] + "�擾���Ă��܂�");

            Destroy(this.gameObject);
            CreateItem();
        }
    }

    // �V�K�I�u�W�F�N�g�̐����֐�
    public void CreateItem()
    {
        Vector3 vPos;

        vPos = new Vector3(Random.Range(-InstantiateX, InstantiateX), transform.position.y, Random.Range(-InstantiateZ, InstantiateZ));
        nStatus = Random.Range(0, (int)eItem.eMax - 1);
        Instantiate(ItemObjectList[nStatus], vPos, Quaternion.identity);
        Debug.Log(nStatus);
    }
}
