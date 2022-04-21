//======================================================================
// Item.cs
//======================================================================
// �J������
//
// 2022/04/09 author�F���R��P �}�l�[�W���Ƃ̕���A�A�C�e���p
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private ItemManager itemManager;
    private ItemUI itemUI;

    // Start is called before the first frame update
    void Start()
    {
        itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        itemUI = GameObject.Find("ItemManager").GetComponent<ItemUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            // �A�C�e���擾�����J�E���g
            if(itemManager.nItemCount(1) == 1) itemUI.CreateItemUI();

            // �I�u�W�F�N�g���폜���A�V�����I�u�W�F�N�g�𐶐�(��)
            Destroy(this.gameObject);
            itemManager.CreateItem();
        }
    }
}
