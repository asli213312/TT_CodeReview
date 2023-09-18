using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "MoleConfig")]
public class MoleConfig : ScriptableObject
{
    public MoleConfig(MoleAppearance appearance, MoleStatistics statistics, MoleEffects effects)
    {
        Appearance = appearance;
        Statistics = statistics;
        Effects = effects;
    }
    
    public MoleAppearance Appearance;
    public MoleStatistics Statistics;
    public MoleEffects Effects;
}

[Serializable]
public class MoleAppearance
{
    public GameObject Prefab;
    
    public MoleAppearance(GameObject prefab)
    {
        Prefab = prefab;
    }
}

[Serializable]
public class MoleStatistics
{
    public MoleType Type;
    public int Health;
    public float Damage;
    public float Speed;
    public float PointMultiplier;
    public float TimeToDisappear;
}

[Serializable]
public class MoleEffects
{
    public ParticleSystem ParticleOnDeath;
    public ParticleSystem ParticleOnDisappear;

    public MoleEffects(ParticleSystem particleOnDeath, ParticleSystem particleOnDisappear)
    {
        ParticleOnDeath = particleOnDeath;
        ParticleOnDisappear = particleOnDisappear;
    }
}
