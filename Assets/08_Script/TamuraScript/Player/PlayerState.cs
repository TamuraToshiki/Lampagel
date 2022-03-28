//======================================================================
// PlayerManager.cs
//======================================================================
// �J������
//
// 2022/03/01 author�F�c���q�� ��ԑJ�ڂ��s���X�N���v�g�쐬
// 2022/03/03 author�F�c���q�� �e�N���X�ɂ��邽��abstract�ɂ���
// 2022/03/11 author�F�c���q�� �o�[�X�g��Ԓǉ�(�o�O����)
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerManager : MonoBehaviour
{
    // ���[�h���
    protected enum StateEnum
    {
        eNormal = 0,
        eHard,
        eBurst,
    }
    static protected StateEnum eState = StateEnum.eNormal;

    // ���W�b�h�{�f�B
    protected Rigidbody rb;

    // �o�[�X�g
    //�������������t����
    [SerializeField] private int interbalTime = 3;

    // ������������
    private bool b_release = false;

    // ��t�p
    private bool b_receiptA = false, b_receiptB = false;
    private int n_interbalA = 0, n_interbalB = 0;

    // ���݃��[�h�擾
    public bool IsNormal => eState == StateEnum.eNormal;
    public bool IsHard => eState == StateEnum.eHard;
    public bool IsBurst => eState == StateEnum.eBurst;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // �L�[�{�[�h�ړ� 
        if (Input.GetMouseButton(0) && Input.GetMouseButton(1) ||
            Input.GetAxis("LTrigger") >= 0.3f && Input.GetAxis("RTrigger") >= 0.3f)
        {
            GotoHardState();
        }
        else
        {
            GotoNormalState();
        }

        // �o�[�X�g�ڍs
        if (Check(Input.GetMouseButtonUp(0), Input.GetMouseButtonUp(1)))
        {
            GotoBurstState();
        }
    }

    // ���������`�F�b�N�֐�(�|���쐬)
    bool Check(bool KeyA, bool KeyB)
    {
        // KeyA ��t
        if (KeyA)
        {
            b_receiptA = true;
        }

        if (b_receiptA)
        {
            Debug.Log("A��t��" + n_interbalA);
            n_interbalA++;
        }

        if (n_interbalA > interbalTime)
        {
            Debug.Log("A��t��~");
            b_receiptA = false;
            n_interbalA = 0;
        }


        // KeyB ��t
        if (KeyB)
        {
            b_receiptB = true;
        }

        if (b_receiptB)
        {
            Debug.Log("B��t��" + n_interbalB);
            n_interbalB++;
        }

        if (n_interbalB > interbalTime)
        {
            Debug.Log("B��t��~");
            b_receiptB = false;
            n_interbalB = 0;
        }

        // ����
        if (b_receiptA && b_receiptB)
        {
            b_release = true;
            return b_release;
        }

        b_release = false;

        return b_release;
    }

    // �m�[�}�����[�h�Ɉڍs
    public void GotoNormalState()
    {
        eState = StateEnum.eNormal;
    }

    // �n�[�h���[�h�Ɉڍs
    public void GotoHardState()
    {
        eState = StateEnum.eHard;
    }

    // �o�[�X�g���[�h�Ɉڍs
    public void GotoBurstState()
    {
        eState = StateEnum.eBurst;
    }
}
