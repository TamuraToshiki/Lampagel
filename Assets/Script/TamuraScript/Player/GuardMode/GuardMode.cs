//======================================================================
// GuardMode.cs
//======================================================================
// �J������
//
// 2022/03/01 author�F�c���q�� �K�[�h��Ԃ��Ǘ�����X�N���v�g
//                             �~�܂�@�\����
// 2022/03/03 author�F�c���q�� �K�[�h�Q�[�W�Ȃǂ̃K�[�h�ɕK�v�ȋ@�\����
// 2022/03/11 author�F�c���q�� UI�@�\����(���Ԃ��Ȃ������߁A��蒼������)
//                             ��������(�G�Ɍ��ʂȂ�...)
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����R���|�[�l���g�A�^�b�`
[RequireComponent(typeof(Stop))]
[RequireComponent(typeof(UIGauge))]
[RequireComponent(typeof(GuardBurst))]

public class GuardMode : MonoBehaviour
{
    // ��~
    private Stop stop;

    // �o�[�X�g
    private UIGauge UIgauge;
    private GuardBurst burst;

    // ���W�b�h�{�f�B
    private Rigidbody rb;

    // �X�e�[�g
    private PlayerState state;

    // �K�[�h�Q�[�W
    [SerializeField] private float fMaxGuardGauge;
    private float fGuardGauge;
    private bool bGuardGauge;
    [SerializeField] private float fRecovery;
    [SerializeField] private float cost;

    // �Q�[�W�擾
    public float GetMaxGuardGauge => fMaxGuardGauge;
    public float GetGuardGauge => fGuardGauge;

    // Start is called before the first frame update
    void Start()
    {
        stop = GetComponent<Stop>();
        burst = GetComponent<GuardBurst>();
        UIgauge = GetComponent<UIGauge>();
        state = GetComponent<PlayerState>();
        rb = GetComponent<Rigidbody>();

        // �̗͖��^��
        fGuardGauge = fMaxGuardGauge;
    }

    // Update is called once per frame
    void Update()
    {
        // UI
        UIgauge.Refresh(fMaxGuardGauge, fGuardGauge);
        // �n�[�h���[�h����Ȃ��Ȃ�Q�[�W��
        if (!state.IsHard)
        {
            RecoveryGauge();
        }
        // �n�[�h���[�h�Ȃ�Q�[�W����
        if(state.IsHard)
        {
            SubtractGauge();
        }
        // �o�[�X�g���[�h�Ȃ�
        if(state.IsBurst)
        {
            // ����
            burst.Explode();
            state.GotoNormalState();
        }

        // �n�[�h���[�h���Q�[�W�c�ʂ�����Ȃ��~
        if(state.IsHard && fGuardGauge > 0)
        {
            stop.DoStop(rb);
        }
        else
        {
            state.GotoNormalState();
        }
    }

    // �Q�[�W��
    private void RecoveryGauge()
    {
        // �Q�[�W�ʉ�
        fGuardGauge += fRecovery;
        if(fGuardGauge >= fMaxGuardGauge)
        {
            fGuardGauge = fMaxGuardGauge;
        }
    }

    // �Q�[�W����
    private void SubtractGauge()
    {
        fGuardGauge -= cost;
        if (fGuardGauge < 0)
        {
            fGuardGauge = 0;
        }
    }
}
