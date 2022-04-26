//======================================================================
// Boss2.cs
//======================================================================
// �J������
//
// 2022/04/15 author�F���쏫�V �{�X2(�o�C�o�C��)�����J�n
// 2022/04/26 author�F���쏫�V �A�j���[�V�����ǉ�(�ړ��E�U��)
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss2 : MonoBehaviour
{
    // �v���C���[���
    [SerializeField] private GameObject player;

    // ���􂷂�G���Ie
    [SerializeField] private GameObject DivisionEnemy;

    private Animator animation;

    void Start()
    {
        player = GameObject.Find("Player");

        animation = GetComponent<Animator>();
    }

    void Update()
    {
        //Move(myAgent, Player);
        //CreateDivision();

        if(Input.GetKey(KeyCode.O))
        {
            animation.SetTrigger("attack");
        }
    }

    // �{�X�̕�������
    void CreateDivision(int DivisionCnt)
    {
        for (int i = 0; i < DivisionCnt; i++)
        {
            Instantiate(DivisionEnemy, new Vector3(0, 0, 20), this.transform.rotation);
        }
    }

    // �v���C���[�Ƃ̐ڐG��
    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[�Ƃ̏Փˎ��_���[�W
        if (other.CompareTag("Player"))
        {
            CreateDivision(2);
        }
    }

}
