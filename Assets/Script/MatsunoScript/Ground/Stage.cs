using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    // �X�e�[�W�T�C�Y
    int StageSize = 20;
    // �������ꂽ�X�e�[�W�̐�
    int StageIndex;
    // �v���C���[
    public Transform Target;
    // �X�e�[�W�v���n�u
    public GameObject[] stagenum;
    //�X�^�[�g���ɂǂ̃C���f�b�N�X����X�e�[�W�𐶐�����̂�
    public int FirstStageIndex;
    // ���O�ɐ������Ă����X�e�[�W�̐�
    public int aheadStage;
    //���������X�e�[�W�̃��X�g
    public List<GameObject> StageList = new List<GameObject>();

    void Start()
    {
        // �����X�e�[�W�ԍ���ݒ�(1 -1 => 0)
        StageIndex = FirstStageIndex - 1;
        // �����ɃX�e�[�W�𐶐�(3)
        StageManager(aheadStage);
    }

    void Update()
    {
        // �v���C���[�̈ʒu����X�e�[�W�ԍ����Z�o
        int targetPosIndex = (int)(Target.position.z / StageSize);

        // �v���C���[�̌��݂���X�e�[�W�ԍ��@+ �����X�e�[�W�����X�e�[�W�ԍ��𒴂�����
        if (targetPosIndex + aheadStage > StageIndex)
        {
            StageManager(targetPosIndex + aheadStage);

        }


        Debug.Log("�X�e�[�W�ԍ�" + targetPosIndex);
        Debug.Log("�X�e�[�W�ԍ�" + StageIndex);
    }
    void StageManager(int maps)
    {
        // �X�e�[�W�ԍ���
        if (maps <= StageIndex)
            return;

        // �w�肵���X�e�[�W�܂ō쐬����
        for (int i = StageIndex + 1; i <= maps; i++)
        {

            GameObject stage = MakeStage(i);

            // ���X�g�ɃX�e�[�W��ǉ�
            StageList.Add(stage);
        }
        // �Â��X�e�[�W���폜����
        while (StageList.Count > aheadStage + 1)
        {
            DestroyStage();
        }

        StageIndex = maps;
    }
    // �X�e�[�W�𐶐�����
    GameObject MakeStage(int index)
    {
        int nextStage = Random.Range(0, stagenum.Length);

        // Z�����ɃX�e�[�W�𐶐�
        GameObject stageObject = (GameObject)Instantiate(stagenum[nextStage], new Vector3(0, 0, index * StageSize), Quaternion.identity);

        return stageObject;
    }


    void DestroyStage()
    {
        // ���X�g����X�e�[�W���폜����
        GameObject oldStage = StageList[0];
        StageList.RemoveAt(0);
        Destroy(oldStage);
    }

}
