//======================================================================
// SceneData.cs
//======================================================================
// �J������
//
// 2022/04/23 author:�|���W�j�Y�@����i�V�[���Ǘ����ʓ|�Ȃ̂Łj
//                               SceneObject.cs�������Ɛ������Ȃ����ߒ���
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/CreateSceneData")]

public class SceneData : ScriptableObject
{
    [Header("Title")]
    [Header("<SceneList>")]
    [Header("���ǉ��ł��Ȃ��Ƃ���BuildSeeting�ɃV�[�����ǉ�����Ă邩�m�F")]
    public SceneObject TitleScene;

    [Header("GameScene")]
    public List<SceneObject> GameScene = new List<SceneObject>();

    [Header("MovieScene")] // �� �ePlanet�Ԃ̃V�[���J�ڂŋ��ރX�e�[�W�Љ�
    public List<SceneObject> MovieScene = new List<SceneObject>();
}
