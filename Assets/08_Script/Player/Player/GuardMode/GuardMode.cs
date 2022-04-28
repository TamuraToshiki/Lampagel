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

    // �d�����Ƀ��[�e���邽�߂̕ϐ�
    bool bGuardStart = false;

    //*���}* �G�t�F�N�g�X�N���v�g
    [SerializeField] AID_PlayerEffect effect;

    // �K�[�h���f���ƃf�t�H���g���f��
    [SerializeField] private GameObject DefaultModel;
    [SerializeField] private GameObject GuardModel;

    // �K�[�h���Ɍ����ς��邽�߂̕ϐ�
    private Vector3 vStartPos = Vector3.zero;
    private Vector3 vCurrentForce = Vector3.zero;

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
        if (!state.IsHard)
        {
            // �Q�[�W��
            RecoveryGauge();
            DefaultModel.SetActive(true);
            GuardModel.SetActive(false);
        }
        // �n�[�h���[�h�Ȃ�
        if (state.IsHard)
        {
            if(!bGuardStart)
            {
                vStartPos = GetMousePosition();
                bGuardStart = true;
            }

            // �Q�[�W����
            SubtractGauge();
            DefaultModel.SetActive(false);
            GuardModel.SetActive(true);

            // ���������}�E�X���W�̈ʒu���擾
            var position = GetMousePosition();
            // �}�E�X�̏������W�Ɠ����������W�̍������擾
            vCurrentForce = vStartPos - position;

            // ��������������
            if (vCurrentForce != new Vector3(0, 0, 0))
            {
                transform.rotation = Quaternion.LookRotation(vCurrentForce);
            }
        }
        // �o�[�X�g���[�h�Ȃ�
        if (state.IsBurst)
        {
            //*���}*
            effect.StartEffect(1, this.gameObject, 1.0f);

            // ����
            burst.Explode(fStockBurst);
            // �u�ԓI�ɗ͂������Ă͂���
            rb.AddForce(transform.forward * fStockBurst, ForceMode.Impulse);
            status.bArmor = true;
            status.fBreakTime = 0.0f;
            state.GotoNormalState();
            fStockBurst = 0.0f;
            bGuardStart = false;
        }

        // �n�[�h���[�h���Q�[�W�c�ʂ�����Ȃ��~
        if (state.IsHard && status.Stamina > 0)
        {
            stop.DoStop(rb);
        }
        else
        {
            state.GotoNormalState();
            state.bGuard = false;
            bGuardStart = false;
        }
    }

    // �Q�[�W��
    private void RecoveryGauge()
    {
        // �Q�[�W�ʉ�
        status.Stamina += nRecovery;
        if (status.Stamina >= status.MaxStamina)
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

    private Vector3 GetMousePosition()
    {
        return new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
    }

}
