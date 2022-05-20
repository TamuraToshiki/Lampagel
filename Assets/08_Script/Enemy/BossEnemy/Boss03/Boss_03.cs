//======================================================================
// Boss03.cs
//======================================================================
// �J������
//
// 2022/05/11 author �|���F�G���_�[�}�W�V��������
//                         �A�j���[�V�����C�x���g�ɋN�����ċN����
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_03 : MonoBehaviour
{
    // �v���C���[
    private GameObject player;
    // �{�X�̊��N���X
    private EnemyBase BossBase;
    // FireWall�͈̔̓I�u�W�F�N�g
    public GameObject BurstCircle;
    // �G�t�F�N�g�֘A
    EnemyEffect enemyEffect;
    GameObject objEffect;

    [SerializeField] private GameObject Circle;

    // �T�[�N���T�C�Y
    [Header("�\���T�[�N���̃T�C�Y")] [SerializeField] private Vector3 vCircleSize = new Vector3(0.5f, 0.5f, 0.5f);

    // �Β��T�C�Y
    [Header("�Β��T�C�Y")] [SerializeField] private Vector3 vFireSize = new Vector3(0.5f, 3.0f, 0.5f);

    // �Ռ��g�T�C�Y
    [Header("�Ռ��g�ŏ��T�C�Y")] [SerializeField] private Vector3 vBurstMinSize = new Vector3(0.1f, 0.1f, 0.1f);
    [Header("�Ռ��g�ő�T�C�Y")] [SerializeField] private Vector3 vBurstMaxSize = new Vector3(20.0f, 0.1f, 20.0f);
    public bool IsBurst = false;

    public int nCircleInterval = 45;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        BossBase = GetComponent<EnemyBase>();

        // �G�t�F�N�g�擾
        enemyEffect = GetComponent<EnemyEffectBase>().GetEffect;
        BurstCircle.SetActive(false);
    }

    

    // �A���Β� ************************************************
    // �A�j���[�V�����C�x���g�����܂���
    private void FirePillarAttack()
    {
        GameObject cube;

        // �����蔻��p�L���[�u
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // �����蔻��p�L���[�u���\��
        cube.GetComponent<MeshRenderer>().enabled = false;

        // �Β��̃T�C�Y�A���W�A�p�x�ݒ�
        cube.transform.localScale = vFireSize;
        transform.Rotate(-90.0f, 0.0f, 0.0f);
        cube.transform.position = player.transform.position;

        // �Β��R���|�[�l���g����
        // ���蔲���锻��ɂ���
        cube.GetComponent<BoxCollider>().isTrigger = true;

        // �K�v�ȏ����Z�b�g
        Circle.transform.localScale = vCircleSize;

        // Fire.cs�ǉ�
        cube.AddComponent<Fire>();

        // �U���T�[�N���Z�b�g
        cube.GetComponent<Fire>().SetCircle(Circle);

        // �G���Z�b�g
        cube.GetComponent<Fire>().enemy = gameObject;

        // �v���C���[���Z�b�g
        cube.GetComponent<Fire>().player = player;

        // �v���C���[���Z�b�g
        cube.GetComponent<Fire>().effect = enemyEffect;
    }
    //**********************************************************



    // �A���Ռ��g **********************************************
    void StartBurst()
    {
        if(IsBurst == false)
        {
            IsBurst = true;
            StartCoroutine(BurstWave(1));
            Debug.Log("����");
        }
        
    }

    private IEnumerator BurstWave(int atkCount)
    {
        for(int n = 0; n <= atkCount; n++)
        {
            Debug.Log(n);
            // �Ռ��g�I�u�W�F�N�g����
            GameObject circle;
            circle = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

            // �Ռ��g�̃T�C�Y�A���W�A�p�x�ݒ�
            circle.transform.localScale = vBurstMinSize;
            circle.transform.rotation = this.transform.rotation;
            circle.transform.position = this.transform.position;
            circle.GetComponent<CapsuleCollider>().isTrigger = true;

            // BurstCircle�ǉ��A���Z�b�g
            circle.AddComponent<BurstCircle>();
            BurstCircle burstcircle = circle.GetComponent<BurstCircle>();
            burstcircle.SetPlayer(BossBase.GetComponent<EnemyBase>().player);
            burstcircle.SetEnemy(gameObject);

            float sizePercent;

            // �L����Ռ��g
            for (int i = 0; i <= nCircleInterval; i++)
            {

                yield return null;
                sizePercent = (float)i / nCircleInterval;
                circle.transform.localScale = Vector3.Lerp(vBurstMinSize, vBurstMaxSize, sizePercent);

            }

            // �]�C
            for (int i = 0; i <= 20; i++)
            {
                yield return null;

            }

            Destroy(circle);
        }

        // �]�C
        for (int i = 0; i <=�@60; i++)
        {
            yield return null;

        }

        IsBurst = false;
    }
    //**********************************************************
}



