//======================================================================
// Stop.cs
//======================================================================
// �J������
//
// 2022/03/1 author�F�c���q�� �ړ����x��0�ɂ���
// 2022/03/5 author�F�c���q�� �v�����i�[�˗��B���X�Ɏ~�܂�悤�ɕύX                                                          
//                             
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void DoStop(Rigidbody rb)
    {
        // ��~�\�Ȃ��~����
        rb.velocity *= 0.9f;
    }
}
