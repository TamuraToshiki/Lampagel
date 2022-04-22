using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    GameObject cube;
    GameObject player;
    GameObject boss;

    // �G�t�F�N�g�֘A
    EnemyEffect enemyEffect;
    GameObject objEffect;

    // �Ή����ˋ���
    [Header("�Ή����˂̋���")][SerializeField] float fDistance = 3.0f;

    // �U���\���e�N�X�`��
    [Header("�U���\���T�[�N��")] [SerializeField] private GameObject Circle;

    public void SetPlayer(GameObject obj) { player = obj; }


    private void Start()
    {
        boss = this.gameObject;

        // �G�t�F�N�g�擾�iBoss01.cs���j
        enemyEffect = boss.GetComponent<Boss01>().GetEffect;
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

        // �T�C�Y�A���W�A�p�x�ݒ�
        cube.transform.localScale = new Vector3(1.5f, 3.0f, 1.5f);
        cube.transform.rotation = transform.rotation;
        cube.transform.position = vpos;

        // �Β��R���|�[�l���g����
        // ���蔲���锻��ɂ���
        cube.GetComponent<BoxCollider>().isTrigger = true;

        // �U���\���T�[�N���̃T�C�Y�ݒ�
        Circle.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);

        // �Β��R���|�[�l���g�ǉ�
        cube.AddComponent<Fire>();

        // �\���T�[�N���Z�b�g
        cube.GetComponent<Fire>().SetCircle(Circle);

        // �G���Z�b�g
        cube.GetComponent<Fire>().enemy = gameObject;

        // �v���C���[���Z�b�g
        cube.GetComponent<Fire>().player = player;
    }

    //-------------------------
    // �Ή����ː���
    //-------------------------
    public void CreateFlame(Vector3 vpos)
    {
        // ����p�L���[�u����
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // �T�C�Y�A���W�A�p�x�ݒ�
        cube.transform.localScale = new Vector3(1.5f, 1.0f, 5.0f);
        cube.transform.rotation = this.transform.rotation;
        cube.transform.position = new Vector3(transform.position.x + transform.forward.x * fDistance, transform.position.y, transform.position.z + transform.forward.z * fDistance);

        // �Ή����˃R���|�[�l���g�ǉ�
        cube.AddComponent<Flamethrower>();

        // ���Z�b�g
        cube.GetComponent<Flamethrower>().SetEnemy(gameObject);
        cube.GetComponent<Flamethrower>().SetPlayer(player);
        cube.GetComponent<Flamethrower>().SetDiss(fDistance);

        // ���蔲����ݒ�
        cube.GetComponent<BoxCollider>().isTrigger = true;

        // �L���[�u���\��
        cube.GetComponent<MeshRenderer>().enabled = false;

        // �G�t�F�N�g����
        objEffect = enemyEffect.CreateEffect(EnemyEffect.eEffect.eFlame, gameObject);
        cube.GetComponent<Flamethrower>().SetEffect(objEffect);
    }
}
