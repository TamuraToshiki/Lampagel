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

public class GuardMode : PlayerManager
{
    // ��~
    private Stop stop;

    // �o�[�X�g
    private UIGauge UIgauge;
    private GuardBurst burst;

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
    protected override void Start()
    {
        base.Start();

        stop = GetComponent<Stop>();
        burst = GetComponent<GuardBurst>();
        UIgauge = GetComponent<UIGauge>();

        // �̗͖��^��
        fGuardGauge = fMaxGuardGauge;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // UI
        UIgauge.Refresh(fMaxGuardGauge, fGuardGauge);
        // �n�[�h���[�h����Ȃ��Ȃ�Q�[�W��
        if (!IsHard)
        {
            RecoveryGauge();
        }
        // �n�[�h���[�h�Ȃ�Q�[�W����
        if(IsHard)
        {
            SubtractGauge();
        }
        // �o�[�X�g���[�h�Ȃ�
        if(IsBurst)
        {
            // ����
            burst.Explode();
            GotoNormalState();
        }

        // �n�[�h���[�h���Q�[�W�c�ʂ�����Ȃ��~
        if(IsHard && fGuardGauge > 0)
        {
            stop.DoStop(rb);
        }
        else
        {
            GotoNormalState();
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
