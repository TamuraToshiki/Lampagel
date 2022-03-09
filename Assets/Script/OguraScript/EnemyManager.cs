//======================================================================
// Player.cs
//======================================================================
// �J������
//
// 2022/03/02 author�F���쏫�V �Z�Z�쐬
// 2022/03/03 author�F���c�B�� �Z�Z�̏����ǉ�
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
    //public GameObject GetPlayer { get { return player; } }

    GameObject enemy;

    // Start is called before the first frame update
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
            CreateEnemy();
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
