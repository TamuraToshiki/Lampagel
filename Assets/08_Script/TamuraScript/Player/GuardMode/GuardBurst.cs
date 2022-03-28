//======================================================================
// GuardBurst.cs
//======================================================================
// �J������
//
// 2022/03/02 author�F�c���q�� �������A�G�ꂽ�I�u�W�F�N�g��
//                             ������΂��X�N���v�g�쐬                              
// 2022/03/11 author�F�c���q�� �������d���Ȃ肻�����������߁A�����蔻���
//                             ������΂����@����A���a���w�肷����@�ɕύX
// 2022/03/27 author�F�c���q�� �G�̃_���[�W�ňЗ͂��ς��悤�ϓ�
//                             
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBurst : MonoBehaviour
{
    [SerializeField] private float UpandDown = 10.0f;

    PlayerStatus status;

    // Start is called before the first frame update
    void Start()
    {
        status = GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explode(float power)
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
                rb.AddExplosionForce(power * status.MaxBurstPower, transform.position, power * status.MaxBurstRadisu, UpandDown);
            }
        }
    }
}
