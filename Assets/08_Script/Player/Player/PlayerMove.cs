//======================================================================
// PlayerMove.cs
//======================================================================
// �J������
//
// 2022/03/01 author�F���쏫�V �v���C���[�̈ړ��쐬(�}�E�X)
// 2022/03/05 author�F�c���q�� ��ʂ̂ǂ��𑀍삵�Ă������悤�ɑ����
// 2022/03/09 author�F�c���q�� �p�b�h�������
// 2022/03/09 author�F�c���q�� �ړ������������悤�ɕύX
// 2022/03/25 author�F�c���q�� �A�j���[�V��������
// 2022/03/27 author�F�c���q�� update�̍ŏ��ɃA�j���[�V�����������Ă���悤�ύX
// 2022/03/28 author�F�|���@���} �G�t�F�N�g�����g�ݍ���
// 2022/05/02                    ���g�ݍ���
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����R���|�[�l���g�A�^�b�`
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CameraShaker))]

public class PlayerMove : MonoBehaviour
{

    [SerializeField] private LineRenderer Direction = null;  // ���˕���
    [SerializeField] private Animator anime;
    [SerializeField] private GameObject effectmove;
    private PlayerState state;
    private PlayerStatus status;
    private Rigidbody rb;
    private CameraShaker shaker;
    private SoundManager sound;

    private Vector3 vCurrentForce = Vector3.zero; // ���˕����̗�   
    private Vector3 vDragStart = Vector3.zero; // �h���b�O�J�n�_
    
    [Header("���ˈЗ�")]
    [SerializeField] private float fInitial = 100.0f; // �����{��
    [Header("������")]
    [SerializeField] private float fLate = 0.85f; // ������
    [Header("�ő�З͂ɓ��B���鎞��")]
    [SerializeField] private float fInputTime = 0.8f;
    [SerializeField] private float fStockPower = 0; // �~�ώ���
    private float fTimeToMove = 999.0f; // Time.deltaTime���g���ꍇ
    //private int nTimeToMove = 999;    // �t���[�����g���ꍇ
    [SerializeField] private float fDistance = 0; // �X�e�[�^�X�̃X�s�[�h�ƘA�� //�㏸��0.02
    //[SerializeField] private int nDistance = 0; // �X�e�[�^�X�̃X�s�[�h�ƘA�� //�㏸��1
    private bool bShot = false;

 
    

    //*���}* �G�t�F�N�g�X�N���v�g
    [SerializeField] AID_PlayerEffect effect;
    

    void Start()
    {
        state = GetComponent<PlayerState>();
        status = GetComponent<PlayerStatus>();
        rb = GetComponent<Rigidbody>();
        shaker = GetComponent<CameraShaker>();
        effectmove.SetActive(false);

        Direction.enabled = false;
    }

    void Update()
    {
        // �����R���|�[�l���g�����Ȃ���
        SetComponent();

        // Y���Œ�
        FixedtoY();

        // �A�j���[�V����
        MoveAnim();
        // �X�e�[�g�Ǘ�
        IsState(!state.IsNormal);
        // �����]��
        LookToMove(rb.velocity);
        // �ړ�
        PadMove();
        KeyBoardMove();

        MoveBrake();

    }

    // �A�j���[�V���� ******************************************
    void MoveAnim()
    {
        anime.SetFloat("pull", fStockPower);
        anime.SetFloat("blowway", rb.velocity.magnitude);
    }
    //**********************************************************

    // ��� ****************************************************
    void IsState(bool state)
    {
        if (state)
        {
            fStockPower = 0;
            Direction.enabled = false;
            effectmove.SetActive(false);
            return;
        }
    }
    //**********************************************************

    // �ړ������Ɍ��� ******************************************
    void LookToMove(Vector3 vector)
    {
        if (vector != new Vector3(0, 0, 0))
        {
            transform.rotation = Quaternion.LookRotation(vector);
        }
    }
    //**********************************************************

    // �}�E�X���W��3D���W�ɕϊ� ********************************
    private Vector3 GetMousePosition()
    {
        return new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
    }
    //**********************************************************

    // �u���[�L���� ********************************************
    void MoveBrake()
    {
        // float Time.deltaTime���g�p�����
        if (fTimeToMove > fDistance)
        {
            // ����
            rb.velocity *= fLate;
        }
        else
        {
            fTimeToMove += Time.deltaTime;
        }

        // int �t���[���𗘗p�����
        //if (nTimeToMove > nDistance)
        //{
        //    rb.velocity *= fLate;
        //}
        //else
        //{
        //    nTimeToMove ++;
        //}
    }
    //**********************************************************

    // �L�[�{�[�h���� ******************************************
    private void KeyBoardMove()
    {
        // ���N���b�N����
        if (Input.GetMouseButton(0))
        {
            // �����ꂽ�Ƃ�
            if (Input.GetMouseButtonDown(0))
            {
                if (state.IsNormal) sound.Play_PlayerCharge(this.gameObject);
                vDragStart = GetMousePosition(); // �}�E�X�̏����ʒu���擾
                fStockPower = 0;
            }
            
            var position = GetMousePosition(); // ���������}�E�X���W�̈ʒu���擾            
            vCurrentForce = vDragStart - position; // �}�E�X�̏������W�Ɠ����������W�̍������擾
            
            LookToMove(vCurrentForce); // ��������������

            // ���̈������菈��
            Direction.enabled = true;
            // ���������Ƌt�ɖ�󂪏o��悤��
            Direction.SetPosition(0, rb.position); 
            Direction.SetPosition(1, rb.position - vCurrentForce.normalized * 2);

            // �}�E�X�������Ă�ԁA�З͂����߂�
            if (fStockPower < fInputTime)
            {
                fStockPower += Time.deltaTime;
            }
            else
            {
                fStockPower = fInputTime;
            }

        }

        // ���N���b�N���ꂽ�Ƃ�
        if (Input.GetMouseButtonUp(0))
        {
            if (state.IsNormal) sound.Play_PlayerShotWeek(this.gameObject);

            // �u�ԓI�ɗ͂������Ă͂���
            rb.AddForce(vCurrentForce.normalized * fStockPower * fInitial, ForceMode.Impulse);
            status.fBreakTime = 0.0f;
            vCurrentForce = Vector3.zero;
            effectmove.SetActive(true);
            

            // ������
            fStockPower = 0;
            Direction.enabled = false;
            effect.StartEffect(0, this.gameObject, 1.0f);


            fTimeToMove = 0;
            //nTimeToMove = 0;
        }
    }
    //**********************************************************

    private void PadMove()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        // �X�e�B�b�N��|���Ă�Ȃ�
        if (Mathf.Abs(x) >= 0.5f || Mathf.Abs(y) >= 0.5f)
        {
            if (state.IsNormal && bShot == true) sound.Play_PlayerCharge(this.gameObject);

            // �t���O�𗧂Ă�
            bShot = true;
            // ���͕������t�ɂ��Ď󂯎��
            vCurrentForce = new Vector3(-x * 1000, 0, -y * 1000);

            // ��������������
            transform.rotation = Quaternion.LookRotation(vCurrentForce);

            // ���̈������菈��
            Direction.enabled = true;
            // ���������Ƌt�ɖ�󂪏o��悤��
            Direction.SetPosition(0, rb.position);
            Direction.SetPosition(1, rb.position - vCurrentForce.normalized * 2);

            fStockPower += Time.deltaTime;
            if (fStockPower < 2)
            {
                fStockPower += Time.deltaTime;
            }
        }
        else if (bShot == true)
        {
            if (state.IsNormal) sound.Play_PlayerShotWeek(this.gameObject);

            // �t���O������
            bShot = false;
            // �u�ԓI�ɗ͂������Ă͂���
            rb.AddForce(vCurrentForce.normalized * fStockPower * fInitial, ForceMode.Impulse);
            effectmove.SetActive(true);
            // ������
            fStockPower = 0;
            status.fBreakTime = 0.0f;
            vCurrentForce = Vector3.zero;
            Direction.enabled = false;

            //*���}*
            effect.StartEffect(0, this.gameObject, 1.0f);

            fTimeToMove = 0;
            //nTimeToMove = 0;
        }
    }
    //**********************************************************

    // SoundManager������ ************************************
    void SetComponent()
    {
        if(sound == null)
        {
            sound = GameObject.FindWithTag("SoundPlayer").GetComponent<SoundManager>();
        }
    }
    //**********************************************************

    // Y���Œ� *************************************************
    void FixedtoY()
    {
        
    }

    //*���}*
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            shaker.Do();
        }

    }
}