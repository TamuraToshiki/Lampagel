//======================================================================
// Ice.cs
//======================================================================
// �J������
//
// 2022/04/15 author�F�����x ����J�n�@�X������
//
//======================================================================

using UnityEngine;

public class Ice : MonoBehaviour
{
    // �X���̎���
    float fLifeTime = 6.0f;

    // �U���T�[�N�����o�Ă���X�����o�鎞��
    float fAttackStart = 1.0f;

    GameObject player;
    GameObject enemy;
    GameObject AttackCircle, TimeCircle;

    // 1�t���[���Ŋg�傷��T�C�Y
    float fScale;

    public void SetPlayer(GameObject obj) { player = obj; }
    public void SetEnemy(GameObject obj) { enemy = obj; }
    public void SetCircle(GameObject obj) { AttackCircle = obj; }


    //----------------------------------
    // ������
    //----------------------------------
    private void Start()
    {
        // �L����T�[�N������
        TimeCircle = Instantiate(AttackCircle, new Vector3(player.transform.position.x, 0.1f, player.transform.position.z), AttackCircle.transform.rotation);
        
        // �����l�̓[��
        TimeCircle.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);

        // 1�t���[���Ŋg�傷��T�C�Y���v�Z
        fScale = AttackCircle.transform.localScale.x / (fAttackStart * 50.0f);

        // �U���T�[�N������
        AttackCircle = Instantiate(AttackCircle, new Vector3(player.transform.position.x, 0.1f, player.transform.position.z), AttackCircle.transform.rotation);
        
        // �T�[�N���̓����x��������
        AttackCircle.GetComponent<SpriteRenderer>().color -= new Color32(0, 0, 0, 125);

    }

    //----------------------------------
    // �X�V
    //----------------------------------
    void Update()
    {
        Destroy(gameObject, fLifeTime);

        // �T�[�N������
        Destroy(AttackCircle, fAttackStart);
        Destroy(TimeCircle, fAttackStart);

        // �T�[�N�����Ō�A�L���[�u�\��
        if(AttackCircle == null)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }

    }

    private void FixedUpdate()
    {
        // �T�[�N���̊g��
        if (TimeCircle != null)
            TimeCircle.transform.localScale = new Vector3(TimeCircle.transform.localScale.x + fScale, TimeCircle.transform.localScale.y + fScale, 1.0f);
    }
}
