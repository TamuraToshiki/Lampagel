using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �X�t�B�A�R���C�_�[��v������
[RequireComponent(typeof(SphereCollider))]
public class GuardBurst : MonoBehaviour
{
    [SerializeField] private float radius = 5.0f;
    [SerializeField] private float power = 10.0f;

    SphereCollider col;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<SphereCollider>();
        radius = col.radius;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        // �v�f�̒��g�����[�v����
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // ������΂�
                rb.AddExplosionForce(power, transform.position, radius);
            }
        }
    }
}
