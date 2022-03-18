//======================================================================
// PlayerState.cs
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

public class PlayerState : MonoBehaviour
{
    // ���[�h���
    private enum StateEnum
    {
        eNormal = 0,
        eHard,
        eBurst,
    }
    private StateEnum eState = StateEnum.eNormal;

    // ���W�b�h�{�f�B
    private Rigidbody rb;

    // �o�[�X�g
    //�������������t����
    private float time;
    [SerializeField] private float fInterbalTime = 3;

    // ������������
    bool bLflg = false;
    bool bRflg = false;

    // ���݃��[�h�擾
    public bool IsNormal => eState == StateEnum.eNormal;
    public bool IsHard => eState == StateEnum.eHard;
    public bool IsBurst => eState == StateEnum.eBurst;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // �L�[�{�[�h�ړ� 
        if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
        {
            GotoHardState();
        }
        else
        {
            GotoNormalState();
        }

        // �o�[�X�g�ڍs
        if (IsDoubleTrigger(Input.GetMouseButtonUp(0), Input.GetMouseButtonUp(1)))
        {
            GotoBurstState();
        }
    }

    private bool IsDoubleTrigger(bool LB, bool RB)
    {
        if (LB) bLflg = true;
        if (RB) bRflg = true;

        if(bLflg || bRflg)
        {
            time += Time.deltaTime;

            if(time <= fInterbalTime)
            {
                if(LB && bRflg)
                {
                    bLflg = false;
                    bRflg = false;
                    time = 0.0f;
                    return true;
                }
                if(RB && bLflg)
                {
                    bLflg = false;
                    bRflg = false;
                    time = 0.0f;
                    return true;
                }
            }
            else
            {
                bLflg = false;
                bRflg = false;
                time = 0.0f;
            }
        }
        return false;
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
