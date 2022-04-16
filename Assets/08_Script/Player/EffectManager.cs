using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] List<GameObject> EffectList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �G�t�F�N�g����
    // ����1:���[�v�Ȃ�true,false�Ȃ���
    // ����2:����������I�u�W�F�N�g
    // ����3:�����ꏊ
    // ����4:�����������ۂ̊p�x
    // ����5:�����x��
    public IEnumerator CreateEfect(bool loop, GameObject obj, Vector3 pos, Quaternion roll, float time = 0)
    {
        // �x������
        yield return new WaitForSeconds(time);
        // ��������
        Instantiate(obj, pos, roll);
        // ���[�v������̂����Ǘ�����
        if(loop)
        {
            EffectList.Add(obj);
        }
    }

    public void DestroyEffect()
    {
        
    }
}
