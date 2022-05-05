//======================================================================
// BossBase.cs
//======================================================================
// �J������
//
// 2022/04/15 author�F���쏫�V �{�X�̊��N���X����
// 2022/05/02 author�F�����x   �^�[�Q�b�g�}�[�J�[�����ǉ�
// 2022/05/05 author�F�|���@�v���C���[�̑��x�ɑ΂��ă_���[�W�o����悤��
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBase : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    public GameObject player { get; set; }
    private NavMeshAgent myAgent;
    private Animator animator;

    // HP
    public int nHp;

    // ���x�ɑ΂���_���[�W�␳
    float fSpeedtoDamage = 0.03f;

    //*���}*
    [SerializeField] GameObject Portals;
    bool bPortal = false;

    // �O�t���[���̍��W
    private Vector3 vOldPos;

    // �_���[�WUI
    [SerializeField] private GameObject DamageObj;

    [Header("���S�����ʉ�")]
    [SerializeField] private AudioClip DeathSE;

    [Header("�G�t�F�N�g�V�X�e��")]
    [SerializeField] EnemyEffect effect;

    [Header("�G�}�[�J�[")]
    [SerializeField] Canvas Marker;

    public EnemyEffect GetEffect { get { return effect; } }

    void Start()
    {
        player = GameObject.Find("Player");

        nHp = enemyData.nBossHp + (enemyData.nLevel * enemyData.nUpHP);

        // �i�r���b�V���������i�X�e�[�^�X����X�s�[�h���擾�j
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.speed = enemyData.fSpeed;

        // �A�j���[�^�[������
        animator = GetComponent<Animator>();

        // �G�^�[�Q�b�g�}�[�J�[����
        Marker = Instantiate(Marker, Vector3.zero, Quaternion.identity);

        // �{�X�����Z�b�g
        Marker.GetComponentInChildren<TargetMarker>().target = gameObject.transform;

    }

    void Update()
    {
        Move();
        Death();

    }

    // ���S
    public bool Death()
    {
        // HP0�ȉ��ŏ���
        if (nHp <= 0)
        {
            //*���}*
            if (bPortal == false)
            {
                // ���ʉ��Đ�
                AudioSource.PlayClipAtPoint(DeathSE, transform.position);

                Instantiate(Portals, this.gameObject.transform.position, Quaternion.identity);
                bPortal = true;
            }

            // �^�[�Q�b�g�}�[�J�[����
            Destroy(Marker);

            // �S�ď���
            Destroy(this.gameObject);

            return true;
        }
        return false;
    }

    // �ړ�
    public void Move()
    {
        // ���̏ꏊ���v�Z
        Vector3 nextPoint = myAgent.steeringTarget;
        Vector3 targetDir = nextPoint - transform.position;

        // ��]
        Quaternion targetRotation = Quaternion.LookRotation(targetDir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 120f * Time.deltaTime);

        // ���݂̃v���C���[�̈ʒu��ڎw��
        myAgent.SetDestination(player.transform.position);

        // �ߋ����W���X�V
        vOldPos = gameObject.transform.position;
    }

    // �v���C���[�Ƃ̐ڐG��
    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[�Ƃ̏Փˎ��_���[�W
        if (other.CompareTag("Player"))
        {
            // �_���[�W����
            Damege();
        }
    }

    // �_���[�W�̏���
    public void Damege()
    {
        // �_���[�W����
        int n = (int)((player.GetComponent<PlayerStatus>().Attack * (int)player.GetComponent<Rigidbody>().velocity.magnitude) * fSpeedtoDamage);
        nHp -= n;

        // �_���[�W�\�L
        ViewDamage(n);
    }

    // �_���[�W�\�L
    private void ViewDamage(int damage)
    {
        // �e�L�X�g�̐���
        GameObject text = Instantiate(DamageObj);
        text.GetComponent<TextMesh>().text = damage.ToString();

        // �������炵���ʒu�ɐ���(z + 1.0f)
        text.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.0f);
    }
}
