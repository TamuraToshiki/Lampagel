using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffectBase : MonoBehaviour
{

    [Header("�G�t�F�N�g�V�X�e��")]
    [SerializeField] public EnemyEffect effect;

    public EnemyEffect GetEffect { get { return effect; } }
}
