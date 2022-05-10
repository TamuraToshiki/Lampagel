using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/CreateEnemyEffectData")]

public class EnemyEffectData : ScriptableObject
{
    [Header("�G�t�F�N�g")]
    [SerializeField] List<GameObject> EffectList;

    public List<GameObject> GetEffectList { get { return EffectList; } }
}
