using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHPUI : MonoBehaviour
{
    public GameObject Boss;

    float fMaxHP, fNowHp;

    Slider slider;
    TextMeshProUGUI textMesh;

    void Start()
    {
        // �̗͎擾
        fMaxHP = fNowHp = Boss.GetComponent<BossBase>().nHp;

        // ������
        slider = GetComponent<Slider>();
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
    }

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
