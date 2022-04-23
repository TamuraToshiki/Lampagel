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

    // 0423 �V�[���Ǘ�
    public SceneData SceneData;
    [SerializeField] string sNowScene = "���݂̃V�[��";

    private void Start()
    {
        DecideNextScene();
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
        m_nextScene = null;
        sNowScene = SceneManager.GetActiveScene().name;

        for(int i = 0; i <= SceneData.GameScene.Count; i++)
        {
            Debug.Log(SceneData.GameScene[i].m_SceneName);
            if(sNowScene == SceneData.GameScene[i].m_SceneName)
            {
                m_nextScene = SceneData.GameScene[i + 1];
                break;
            }
        }
        if (m_nextScene == null) Debug.LogError("���ɑJ�ڂ���V�[��������܂���");
    }
    //**********************************************************
}
