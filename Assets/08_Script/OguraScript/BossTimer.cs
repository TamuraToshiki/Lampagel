//======================================================================
// BossTimer.cs
//======================================================================
// �J������
//
// 2022/03/24 author�F�����x ����J�n�@�{�X�o���^�C�}�[�����ǉ��B
//                           �e�L�X�g�̔��f�A�Q�[�W�����B
// 2022/04/04 author�F�����x ���{�X�p�ɏ�������
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

    // �o������{�X
    [Header("�o������{�X")][SerializeField]GameObject Boss;

    GameObject Player;

    void Start()
    {
        // ������
        slider = GetComponent<Slider>();
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        fTimer = fCount;
        Player = GameObject.Find("Player");
    }

    void Update()
    {
        fTimer -= Time.deltaTime;

        // 0�b�ɂȂ�����
        if(fTimer < 0.0f)
        {
            fTimer = 0.0f;

            // �{�X�̏o������(���W�͓K��)
            // �v���C���[�̏�����ɏo��
            Vector3 pos = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z + 20.0f);   
            Boss = Instantiate(Boss, pos, Boss.transform.rotation);

            // ���{�X�Ƀv���C���[���Z�b�g
            if(Boss.GetComponent<EnemyBase>())
                Boss.GetComponent<EnemyBase>().SetPlayer(Player);

            // ���Łi���j
            Destroy(gameObject);
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
