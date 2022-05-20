//======================================================================
// BossDirection.cs
//======================================================================
// �J������
//
// 2022/04/24 author:�|���W�j�Y�@��{�X�o�ꉉ�o����
// 2022/04/25                    Effect��������
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDirection : MonoBehaviour
{
    // �K�v�I�u�W�F�N�g�f�[�^
    GameObject Cameraobj = null;
    public GameObject Bossobj = null;
    GameObject UIobj = null;
    EffectPlayer effectPlayer = null;
    BGMPlayer BGMPlayer = null;
    [SerializeField] AnimationCurve Zoomcurve;
    [SerializeField] AnimationCurve Returncurve;

    Vector3 oldCameraPos = new Vector3();
    Vector3 BossPos = new Vector3();

    int nToFlame = 60;
    int nZoomTime = 3;
    int nShowTime = 2;
    int nReturnTime = 1;

    // ���o����
    public bool bDirection = false;

    private void Update()
    {
        setBossPosition();
    }

    public void StartDirection(int bgmNumber)
    {
        // ���o�J�n
        bDirection = true;

        nToFlame = 60;
        nZoomTime = 3;
        nShowTime = 2;
        nReturnTime = 1;

        // �V�[������K�v�ȃf�[�^���擾
        Cameraobj = GameObject.FindWithTag("MainCamera");
        Bossobj = GameObject.FindWithTag("DirectionPoint");
        UIobj = GameObject.FindWithTag("UI");
        BGMPlayer = GameObject.FindWithTag("SoundPlayer").gameObject.GetComponent<BGMPlayer>();
        effectPlayer = Cameraobj.GetComponent<EffectPlayer>();

        nZoomTime = nZoomTime * nToFlame;
        nShowTime = nShowTime * nToFlame;
        nReturnTime = nReturnTime * nToFlame;

        oldCameraPos = Cameraobj.transform.position; // �J�����̈ʒu��ۑ�     
        BossPos.x = Bossobj.transform.position.x;     // �{�X�̈ʒu[X]���擾
        BossPos.y = oldCameraPos.y;                   // ����[Y]�͌Œ�
        BossPos.z = Bossobj.transform.position.z;     // �{�X�̈ʒu[Z]���擾
        Cameraobj.GetComponent<CameraController>().bOnDirection = true; // �v���C���[�ւ̃J�����Ǐ]������
        StartCoroutine(effectPlayer.BlackFogAnimaion(0));               // ����������
        UIobj.SetActive(false);                                         // ���o�ɏW�������邽��UI���\����

        StartCoroutine(CameraMan(bgmNumber));
    }

    IEnumerator CameraMan(int Number)
    {
        BGMPlayer.StopBGM();
        

        float percentPos;
        // �ߊ�� ==============================================
        for (int i = 0; i <= nZoomTime; i++)
        {
            yield return null;
            percentPos = Zoomcurve.Evaluate((float)i / nZoomTime);
            Cameraobj.transform.position = Vector3.Lerp(oldCameraPos, BossPos, percentPos);
        }
        // �~�܂� ==============================================
        StartCoroutine(effectPlayer.BlackFogAnimaion(1));

        // �X�e�[�W�ɉ�����BGM��炷
        switch(Number)
        {
            case 1:
                BGMPlayer.Stage1_Boss(); ;
                break;
            case 2:
                BGMPlayer.Stage2_Boss(); ;
                break;
            case 3:
                BGMPlayer.Stage3_Boss(); ;
                break;
            case 4:
                BGMPlayer.Stage4_Boss(); ;
                break;
            case 5:
                BGMPlayer.Stage5_Boss(); ;
                break;
            case 6:
                BGMPlayer.Stage6_Boss(); ;
                break;
            case 7:
                BGMPlayer.Stage7_Boss(); ;
                break;

            default:
                Debug.LogError("�Y�����Ȃ�BGM���I������܂���");
                break;
        }
        

        for (int n = 0; n <= nShowTime; n++)
        {
            yield return null;
            
        }
        // �߂� ================================================
        for (int t = 0; t <= nReturnTime; t++)
        {
            yield return null;
            percentPos = Returncurve.Evaluate((float)t / nReturnTime);
            Cameraobj.transform.position = Vector3.Lerp(BossPos, oldCameraPos, percentPos);
        }
        Cameraobj.GetComponent<CameraController>().bOnDirection = false; // �v���C���[�ւ̃J�����Ǐ]�L����
        UIobj.SetActive(true);

        // ���o�I��
        bDirection = false;
    }

    // �����{�X�������Ƒ����Ă���
    void setBossPosition()
    {
        if(Bossobj == null)
        {
           
        }
        else
        {
            BossPos.x = Bossobj.transform.position.x;     // �{�X�̈ʒu[X]���擾
            BossPos.y = oldCameraPos.y;                   // ����[Y]�͌Œ�
            BossPos.z = Bossobj.transform.position.z;     // �{�X�̈ʒu[Z]���擾
        }
        
    }
}







