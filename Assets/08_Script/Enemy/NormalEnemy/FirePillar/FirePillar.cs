//======================================================================
// FirePillar.cs
//======================================================================
// �J������
//
// 2022/03/21 author�F�����x ����J�n�@�G�̉Β��U��
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class FirePillar : MonoBehaviour
{
    // �����蔻��p�L���[�u
    GameObject cube;

    EnemyBase enemyBase;
    GameObject player;

    // �G�t�F�N�g�֘A
    EnemyEffect enemyEffect;
    GameObject objEffect;

    [SerializeField] private GameObject Circle;

    // �T�[�N���T�C�Y
    [Header("�\���T�[�N���̃T�C�Y")] [SerializeField] private Vector3 vCircleSize = new Vector3(0.5f, 0.5f, 0.5f);

    // �Β��T�C�Y
    [Header("�Β��T�C�Y")] [SerializeField] private Vector3 vFireSize = new Vector3(0.5f, 3.0f, 0.5f);

    private void Start()
    {
        // �G�t�F�N�g�擾
        enemyEffect = GetComponent<EnemyEffectBase>().GetEffect;

        // �G�l�~�[�x�[�X���擾
        enemyBase = this.GetComponent<EnemyBase>();

        // �v���C���[�擾
        player = enemyBase.player;
    }

    private void FirePillarAttack()
    {
        // �����蔻��p�L���[�u
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // �����蔻��p�L���[�u���\��
        cube.GetComponent<MeshRenderer>().enabled = false;

        // �Β��̃T�C�Y�A���W�A�p�x�ݒ�
        cube.transform.localScale = vFireSize;
        transform.Rotate(-90.0f,0.0f,0.0f);
        cube.transform.position = player.transform.position;

        // �Β��R���|�[�l���g����
        // ���蔲���锻��ɂ���
        cube.GetComponent<BoxCollider>().isTrigger = true;

        // �K�v�ȏ����Z�b�g
        Circle.transform.localScale = vCircleSize;

        // Fire.cs�ǉ�
        cube.AddComponent<Fire>();

        // �U���T�[�N���Z�b�g
        cube.GetComponent<Fire>().SetCircle(Circle);

        // �G���Z�b�g
        cube.GetComponent<Fire>().enemy = gameObject;
    
        // �v���C���[���Z�b�g
        cube.GetComponent<Fire>().player = player;

        // �v���C���[���Z�b�g
        cube.GetComponent<Fire>().effect = enemyEffect;
    }
}
