using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    //�|�[�Y�؂�ւ��p�t���O
    [SerializeField] bool bGameOver;
    [SerializeField] GameObject gGameOverObj;

    private void Start()
    {
        bGameOver = false;
    }

    private void Update()
    {
        SetGameOverObj(true);
    }
    public void SetGameOverObj(bool b)
    {
        gGameOverObj.SetActive(b);
    }
}
