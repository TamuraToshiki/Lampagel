using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/CreatePortaDataBase")]

public class PortalDataBase : ScriptableObject
{
    [Header("PortalDataBase")]

    [SerializeField]
    public List<PortalData> PortalList = new List<PortalData>();

    //�@�|�[�^�����X�g��Ԃ�
    public List<PortalData> GetPortalList()
    {
        return PortalList;
    }
}
