using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMode : PlayerManager
{
    // ��~
    Stop stop;

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

        // �̗͖��^��
        fGuardGauge = fMaxGuardGauge;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // �n�[�h���[�h����Ȃ��Ȃ��
        if (!IsHard)
        {
            RecoveryGauge();
        }
        if(IsHard)
        {
            SubtractGauge();
        }

        // �n�[�h���[�h���Q�[�W�c�ʂ�����Ȃ��~
        if(IsHard && fGuardGauge > 0)
        {
            stop.DoStop(rb);
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
