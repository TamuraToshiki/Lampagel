//======================================================================
// EnemyData.cs
//======================================================================
// �J������
//
// 2022/04/08 author�F���쏫�V �G�̃f�[�^
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/CreateEnemyData")]

public class EnemyData : ScriptableObject
{

    // public List<EnemyStatus> EnemyStatusList = new List<EnemyStatus>();

    // ��{�X�e�[�^�X
    public int nLevel { get; set; } = 1;
    public int nHp { get; set; } = 20;
    public int nAttack { get; set; } = 10;
    public float fSpeed { get; set; } = 1.0f;

    // ���x���A�b�v���̃X�e�[�^�X�㏸�l
    public int nUpHP { get; set; } = 1;
    public int nUpAttack { get; set; } = 1;


}
