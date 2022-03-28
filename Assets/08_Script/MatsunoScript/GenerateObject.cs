using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObject : MonoBehaviour
{
    // ��������GameObject
    [SerializeField]
    private GameObject createPrefab;
    // ��������͈�A
    [SerializeField]
    private Transform rangeA;
    // ��������͈�B
    [SerializeField]
    private Transform rangeB;

    // �o�ߎ���
    private float time;

    void Update()
    {
        // �O�t���[������̎��Ԃ����Z���Ă���
        time = time + Time.deltaTime;

        // ��1�b�u���Ƀ����_���ɐ��������悤�ɂ���B
        if (time > 1.0f)
        {
            // rangeA��rangeB��x���W�͈͓̔��Ń����_���Ȑ��l���쐬
            float x = Random.Range(rangeA.position.x, rangeB.position.x);
            // rangeA��rangeB��y���W�͈͓̔��Ń����_���Ȑ��l���쐬
            float y = 0.0f;
            // rangeA��rangeB��z���W�͈͓̔��Ń����_���Ȑ��l���쐬
            float z = Random.Range(rangeA.position.z, rangeB.position.z);

            // GameObject����L�Ō��܂��������_���ȏꏊ�ɐ���
            Instantiate(createPrefab, new Vector3(x, y, z), createPrefab.transform.rotation);

            // �o�ߎ��ԃ��Z�b�g
            time = 0f;
        }
    }
}
