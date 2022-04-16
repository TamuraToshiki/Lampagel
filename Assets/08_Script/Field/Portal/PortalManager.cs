//======================================================================
// SceneObject.cs
//======================================================================
// �J������
//
// 2022/03/28 author�F���쏫�V �e�|�[�^���o���A�̔j��
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����R�[�h
public class PortalManager : MonoBehaviour
{
    public GameObject PortalA;
    public GameObject PortalB;


    void Start()
    {

    }

    void Update()
    {
        if (PortalA != null && PortalB != null)
        {
            DestroyPortal(PortalA, PortalB);
            DestroyPortal(PortalB, PortalA);
        }
    }

    void DestroyPortal(GameObject Portal1, GameObject Portal2)
    {
        if (Portal1.gameObject != null)
        {
            
            if (Portal2.GetComponentInChildren<PortalBarrier>() == null)
            {
                Destroy(Portal1.gameObject);
            }
        }
    }
}
