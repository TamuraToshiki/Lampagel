//======================================================================
// Boss05.cs
//======================================================================
// �J������
//
// 2022/05/12 author �|���F�T�C�R�A�C����
//                         
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss05 : MonoBehaviour
{
    // �v���C���[
    private GameObject player;
    // �{�X�̊��N���X
    private EnemyBase BossBase;
    
    // �G�t�F�N�g�֘A
    EnemyEffect enemyEffect;
    GameObject objEffect;

    int nPatarn = 2;
    float fTimer = 0;
    
    [Header("�U���p�x")]
    [SerializeField, Range(0.0f, 20.0f)] private float fAttackTime = 10.0f;
    

    [Header("���[�U�[�e���x")]
    [SerializeField] float fSpeed = 5.0f;
    [SerializeField] List<GameObject> Mazzles = new List<GameObject>();
    //�@���ˊԊu
    int nLBulletInterbal = 30; 
    // ���ː�
    int nFireCount = 3;

    [Header("�o��������r�b�g���Z�b�g")]
    [SerializeField] GameObject BittonSet;

    

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        BossBase = GetComponent<EnemyBase>();

        // �G�t�F�N�g�擾
        enemyEffect = GetComponent<EnemyEffectBase>().GetEffect;

        nPatarn = 2;
    }



    void Update()
    {
        fTimer += Time.deltaTime;

        if(fTimer > fAttackTime)
        {
            switch(nPatarn)
            {
                case 1:
                    DisarrayedBullet();
                    break;

                case 2:
                    SetBitton();
                    break;
            }

            fTimer = 0;
        }
        
    }



    // �r�[�����e ************************************************
    // �R���[�`���Ǘ�
    void DisarrayedBullet()
    {
        StartCoroutine(BeamBullet());
        nPatarn = 2;
    }

    IEnumerator BeamBullet()
    {
        for(int l = 0; l < nFireCount; l++)
        {
            for (int i = 0; i < Mazzles.Count; i++)
            {
                // �e�𐶐�
                GameObject Sphere;
                Sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                // �e�̃T�C�Y�A���W�A�p�x�ݒ�
                Sphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                Sphere.transform.rotation = Mazzles[i].transform.rotation;
                Sphere.transform.position = new Vector3(Mazzles[i].transform.position.x + Mazzles[i].transform.forward.x, Mazzles[i].transform.position.y, Mazzles[i].transform.position.z + Mazzles[i].transform.forward.z);
                // �e�ɃR���|�[�l���g�ǉ�
                Sphere.AddComponent<LazerBullet>();

                // �e�̃R���|�[�l���g�ɏ����Z�b�g
                LazerBullet bullet = Sphere.GetComponent<LazerBullet>();
                bullet.Speed = fSpeed;
                bullet.SetPlayer(BossBase.GetComponent<EnemyBase>().player);
                bullet.SetEnemy(gameObject);

                // �G�t�F�N�g����
                objEffect = enemyEffect.CreateEffect(EnemyEffect.eEffect.eFireBall, gameObject);
                bullet.SetEffect(objEffect);

                // ���蔲����悤��
                Sphere.GetComponent<SphereCollider>().isTrigger = true;
            }

            for (int n = 0; n < nLBulletInterbal; n++)
            {
                yield return null;
            }
        }        
    }
    //**********************************************************



    // �T�C�h���[�U�[�r�[�� ************************************
    void SetBitton()
    {
        GameObject obj;
        obj = Instantiate(BittonSet, player.transform.position, Quaternion.identity);
        obj.GetComponent<PincerLazer>().SetEnemy(this.gameObject);
        nPatarn = 1;
    }
    //**********************************************************

    
}
