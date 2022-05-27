//======================================================================
// Boss04.cs
//======================================================================
// �J������
//
// 2022/05/11 author �|���F�X�J���h���S������
//                         �A�j���[�V�����C�x���g�ɋN�����ċN����
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss04 : MonoBehaviour
{
    // �v���C���[
    private GameObject player;
    // �{�X�̊��N���X
    private EnemyBase BossBase;
    // �ˏo��
    [SerializeField] GameObject Mazzle;
    // FireWall�͈̔̓I�u�W�F�N�g
    public GameObject WallCircle;
    // �G�t�F�N�g�֘A
    EnemyEffect enemyEffect;
    GameObject objEffect;

    [Header("�U���p�x")]
    [SerializeField, Range(0.0f, 10.0f)] private float fAttackTime = 10.0f;

    [Header("�΋����x")]
    [SerializeField] float fSpeed = 5.0f;

    [Header("�Ή����˂̋���")]
    [SerializeField] float fDistance = 3.0f;

    [Header("�Ή����˂̎���")] //�����邩��
    [SerializeField] float fFlameTime = 1.0f;

    private float fAttackCount;
    private int nSetAttack;
    private int nFireDamageCol = 10;
    private int nWallTime = 180;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        BossBase = GetComponent<EnemyBase>();

        // �G�t�F�N�g�擾
        enemyEffect = GetComponent<EnemyEffectBase>().GetEffect;
        WallCircle.SetActive(false);
    }



    void Update()
    {
        FireWallDamage();

        if(Input.GetKeyDown(KeyCode.N))
        {
            StartFireWall();
        }
    }

    // �΋��A�e ************************************************
    // �A�j���[�V�����C�x���g�����܂���
    void FireBullet()
    {
        // �e�𐶐�
        GameObject Sphere;
        Sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        // �e�̃T�C�Y�A���W�A�p�x�ݒ�
        Sphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        Sphere.transform.rotation = Mazzle.transform.rotation;
        Sphere.transform.position = new Vector3(Mazzle.transform.position.x + Mazzle.transform.forward.x, Mazzle.transform.position.y, Mazzle.transform.position.z + Mazzle.transform.forward.z);

        // �e�ɃR���|�[�l���g�ǉ�
        Sphere.AddComponent<Bullet>();

        // �e�̃R���|�[�l���g�ɏ����Z�b�g
        Bullet bullet = Sphere.GetComponent<Bullet>();
        bullet.Speed = fSpeed;
        bullet.SetPlayer(BossBase.GetComponent<EnemyBase>().player);
        bullet.SetEnemy(gameObject);

        // �G�t�F�N�g����
        objEffect = enemyEffect.CreateEffect(EnemyEffect.eEffect.eFireBall, gameObject);
        bullet.SetEffect(objEffect);

        // ���蔲����悤��
        Sphere.GetComponent<SphereCollider>().isTrigger = true;
    }
    //**********************************************************

    // �ガ�����u���X *****************************************
    // �^�C�~���O�悭�A�j���[�V�����C�x���g������
    void FireBreth()
    {
        // �����蔻��p�̃L���[�u����
        GameObject cube;
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // �T�C�Y�A���W�A�p�x�ݒ�
        cube.transform.localScale = new Vector3(1.0f, 1.0f, 5.0f);
        cube.transform.rotation = this.transform.rotation;
        cube.transform.position = new Vector3(transform.position.x + transform.forward.x * fDistance, transform.position.y, transform.position.z + transform.forward.z * fDistance);

        // �Ή����˂̃R���|�[�l���g��ǉ�
        cube.AddComponent<Flamethrower>();

        // �G�Z�b�g
        cube.GetComponent<Flamethrower>().enemy = gameObject;

        // �v���C���[�Z�b�g
        cube.GetComponent<Flamethrower>().player = BossBase.player;

        // �Ή����ˋ����Z�b�g
        cube.GetComponent<Flamethrower>().fDis = fDistance;

        // �G�t�F�N�g����
        objEffect = enemyEffect.CreateEffect(EnemyEffect.eEffect.eFlame, gameObject);

        // �G�t�F�N�g�Z�b�g
        cube.GetComponent<Flamethrower>().effect = objEffect;

        // ���蔲���锻���
        cube.GetComponent<BoxCollider>().isTrigger = true;

        // �U���t���O��ON�i�G�������Ȃ��Ȃ�j
        BossBase.bAttack = true;

        // �����蔻��L���[�u���\��
        cube.GetComponent<MeshRenderer>().enabled = false;

        // �Ή����ˎ��Ԑݒ�
        cube.GetComponent<Flamethrower>().fLifeTime = fFlameTime;
    }
    //**********************************************************

    // ���̕� *************************************************
    // �^�C�~���O�悭�A�j���[�V�������āA���΂炭�c��
    void StartFireWall()
    {
        Debug.Log("a");
        StartCoroutine(FireWall());
    }

    void FireWallDamage()
    {
        

        if(WallCircle.GetComponent<FireWall>().bInArea == true)
        {
            nSetAttack = BossBase.GetComponent<EnemyBase>().GetEnemyData.nAttack;
            player.GetComponent<PlayerHP>().OnDamage(nSetAttack / nFireDamageCol);
        }
    }

    private IEnumerator FireWall()
    {
        for(int i = 0; i <= nWallTime; i++)
        {
            yield return null;
            WallCircle.SetActive(true);
        }
        WallCircle.GetComponent<FireWall>().bInArea = false;
        WallCircle.SetActive(false);
    }

    //**********************************************************
}
