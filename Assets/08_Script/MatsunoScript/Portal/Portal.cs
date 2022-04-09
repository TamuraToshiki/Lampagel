//======================================================================
// SceneObject.cs
//======================================================================
// �J������
//
// 2022/04/01 author�F���쏫�V �|�[�^���̃V�[���J��
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

    // �v���C���[�ƐڐG�ŃV�[���J��
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            // �V�[�����ԁ@1�b
            FadeManager.Instance.LoadScene("TesrScene", 1.0f);
        }
    }
}
