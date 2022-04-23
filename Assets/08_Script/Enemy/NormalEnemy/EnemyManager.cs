//======================================================================
// EnemyManager.cs
//======================================================================
// �J������
//
// 2022/03/05 author�F�����x ����J�n�@�G���������ǉ�
// 2022/03/18 author�F�����x ��ʊO�ɓG����������悤��
// 2022/03/28 author�F�����x �G�̃��x���A�b�v�����ǉ�
// 2022/04/21 author�F�����x ���x����EnemyData����擾����悤�ɕύX
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

    // �v���C���[�Ƃǂꂾ������Đ������邩
    [Header("��������")] [SerializeField] Vector2 vDistance = new Vector2(15.0f, 8.0f);
    Vector2 vInstantePos;

    // �G�̎��
    [SerializeField] List<GameObject> EnemyList;

    // �o�����Ă���G�̃��X�g
    public List<GameObject> NowEnemyList;

    // �G�̃��x���A�b�v�֘A
    [Header("�G�̃��x���A�b�v�b��")] [SerializeField] float fLevelUpTime = 20.0f;
    float fLevelUpCount;
    int nEnemyLevel = 0;

    GameObject player;
    GameObject enemy;

    int debug = 0;

    
    
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
        for (int i = 0; i < MaxEnemy; i++)
        {
            CreateEnemy();
        }

        
        //DontDestroyOnLoad(this.gameObject);
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
        if (fLevelUpCount < 0.0f)
        {
            // ������
            fLevelUpCount = fLevelUpTime;

            // �G�̃��x�����グ��
            nEnemyLevel++;
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
        enemy.GetComponent<EnemyBase>().GetEnemyData.nLevel = nEnemyLevel;

        // �}�l�[�W�����Z�b�g
        enemy.GetComponent<EnemyBase>().manager = gameObject.GetComponent<EnemyManager>();

        // �v���C���[���Z�b�g
        enemy.GetComponent<EnemyBase>().player = player;

        // ���X�g�ɒǉ�
        NowEnemyList.Add(enemy);
    }

    //---------------
    // ���W�v�Z
    //---------------
    private Vector3 CreatePos()
    {
        // �v���C���[�̍��[�̈ʒu�����߂�
        Vector2 tmpPos = new Vector2(player.transform.position.x - vInstantePos.x, player.transform.position.z - vInstantePos.y);

        // �o���ʒu�������_���Ɍv�Z�i�v���C���[�̍��[����E�[�̊ԂŐ����j
        Vector3 vPos = new Vector3(Random.Range(tmpPos.x, tmpPos.x + (vInstantePos.x * 2)), 0.5f, Random.Range(tmpPos.y, tmpPos.y + (vInstantePos.y * 2)));

        // �v���C���[�Ƃ̋������v�Z
        Vector3 vCreatePos = vPos - player.transform.position;

        // ��ʊO�łȂ���΁A������x�v�Z  �iTODO:���܂�悭�Ȃ��B���P�̗]�n����j
        while ((vCreatePos.x < vDistance.x && vCreatePos.x > -vDistance.x) && (vCreatePos.y < vDistance.y && vCreatePos.y > -vDistance.y))
        {
            vPos = new Vector3(Random.Range(tmpPos.x, tmpPos.x + (vInstantePos.x * 2)), 0.5f, Random.Range(tmpPos.y, tmpPos.y + (vInstantePos.y * 2)));
            vCreatePos = vPos - player.transform.position;

            // �����I��(�������[�v�ɓ���Ȃ��悤��)
            debug++;
            if (debug > 100)
            {
                Debug.Log("�K�����G���[");
                debug = 0;
                return vPos;
            }
        }

        return vPos;
    }
}
