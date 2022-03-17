using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

//======================================================================
// PostProcessControl.cs
//======================================================================
// �J������
//
// 2022/03/05 author�F���c�B�� �_���[�W�\���I���I�t����
//
//======================================================================

public class PostProcessControl : MonoBehaviour
{
    [SerializeField]
    private PostProcessVolume postProcessVolume;


    void Start()
    {
        //�|�X�g�v���Z�X���Q�b�g
        postProcessVolume = this.GetComponent<PostProcessVolume>();
    
    }

    void Update()
    {
        //���^�[���L�[��������
        if(Input.GetKeyDown(KeyCode.Return))
        {
            //�G�t�F�N�g���ʂ��̂��̂̃I���I�t�؂�ւ�
            if(postProcessVolume.isGlobal)
            {
                SetIsGroval(false);
            }
            else
            {
                SetIsGroval(true);
            }  
        }
    }
    

    //�Q�[����ŃG�t�F�N�g���ʂ��I���ɂ��邩�ǂ���
    void SetIsGroval(bool b)
    {
        postProcessVolume.isGlobal = b;
    }
 
}
