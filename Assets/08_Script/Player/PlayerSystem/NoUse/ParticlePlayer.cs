//======================================================================
// ParticlePlayer.cs
//======================================================================
// �J������
//
// 2022/04/10 author�F�|�� ParticleSystem��C�ӋN������X�N���v�g�쐬
// 2022/04/10 author�F�|�� �����Player�ŊǗ������ق������₷��
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
    [SerializeField] GameObject[] particle = null;
    [SerializeField] List<GameObject> PlayingEffect = new List<GameObject>();
    public int nListnum = 0;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Shooting(this.gameObject);
        }
    }

    public void ShotBlast(GameObject player)
    {
        PlayEffectOneShot(particle[0], player);        
    }

    public void Shooting(GameObject player)
    {
        PlayEffectFollow(particle[1], player);
    }

    void PlayEffectOneShot(GameObject setparticle, GameObject gameObject)
    {
        GameObject particleObject = null;
        particleObject = Instantiate(setparticle, gameObject.transform.position, gameObject.transform.rotation);
    }

    void PlayEffectFollow(GameObject setparticle, GameObject gameObject)
    {
        GameObject particleObject = null;
        particleObject = Instantiate(setparticle, gameObject.transform.position, gameObject.transform.rotation);
        particleObject.AddComponent<ParticleFollowPlayer>().player = gameObject;
        particleObject.GetComponent<ParticleFollowPlayer>().nParticleNum = nListnum++;
        PlayingEffect.Add(particleObject);
        
    }
}
