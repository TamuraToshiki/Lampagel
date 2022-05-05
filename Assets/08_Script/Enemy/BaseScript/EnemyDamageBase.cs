//======================================================================
// EnemyDamageBase.cs
//======================================================================
// �J������
//
// 2022/05/02 author�F�����x ����J�n�@�G�_���[�W�������ڍs�B
// 2022/05/05 author�F�|���@�v���C���[�̑��x�ɑ΂��ă_���[�W�o����悤��

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageBase : MonoBehaviour
{
    // �����}�e���A���ۑ��p
    Material TmpMat;

    // �q�I�u�W�F�N�g�ۑ��p
    SkinnedMeshRenderer[] renders;

    // �_���[�W�t���O
    bool bDamage = false;

    GameObject player;

    [Header("DamegeUI")]
    [SerializeField] private GameObject DamageObj;

    [Header("�_���[�W���ɕύX����}�e���A��")]
    [SerializeField] private Material mat;

    [Header("�ω����鎞��")]
    [SerializeField] private float fDamageTime = 0.2f;
    float fDamageCount;

    // ���x�ɑ΂���_���[�W�␳
    float fSpeedtoDamage = 0.03f;

    //--------------------------
    // ������
    //--------------------------
    void Start()
    {
        // SkinnedMeshRenderer�����̎q�I�u�W�F�N�g�擾
        renders = GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (var meshRenderer in renders)
        {
            // �ŏ��̃}�e���A����ۑ�
            TmpMat = meshRenderer.material;
        }

        // �J�E���g������
        fDamageCount = fDamageTime;

        // �v���C���[�擾
        player = gameObject.GetComponent<EnemyBase>().player;
    }

    //--------------------------
    // �X�V
    //--------------------------
    void Update()
    {
        // �_���[�W���󂯂Ă��Ȃ���΃X�L�b�v
        if (!bDamage) return;

        // �_���[�W�J�E���g����
        fDamageCount -= Time.deltaTime;
        

        if(fDamageCount < 0.0f)
        {
            // ������
            fDamageCount = fDamageTime;
            bDamage = false;

            foreach (var meshRenderer in renders)
            {
                // �ŏ��̃}�e���A���ɖ߂�
                meshRenderer.material = TmpMat;
            }
        }
    }

    //----------------------------
    // �v���C���[�Ƃ̐ڐG��
    //----------------------------
    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[�Ƃ̏Փˎ��_���[�W
        if (other.CompareTag("Player"))
        {
            // �_���[�W����
            int n = (int)((player.GetComponent<PlayerStatus>().Attack * (int)player.GetComponent<Rigidbody>().velocity.magnitude) * fSpeedtoDamage);
            gameObject.GetComponent<EnemyBase>().nHp -= n;
            ViewDamage(n);

            // ��u�F��ς���
            ChangeMaterial();
        }
    }

    //----------------------------
    // �_���[�W����
    //----------------------------
    public void IsDamage(int damage)
    {
        // �_���[�W����
        gameObject.GetComponent<EnemyBase>().nHp -= damage;
        ViewDamage(damage);

        // ��u�F��ς���
        ChangeMaterial();
    }

    //----------------------------
    // �_���[�W�\�L
    //----------------------------
    private void ViewDamage(int damage)
    {
        // �e�L�X�g�̐���
        GameObject text = Instantiate(DamageObj);
        text.GetComponent<TextMesh>().text = damage.ToString();

        // �������炵���ʒu�ɐ���(z + 1.0f)
        text.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.0f);
    }

    //--------------------------
    // �}�e���A���ύX����
    //--------------------------
    public void ChangeMaterial()
    {
        foreach (var meshRenderer in renders)
        {
            // �w�肵���}�e���A���ɕύX
            meshRenderer.material = mat;

            // �_���[�W�t���OON
            bDamage = true;
        }
    }
}
