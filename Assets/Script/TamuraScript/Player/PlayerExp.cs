using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    PlayerStatus status;

    // Start is called before the first frame update
    void Start()
    {
        status = GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddExp(int exp)
    {
        status.Exp += exp;

        if(status.MaxExp <= status.Exp)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        status.Level++;


        status.Exp = 0;
        // ���̃��x���A�b�v�܂ł̌o���l�ʂ𑝂₷
        status.MaxExp += status.UpExp;
    }
}
