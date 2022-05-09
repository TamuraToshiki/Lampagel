//======================================================================
// EnemyData.cs
//======================================================================
// �J������
//
// 2022/04/08 author�F���쏫�V �G���G�̃X�e�[�^�X
// 2022/04/17 author�F���쏫�V �{�X�̃X�e�[�^�X�ǉ�
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

    // �{�X
    [Tooltip("�{�X�̃��x��")]  public int BossLevel = 1;
    [Tooltip("�{�X��HP")]      public int BossHp = 200;
    [Tooltip("�{�X�̍U����")]  public int BossAttack = 10;
    [Tooltip("�{�X�̑��x")]    public float BossSpeed = 4.0f;

    // ���x���A�b�v���̃X�e�[�^�X�㏸�l
    public int nUpHP { get; set; } = 1;
    public int nUpAttack { get; set; } = 1;


}
