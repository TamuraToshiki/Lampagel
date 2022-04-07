//======================================================================
// MoveObject.cs
//======================================================================
// �J������
//
// 2022/04/08 author�F���쏫�V �v���C���[�ɍ��킹�ăI�u�W�F�N�g���ړ�������
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        Transform a = this.transform;
        Vector3 point = a.position;
        point.x = +Camera.main.transform.position.x;
        point.y = 0f;
        point.z = +Camera.main.transform.position.z;

        a.position = point;
    }
}
