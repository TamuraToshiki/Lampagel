//======================================================================
// FireWall.cs
//======================================================================
// �J������
//
// 2022/05/11 author �|���F�΂̕Ǐ���
//                         �A�j���[�V�����C�x���g�ɋN�����ċN����
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour
{

    public bool bInArea = false;

    private void Start()
    {
        bInArea = false;
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("�͈͓�");
            bInArea = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("�͈͊O");
            bInArea = false;
        }
    }


}
