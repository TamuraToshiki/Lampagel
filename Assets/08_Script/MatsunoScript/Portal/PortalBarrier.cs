using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBarrier : MonoBehaviour
{
    // �|�[�^���f�[�^
    public PortalData PortalData;

    void Start()
    {
    }

    void Update()
    {
       // Debug.Log(PortalData.Hp);
    }

    // �o���A�̓����蔻��
    private void OnCollisionEnter(Collision collision)
    {
        // �v���C���[�ƐڐG
        if (collision.transform.tag == "Player")
        {
            // �o���A�c�ʌ��炷
            PortalData.Hp--;

            if (PortalData.Hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
