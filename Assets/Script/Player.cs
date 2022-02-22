using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    private bool b_shot = false;
    private Vector2 StartPos;
    private Vector2 EndPos;
    private Vector2 Move;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        First();

        // �L�[�{�[�h�ړ�
        if(Input.GetMouseButton(1)) // ���N���b�N
        {
            if(Input.GetMouseButton(2)) // �E�N���b�N
            {
                if(!b_shot)
                {
                    StartPos = Input.mousePosition;
                }
                b_shot = true;
            }
        }
        else
        {
            if(b_shot)
            {
                EndPos = Input.mousePosition;
                Move = (StartPos - EndPos).normalized;
                b_shot = false;
            }
        }


        // �p�b�h�ړ�

        // �n�[�h���[�h

        Last();
    }

    private void First()
    {

    }

    private void Last()
    {
        rb.AddForce(Move, ForceMode2D.Impulse);
    }
}
