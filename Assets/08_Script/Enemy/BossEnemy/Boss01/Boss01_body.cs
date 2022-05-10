
//======================================================================
// boss01_body.cs
//======================================================================
// �J������
//
// 2022/04/06 author�F�����x ����J�n�@�{�X�̑̏���
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss01_body : MonoBehaviour
{
    // ���f�����m�̋���
    float fDistance = 1.2f;

    // �Ǐ]���f���̉�]���x
    float fRotSpeed = 3.0f;

    // ���g�̑O�ɂ���I�u�W�F�N�g�i�̂Ȃ瓪�A���Ȃ�́j
    GameObject FrontObject;

    // �{�X�̓����
    GameObject HeadObject;

    public void SetBossFront(GameObject obj) { FrontObject = obj; }
    public void SetBossHead(GameObject obj) { HeadObject = obj; }



    void Update()
    {
        DeathHead();

        // �̂̍��W�𒲐�
        gameObject.transform.position = new Vector3(FrontObject.transform.position.x - FrontObject.transform.forward.x * fDistance, 
                                                    FrontObject.transform.position.y,
                                                    FrontObject.transform.position.z - FrontObject.transform.forward.z * fDistance);


        // ���ɂ�������]����悤��
        Quaternion rot = Quaternion.LookRotation(FrontObject.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, fRotSpeed * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[�Ƃ̏Փˎ��_���[�W
        if (other.CompareTag("Player"))
        {
            // �_���[�W����
            HeadObject.GetComponent<EnemyDamageBase>().TailDamage();
        }
    }

    // ���S����
    private void DeathHead()
    {
        if (HeadObject.GetComponent<EnemyBase>().nHp <= 0)
            Destroy(this.gameObject);
    }
}
