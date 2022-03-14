//======================================================================
// EnemyManager.cs
//======================================================================
// �J������
//
// 2022/03/05 ����J�n �G�o�������ǉ�
// 2022/03/11 �G�������x�ifCreateTime�j�̒ǉ�
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �d���֎~
[DisallowMultipleComponent]

public class EnemyManager : MonoBehaviour
{
    // �G�̍ő吔
    [Header("�G�̐���MAX")] [SerializeField] int MaxEnemy = 2;
    // �o���͈�
    [Header("�G�̏o�����W�͈�")] [SerializeField, Range(1.0f, 100.0f)] float InstantiateX = 6.5f;
    [SerializeField, Range(1.0f, 100.0f)] float InstantiateZ = 3.5f;

    // �G�̎��
    [SerializeField] List<GameObject> EnemyList;
    // �o�����Ă���G�̃��X�g
    public List<GameObject> NowEnemyList;

    GameObject player;
    GameObject enemy;

    // �G�����^�C��
    private float fCreateTime = 1.0f;


    void Start()
    {
        player = GameObject.Find("Player");

        for(int i = 0; i < MaxEnemy;i++)
        {
            CreateEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ��������V��������
        if (NowEnemyList.Count < MaxEnemy)
        {
            // 1�b�o�߂œG����
            fCreateTime -= Time.deltaTime;
            if(fCreateTime < 0.0f)
            {
                CreateEnemy();
                fCreateTime = 1.0f;
            }
        }
    }

    // �G�𐶐�
    private void CreateEnemy()
    {
        enemy = Instantiate(EnemyList[Random.Range(0, EnemyList.Count)], CreatePos(), Quaternion.identity);
        enemy.GetComponent<EnemyBase>().SetManager(gameObject.GetComponent<EnemyManager>());
        enemy.GetComponent<EnemyBase>().SetPlayer(player);
        NowEnemyList.Add(enemy);
    }

    private Vector3 CreatePos()
    {
        Vector3 vPos;
        vPos = new Vector3(Random.Range(-InstantiateX, InstantiateX), 1.0f, Random.Range(-InstantiateZ, InstantiateZ));
        return vPos;
    }
}
