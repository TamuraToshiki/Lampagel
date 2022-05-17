//======================================================================
// ItemUI.cs
//======================================================================
// �J������
//
// 2022/04/09 author�F���R��P �����J�n
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("UI");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateItemUI(int itemNum, GameObject itemImage)
    {
        GameObject prefab = (GameObject)Instantiate(itemImage, new Vector3(-830.0f + 90.0f * itemNum, 365.0f, 0.0f), Quaternion.identity);
        prefab.transform.SetParent(canvas.transform, false);
    }
}
