//======================================================================
// PlayerHP.cs
//======================================================================
// �J������
//
// 2022/03/25 author�F�c���q�� �쐬�J�n
// 2022/03/27 author�F�c���q�� hard���[�h�Ȃ疳��������@�\�ǉ�
// 2022/03/28 author�F�|���@���} �G�t�F�N�g�����g�ݍ���
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    PlayerStatus status;
    PlayerState state;

    //*���}* �G�t�F�N�g�X�N���v�g
    [SerializeField] AID_PlayerEffect effect;
    [SerializeField] SoundManager soundManager;

    // �T�E���h�}�l�[�W���[

    // Start is called before the first frame update
    void Start()
    {
        status = GetComponent<PlayerStatus>();
        state = GetComponent<PlayerState>();
        StartCoroutine("AutoHeal");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            OnDamage(10);
        }
    }

    // �_���[�W�R�[���o�b�N�֐�
    public void OnDamage(int damage)
    {
        // 0�ȉ��Ȃ玀��ł邽�߃��^�[��
        if (state.IsDie) return;
        if (status.bArmor) return;
        // �n�[�h���[�h�Ȃ疳��
        if (state.IsHard)
        {
            //*���}*
            effect.StartEffect(6, this.gameObject, 0.5f);
            // damage���X�g�b�N����
            this.GetComponent<GuardMode>().AddStockExplode(status.BurstStock);
            return;
        }

        StartCoroutine(StartVibation());
        soundManager.Play_PlayerDamage(this.gameObject);

        // �_���[�W��^����
        status.HP -= damage;
        effect.StartEffect(8, this.gameObject, 0.5f);
        if (status.HP <= 0)
        {
            state.GotoDieState();
        }
    }

    IEnumerator AutoHeal()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);

            if(status.HP <= status.MaxHP)
            {
                if (state.IsDie) yield break;
                // �񕜁�(�񕜗�)
                status.HP += status.UpHP;
            }
            //�@���̃t���[���ɔ�΂�
            yield return null;
        }
    }

    // �U���R���[�`��(��_���[�W)
    IEnumerator StartVibation()
    {

        XInputDotNetPure.GamePad.SetVibration(0, 9, 9);
        yield return new WaitForSecondsRealtime(0.1f);
        XInputDotNetPure.GamePad.SetVibration(0, 0, 0);
    }
}
