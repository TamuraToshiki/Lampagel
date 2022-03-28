using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObject : MonoBehaviour
{
    //�@�n�ʂ̃Q�[���I�u�W�F�N�g
    public Terrain terrain;
    //�@�����X�^�[�̍ő吔
    public int maxNum;
    //�@�����X�^�[�̃v���n�u
    public GameObject[] monsters;
    //�@���̃L�����Ƃ̋���
    public float radius;


    void Start()
    {

        //�@�z�u����G�̐e�̃Q�[���I�u�W�F�N�g�𐶐�����
        GameObject parentObj = new GameObject("Object");

        //�@�z�u����ő吔���J��Ԃ�
        for (int i = 0; i < maxNum; i++)
        {

            //�@�C���X�^���X���������������ǂ����H
            bool check = false;
            RaycastHit hit;

            //�@�����_���l������ϐ�
            float randX;
            float randZ;
            //�@�G���l���ƈʒu���d�Ȃ�����J�E���g���鐔��
            int count = 0;

            //�@�G�̔z�u���o�������A�J��Ԃ���3����z������I��
            while (!check && count < 3)
            {
                //�@Terrain�̃T�C�Y�ɍ��킹�ă����_���l���쐬
                randX = Random.Range(terrain.GetPosition().x, terrain.GetPosition().x + terrain.terrainData.size.x);
                randZ = Random.Range(terrain.GetPosition().z, terrain.GetPosition().z + terrain.terrainData.size.z);

                //�@Terrain�ƐڐG�����ʒu��T��
                if (Physics.Raycast(new Vector3(randX, terrain.GetPosition().y + terrain.terrainData.size.y, randZ), Vector3.down, out hit, terrain.GetPosition().y + terrain.terrainData.size.y + 100f, LayerMask.GetMask("Field")))
                {
                    //�@Player�AMonster�ABlock�Ƃ������O�̃��C���[�ƐڐG���ĂȂ���Βn�ʂ̐ڐG�|�C���g�ɓG��z�u
                    if (!Physics.SphereCast(new Vector3(randX, terrain.GetPosition().y + terrain.terrainData.size.y, randZ), radius, Vector3.down, out hit, terrain.GetPosition().y + terrain.terrainData.size.y + 100f, LayerMask.GetMask("Player")))
                    {
                        GameObject tempObj = Instantiate(monsters[Random.Range(0, monsters.Length)], hit.point, Quaternion.identity) as GameObject;
                        tempObj.transform.SetParent(parentObj.transform);
                        check = true;
                    }
                    else
                    {
                        count++;
                    }
                }
            }
        }
        //�@�ǂꂾ���̓G���z�u���ꂽ���m�F
        Debug.Log(parentObj.transform.childCount);
    }
}
