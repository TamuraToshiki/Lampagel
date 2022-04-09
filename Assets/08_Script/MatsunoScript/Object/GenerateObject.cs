//======================================================================
// GenerateObject.cs
//======================================================================
// �J������
//
// 2022/03/21 author�F���쏫�V �w��͈͓��ŃI�u�W�F�N�g��������
// 2022/04/08 author�F���쏫�V �㉺���E4�ӏ��œK�p(�v���t�@�N�^�����O)
// 2022/04/09 author�F���쏫�V ��������I�u�W�F�N�g�������_����
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObject : MonoBehaviour
{
    // �I�u�W�F�N�g�f�[�^
    public ObjectData ObjectData;
    // ��������͈�A
    [SerializeField]
    private Transform rangeA;
    // ��������͈�B
    [SerializeField]
    private Transform rangeB;
    // �o�ߎ���
    private float time;

    void Update()
    {
        // �O�t���[������̎��Ԃ����Z
        time = time + Time.deltaTime;

        // �b���Ń����_���ɐ��������悤�ɂ���
        if (time > ObjectData.GenerateTime)
        {
            // rangeA��rangeB��x���W�͈͓̔��Ń����_���Ȑ��l���쐬
            float x = Random.Range(rangeA.position.x, rangeB.position.x);
            // rangeA��rangeB��y���W�͈͓̔��Ń����_���Ȑ��l���쐬
            float y = 0.0f;
            // rangeA��rangeB��z���W�͈͓̔��Ń����_���Ȑ��l���쐬
            float z = Random.Range(rangeA.position.z, rangeB.position.z);

            // ��������I�u�W�F�N�g�������_���Ō���
            int OvjectNo = Random.Range((int)ObjectData.FieldObject.eFieldObject1, (int)ObjectData.FieldObject.eFieldObjectMax);

            // GameObject����L�Ō��܂��������_���ȏꏊ�ɐ���
            Instantiate(ObjectData.FieldObjectList[OvjectNo], new Vector3(x, y, z), this.transform.rotation);

            // �o�ߎ��ԃ��Z�b�g
            time = 0f;
        }
    }
}
