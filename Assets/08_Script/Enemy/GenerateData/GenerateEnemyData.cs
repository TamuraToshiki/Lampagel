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
        [SerializeField] public int MaxEnemy = 10;

        [Header("-�G���-")]
        [SerializeField] public List<GameObject> EnemyList = new List<GameObject>();

        [Header("-�{�X-")]
        [SerializeField] public GameObject BossEnemy;

        
    }

    private void Awake()
    {
        
    }

    [Header("<�e�X�e�[�W�G�������X�g>")]
    [Header("���e���X�g[5]�Œ�")]
    [SerializeField] public StageData[] Planet1 = new StageData[5];

    [SerializeField] public StageData[] Planet2 = new StageData[5];

    [SerializeField] public StageData[] Planet3 = new StageData[5];

    [SerializeField] public StageData[] Planet4 = new StageData[5];

    [SerializeField] public StageData[] Planet5 = new StageData[5];

    [SerializeField] public StageData[] Planet6 = new StageData[5];

    [SerializeField] public StageData[] Planet7 = new StageData[5];



    // �Ăяo����
    // generateEnemy.Planet1[1].MaxEnemy; <1-1> �́@�ő�G���擾
    // generateEnemy.Planet3[5].EnemyList; <3-5> �́@�o���G��ގ擾
}


