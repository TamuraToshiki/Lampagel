//======================================================================
// EnemyManager.cs
//======================================================================
// �J������
//
// 2022/03/05 author�F�����x ����J�n�@�G���������ǉ�
// 2022/03/18 author�F�����x ��ʊO�ɓG����������悤��
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
    // �v���C���[�Ƃǂꂾ������Đ������邩
    [Header("��������")] [SerializeField] Vector2 vDistance = new Vector2(10.0f, 5.0f);

    // �G�̎��
    [SerializeField] List<GameObject> EnemyList;
    // �o�����Ă���G�̃��X�g
    public List<GameObject> NowEnemyList;
    // �G�����̎���
    float fCreateTime = 1.0f;

    

    GameObject player;
    GameObject enemy;

    int debug = 0;


    void Start()
    {
        player = GameObject.Find("Player");

        // �G����
        for (int i = 0; i < MaxEnemy;i++)
        {
            CreateEnemy();
        }
    }

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
        // �o���ʒu�������_���Ɍv�Z
        Vector3 vPos = new Vector3(Random.Range(-InstantiateX, InstantiateX), 0.5f, Random.Range(-InstantiateZ, InstantiateZ));

        // �v���C���[�Ƃ̋������v�Z
        Vector3 vCreatePos = vPos - player.transform.position;

        // ��ʊO�łȂ���΁A������x�v�Z
        while ((vCreatePos.x < vDistance.x && vCreatePos.x > -vDistance.x) && (vCreatePos.y < vDistance.y && vCreatePos.y > -vDistance.y))
        {
            vPos = new Vector3(Random.Range(-InstantiateX, InstantiateX), 0.5f, Random.Range(-InstantiateZ, InstantiateZ));
            vCreatePos = vPos - player.transform.position;

            // �����I��(�������[�v�ɓ���Ȃ��悤��)
            debug++;
            if(debug > 100)
            {
                Debug.Log("�K�����G���[");
                debug = 0;
                return vPos;
            }
        }

        return vPos;
    }
}