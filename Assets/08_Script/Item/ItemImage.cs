//======================================================================
// ItemImage.cs
//======================================================================
// �J������
//
// 2022/04/21 author�F�|���W�j�Y�@�쐬�A�A�C�e���摜�f�[�^
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/CreateItemImageData")]

public class ItemImage : ScriptableObject
{
    [Header("�A�C�e���摜")] // �Q�[�����\������A�C�e���摜
    public List<Sprite> ItemImageList = new List<Sprite>();
}
