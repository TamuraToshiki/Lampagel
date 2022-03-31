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

public class GuardMode : MonoBehaviour
{
    // ��~
    private Stop stop;

    // �o�[�X�g
    private GuardBurst burst;

    // ���W�b�h�{�f�B
    private Rigidbody rb;

    // �X�e�[�g
    private PlayerState state;

    // �K�[�h�Q�[�W
    private PlayerStatus status;
    [SerializeField] private int nRecovery = 2;
    [SerializeField] private int nCost = 1;

    // �����З͂����[
    private float fStockBurst = 0.0f;

    //*���}* �G�t�F�N�g�X�N���v�g
    [SerializeField] AID_PlayerEffect effect;

    // Start is called before the first frame update
    void Start()
    {
        stop = GetComponent<Stop>();
        burst = GetComponent<GuardBurst>();
        state = GetComponent<PlayerState>();
        rb = GetComponent<Rigidbody>();
        status = GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("�o�[�X�g��:" + fStockBurst);

        // �n�[�h���[�h����Ȃ��Ȃ�Q�[�W��
        if (!state.IsHard)
        {
            RecoveryGauge();
            this.transform.localScale = new Vector3(1, 1, 1);
            //fStockBurst = 0.0f;
        }
        // �n�[�h���[�h�Ȃ�Q�[�W����
        if(state.IsHard)
        {
            SubtractGauge();
            this.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        }
        // �o�[�X�g���[�h�Ȃ�
        if(state.IsBurst)
        {
            //*���}*
            effect.StartEffect(1, this.gameObject, 1.0f);

            // ����
            burst.Explode(fStockBurst);
            // �u�ԓI�ɗ͂������Ă͂���
            rb.AddForce(transform.forward * fStockBurst, ForceMode.Impulse);
            state.GotoNormalState();
            fStockBurst = 0.0f;
        }

        // �n�[�h���[�h���Q�[�W�c�ʂ�����Ȃ��~
        if(state.IsHard && status.Stamina > 0)
        {
            stop.DoStop(rb);

            //*���}*
            effect.StartEffect(2, this.gameObject, 1.0f);
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
