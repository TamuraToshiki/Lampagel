//======================================================================
// GenerateCircleObject.cs
//======================================================================
// �J������
//
// 2022/04/08 author�F���쏫�V �C�x���g�I�u�W�F�N�g(�~��ɃI�u�W�F�N�g�z�u)�̎���
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCircleObject : MonoBehaviour
{
    // ��������I�u�W�F�N�g
    public GameObject CircleObject;

    // �����J�n�̏ꏊ
    public GameObject CenterObject;

    // ��������I�u�W�F�N�g�̐�
    public int ObjecCount = 40;

    // ���a
    public float distance = 5.0f;

<<<<<<< HEAD
=======
    void Start()
    {

    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Z))
        {
            GenerateCircle(CircleObject,   // ��������I�u�W�F�N�g
                           ObjecCount,     // �������鐔
                           CenterObject,   // ���S�̃I�u�W�F�N�g
                           distance,       // �e�I�u�W�F�N�g�̋���
                           true            // �����Ɍ����邩�ǂ���
                           );                                  
        }
    }

>>>>>>> matsuno
    public void GenerateCircle(GameObject prefab, int count, GameObject center, float distance, bool isLookAtCenter)
    {
        for (int i = 0; i < count; i++)
        {
            var position = center.transform.position + (Quaternion.Euler(0f, 360f / count * i, 0f) * center.transform.forward * distance);
            var obj = Instantiate(prefab, position, Quaternion.identity);

            // �����Ɍ����邩�ǂ���
            if (isLookAtCenter)
            {
                obj.transform.LookAt(center.transform);
            }
        }
    }

    // �v���C���[�ƐڐG�ŃV�[���J��
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            GenerateCircle(CircleObject,   // ��������I�u�W�F�N�g
                            ObjecCount,     // �������鐔
                            CenterObject,   // ���S�̃I�u�W�F�N�g
                            distance,       // �e�I�u�W�F�N�g�̋���
                            true            // �����Ɍ����邩�ǂ���
                            );
        }
    }
}
