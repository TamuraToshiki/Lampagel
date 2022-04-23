//======================================================================
// SceneObject.cs
//======================================================================
// �J������
//
// 2022/04/01 author�F���쏫�V �|�[�^���̃V�[���J��
// 2022/04/23 author�F�|���W�j�Y�@�V�[���ǉ����ʓ|�Ȃ���SceneData�ǉ�
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    // ���̃V�[��
    public SceneObject m_nextScene;   
    public SceneData SceneData;        // 0423�ǉ�
    string sNowScene = "���݂̃V�[��"; // 0423�ǉ�

    private void Start()
    {
        DecideNextScene(); // 0423�ǉ�
    }

    // �v���C���[�ƐڐG�ŃV�[���J��
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            // �V�[�����ԁ@1�b
            FadeManager.Instance.LoadScene(m_nextScene, 1.0f);
        }
    }



    // 0423 �V�[������ *****************************************
    void DecideNextScene()
    {
        m_nextScene = null;                             // �O�̃f�[�^���c��Ȃ��悤����
        sNowScene = SceneManager.GetActiveScene().name; // ���̃V�[���̖��O���Ƃ�

        for(int i = 0; i <= SceneData.GameScene.Count; i++)
        {
            if(sNowScene == SceneData.GameScene[i].m_SceneName)
            {
                m_nextScene = SceneData.GameScene[i + 1]; // ���̃V�[���𓖂Ă�
                break;                                    // ���̃V�[��������Ό�͂���
            }
        }

        if (m_nextScene == null) 
            Debug.LogError("����͍ŏI�X�e�[�W�H");
    }
    //**********************************************************
}
