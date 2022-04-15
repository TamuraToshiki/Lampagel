//======================================================================
// SceneObject.cs
//======================================================================
// �J������
//
// 2022/03/28 author�F���쏫�V �|�[�^���̃f�[�^
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/CreatePortaData")]
public class PortalData : ScriptableObject
{
    [Header("Portal")]

    // �|�[�^���������܂ł̉�
    [System.NonSerialized]
    public int Hp = 2;
}
