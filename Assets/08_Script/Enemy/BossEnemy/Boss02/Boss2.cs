// Boss2.cs
//======================================================================
// �J������
//
// 2022/04/15 author�F���쏫�V �{�X2(�o�C�o�C��)�����J�n
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

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        //Move(myAgent, Player);
        //CreateDivision();
    }

    // �{�X�̕�������
    void CreateDivision()
    {

    }

}
