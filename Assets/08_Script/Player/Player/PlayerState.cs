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

    private bool m_isLAxisInUse = false;
    private bool m_isRAxisInUse = false;

    GuardMode guardMode;

    // �d���t���O
    public bool bGuard = false;
    public bool bPadGuard = false;

    // ���݃��[�h�擾
    public bool IsNormal => eState == StateEnum.eNormal;
    public bool IsHard => eState == StateEnum.eHard;
    public bool IsBurst => eState == StateEnum.eBurst;
    public bool IsDie => eState == StateEnum.eDie;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        guardMode = GetComponent<GuardMode>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDie) return;

        //// XBox���� ***********************************************
        //if (IsDoubleTrigger_XBOXTriggerDown(Input.GetAxis("LTrigger"), Input.GetAxis("RTrigger")))
        //{
        //    if (!guardMode.bGuardPenalty)
        //    {
        //        GotoHardState();
        //    }
        //}

        //// �o�[�X�g�ڍs
        //if (IsDoubleTrigger_XBOXTriggerUP(Input.GetAxis("LTrigger"), Input.GetAxis("RTrigger")))
        //{
        //    if (bGuard)
        //    {
        //        GotoBurstState();
        //    }
        //}
        //***********************************************************

        // XBox���� ************************************************* 
        //if (IsDoubleTrigger(Input.GetKeyDown("joystick button 4"), Input.GetKeyDown("joystick button 5")))
        //{
        //    if (!guardMode.bGuardPenalty)
        //    {              
        //        GotoHardState();
        //    }

        //}
        if (Input.GetAxis("LTrigger") >= 0.1f && Input.GetAxis("RTrigger") >= 0.1f)
        {
            GotoHardState();
            bPadGuard = true;
        }

        if (Input.GetAxis("LTrigger") <= 0.1f && Input.GetAxis("RTrigger") <= 0.1f)
        {
            if (bPadGuard)
            {
                Debug.Log("aaa");
                // ���ʐU��������
                StartCoroutine("StartVibation");
                GotoBurstState();
                bPadGuard = false;
            }
        }

        // �o�[�X�g�ڍs
        //if (IsDoubleTrigger(Input.GetKeyUp("joystick button 4"), Input.GetKeyUp("joystick button 5")))
        //{
        //    if (bGuard)
        //    {
        //        // ���ʐU��������
        //        StartCoroutine("StartVibation");
        //        GotoBurstState();
        //    }
        //}
        //***********************************************************

        // �L�[�{�[�h����********************************************
        if (IsDoubleTrigger(Input.GetMouseButtonDown(0), Input.GetMouseButtonDown(1)))
        {
            if (!guardMode.bGuardPenalty)
            {
                GotoHardState();
            }
        }

        // �o�[�X�g�ڍs
        if (IsDoubleTrigger(Input.GetMouseButtonUp(0), Input.GetMouseButtonUp(1)))
        {
            if (bGuard)
            {
                
                StartCoroutine(StartVibation());
                GotoBurstState();
            }
        }
        //***********************************************************
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

    // XBOX�R���g���[���[���� **********************************
    public bool IsDoubleTrigger_XBOXTriggerDown(float LB, float RB)
    {
        if (LB != 0)
        {
            if (m_isLAxisInUse == false)
            {
                Debug.Log("������");
                bLflg = true;
                m_isLAxisInUse = true;
            }
        }
        if (LB == 0)
        {

            m_isLAxisInUse = false;
        }

        if (RB != 0)
        {
            if (m_isRAxisInUse == false)
            {
                bRflg = true;
                m_isRAxisInUse = true;
            }
        }
        if (RB == 0)
        {
            m_isLAxisInUse = false;
        }



        if (bLflg || bRflg)
        {
            time += Time.deltaTime;

            if (time <= fInterbalTime)
            {
                if (LB > 0.1 && bRflg)
                {
                    bLflg = false;
                    bRflg = false;
                    bGuard = true;
                    time = 0.0f;
                    return true;
                }
                if (RB > 0.1 && bLflg)
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

    public bool IsDoubleTrigger_XBOXTriggerUP(float LB, float RB)
    {
        
        if (LB != 0)
        {
            if (m_isLAxisInUse == false)
            {
                Debug.Log("������");
                bLflg = true;
                m_isLAxisInUse = true;
            }
        }
        if (LB == 0)
        {

            m_isLAxisInUse = false;
        }

        if (RB != 0)
        {
            if (m_isRAxisInUse == false)
            {
                bRflg = true;
                m_isRAxisInUse = true;
            }
        }
        if (RB == 0)
        {
            m_isLAxisInUse = false;
        }


        if (bLflg || bRflg)
        {
            time += Time.deltaTime;

            if (time <= fInterbalTime)
            {
                if (LB > 0.1 && bRflg)
                {
                    bLflg = false;
                    bRflg = false;
                    bGuard = true;
                    time = 0.0f;
                    return true;
                }
                if (RB > 0.1 && bLflg)
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
    //**********************************************************

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

    // �U���R���[�`��
    IEnumerator StartVibation()
    {
        
        XInputDotNetPure.GamePad.SetVibration(0, 5, 5);
        yield return new WaitForSecondsRealtime(0.5f);
        XInputDotNetPure.GamePad.SetVibration(0, 0, 0);
    }

    
}
