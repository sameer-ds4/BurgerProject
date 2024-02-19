using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "ParticleSystemData", menuName = "ScriptableObjects/ParticleSystemData", order = 0)]

public class ParticleSystemData : ScriptableObject
{
    public List <ParticleSystem> particleSystems;


    public void PlayFXs(Vector3 pos, Vector3 posOffSet, int FxsId, Vector3 size, Transform prient)
    {
        ParticleSystem particlesCash = Instantiate(particleSystems[FxsId], (pos + posOffSet), Quaternion.Euler(new Vector3(0, 0, 0)));
        particlesCash.transform.localScale = size;
        particlesCash.Play();
        // particlesCash.transform.parent = LevelManager.Instance.currenterLevelContainer.trash;
        particlesCash.transform.parent = prient;
    }
    public void PlayFXs(Vector3 pos, Vector3 posOffSet, int FxsId, Vector3 size)
    {
        ParticleSystem particlesCash = Instantiate(particleSystems[FxsId], (pos + posOffSet), Quaternion.Euler(new Vector3(-90, 0, 0)));
        particlesCash.transform.localScale = size;
        particlesCash.Play();
    }
    public void PlayFXs(Vector3 pos, Vector3 posOffSet, int FxsId, Vector3 size, float daly)
    {
        ParticleSystem particlesCash = Instantiate(particleSystems[FxsId], (pos + posOffSet), Quaternion.Euler(new Vector3(-0, 0, 0)));
        particlesCash.transform.localScale = size;
        DOVirtual.DelayedCall(daly, () =>
        {
            particlesCash.Play();
        });
    }
}
