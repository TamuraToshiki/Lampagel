//======================================================================
// EnemyManager.cs
//======================================================================
// �J������
//
// 2022/03/05 author�F�����x ����J�n�@�G���������ǉ�
<<<<<<< HEAD:Assets/Script/OguraScript/EnemyManager.cs
=======
// 2022/03/18 author�F�����x ��ʊO�ɓG����������悤��
// 2022/03/28 author�F�����x �G�̃��x���A�b�v�����ǉ�
>>>>>>> d2f65eada7be6604d61b693afd0e28d3b8accd2c:Assets/08_Script/OguraScript/EnemyManager.cs
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
<<<<<<< HEAD:Assets/Script/OguraScript/EnemyManager.cs
=======

    // �v���C���[�Ƃǂꂾ������Đ������邩
    [Header("��������")] [SerializeField] Vector2 vDistance = new Vector2(10.0f, 5.0f);
>>>>>>> d2f65eada7be6604d61b693afd0e28d3b8accd2c:Assets/08_Script/OguraScript/EnemyManager.cs

    // �G�̎��
    [SerializeField] List<GameObject> EnemyList;
    // �o�����Ă���G�̃��X�g
    public List<GameObject> NowEnemyList;
    // �G�����̎���
    float fCreateTime = 1.0f;

<<<<<<< HEAD:Assets/Script/OguraScript/EnemyManager.cs
=======
    // �G�̃��x���A�b�v�֘A
    [Header("�G�̃��x���A�b�v�b��")][SerializeField]float fLevelUpTime = 20.0f;
    float fLevelUpCount;
    int nEnemyLevel = 0;

>>>>>>> d2f65eada7be6604d61b693afd0e28d3b8accd2c:Assets/08_Script/OguraScript/EnemyManager.cs
    GameObject player;
    GameObject enemy;


    //---------------
    // ������
    //---------------
    void Start()
    {
        player = GameObject.Find("Player");
        fLevelUpCount = fLevelUpTime;

        // �G����
        for (int i = 0; i < MaxEnemy;i++)
        {
            CreateEnemy();
        }
    }

    //---------------
    // �X�V
    //---------------
    void Update()
    {
        // ��������V��������
        if (NowEnemyList.Count < MaxEnemy)
        {
            // 1�b���ɐ���(��)
            fCreateTime -= Time.deltaTime;
            if(fCreateTime < 0.0f)
            {
                CreateEnemy();
                fCreateTime = 1.0f;
            }
        }

        // ���Ԃɉ����ēG�̃��x���A�b�v
        fLevelUpCount -= Time.deltaTime;
        if(fLevelUpCount < 0.0f)
        {
            // ������
            fLevelUpCount = fLevelUpTime;

            // �G�̃��x�����グ��
            nEnemyLevel++;

            Debug.Log(nEnemyLevel);
        }

    }

    //---------------
    // �G�𐶐�
    //---------------
    private void CreateEnemy()
    {
        // �G���擾
        enemy = Instantiate(EnemyList[Random.Range(0, EnemyList.Count)], CreatePos(), Quaternion.identity);

        // �G���x���Z�b�g
        enemy.GetComponent<StatusComponent>().Level = nEnemyLevel;

        enemy.GetComponent<EnemyBase>().SetManager(gameObject.GetComponent<EnemyManager>());
        enemy.GetComponent<EnemyBase>().SetPlayer(player);
        NowEnemyList.Add(enemy);
    }

    //---------------
    // ���W�v�Z
    //---------------
    private Vector3 CreatePos()
    {
        Vector3 vPos = new Vector3(Random.Range(-InstantiateX, InstantiateX), 1.0f, Random.Range(-InstantiateZ, InstantiateZ));
        return vPos;
    }
}
