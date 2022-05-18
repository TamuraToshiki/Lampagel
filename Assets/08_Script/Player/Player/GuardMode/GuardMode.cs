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
// 2022/05/05                    �o�[�X�g�̔������_���[�W����񐔂֕ύX
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
    [SerializeField] private LineRenderer Direction = null;  // ���˕���

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

    // �����З͂����[
    private int fStockBurst = 0;

    // �d�����Ƀ��[�e���邽�߂̕ϐ�
    bool bGuardStart = false;

    //*���}* �G�t�F�N�g�X�N���v�g
    [SerializeField] AID_PlayerEffect effect;

    // �T�E���h�G�t�F�N�g
    [SerializeField] SoundManager soundManager;
    float fVibeInterbal  = 1.0f;
    float fCountTime = 0;

    // �K�[�h���f���ƃf�t�H���g���f��
    [SerializeField] private GameObject DefaultModel;
    [SerializeField] private GameObject GuardModel;

    // �K�[�h���Ɍ����ς��邽�߂̕ϐ�
    private Vector3 vStartPos = Vector3.zero;
    private Vector3 vCurrentForce = Vector3.zero;

    // �K�[�h�y�i���e�B
    public bool bGuardPenalty = false;
    float fGuardPenaltyTime = 1.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        stop = GetComponent<Stop>();
        burst = GetComponent<GuardBurst>();
        state = GetComponent<PlayerState>();
        rb = GetComponent<Rigidbody>();
        status = GetComponent<PlayerStatus>();

        fVibeInterbal = 1.0f;
        fCountTime = 0;
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
            // �K�[�h�y�i���e�B����
            if(bGuardPenalty)
            {
                fGuardPenaltyTime += Time.deltaTime;
                if(fGuardPenaltyTime >= status.fGuardPenalty)
                {
                    fGuardPenaltyTime = 0.0f;
                    bGuardPenalty = false;
                }
            }
        }
        // �n�[�h���[�h�Ȃ�
        if (state.IsHard)
        {
            // �L�[�{�[�h
            if(!bGuardStart)
            {
                vStartPos = GetMousePosition();
                bGuardStart = true;
            }
            // �p�b�h
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            if (Mathf.Abs(x) >= 0.5f || Mathf.Abs(y) >= 0.5f)
            {
                // ���͕������t�ɂ��Ď󂯎��
                vCurrentForce = new Vector3(-x * 1000, 0, -y * 1000);

                // ��������������
                transform.rotation = Quaternion.LookRotation(vCurrentForce);
            }


            // �Q�[�W����
            SubtractGauge();
            DefaultModel.SetActive(false);
            GuardModel.SetActive(true);

            // ���������}�E�X���W�̈ʒu���擾
            var position = GetMousePosition();
            // �}�E�X�̏������W�Ɠ����������W�̍������擾
            vCurrentForce = vStartPos - position;

            // �K�[�h���Ƀ_���[�W������������
            if (fStockBurst >= 0.01f)
            {
                // ���̈������菈��
                Direction.enabled = true;
                // ���������Ƌt�ɖ�󂪏o��悤��
                Direction.SetPosition(0, rb.position);
                Direction.SetPosition(1, rb.position - transform.forward * 2);
            }

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
            soundManager.Play_PlayerBurst(this.gameObject);

            // ����
            burst.Explode(fStockBurst);
            // �u�ԓI�ɗ͂������Ă͂���
            rb.AddForce(transform.forward * fStockBurst, ForceMode.Impulse);
            Direction.enabled = false;
            status.bArmor = true;
            status.fBreakTime = 0.0f;
            state.GotoNormalState();
            fStockBurst = 0;
            bGuardStart = false;
        }

        // �n�[�h���[�h���Q�[�W�c�ʂ�����Ȃ��~
        if (state.IsHard && status.Stamina > 0)
        {
            if(fCountTime < 0)
            {
                StartCoroutine(StartVibation());
                fCountTime = fVibeInterbal;
            }
            fCountTime -= Time.deltaTime;


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
        status.Stamina += status.StaminaRecovery;
        if (status.Stamina >= status.MaxStamina)
        {
            status.Stamina = status.MaxStamina;
        }
    }

    // �Q�[�W����
    private void SubtractGauge()
    {
        status.Stamina -= status.StaminaCost;
        if (status.Stamina < 0)
        {
            status.Stamina = 0;
            // �K�[�h�y�i���e�B����
            soundManager.Play_PlayerGuardBreak(this.gameObject);
            bGuardPenalty = true;
        }
    }

    public void AddStockExplode(float damage)
    {
        //�񐔐��ɕύX�i�|���j
        fStockBurst += 1;
        soundManager.Play_PlayerDamageatGuardA(this.gameObject);
        if(fStockBurst > 6)
        {
            fStockBurst = 6;
        }
    }

    private Vector3 GetMousePosition()
    {
        return new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
    }

    // �U���R���[�`��(�K�[�h�U��)
    IEnumerator StartVibation()
    {

        XInputDotNetPure.GamePad.SetVibration(0, 1, 1);
        yield return new WaitForSecondsRealtime(0.1f);
        XInputDotNetPure.GamePad.SetVibration(0, 0, 0);
        yield return new WaitForSecondsRealtime(0.1f);
        XInputDotNetPure.GamePad.SetVibration(0, 1, 1);
        yield return new WaitForSecondsRealtime(0.1f);
        XInputDotNetPure.GamePad.SetVibration(0, 0, 0);
    }
}
