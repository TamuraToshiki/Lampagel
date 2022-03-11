//======================================================================
// StatusComponent.cs
//======================================================================
// �J������
//
// 2022/03/05 ����J�n �X�e�[�^�X�֘A
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class StatusComponent : MonoBehaviour
{
    public int Level { get; set; }
    public int HP { get; set; }
    public int Attack { get; set; }
    public float Speed { get; set; }
}
