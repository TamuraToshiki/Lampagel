//======================================================================
// BossTimer.cs
//======================================================================
// �J������
//
// 2022/03/24 author�F�����x ����J�n�@�{�X�o���^�C�}�[�����ǉ��B
//                           �e�L�X�g�̔��f�A�Q�[�W�����B
// 2022/04/04 author�F�����x ���{�X�p�ɏ�������
// 2022/05/10 author�F�����x �{�XHPUI�p�ɉ���
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BossTimer : MonoBehaviour
{
    Slider slider;
    TextMeshProUGUI textMesh;

    private int second;
    private int minute;

    // �c�莞��
    float fTimer;

    // HP
    float fMaxHP, fNowHp;
    [SerializeField]bool bSetBoss = false;
    bool bSetHP = false; // �\�L�o�O�΍�


    [Header("�{�X�o������")]
    [SerializeField] float fCount = 60.0f;

    [Header("�o������{�X")]
    [SerializeField] GameObject Boss;

    [Header("�{�XHPUI")]
    [SerializeField] GameObject bossHPUI;

    [SerializeField] GameObject Player;

    void Start()
    {
        Debug.Log("aaa");

        // ������
        slider = GetComponent<Slider>();
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        fTimer = fCount;
        Player = GameObject.FindWithTag("Player");

        bSetBoss = false;
    }

    void Update()
    {
        // 0�b�ɂȂ�����
        if (fTimer < 0.0f)
        {
            

            if (bSetBoss == false)
            {
                

                // �{�X�̏o������(���W�͓K��)
                // �v���C���[�̏�����ɏo��
                Vector3 pos = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z + 20.0f);
                Boss = GameObject.Find("EnemyManager").GetComponent<EnemyManager>().Boss;
                Boss = Instantiate(Boss, pos, Boss.transform.rotation);

                // ���{�X�Ƀv���C���[���Z�b�g
                if (Boss.GetComponent<EnemyBase>())
                {
                    Player = Boss.GetComponent<EnemyBase>().player;
                }

                // �X���C�_�[��HP�p��
                fMaxHP = fNowHp = Boss.GetComponent<EnemyBase>().nHp;
               
                bSetBoss = true;
            }

            // �Ȃ����ŏ������{�X��HP���O�Ȃ̂�
            if (fMaxHP == 0 && bSetHP == false)
            {
                fMaxHP = Boss.GetComponent<EnemyBase>().nHp;
                fNowHp = Boss.GetComponent<EnemyBase>().nHp;
                bSetHP = true;
            }
            else if(bSetHP == true)
            {
                // �Q�[�W����
                slider.value = fNowHp / fMaxHP;
                fNowHp = Boss.GetComponent<EnemyBase>().nHp;
            }
        
            if(fNowHp <= 0)
            {
                fNowHp = 0;
                bSetHP = false;
            }

            textMesh.text = fNowHp.ToString();

            fTimer = -1.0f;
        }
        else 
        {
            fTimer -= Time.deltaTime;

            // �Q�[�W����
            slider.value = fTimer / fCount;

            // ���A�b�̌v�Z
            minute = (int)fTimer / 60;
            second = (int)fTimer % 60;

            // �e�L�X�g�ɔ��f
            textMesh.text = minute.ToString("d2") + ":" + second.ToString("d2");
        }

        // �X�e�[�W�̐؂�ւ������m����ׁA�uactiveSceneChanged�v�ɂ��̊֐�������
        SceneManager.activeSceneChanged += ActiveSceneChanged;


        //// 0�b�ɂȂ�����
        //if (fTimer < 0.0f)
        //{
        //    fTimer = -1.0f;

        //    // �{�X�̏o������(���W�͓K��)
        //    // �v���C���[�̏�����ɏo��
        //    Vector3 pos = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z + 20.0f);
        //    Boss = Instantiate(Boss, pos, Boss.transform.rotation);

        //    // ���{�X�Ƀv���C���[���Z�b�g
        //    if (Boss.GetComponent<EnemyBase>()) { Player = Boss.GetComponent<EnemyBase>().player; }

        //    // �X���C�_�[��HP�p��
        //    fMaxHP = fNowHp = Boss.GetComponent<EnemyBase>().nHp;

        //    bSetBoss = true;

        //}
        //else if (!(Boss == null))
        //{
        //    // �Ȃ����ŏ������{�X��HP���O�Ȃ̂�
        //    if (fMaxHP == 0)
        //    {
        //        fMaxHP = Boss.GetComponent<EnemyBase>().nHp;
        //    }

        //    fNowHp = Boss.GetComponent<EnemyBase>().nHp;

        //    // �Q�[�W����
        //    slider.value = fNowHp / fMaxHP;

        //    textMesh.text = "";

        //}
        //else
        //{



        //}


        //fTimer -= Time.deltaTime;

        //// �Q�[�W����
        //slider.value = fTimer / fCount;

        //// ���A�b�̌v�Z
        //minute = (int)fTimer / 60;
        //second = (int)fTimer % 60;

        //// �e�L�X�g�ɔ��f
        //textMesh.text = minute.ToString("d2") + ":" + second.ToString("d2");

    }

    // �V�[���̐؂�ւ������m **********************************
    void ActiveSceneChanged(Scene thisScene, Scene nextScene)
    {
        Start();
    }
    //**********************************************************
}
