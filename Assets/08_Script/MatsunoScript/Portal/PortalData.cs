using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MatsunoScript/MyScriptable/Create PortaData")]

public class PortalData : ScriptableObject
{
    [Header("Portal")]

    // �|�[�^���������܂ł̉�
    [System.NonSerialized]
    public int Hp = 2;
}
