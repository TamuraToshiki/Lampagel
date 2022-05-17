using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningAttribute : MonoBehaviour
{
    EnemyBase enemyBase;

    // Start is called before the first frame update
    void Start()
    {
        // 2�b��ɃR���|�[�l���g������
        Destroy(GetComponent<BurningAttribute>(), 2);
        enemyBase = GetComponent<EnemyBase>();
        if(enemyBase != null)
        {
            StartCoroutine("Burning");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Burning()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            enemyBase.nHp -= 5;
            Debug.Log(enemyBase.nHp);
            //�@���̃t���[���ɔ�΂�
            yield return null;
        }
    }
}
