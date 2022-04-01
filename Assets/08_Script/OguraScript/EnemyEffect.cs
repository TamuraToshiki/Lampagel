//======================================================================
// EnemyEffect.cs
//======================================================================
// �J������
//
// 2022/03/30 author�F�����@�G�t�F�N�g���������ǉ�
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffect : MonoBehaviour
{
    [Header("�G�t�F�N�g")][SerializeField] List<GameObject> EffectList;

    public enum eEffect
    {
        eFireBall = 0,
        eFlame,
        eScratch,

        eMax_Effect
    }

    //--------------------------------
    // �G�t�F�N�g����
    //--------------------------------
    public GameObject CreateEffect(eEffect num, GameObject obj, float time = 5.0f)
    {
        GameObject Effect = Instantiate(EffectList[(int)num], obj.transform.position, obj.transform.rotation);
        Destroy(Effect, time);

        return Effect;
    }


    //--------------------------------
    // �G�t�F�N�g�����iPos�ύXVer�j
    //--------------------------------
    public GameObject CreateEffect(eEffect num, Vector3 pos ,GameObject obj, float time = 5.0f)
    {
        GameObject Effect = Instantiate(EffectList[(int)num], pos, obj.transform.rotation);
        Destroy(Effect, time);

        return Effect;
    }
}
