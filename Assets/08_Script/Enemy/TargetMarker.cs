
//======================================================================
// TargetMaker.cs
//======================================================================
// �J������
//
// 2022/04/22 author�F�����x�@����J�n
//

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]

public class TargetMarker : MonoBehaviour
{
    // �}�[�J�[���o���^�[�Q�b�g
    [SerializeField]private Transform target;

    // ���摜
    [SerializeField]private Image arrow;

    private Camera mainCamera;
    private RectTransform rectTransform;

    private void Start()
    {
        mainCamera = Camera.main;
        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        // �^�[�Q�b�g�����Ȃ��Ƃ��X�V���Ȃ�
        if (target == null) return;

        // �L�����o�X�̑傫���擾
        float canvasScale = transform.root.localScale.z;

        // �������W�擾
        Vector3 center = 0.5f * new Vector3(Screen.width, Screen.height);

        // �^�[�Q�b�g�̃X�N���[�����W�����߂�
        Vector3 pos = mainCamera.WorldToScreenPoint(target.position) - center;

        // ��ʒ[�̕\���ʒu����
        Vector2 halfSize = 0.5f * canvasScale * rectTransform.sizeDelta;
        float d = Mathf.Max(Mathf.Abs(pos.x / (center.x - halfSize.x)),Mathf.Abs(pos.y / (center.y - halfSize.y)));

        // �^�[�Q�b�g�̃X�N���[�����W����ʊO�Ȃ�A��ʒ[�ɂȂ�悤��������
        bool isOffscreen = (pos.z < 0f || d > 1f);
        if (isOffscreen)
        {
            pos.x /= d;
            pos.y /= d;
        }
        rectTransform.anchoredPosition = pos / canvasScale;

        // �^�[�Q�b�g����ʊO�Ȃ�A���\��
        arrow.enabled = isOffscreen;
        if (isOffscreen)
        {
            arrow.rectTransform.eulerAngles = new Vector3(0.0f, 0.0f,Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg);
        }
    }
}
