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

    private Vector3 VOriginPosition;
    private Quaternion QOriginRotation;

    // �ǉ�_�h�炷�O�̃J�������W
    private Vector3 VoldPos;
    private Quaternion QoldQuater;

    void Start()
    {
        VOriginPosition = ShakeObject.localPosition;
        QOriginRotation = ShakeObject.localRotation;
    }

    protected void Update()
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
        VoldPos = ShakeObject.localPosition;
        QoldQuater = ShakeObject.localRotation;

        float shakeIntensity = fShakeIntensity;
        while (shakeIntensity > 0)
        {
            // �h�炵�Ă���Œ��̃J�������W�擾
            VOriginPosition = ShakeObject.localPosition;
            QOriginRotation = ShakeObject.localRotation;

            ShakeObject.localPosition = VOriginPosition + Random.insideUnitSphere * shakeIntensity;
            ShakeObject.localRotation = new Quaternion(
                QOriginRotation.x + Random.Range(-shakeIntensity, shakeIntensity) * fShakeAmount,
                QOriginRotation.y + Random.Range(-shakeIntensity, shakeIntensity) * fShakeAmount,
                QOriginRotation.z + Random.Range(-shakeIntensity, shakeIntensity) * fShakeAmount,
                QOriginRotation.w + Random.Range(-shakeIntensity, shakeIntensity) * fShakeAmount);
            shakeIntensity -= fShakeDecay;
            yield return false;
        }

        // �h�炷�O�̃J�������W�ɖ߂�
        ShakeObject.localPosition = VoldPos;
        ShakeObject.localRotation = QoldQuater;
    }
}