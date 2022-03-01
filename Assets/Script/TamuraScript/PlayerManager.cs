using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerManager : MonoBehaviour
{
    // ���[�h���
    protected enum StateEnum
    {
        eNormal = 0,
        eHard,
    }
    protected StateEnum eState;

    protected Rigidbody rb;

    // ���݃��[�h�擾
    public bool IsNormal => eState == StateEnum.eNormal;
    public bool IsHard => eState == StateEnum.eHard;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // �L�[�{�[�h�ړ� 
        if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
        {
            eState = StateEnum.eHard;
        }
        else
        {
            eState = StateEnum.eNormal;
        }
    }
}
