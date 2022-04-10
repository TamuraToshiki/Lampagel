//======================================================================
// CameraShaker.cs
//======================================================================
// �J������
//
// 2022/03/05 author�F���c�B�� �J�����̗h��쐬
// 2022/03/24 author�F�|���W�j�Y �J������яC��
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

    private Vector3 vOriginPosition;
    private Quaternion qOriginRotation;

    // �ǉ�_�h�炷�O�̃J�������W
    private Vector3 vOldPos;
    private Quaternion qOldQuater;

    void Start()
    {
        vOriginPosition = ShakeObject.localPosition;
        qOriginRotation = ShakeObject.localRotation;
    }

    void Update()
    {
        // �L�[�{�[�h�ړ� 
        if (Input.GetKeyDown(KeyCode.Q))
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
        // �h�炷�O�̃J�������W�擾
        vOldPos = ShakeObject.localPosition;
        qOldQuater = ShakeObject.localRotation;

        float shakeIntensity = fShakeIntensity;
        while (shakeIntensity > 0)
        {
            // �h�炵�Ă���Œ��̃J�������W�擾
            vOriginPosition = ShakeObject.localPosition;
            qOriginRotation = ShakeObject.localRotation;

            ShakeObject.localPosition = vOriginPosition + Random.insideUnitSphere * shakeIntensity;
            ShakeObject.localRotation = new Quaternion(
                qOriginRotation.x + Random.Range(-shakeIntensity, shakeIntensity) * fShakeAmount,
                qOriginRotation.y + Random.Range(-shakeIntensity, shakeIntensity) * fShakeAmount,
                qOriginRotation.z + Random.Range(-shakeIntensity, shakeIntensity) * fShakeAmount,
                qOriginRotation.w + Random.Range(-shakeIntensity, shakeIntensity) * fShakeAmount);
            shakeIntensity -= fShakeDecay;
            yield return false;
        }

        // �h�炷�O�̃J�������W�ɖ߂�
        ShakeObject.localPosition = vOldPos;
        ShakeObject.localRotation = qOldQuater;
    }
}