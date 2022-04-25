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
    GameObject Bossobj = null;
    GameObject UIobj = null;
    EffectPlayer effectPlayer = null;
    [SerializeField] AnimationCurve Zoomcurve;
    [SerializeField] AnimationCurve Returncurve;

    Vector3 oldCameraPos = new Vector3();
    Vector3 BossPos = new Vector3();

    int nToFlame = 60;
    int nZoomTime = 3;
    int nShowTime = 2;
    int nReturnTime = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            StartDirection();
        }
    }

    public void StartDirection()
    {
        // �V�[������K�v�ȃf�[�^���擾
        Cameraobj = GameObject.FindWithTag("MainCamera");
        Bossobj = GameObject.FindWithTag("Boss");
        UIobj = GameObject.FindWithTag("UI");
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

        StartCoroutine(CameraMan());
    }

    IEnumerator CameraMan()
    {
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
    }
}







