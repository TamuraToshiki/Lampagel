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
    private int nUpCount;

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
        if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
        {
            GotoHardState();
        }

        // �o�[�X�g�ڍs
        if(Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            nUpCount++;
            StartCoroutine(IsPossibleBurst());
        }
    }

    IEnumerator IsPossibleBurst()
    {
        // 0.3�b�ҋ@
        yield return new WaitForSeconds(0.3f);

        if(nUpCount >= 2)
        {
            GotoBurstState();
        }
        else
        {
            GotoNormalState();
        }
        nUpCount = 0;

        yield break;
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
