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
// 2022/03/27 author�F�c���q�� �����̈З͂�~����@�\����
// 2022/03/28 author�F�|���@���} �G�t�F�N�g�����g�ݍ���
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����R���|�[�l���g�A�^�b�`
[RequireComponent(typeof(Stop))]
[RequireComponent(typeof(GuardBurst))]

public class GuardMode : PlayerManager
{
    // ��~
    private Stop stop;

    // �o�[�X�g
    private GuardBurst burst;

    // �K�[�h�Q�[�W
    private PlayerStatus status;
    [SerializeField] private int nRecovery = 2;
    [SerializeField] private int nCost = 1;

    // �����З͂����[
    private float fStockBurst = 0.0f;

    //*���}* �G�t�F�N�g�X�N���v�g
    [SerializeField] AID_PlayerEffect effect;

    // �K�[�h���f���ƃf�t�H���g���f��
    [SerializeField] private GameObject DefaultModel;
    [SerializeField] private GameObject GuardModel;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        stop = GetComponent<Stop>();
        burst = GetComponent<GuardBurst>();
<<<<<<< HEAD:Assets/08_Script/TamuraScript/GuardMode/GuardMode.cs
        UIgauge = GetComponent<UIGauge>();

        // �̗͖��^��
        fGuardGauge = fMaxGuardGauge;
=======
        state = GetComponent<PlayerState>();
        rb = GetComponent<Rigidbody>();
        status = GetComponent<PlayerStatus>();
>>>>>>> d2f65eada7be6604d61b693afd0e28d3b8accd2c:Assets/08_Script/TamuraScript/Player/GuardMode/GuardMode.cs
    }

    // Update is called once per frame
    protected override void Update()
    {
<<<<<<< HEAD
<<<<<<< HEAD:Assets/08_Script/TamuraScript/GuardMode/GuardMode.cs
        base.Update();

        // UI
        UIgauge.Refresh(fMaxGuardGauge, fGuardGauge);
=======
        Debug.Log(fStockBurst);
>>>>>>> d2f65eada7be6604d61b693afd0e28d3b8accd2c:Assets/08_Script/TamuraScript/Player/GuardMode/GuardMode.cs
        // �n�[�h���[�h����Ȃ��Ȃ�Q�[�W��
        if (!IsHard)
=======
        // �n�[�h���[�h����Ȃ�
        if (!state.IsHard)
>>>>>>> 68d270aaeb83160bd5b19f520e97cbc3b59f9431
        {
            // �Q�[�W��
            RecoveryGauge();
            DefaultModel.SetActive(true);
            GuardModel.SetActive(false);
        }
<<<<<<< HEAD
        // �n�[�h���[�h�Ȃ�Q�[�W����
        if(IsHard)
=======
        // �n�[�h���[�h�Ȃ�
        if(state.IsHard)
>>>>>>> 68d270aaeb83160bd5b19f520e97cbc3b59f9431
        {
            // �Q�[�W����
            SubtractGauge();
            DefaultModel.SetActive(false);
            GuardModel.SetActive(true);
        }
        // �o�[�X�g���[�h�Ȃ�
        if(IsBurst)
        {
            //*���}*
            effect.StartEffect(1, this.gameObject, 1.0f);

            // ����
<<<<<<< HEAD:Assets/08_Script/TamuraScript/GuardMode/GuardMode.cs
            burst.Explode();
            GotoNormalState();
        }

        // �n�[�h���[�h���Q�[�W�c�ʂ�����Ȃ��~
        if(IsHard && fGuardGauge > 0)
=======
            burst.Explode(fStockBurst);
            // �u�ԓI�ɗ͂������Ă͂���
            rb.AddForce(transform.forward * fStockBurst, ForceMode.Impulse);
            state.GotoNormalState();
            fStockBurst = 0.0f;
        }

        // �n�[�h���[�h���Q�[�W�c�ʂ�����Ȃ��~
        if(state.IsHard && status.Stamina > 0)
>>>>>>> d2f65eada7be6604d61b693afd0e28d3b8accd2c:Assets/08_Script/TamuraScript/Player/GuardMode/GuardMode.cs
        {
            stop.DoStop(rb);

            //*���}*
            effect.StartEffect(2, this.gameObject, 1.0f);
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
        status.Stamina += nRecovery;
        if(status.Stamina >= status.MaxStamina)
        {
            status.Stamina = status.MaxStamina;
        }
    }

    // �Q�[�W����
    private void SubtractGauge()
    {
        status.Stamina -= nCost;
        if (status.Stamina < 0)
        {
            status.Stamina = 0;
        }
    }

    public void AddStockExplode(float damage)
    {
        fStockBurst += damage;
    }
}
