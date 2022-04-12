using System.Collections;
using UnityEngine;

public class Marker : MonoBehaviour
{
    // �G�I�u�W�F�N�g
    [SerializeField]private GameObject Target;

    private RectTransform Rect;

    // �I�t�Z�b�g
    private Vector3 offset = new Vector3(0, 1.5f, 0);

    // ���E�����H�i��ʂ̑傫���ɂ��ς�邽�ߖv�j
    [SerializeField] private Vector2 vPos = new Vector2(620, 350);
    private float fPos = 40.0f;


    void Start()
    {
        Rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        // �^�[�Q�b�g���Ŏ��A�����ɏ���
        if (Target == null) Destroy(gameObject);


        // UI���W�̍X�V
        if(Rect.position.x < vPos.x && Rect.position.x > fPos)
            Rect.position = new Vector2(RectTransformUtility.WorldToScreenPoint(Camera.main, Target.transform.position + offset).x, Rect.position.y);

        if (Rect.position.y < vPos.y && Rect.position.y > fPos)
            Rect.position = new Vector2(Rect.position.x, RectTransformUtility.WorldToScreenPoint(Camera.main, Target.transform.position + offset).y);


        //Rect.position = RectTransformUtility.WorldToScreenPoint(Camera.main, Target.position + offset);
    }


}