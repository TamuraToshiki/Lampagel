//======================================================================
// ObjectData.cs
//======================================================================
// �J������
//
// 2022/03/28 author�F���쏫�V �t�B�[���h�I�u�W�F�N�g�̃f�[�^
// 2022/04/09 author�F���쏫�V �I�u�W�F�N�g�̗񋓑̂��`
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/CreateObjectData")]

public class ObjectData : ScriptableObject
{
    [Header("Object")]

    // ��������I�u�W�F�N�g���X�g
    public List<GameObject> FieldObjectList = new List<GameObject>();

    // �I�u�W�F�N�g�����̎��ԊԊu
    public float GenerateTime = 5.0f;

    public enum FieldObject
    {
        eFieldObject1 = 0,
        eFieldObject2,
        eFieldObject3,
        eFieldObject4,
        eFieldObject5,
        eFieldObject6,
        eFieldObjectMax,
    }
}
