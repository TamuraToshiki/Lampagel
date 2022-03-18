//======================================================================
// GuardBurst.cs
//======================================================================
// �J������
//
// 2022/03/02 author�F�c���q�� �������A�G�ꂽ�I�u�W�F�N�g��
//                             ������΂��X�N���v�g�쐬                              
// 2022/03/11 author�F�c���q�� �������d���Ȃ肻�����������߁A�����蔻���
//                             ������΂����@����A���a���w�肷����@�ɕύX
//                             
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBurst : MonoBehaviour
{
    [SerializeField] private float radius = 5.0f;
    [SerializeField] private float power = 10.0f;
    [SerializeField] private float UpandDown = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void OnCollisionEnter(Collision col)
    //{
    //    Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

    //    // �v�f�̒��g�����[�v����
    //    foreach (Collider hit in colliders)
    //    {
    //        Rigidbody rb = hit.GetComponent<Rigidbody>();
    //        if (rb != null)
    //        {
    //            // ������΂�
    //            rb.AddExplosionForce(power, transform.position, radius);
    //        }
    //    }
    //}

    public void Explode()
    {
        // �G��T��
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

        // �v�f�̒��g�����[�v����
        foreach (GameObject hit in enemys)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            // �G�̕������Z��ON�ɕύX
            rb.isKinematic = false;

            if (rb != null)
            {
                // ������΂�
                rb.AddExplosionForce(power, transform.position, radius, UpandDown);
            }
        }
    }
}
