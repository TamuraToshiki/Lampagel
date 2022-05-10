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
        eDie,
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

    // �d���t���O
    public bool bGuard = false;

    // ���݃��[�h�擾
    public bool IsNormal => eState == StateEnum.eNormal;
    public bool IsHard => eState == StateEnum.eHard;
    public bool IsBurst => eState == StateEnum.eBurst;
    public bool IsDie => eState == StateEnum.eDie;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDie) return;

        // �L�[�{�[�h�ړ� 
        if (IsDoubleTrigger(Input.GetMouseButtonDown(0), Input.GetMouseButtonDown(1)) ||
        Input.GetAxis("LTrigger") >= 0.3f && Input.GetAxis("RTrigger") >= 0.3f)
        {
            GotoHardState();
        }

        // �o�[�X�g�ڍs
        if (IsDoubleTrigger(Input.GetMouseButtonUp(0), Input.GetMouseButtonUp(1)) 
            || Input.GetAxis("LTrigger") <= 0.2f && Input.GetAxis("RTrigger") <= 0.2f)
        {
            if(bGuard)
            {
                GotoBurstState();
            }
        }
    }

    public bool IsDoubleTrigger(bool LB, bool RB)
    {
        if (LB) bLflg = true;
        if (RB) bRflg = true;

        if (bLflg || bRflg)
        {
            time += Time.deltaTime;

            if (time <= fInterbalTime)
            {
                if (LB && bRflg)
                {
                    bLflg = false;
                    bRflg = false;
                    bGuard = true;
                    time = 0.0f;
                    return true;
                }
                if (RB && bLflg)
                {
                    bLflg = false;
                    bRflg = false;
                    bGuard = true;
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
        bGuard = false;
        return false;
    }

    // �m�[�}�����[�h�Ɉڍs
    public void GotoNormalState()
    {
        if (!IsDie)
            eState = StateEnum.eNormal;
    }

    // �n�[�h���[�h�Ɉڍs
    public void GotoHardState()
    {
        if (!IsDie)
            eState = StateEnum.eHard;
    }

    // �o�[�X�g���[�h�Ɉڍs
    public void GotoBurstState()
    {
        if (!IsHard) return;
        eState = StateEnum.eBurst;
    }

    public void GotoDieState()
    {
        eState = StateEnum.eDie;
    }
}
