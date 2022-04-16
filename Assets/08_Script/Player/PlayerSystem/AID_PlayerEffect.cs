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

public class AID_PlayerEffect : MonoBehaviour
{
    [Header("�����G�t�F�N�g")]
    [SerializeField] List<GameObject> EffectList;

    private void Update()
    {
        // �e�X�g
        if(Input.GetKeyDown(KeyCode.E))
        {
            StartEffect(0, this.gameObject, 1.0f);
        }
    }

    public void StartEffect(int listnum, GameObject player, float time)
    {
        GameObject Effect = null;
        Effect = Instantiate(EffectList[listnum], player.transform.position, player.transform.rotation);
        Destroy(Effect, time);
    }
}
