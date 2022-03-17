//======================================================================
// CameraShaker.cs
//======================================================================
// �J������
//
// 2022/03/05 author�F���c�B�� �J�����̗h��쐬
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraShaker : MonoBehaviour
{
    [Header("Shake")]
    // �����ɃJ�����I�u�W�F�N�g��ݒ肷��
    public Transform ShakeObject = null;
    // �J�����̗h��̋���
    public float fShakeIntensity = 0.02f;
    // �h��̌��Z�l
    public float fShakeDecay = 0.002f;
    // �h��̋����W��
    public float fShakeAmount = 0.2f;       

    private Vector3 VOriginPosition;
    private Quaternion QOriginRotation;

    void Start()
    {
        VOriginPosition = ShakeObject.localPosition;
        QOriginRotation = ShakeObject.localRotation;
    }

    protected void Update()
    {
        // �L�[�{�[�h�ړ� 
        if (Input.GetMouseButton(0))
        {
            Do();
        }
    }

    public void Do()
    {
        StopAllCoroutines();
        StartCoroutine(Shake());
    }

    public IEnumerator Shake()
    {
        float shakeIntensity = fShakeIntensity;
        while (shakeIntensity > 0)
        {
            ShakeObject.localPosition = VOriginPosition + Random.insideUnitSphere * shakeIntensity;
            ShakeObject.localRotation = new Quaternion(
                QOriginRotation.x + Random.Range(-shakeIntensity, shakeIntensity) * fShakeAmount,
                QOriginRotation.y + Random.Range(-shakeIntensity, shakeIntensity) * fShakeAmount,
                QOriginRotation.z + Random.Range(-shakeIntensity, shakeIntensity) * fShakeAmount,
                QOriginRotation.w + Random.Range(-shakeIntensity, shakeIntensity) * fShakeAmount);
            shakeIntensity -= fShakeDecay;
            yield return false;
        }
        ShakeObject.localPosition = VOriginPosition;
        ShakeObject.localRotation = QOriginRotation;
    }
}