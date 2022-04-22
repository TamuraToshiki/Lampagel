//======================================================================
// Pause.cs
//======================================================================
// �J������
//
// 2022/03/23 author�F���c�B�� �|�[�Y�@�\����
// 2022/03/25 author�F���c�B�� �|�[�Y���j���[�E���x���A�b�v���j���[�@�\�ǉ� 
// 2022/04/25 author�F���c�B�� �|�[�Y�o�O�C�� 
// 2022/04/21 author�F�|���W�j�Y �A�C�e������@�\�ǉ��i�_�u�蔭���ǂ����悤���j
//
//======================================================================
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    //�|�[�Y�؂�ւ��p�t���O
    [SerializeField] int  nMenuFrame;
    [SerializeField] bool bPause;
    [SerializeField] bool bResume;
    [SerializeField] bool bLevelUpPause;
    [SerializeField] GameObject gPauseMenu;
    [SerializeField] GameObject gLevelUpPause;
    [SerializeField] List<GameObject> gPauseMenuChoice;
    [SerializeField] List<GameObject> gLevelUpMenuChoice;

    // 4/21 �ǉ��i�|�j
    [SerializeField] ItemManager itemManager;
    public Image SelectItemIcon_L, SelectItemIcon_C, SelectItemIcon_R;
    int setItem_L = 0, setItem_C = 0, setItem_R = 0;
    bool bLotteryComp = false;


    void Start()
    {
        //������
        nMenuFrame = 0;
        bPause = false;
        bLevelUpPause = false;
        bResume = true;

        // 4/21 �ǉ�
        itemManager = GameObject.FindWithTag("ItemManager").GetComponent<ItemManager>();
        
    }
    void Update()
    {
        //P�L�[��������
        //===============================
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (bResume)
            {
                SetbPause(true);
            }
            else
            {
                SetbPause(false);
            }
        }
        //�|�[�Y���j���[
        if (bPause)
        {
            PauseMenu(bPause);
        }
        else
        {
            PauseMenu(bPause);
        }
        //-------------------------------------

        //L�L�[��������
        //=====================================
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (bResume)
            {
                SetbLevelPause(true);
            }
            else
            {
                SetbLevelPause(false);
            }
        }
        //���x���A�b�v���̃|�[�Y���j���[
        if(bLevelUpPause)
        {
            // 4/21 �ǉ�
            //itemManager
            levelUpPause(bLevelUpPause);
        }
        else
        {
            levelUpPause(bLevelUpPause);
        }
        //------------------------------------------ 

        if (bLevelUpPause || bPause)
        {
            Stop();
        }
        else
        {
            Resume();
        }

        //============================================
        //�o�O�h�~�i2�̃t���O�������ɂȂ邱�Ƃ�h���j
        //============================================
        if (bPause && bResume)
            SetbResume(false);
        if (!bPause && !bResume)
            SetbResume(true);
        if (bResume && bLevelUpPause)
            SetbResume(false);
        //if (!bResume && !bLevelUpPause)
        //    SetbResume(true);
        if (bPause && bLevelUpPause)
            SetbPause(false);
        //if (!bPause && !bLevelUpPause)
        //    SetbResume(true);
        //--------------------------------------------

    }
    
    //���Ԓ�~
    private void Stop()
    {
        Time.timeScale = 0;  // ���Ԓ�~
    }

    //�ĊJ
    private void Resume()
    {
        Time.timeScale = 1;  // �ĊJ
    }

    private void SetbPause(bool b)
    {
        bPause = b;
    }
    private void SetbLevelPause(bool b)
    {
        bLevelUpPause = b;
    }
    private void SetbResume(bool b)
    {
        bResume = b;
    }
    //�|�[�Y���j���[
    public void PauseMenu(bool b)
    {
        gPauseMenu.SetActive(b);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            nMenuFrame++;
            if (nMenuFrame > 1)
                nMenuFrame = 0;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            nMenuFrame--;
            if (nMenuFrame < 0)
                nMenuFrame = 1;
        }
        if (nMenuFrame == 0)
        {
            gPauseMenuChoice[nMenuFrame].SetActive(true);
            gPauseMenuChoice[nMenuFrame + 1].SetActive(false);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SetbPause(false);               
            }
        }
        if (nMenuFrame == 1)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("�Q�[����߂܂����H");
                Application.Quit();
            }
            gPauseMenuChoice[nMenuFrame].SetActive(true);
            gPauseMenuChoice[nMenuFrame - 1].SetActive(false);
        }
    }
    //�����F���A�b�v�����Ƃ��ɌĂ�ł�(�e�X�g�ňꉞL�L�[�������瓮���悤�ɂ��Ƃ��܂�)
    //���x���A�b�v���j���[
    public void levelUpPause(bool b)
    {
        gLevelUpPause.SetActive(b);

        // LVUP���Ȃ��Ă������ƌĂ΂�邽��if�ǉ��i�|���j
        if(b)
        {
            // �A�C�e���ԍ����蓖�āi�|���j
            if(bLotteryComp == false)
            {
                setItem_L = itemManager.nItemGacha(); // "L" eft
                setItem_C = itemManager.nItemGacha(); // "C" enter
                setItem_R = itemManager.nItemGacha(); // "R" ight

                SelectItemIcon_L.sprite = itemManager.setItemIcon(setItem_L);
                SelectItemIcon_C.sprite = itemManager.setItemIcon(setItem_C);
                SelectItemIcon_R.sprite = itemManager.setItemIcon(setItem_R);

                bLotteryComp = true;
            }
            // ****************************

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                nMenuFrame++;
                if (nMenuFrame > 2)
                    nMenuFrame = 0;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                nMenuFrame--;
                if (nMenuFrame < 0)
                    nMenuFrame = 2;
            }
            if (nMenuFrame == 0)
            {
                Debug.Log("���A�C�e���I��");
                
                gLevelUpMenuChoice[nMenuFrame].SetActive(true);
                gLevelUpMenuChoice[nMenuFrame + 1].SetActive(false);
                gLevelUpMenuChoice[nMenuFrame + 2].SetActive(false);

                //���菈���i�|���j
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    itemManager.nItemCount(setItem_L);
                    SetbLevelPause(false);
                    bLotteryComp = false;
                }
            }
            if (nMenuFrame == 1)
            {
                Debug.Log("�����A�C�e���I��");
                
                gLevelUpMenuChoice[nMenuFrame].SetActive(true);
                gLevelUpMenuChoice[nMenuFrame - 1].SetActive(false);
                gLevelUpMenuChoice[nMenuFrame + 1].SetActive(false);

                //���菈���i�|���j
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    itemManager.nItemCount(setItem_C);
                    SetbLevelPause(false);
                    bLotteryComp = false;
                }
            }
            if (nMenuFrame == 2)
            {
                Debug.Log("�E�A�C�e���I��");
                
                gLevelUpMenuChoice[nMenuFrame].SetActive(true);
                gLevelUpMenuChoice[nMenuFrame - 1].SetActive(false);
                gLevelUpMenuChoice[nMenuFrame - 2].SetActive(false);

                //���菈���i�|���j
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    itemManager.nItemCount(setItem_R);
                    SetbLevelPause(false);
                    bLotteryComp = false;
                }
            }
        }
       
    }
}