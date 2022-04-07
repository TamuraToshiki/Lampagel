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

<<<<<<< HEAD
    // �o���͈�
    [Header("�G�̏o�����W�͈�")] [SerializeField, Range(1.0f, 100.0f)] float InstantiateX = 6.5f;
    [SerializeField, Range(1.0f, 100.0f)] float InstantiateZ = 3.5f;
<<<<<<< HEAD:Assets/Script/OguraScript/EnemyManager.cs
=======

    // �v���C���[�Ƃǂꂾ������Đ������邩
    [Header("��������")] [SerializeField] Vector2 vDistance = new Vector2(10.0f, 5.0f);
>>>>>>> d2f65eada7be6604d61b693afd0e28d3b8accd2c:Assets/08_Script/OguraScript/EnemyManager.cs
=======
    //// �o���͈�
    //[Header("�G�̏o�����W�͈�")] [SerializeField, Range(1.0f, 100.0f)] float InstantiateX = 6.5f;
    //[SerializeField, Range(1.0f, 100.0f)] float InstantiateZ = 3.5f;

    // �v���C���[�Ƃǂꂾ������Đ������邩
    [Header("��������")] [SerializeField] Vector2 vDistance = new Vector2(15.0f, 8.0f);
    Vector2 vInstantePos;
>>>>>>> e2853f8ad6986fc67b6af3dfd7a583e04154f030

    // �G�̎��
    [SerializeField] List<GameObject> EnemyList;
    // �o�����Ă���G�̃��X�g
    public List<GameObject> NowEnemyList;

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

        // �o���͈͂�ݒ�
        vInstantePos = new Vector2(vDistance.x * 1.5f, vDistance.y * 1.5f);

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
            CreateEnemy();
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
<<<<<<< HEAD
        Vector3 vPos = new Vector3(Random.Range(-InstantiateX, InstantiateX), 1.0f, Random.Range(-InstantiateZ, InstantiateZ));
=======
        // �v���C���[�̍��[�̈ʒu�����߂�
        Vector2 tmpPos = new Vector2(player.transform.position.x - vInstantePos.x,player.transform.position.z - vInstantePos.y);

        // �o���ʒu�������_���Ɍv�Z�i�v���C���[�̍��[����E�[�̊ԂŐ����j
        Vector3 vPos = new Vector3(Random.Range(tmpPos.x, tmpPos.x + (vInstantePos.x * 2)), 0.5f, Random.Range(tmpPos.y, tmpPos.y + (vInstantePos.y * 2)));

        // �v���C���[�Ƃ̋������v�Z
        Vector3 vCreatePos = vPos - player.transform.position;

        // ��ʊO�łȂ���΁A������x�v�Z
        while ((vCreatePos.x < vDistance.x && vCreatePos.x > -vDistance.x) && (vCreatePos.y < vDistance.y && vCreatePos.y > -vDistance.y))
        {
            vPos = new Vector3(Random.Range(tmpPos.x, tmpPos.x + (vInstantePos.x * 2)), 0.5f, Random.Range(tmpPos.y, tmpPos.y + (vInstantePos.y * 2)));
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

>>>>>>> e2853f8ad6986fc67b6af3dfd7a583e04154f030
        return vPos;
    }
}
