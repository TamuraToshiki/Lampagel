//======================================================================
// EffectPlayer.cs
//======================================================================
// �J������
//
// 2022/04/09 author�F�|�� PostProcess��C�ӋN������X�N���v�g�쐬
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;//�K�v
using UnityEngine.Rendering.Universal;//�K�v�A�uVolume�v��F�����邽��

public class EffectPlayer : MonoBehaviour
{
    [Header("<�f�o�b�N�͂P�`�O�L�[�Ŋm�F�ł��܂�>")]

    [Header("PostProcess�G�t�F�N�g")]
    [SerializeField] Volume DamageVolume = null;
    public int nDamageInterbal = 30;

    [SerializeField] Volume GuardVolume = null;
    public int nGuardInterbal = 30;
    bool bGuardEffect = false;

    [SerializeField] Volume BurstShotVolume = null;
    public int nBurstShotInterbal = 30;


    private void Start()
    {
        // �x����
        if (DamageVolume == null) Debug.LogError("[DamageVolume] is null!");

        if (GuardVolume == null) Debug.LogError("[GuardVolume] is null!");

        if (BurstShotVolume == null) Debug.LogError("[BurstShotVolume] is null!");
    }

    // �m�F�p
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(DamageEffect());
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GuradEffect();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(BurstShotEffect(nBurstShotInterbal));
        }
    }

    
    
    // �_���[�W���o�@**********************************************************
    public IEnumerator DamageEffect()
    {
        DamageVolume.weight = 1;
        for (int n = nDamageInterbal; n >= 0; n--)
        {           
            yield return null;
            DamageVolume.weight = (float)n / nDamageInterbal;            
        }
        DamageVolume.weight = 0;
    }
    // ************************************************************************

    // �K�[�h���o *************************************************************
    public void GuradEffect()
    {
        bGuardEffect = !bGuardEffect;
        if (bGuardEffect == true)
        {
            StartCoroutine(StartGurad());
        }
        else
        {
            StartCoroutine(EndGurad());
        }
    }

    IEnumerator StartGurad()
    {
        GuardVolume.weight = 0;
        for (int n = 0; n <= nGuardInterbal; n++)
        {
            yield return null;
            GuardVolume.weight = (float)n / nGuardInterbal;
        }
        GuardVolume.weight = 1;
    }

    IEnumerator EndGurad()
    {
        GuardVolume.weight = 1;
        for (int n = nGuardInterbal; n >= 0; n--)
        {
            yield return null;
            GuardVolume.weight = (float)n / nGuardInterbal;
        }
        GuardVolume.weight = 0;
    }
    // ************************************************************************

    // �o�[�X�g�V���b�g���o�@**********************************************************
    public IEnumerator BurstShotEffect(int armorflame) // ���G���Ԓ��ɔ���
    {
        BurstShotVolume.weight = 1;
        for (int n = armorflame; n >= 0; n--)
        {
            yield return null;
            if(n <= armorflame / 3)
            {
                BurstShotVolume.weight = (float)n / nBurstShotInterbal;
            }           
        }
        BurstShotVolume.weight = 0;
    }
    // ************************************************************************
}
