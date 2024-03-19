using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "ParticleSystemData", menuName = "ScriptableObjects/ParticleSystemData", order = 0)]

public class ParticleSystemData : ScriptableObject
{
    public List <ParticleSystem> particleSystems;


    public void PlayFXs(Vector3 pos, Vector3 posOffSet, int FxsId, Vector3 size, Transform parent)
    {
        ParticleSystem particleFX = Instantiate(particleSystems[FxsId], (pos + posOffSet), Quaternion.Euler(new Vector3(0, 0, 0)));
        particleFX.transform.localScale = size;
        particleFX.Play();
        // particlesCash.transform.parent = LevelManager.Instance.currenterLevelContainer.trash;
        particleFX.transform.parent = parent;
    }
    public void PlayFXs(Vector3 pos, Vector3 posOffSet, int FxsId, Vector3 size)
    {
        ParticleSystem particleFX = Instantiate(particleSystems[FxsId], (pos + posOffSet), Quaternion.Euler(new Vector3(-90, 0, 0)));
        particleFX.transform.localScale = size;
        particleFX.Play();
    }
    public void PlayFXs(Vector3 pos, Vector3 posOffSet, int FxsId, Vector3 size, float delay)
    {
        ParticleSystem particleFX = Instantiate(particleSystems[FxsId], (pos + posOffSet), Quaternion.Euler(new Vector3(-0, 0, 0)));
        particleFX.transform.localScale = size;
        DOVirtual.DelayedCall(delay, () =>
        {
            particleFX.Play();
        });
    }
}
