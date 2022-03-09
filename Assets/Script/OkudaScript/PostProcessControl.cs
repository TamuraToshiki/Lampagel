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
    private Vignette Vignette;
    private ChromaticAberration chromaticAberration;

    void Start()
    {
        //�|�X�g�v���Z�X���Q�b�g
        postProcessVolume = this.GetComponent<PostProcessVolume>();
        Vignette = this.GetComponent<Vignette>();
        chromaticAberration = this.GetComponent<ChromaticAberration>();
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
        //V�L�[����������
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Vignette.active)
            {
                SetVignette(false);
            }
            else
            {
                SetVignette(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (chromaticAberration.active)
            {
                SetChromatic(false);
            }
            else
            {
                SetChromatic(true);
            }
        }
    }

    //�Q�[����ŃG�t�F�N�g���ʂ��I���ɂ��邩�ǂ���
    void SetIsGroval(bool b)
    {
        postProcessVolume.isGlobal = b;
    }
    //�_���[�W�\�����ʂ��I���ɂ��邩�ǂ���
    void SetVignette(bool b)
    {
        Vignette.active = b;
    }
    //�K�[�h�\�����I���ɂ��邩�ǂ���
    void SetChromatic(bool b)
    {
        chromaticAberration.active = b;
    }
}
