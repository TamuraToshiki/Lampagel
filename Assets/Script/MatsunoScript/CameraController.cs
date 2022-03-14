using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //�v���C���[���i�[�p
    private GameObject player;
    // ���΋����擾�p
    private Vector3 offset;

    void Start()
    {
        //�@Player�̏����擾
        this.player = GameObject.Find("Player");

        // �J������Player�Ƃ̑��΋��������߂�
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        //�@�V�����g�����X�t�H�[���̒l����
        transform.position = player.transform.position + offset;
    }
}
