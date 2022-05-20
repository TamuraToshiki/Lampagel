//======================================================================
// EnemyManager.cs
//======================================================================
// �J������
//
// 2022/03/05 author�F�����x ����J�n�@�G���������ǉ�
// 2022/03/18 author�F�����x ��ʊO�ɓG����������悤��
// 2022/03/28 author�F�����x �G�̃��x���A�b�v�����ǉ�
// 2022/04/21 author�F�����x ���x����EnemyData����擾����悤�ɕύX
// 2022/04/26 author�F�����x ��ʊO����������ύX
// 2022/04/28 author�F�����x GenerateData����f�[�^���擾����悤�ɕύX
//                           Planet1�ɂ����Ή����Ă��Ȃ����߁A�����K�v����
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// �d���֎~
[DisallowMultipleComponent]

public class EnemyManager : MonoBehaviour
{
    // �G�̍ő吔
    [Header("�G�̐� MIN,MAX")]
    [SerializeField] Vector2Int vEnemyNum = new Vector2Int(0, 20);

    // �G�̍ő吔
    float fChangeNumCount = 0.0f;

    // �G�̍ő吔�ۑ��p
    int nTmpMaxNum;

    [Header("��������")]
    [SerializeField] float fDistance = 20.0f;

    [Header("�G�̃��x���A�b�v���x(�b)")]
    [SerializeField] float fLevelUpTime = 20.0f;

    // �G�̎��
    [SerializeField] List<GameObject> EnemyList;

    // �o�����Ă���G�̃��X�g
    public List<GameObject> NowEnemyList;

    // ���x���A�b�v�J�E���g
    float fLevelUpCount;

    // ���݂̓G�̃��x��
    public int nEnemyLevel = 1;

    // �{�X�o������
    public bool bBoss { get; set; } = false; 

    GameObject player;
    GameObject enemy;
    public GameObject Boss;

    [Header("�W�F�l���[�g�f�[�^")]
    [SerializeField] GenerateEnemyData GenerateEnemyData;

    //---------------
    // ������
    //---------------
    void Start()
    {
        player = GameObject.Find("Player");
        fLevelUpCount = fLevelUpTime;
        

        // �G������
        InitEnemy();

        // �V�[���ؑ֌��m
        SceneManager.activeSceneChanged += SceneChanged;


        DontDestroyOnLoad(this.gameObject);
    }

    //---------------
    // �X�V
    //---------------
    void Update()
    {

        // �G�̍ő吔�̑���
        ChangeNum();

        // �G���x���A�b�v
        LevelUp();

        

        // ��������V��������
        if (NowEnemyList.Count < vEnemyNum.y)
        {
            CreateEnemy();
        }

        RemoveNullData();
    }

    //---------------
    // �G�𐶐�
    //---------------
    private void CreateEnemy()
    {
        if (bBoss) return;

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
        // 1/2�̊m���ŕ��̒l�ɂ���
        int random = Random.Range(0, 1);
        if (random == 0) fDistance *= -1;

        // ��ʊO�̍��W�擾
        Vector3 vPos = Camera.main.ViewportToWorldPoint(new Vector3(fDistance, 0.0f, Camera.main.nearClipPlane));

        // Z���W�����炷
        vPos.z = Random.Range(player.transform.position.z - 5.0f, player.transform.position.z + 5.0f);

        return vPos;
    }

    //---------------
    // ���x���A�b�v����
    //---------------
    void LevelUp()
    {
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


    //-----------------------
    // �G�̐��𒲐�
    //-----------------------
    void ChangeNum()
    {
        // �J�E���g����
        fChangeNumCount += Time.deltaTime * 0.1f;

        // �G�̍ő吔�̑���
        vEnemyNum.y = (int)Mathf.Abs(nTmpMaxNum * Mathf.Sin(fChangeNumCount));
    }


    //-----------------------
    // �V�[�����Ƃɏ�����
    //-----------------------
    private void InitEnemy()
    {
        bBoss = false;

        // ���݃V�[���̎擾
        Scene scene = SceneManager.GetActiveScene();

        // ���݃V�[���̃r���h�ԍ��擾
        int nSceneNo = scene.buildIndex;

        //Debug.Log(nSceneNo);

        //// �G�̍ő吔��Generate����擾   *TODO* Planet1�����Ή����Ă��Ȃ����ߕύX�K�{
        //vEnemyNum.y = GenerateEnemyData.Planet1[nSceneNo].MaxEnemy;

        //// �o������G���X�g�擾   *TODO* Planet1�����Ή����Ă��Ȃ����ߕύX�K�{
        //EnemyList = GenerateEnemyData.Planet1[nSceneNo].EnemyList;

        switch(nSceneNo)
        {
            case 1:
                vEnemyNum.y = GenerateEnemyData.Planet1[nSceneNo-1].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet1[nSceneNo-1].EnemyList;
                Boss = GenerateEnemyData.Planet1[nSceneNo - 1].BossEnemy;
                break;
            case 2:
                vEnemyNum.y = GenerateEnemyData.Planet1[nSceneNo-1].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet1[nSceneNo-1].EnemyList;
                Boss = GenerateEnemyData.Planet1[nSceneNo - 1].BossEnemy;
                break;
            case 3:
                vEnemyNum.y = GenerateEnemyData.Planet1[nSceneNo-1].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet1[nSceneNo-1].EnemyList;
                Boss = GenerateEnemyData.Planet1[nSceneNo - 1].BossEnemy;
                break;
            case 4:
                vEnemyNum.y = GenerateEnemyData.Planet1[nSceneNo - 1].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet1[nSceneNo - 1].EnemyList;
                Boss = GenerateEnemyData.Planet1[nSceneNo - 1].BossEnemy;
                break;
            case 5:
                vEnemyNum.y = GenerateEnemyData.Planet1[nSceneNo - 1].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet1[nSceneNo - 1].EnemyList;
                Boss = GenerateEnemyData.Planet1[nSceneNo - 1].BossEnemy;
                break;

            case 6:
                vEnemyNum.y = GenerateEnemyData.Planet2[nSceneNo-6].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet2[nSceneNo-6].EnemyList;
                Boss = GenerateEnemyData.Planet2[nSceneNo - 6].BossEnemy;
                break;
            case 7:
                vEnemyNum.y = GenerateEnemyData.Planet2[nSceneNo - 6].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet2[nSceneNo - 6].EnemyList;
                Boss = GenerateEnemyData.Planet2[nSceneNo - 6].BossEnemy;
                break;
            case 8:
                vEnemyNum.y = GenerateEnemyData.Planet2[nSceneNo - 6].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet2[nSceneNo - 6].EnemyList;
                Boss = GenerateEnemyData.Planet2[nSceneNo - 6].BossEnemy;
                break;
            case 9:
                vEnemyNum.y = GenerateEnemyData.Planet2[nSceneNo - 6].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet2[nSceneNo - 6].EnemyList;
                Boss = GenerateEnemyData.Planet2[nSceneNo - 6].BossEnemy;
                break;
            case 10:
                vEnemyNum.y = GenerateEnemyData.Planet2[nSceneNo - 6].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet2[nSceneNo - 6].EnemyList;
                Boss = GenerateEnemyData.Planet2[nSceneNo - 6].BossEnemy;
                break;

            case 11:
                vEnemyNum.y = GenerateEnemyData.Planet3[nSceneNo - 11].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet3[nSceneNo - 11].EnemyList;
                Boss = GenerateEnemyData.Planet3[nSceneNo - 11].BossEnemy;
                break;
            case 12:
                vEnemyNum.y = GenerateEnemyData.Planet3[nSceneNo - 11].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet3[nSceneNo - 11].EnemyList;
                Boss = GenerateEnemyData.Planet3[nSceneNo - 11].BossEnemy;
                break;
            case 13:
                vEnemyNum.y = GenerateEnemyData.Planet3[nSceneNo - 11].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet3[nSceneNo - 11].EnemyList;
                Boss = GenerateEnemyData.Planet3[nSceneNo - 11].BossEnemy;
                break;
            case 14:
                vEnemyNum.y = GenerateEnemyData.Planet3[nSceneNo - 11].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet3[nSceneNo - 11].EnemyList;
                Boss = GenerateEnemyData.Planet3[nSceneNo - 11].BossEnemy;
                break;
            case 15:
                vEnemyNum.y = GenerateEnemyData.Planet3[nSceneNo - 11].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet3[nSceneNo - 11].EnemyList;
                Boss = GenerateEnemyData.Planet3[nSceneNo - 11].BossEnemy;
                break;

            case 16:
                vEnemyNum.y = GenerateEnemyData.Planet4[nSceneNo - 16].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet4[nSceneNo - 16].EnemyList;
                Boss = GenerateEnemyData.Planet4[nSceneNo - 16].BossEnemy;
                break;
            case 17:
                vEnemyNum.y = GenerateEnemyData.Planet4[nSceneNo - 16].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet4[nSceneNo - 16].EnemyList;
                Boss = GenerateEnemyData.Planet4[nSceneNo - 16].BossEnemy;
                break;
            case 18:
                vEnemyNum.y = GenerateEnemyData.Planet4[nSceneNo - 16].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet4[nSceneNo - 16].EnemyList;
                Boss = GenerateEnemyData.Planet4[nSceneNo - 16].BossEnemy;
                break;
            case 19:
                vEnemyNum.y = GenerateEnemyData.Planet4[nSceneNo - 16].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet4[nSceneNo - 16].EnemyList;
                Boss = GenerateEnemyData.Planet4[nSceneNo - 16].BossEnemy;
                break;
            case 20:
                vEnemyNum.y = GenerateEnemyData.Planet4[nSceneNo - 16].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet4[nSceneNo - 16].EnemyList;
                Boss = GenerateEnemyData.Planet4[nSceneNo - 16].BossEnemy;
                break;

            case 21:
                vEnemyNum.y = GenerateEnemyData.Planet5[nSceneNo - 21].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet5[nSceneNo - 21].EnemyList;
                Boss = GenerateEnemyData.Planet5[nSceneNo - 21].BossEnemy;
                break;
            case 22:
                vEnemyNum.y = GenerateEnemyData.Planet5[nSceneNo - 21].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet5[nSceneNo - 21].EnemyList;
                Boss = GenerateEnemyData.Planet5[nSceneNo - 21].BossEnemy;
                break;
            case 23:
                vEnemyNum.y = GenerateEnemyData.Planet5[nSceneNo - 21].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet5[nSceneNo - 21].EnemyList;
                Boss = GenerateEnemyData.Planet5[nSceneNo - 21].BossEnemy;
                break;
            case 24:
                vEnemyNum.y = GenerateEnemyData.Planet5[nSceneNo - 21].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet5[nSceneNo - 21].EnemyList;
                Boss = GenerateEnemyData.Planet5[nSceneNo - 21].BossEnemy;
                break;
            case 25:
                vEnemyNum.y = GenerateEnemyData.Planet5[nSceneNo - 21].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet5[nSceneNo - 21].EnemyList;
                Boss = GenerateEnemyData.Planet5[nSceneNo - 21].BossEnemy;
                break;

            case 26:
                vEnemyNum.y = GenerateEnemyData.Planet6[nSceneNo - 26].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet6[nSceneNo - 26].EnemyList;
                Boss = GenerateEnemyData.Planet6[nSceneNo - 26].BossEnemy;
                break;
            case 27:
                vEnemyNum.y = GenerateEnemyData.Planet6[nSceneNo - 26].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet6[nSceneNo - 26].EnemyList;
                Boss = GenerateEnemyData.Planet6[nSceneNo - 26].BossEnemy;
                break;
            case 28:
                vEnemyNum.y = GenerateEnemyData.Planet6[nSceneNo - 26].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet6[nSceneNo - 26].EnemyList;
                Boss = GenerateEnemyData.Planet6[nSceneNo - 26].BossEnemy;
                break;
            case 29:
                vEnemyNum.y = GenerateEnemyData.Planet6[nSceneNo - 26].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet6[nSceneNo - 26].EnemyList;
                Boss = GenerateEnemyData.Planet6[nSceneNo - 26].BossEnemy;
                break;
            case 30:
                vEnemyNum.y = GenerateEnemyData.Planet6[nSceneNo - 26].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet6[nSceneNo - 26].EnemyList;
                Boss = GenerateEnemyData.Planet6[nSceneNo - 26].BossEnemy;
                break;

            case 31:
                vEnemyNum.y = GenerateEnemyData.Planet7[nSceneNo - 31].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet7[nSceneNo - 31].EnemyList;
                Boss = GenerateEnemyData.Planet7[nSceneNo - 31].BossEnemy;
                break;
            case 32:
                vEnemyNum.y = GenerateEnemyData.Planet7[nSceneNo - 31].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet7[nSceneNo - 31].EnemyList;
                Boss = GenerateEnemyData.Planet7[nSceneNo - 31].BossEnemy;
                break;
            case 33:
                vEnemyNum.y = GenerateEnemyData.Planet7[nSceneNo - 31].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet7[nSceneNo - 31].EnemyList;
                Boss = GenerateEnemyData.Planet7[nSceneNo - 31].BossEnemy;
                break;
            case 34:
                vEnemyNum.y = GenerateEnemyData.Planet7[nSceneNo - 31].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet7[nSceneNo - 31].EnemyList;
                Boss = GenerateEnemyData.Planet7[nSceneNo - 31].BossEnemy;
                break;
            case 35:
                vEnemyNum.y = GenerateEnemyData.Planet7[nSceneNo - 31].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet7[nSceneNo - 31].EnemyList;
                Boss = GenerateEnemyData.Planet7[nSceneNo - 31].BossEnemy;
                break;

            default:
                Debug.LogError("���݂��Ȃ��V�[�� : " + nSceneNo );
                break;
        }

        // �G�̍ő吔�ۑ�
        nTmpMaxNum = vEnemyNum.y;
    }

    //-----------------------
    // ���X�g����
    //-----------------------
    void RemoveNullData()
    {
        if(NowEnemyList.Count > 0)
        {
            for (int i = 0; i < NowEnemyList.Count; i++)
            {
                if (NowEnemyList[i] == null)
                {
                    Debug.Log("���X�g����폜");
                    NowEnemyList.Remove(NowEnemyList[i]);
                }
            }
        }
        
    }

    //-----------------------
    // �V�[���ؑ֎��A����������
    //-----------------------
    void SceneChanged(Scene thisScene, Scene nextScene)
    {
        InitEnemy();
    }
}
