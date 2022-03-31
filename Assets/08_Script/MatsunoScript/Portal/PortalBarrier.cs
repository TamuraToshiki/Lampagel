using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBarrier : MonoBehaviour
{
    // �|�[�^���f�[�^
    public PortalData PortalData;
    int nHp = 2;

    void Start()
    {
    }

    void Update()
    {
       Debug.Log("����" + nHp + "��Ŋ����");
    }

    // �o���A�̓����蔻��
    private void OnCollisionEnter(Collision collision)
    {
        // �v���C���[�ƐڐG
        if (collision.transform.tag == "Player")
        {
            // �o���A�c�ʌ��炷
            PortalData.Hp--;
            nHp--;

            if (nHp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
