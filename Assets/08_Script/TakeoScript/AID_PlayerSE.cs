//======================================================================
// AID_PlayerEffect.cs
//======================================================================
// �J������
//
// 2022/03/28 author�F�|���@���}�@�v���C���[�̃G�t�F�N�g����
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AID_PlayerSE : MonoBehaviour
{
    [Header("����SE")]
    [SerializeField] List<AudioClip> PlayerSEList;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        // �e�X�g
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartPlayerSE(0);
        }
    }

    public void StartPlayerSE(int listnum)
    {
        audioSource.PlayOneShot(PlayerSEList[listnum]);
    }
}
