//======================================================================
// BossDirection.cs
//======================================================================
// �J������
//
// 2022/04/24 author:�|���W�j�Y�@��{�X�o�ꉉ�o����
// 2022/04/25                    Effect������
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDirection : MonoBehaviour
{
    // �K�v�I�u�W�F�N�g�f�[�^
    GameObject Cameraobj = null;
    GameObject Bossobj        = null;
    GameObject UIobj          = null;
    EffectPlayer effectPlayer = null;
    [SerializeField] AnimationCurve Gocurve; // Editor�p
    [SerializeField] AnimationCurve Backcurve; // Editor�p

    Vector3 oldCameraPos = new Vector3();
    Vector3 BossPos = new Vector3();
    
    float fMovetime = 3.0f; // �ړ�����
    private float startTime;
    

    

    private void Start()
    {
        StartDirection();
    }

    void Update()
    {
        MoveCamera(oldCameraPos, BossPos);       
    }

    public void StartDirection()
    {
        // �V�[������K�v�ȃf�[�^���擾
        Cameraobj = GameObject.FindWithTag("MainCamera");
        Bossobj = GameObject.FindWithTag("Boss"); 
        UIobj = GameObject.FindWithTag("UI");   
        effectPlayer = Cameraobj.GetComponent<EffectPlayer>();

        oldCameraPos = Cameraobj.transform.position ; // �J�����̈ʒu��ۑ�     
        BossPos.x = Bossobj.transform.position.x;     // �{�X�̈ʒu[X]���擾
        BossPos.y = oldCameraPos.y;
        BossPos.z = Bossobj.transform.position.z;     // �{�X�̈ʒu[Z]���擾

        Cameraobj.GetComponent<CameraController>().bOnDirection = true;
        StartCoroutine(effectPlayer.BlackFogAnimaion(0)); // ����������
        UIobj.SetActive(false);           // ���o�ɏW�������邽��UI���\����

    }

    

    void MoveCamera(Vector3 startPos, Vector3 endPos)
    {
        var diff = Time.timeSinceLevelLoad - startTime;
        if (diff > fMovetime) // ���ԂɂȂ�����~�܂�
        {
            transform.position = endPos;
            StartCoroutine(effectPlayer.BlackFogAnimaion(1));
            
            Cameraobj.GetComponent<CameraController>().bOnDirection = effectPlayer.bCompBlackFog;
            UIobj.SetActive(true);
            enabled = false;
        }

        var rate = diff / fMovetime;
        var pos = Gocurve.Evaluate(rate);

        transform.position = Vector3.Lerp(startPos, endPos, rate);
        transform.position = Vector3.Lerp(startPos, endPos, pos);
    }

    




    // Inspector�̃G�f�B�^�[ ***********************************
    void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR

        if (!UnityEditor.EditorApplication.isPlaying || enabled == false)
        {
            oldCameraPos = transform.position;
        }

        UnityEditor.Handles.Label(BossPos, BossPos.ToString());
        UnityEditor.Handles.Label(oldCameraPos, oldCameraPos.ToString());
#endif
        Gizmos.DrawSphere(BossPos, 0.1f);
        Gizmos.DrawSphere(oldCameraPos, 0.1f);

        Gizmos.DrawLine(oldCameraPos, BossPos);
    }
    //**********************************************************
}
