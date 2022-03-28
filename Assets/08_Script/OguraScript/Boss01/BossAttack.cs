using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    GameObject cube;
    GameObject player;
    GameObject boss;

    // �Ή����ˋ���
    [Header("�Ή����˂̋���")][SerializeField] float fDistance = 3.0f;

    // �U���\���e�N�X�`��
    [Header("�U���\���T�[�N��")] [SerializeField] private GameObject Circle;

    public void SetPlayer(GameObject obj) { player = obj; }


    private void Start()
    {
        boss = this.gameObject;
    }

    //-------------------------
    // �Β�����
    //-------------------------
    public void CreateFire(Vector3 vpos)
    {
        // �����蔻��p�L���[�u
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // �����蔻��p�L���[�u�𓧖���
        cube.GetComponent<MeshRenderer>().material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse"); ;
        cube.GetComponent<MeshRenderer>().material.color -= new Color32(255, 255, 255, 255);

        // �Β��̃T�C�Y�A���W�A�p�x�ݒ�
        cube.transform.localScale = new Vector3(1.5f, 3.0f, 1.5f);
        cube.transform.rotation = transform.rotation;
        cube.transform.position = vpos;

        // �Β��R���|�[�l���g����
        // ���蔲���锻��ɂ���
        cube.GetComponent<BoxCollider>().isTrigger = true;

        // �K�v�ȏ����Z�b�g
        Circle.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        cube.AddComponent<Fire>();
        cube.GetComponent<Fire>().SetCircle(Circle);
        cube.GetComponent<Fire>().SetEnemy(gameObject);
        cube.GetComponent<Fire>().SetPlayer(player);
    }

    public void CreateFlame(Vector3 vpos)
    {
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // �T�C�Y�A���W�A�p�x�ݒ�
        cube.transform.localScale = new Vector3(1.5f, 1.0f, 8.0f);
        cube.transform.rotation = this.transform.rotation;
        cube.transform.position = new Vector3(transform.position.x + transform.forward.x * fDistance, transform.position.y, transform.position.z + transform.forward.z * fDistance);

        cube.AddComponent<Flamethrower>();
        cube.GetComponent<Flamethrower>().SetEnemy(gameObject);
        cube.GetComponent<Flamethrower>().SetDiss(fDistance);
        cube.GetComponent<BoxCollider>().isTrigger = true;

        // �����蔻��p�L���[�u�𓧖���(�f�o�b�O�p)
        cube.GetComponent<MeshRenderer>().material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse"); ;
        cube.GetComponent<MeshRenderer>().material.color -= new Color32(255, 255, 255, 255);
    }
}
