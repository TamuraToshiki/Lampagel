using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusText : MonoBehaviour
{
    [SerializeField] GameObject Ogura;
    [SerializeField] GameObject PlayerObj;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerObj == null)
        PlayerObj = GameObject.FindWithTag("Player");
        Ogura.GetComponent<Text>().text = "HP:"              + PlayerObj.GetComponent<PlayerStatus>().HP +"\n"+
                                          "�X�^�~�i:"        + PlayerObj.GetComponent<PlayerStatus>().Stamina +"\n"+
                                          "�U��:"            + PlayerObj.GetComponent<PlayerStatus>().Attack +"\n"+
                                          "�X�s�[�h:"        + PlayerObj.GetComponent<PlayerStatus>().Speed +"\n"+
                                          "�o�[�X�g�͈�:"    + PlayerObj.GetComponent<PlayerStatus>().BurstRadisu +"\n"+
                                          //"�I�[�g�񕜗�:"    + PlayerObj.GetComponent<PlayerStatus>(). + "\n" +
                                          //"�h���C��:"        + PlayerObj.GetComponent<PlayerStatus>(). + "\n" +
                                          //"�񕜕␳:"        + PlayerObj.GetComponent<PlayerStatus>(). + "\n" +
                                          //"�c�@:"            + PlayerObj.GetComponent<PlayerStatus>(). + "\n" +
                                          //"���G����:"        + PlayerObj.GetComponent<PlayerStatus>(). + "\n" +
                                          "�o�[�X�g�З͕␳:" + PlayerObj.GetComponent<PlayerStatus>().BurstPower + "\n";
    }

    // Update is called once per frame    void Update()
    void Update()
    {
        
    }
}
