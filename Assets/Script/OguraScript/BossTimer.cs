//======================================================================
// BossTimer.cs
//======================================================================
// �J������
//
// 2022/03/24 author�F�����x ����J�n�@�{�X�o���^�C�}�[�����ǉ��B
//                           �e�L�X�g�̔��f�A�Q�[�W�����B
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossTimer : MonoBehaviour
{
    Slider slider;
    TextMeshProUGUI textMesh;

    private int second;
    private int minute;

    // �c�莞��
    float fTimer;

    // �{�X�o������
    [Header("�{�X�o������")][SerializeField]float fCount = 60.0f;

    void Start()
    {
        // ������
        slider = GetComponent<Slider>();
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        fTimer = fCount;
    }

    void Update()
    {
        fTimer -= Time.deltaTime;

        // 0�b�ɂȂ�����
        if(fTimer < 0.0f)
        {
            fTimer = 0.0f;

            // �{�X�̏o�������H�H
        }

        // �Q�[�W����
        slider.value =  fTimer/ fCount;

        // ���A�b�̌v�Z
        minute = (int)fTimer / 60;
        second = (int)fTimer % 60;

        // �e�L�X�g�ɔ��f
        textMesh.text = minute.ToString("d2") + ":" + second.ToString("d2");

    }
}
