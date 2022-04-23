//======================================================================
// GenerateEnemyData.cs
//======================================================================
// �J������
//
// 2022/04/23 author:�|���W�j�Y�@���x���f�U�C�����ʓ|�Ȃ̂Ő���
//                   �@�@�@�@�@�@Element0�Ƃ��ς�����
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(menuName = "Scriptable/CreateGenerateEnemyData")]

public class GenerateEnemyData : ScriptableObject
{
    [Serializable] public class StageData
    {
        [Header("-�ő�G��-")]
        [SerializeField] int MaxEnemy = 10;

        [Header("-�G���-")]
        [SerializeField] List<GameObject> EnemyList = new List<GameObject>();

        [Header("-�{�X-")]
        [SerializeField] GameObject BossEnemy = new GameObject();
    }

    [Header("<�e�X�e�[�W�G�������X�g>")]
    [Header("���e���X�g[5]�Œ�")]
    [SerializeField] StageData[] Planet1 = new StageData[5];

    [SerializeField] StageData[] Planet2 = new StageData[5];

    [SerializeField] StageData[] Planet3 = new StageData[5];

    [SerializeField] StageData[] Planet4 = new StageData[5];

    [SerializeField] StageData[] Planet5 = new StageData[5];

    [SerializeField] StageData[] Planet6 = new StageData[5];

    [SerializeField] StageData[] Planet7 = new StageData[5];

    
}


