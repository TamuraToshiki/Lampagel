using UnityEngine;

public class EnemyMaker : MonoBehaviour
{
    // �I�u�W�F�N�g���f���J����
    [SerializeField] private Camera TargetCamera;

    // UI��\��������ΏۃI�u�W�F�N�g
    [SerializeField] private Transform TargetObject;

    // �\������UI
    [SerializeField] private Transform ViewUI;

    // �I�u�W�F�N�g�ʒu�̃I�t�Z�b�g
    [SerializeField] private Vector3 Offset;

    private RectTransform ParentUI;


    //---------------------
    // ������
    //---------------------
    private void Awake()
    {
        // �J�������w�肳��Ă��Ȃ���΃��C���J�����ɂ���
        if (TargetCamera == null)
            TargetCamera = Camera.main;

        // �eUI��RectTransform���擾
        ParentUI = ViewUI.parent.GetComponent<RectTransform>();
    }

    //---------------------
    // �X�V
    //---------------------
    private void Update()
    {
        // UI�̈ʒu�𖈃t���[���X�V
        OnUpdatePosition();
    }

    //---------------------
    // UI�̈ʒu���X�V����
    //---------------------
    private void OnUpdatePosition()
    {
        Transform cameraTransform = TargetCamera.transform;

        // �J�����̌����x�N�g��
        Vector3 cameraDir = cameraTransform.forward;

        // �I�u�W�F�N�g�̈ʒu
        Vector3 targetWorldPos = TargetObject.position + Offset;

        // �J��������^�[�Q�b�g�ւ̃x�N�g��
        Vector3 targetDir = targetWorldPos - cameraTransform.position;

        // �J�����̑O����
        bool bFront = Vector3.Dot(cameraDir, targetDir) > 0;

        // �J�����O���Ȃ�UI�\���A����Ȃ��\��
        ViewUI.gameObject.SetActive(bFront);
        if (!bFront) return;

        // �I�u�W�F�N�g�̃��[���h���W����A�X�N���[�����W�֕ϊ�
        Vector3 ScreenPos = TargetCamera.WorldToScreenPoint(targetWorldPos);

        // �X�N���[�����W����AUI���[�J�����W�֕ϊ�
        RectTransformUtility.ScreenPointToLocalPointInRectangle(ParentUI, ScreenPos, null,out Vector2 uiLocalPos);

        // RectTransform�̃��[�J�����W���X�V
        ViewUI.localPosition = uiLocalPos;
    }
}