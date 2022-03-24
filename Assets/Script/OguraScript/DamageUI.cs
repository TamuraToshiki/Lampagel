//======================================================================
// DamageUI.cs
//======================================================================
// �J������
//
// 2022/03/15 author�F�����x ����J�n�@�_���[�W�\�L����
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUI : MonoBehaviour
{
    // ������b��
    [SerializeField] private float fDeleteTime = 1.0f;

    // ��������
    [SerializeField] private float fMoveRange = 1.0f;

    // �J�E���^�[
    private float TimeCnt;

    private TextMesh NowText;

    void Start()
    {
        TimeCnt = 0.0f;
        Destroy(this.gameObject, fDeleteTime);
        NowText = this.gameObject.GetComponent<TextMesh>();
    }


    void Update()
    {
        // �J�����̌����Ɍ���
        //this.transform.LookAt(Camera.main.transform);

        TimeCnt += Time.deltaTime;

        // �e�L�X�g�̓���
        this.gameObject.transform.localPosition += new Vector3(0, fMoveRange / fDeleteTime * Time.deltaTime, 0);

        // �e�L�X�g�����x
        float alpha = 1.0f - (TimeCnt / fDeleteTime);
        if (alpha <= 0.0f) alpha = 0.0f;

        // �e�L�X�g�J���[
        NowText.color = new Color(NowText.color.r, NowText.color.g, NowText.color.b, alpha);
    }
}