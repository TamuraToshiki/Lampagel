//======================================================================
// Rush.cs
//======================================================================
// Jญ๐
//
// 2022/03/xx authorFฌธx ป์Jn@GฬหiU
// 2022/03/28 authorF|๖@}@vC[ึฬ_[Wป่
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rush : MonoBehaviour
{
    GameObject player;
    GameObject enemy;
    public void SetPlayer(GameObject obj) { player = obj; }
    public void SetEnemy(GameObject obj) { enemy = obj; }


    void Start()
    {
        
    }

    void Update()
    {
        if (enemy == null)
        {
            Destroy(gameObject);
            return;
        }

        enemy.transform.position += enemy.transform.forward * (Time.deltaTime * 5.0f);

        // Gฦ๊ษฎญ
        transform.position = enemy.transform.position;

        Destroy(gameObject, 1.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            // _[W
            //player.GetComponent<StatusComponent>().HP -= enemy.GetComponent<StatusComponent>().Attack;
            //Debug.Log(player.GetComponent<StatusComponent>().HP);

            //*}*
            player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<StatusComponent>().Attack);
        }

        Destroy(gameObject);
    }

}
