//======================================================================
// Title.cs
//======================================================================
// �J������
//
// 2022/04/12 author�F���c�B�� �^�C�g������
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField] int nTitleFrame;
    [SerializeField] SceneObject sNextScene;
    [SerializeField] List<GameObject> gTitleMenuChoice;
    [SerializeField] SoundManager soundManager;

    void Start()
    {
        //������
        nTitleFrame = 1;
    }
    void Update()
    {
        TitleMenu();
    }


    //�^�C�g�����j���[
    public void TitleMenu()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            soundManager.Play_SystemSelect(this.gameObject);
            //nTitleFrame++;
            //if (nTitleFrame > 1)
            //    nTitleFrame = 0;
            nTitleFrame = 1;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            soundManager.Play_SystemSelect(this.gameObject);
            //nTitleFrame--;
            //if (nTitleFrame < 0)
            //    nTitleFrame = 1;
            nTitleFrame = 2;
        }
        //if (nTitleFrame == 0)
        //{
        //    if (Input.GetKeyDown(KeyCode.Return))
        //        ChangeScene();
        //    gTitleMenuChoice[nTitleFrame].SetActive(true);
        //    gTitleMenuChoice[nTitleFrame + 1].SetActive(false);
        //    gTitleMenuChoice[nTitleFrame + 2].SetActive(false);
            
        //}
        //if (nTitleFrame == 1)
        //{
        //    if (Input.GetKeyDown(KeyCode.Return))
        //    {
        //        Debug.Log("�Q�[����߂܂����H");
        //        Application.Quit();
        //    }
        //    gTitleMenuChoice[nTitleFrame].SetActive(true);
        //    gTitleMenuChoice[nTitleFrame - 1].SetActive(false);
        //    gTitleMenuChoice[nTitleFrame + 1].SetActive(false);
        //}

        switch(nTitleFrame)
        {
            case 1:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    soundManager.Play_SystemDecide(this.gameObject);
                    ChangeScene();
                }
                    
                gTitleMenuChoice[0].SetActive(true);
                gTitleMenuChoice[1].SetActive(false);
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    soundManager.Play_SystemDecide(this.gameObject);
                    Application.Quit();
                }
                    
                gTitleMenuChoice[1].SetActive(true);
                gTitleMenuChoice[0].SetActive(false);
                break;
        }
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(sNextScene);
    }
}
