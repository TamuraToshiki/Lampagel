
//======================================================================
// BossHPUI.cs
//======================================================================
// �J������
//
// 2022/05/02 author�F�����x  �{�XHPUI�����쐬�B
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPUI : MonoBehaviour
{
    // [BossTimer.cs]�ɂ�Boss���擾
    [HideInInspector] public GameObject Boss;

    // HP
    float fMaxHP, fNowHp;

    Slider slider;

    //----------------------
    // ������
    //----------------------
    void Start()
    {
        // �̗͎擾
        fMaxHP = fNowHp = Boss.GetComponent<BossBase>().nHp;

        // ������
        slider = GetComponent<Slider>();
    }


    //----------------------
    // �X�V
    //----------------------
    void Update()
    {
        // �{�X���S���j��
        if (Boss == null) Destroy(gameObject);

        // ���݂�HP�擾
        fNowHp = Boss.GetComponent<BossBase>().nHp;

        // �Q�[�W����
        slider.value = fNowHp / fMaxHP;
    }
}
