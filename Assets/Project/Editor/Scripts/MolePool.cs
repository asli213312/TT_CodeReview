using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MolePool : MonoBehaviour
{
    private readonly Queue<Mole> _molesPool = new Queue<Mole>();

    [Inject] private Player _player;
    public void Initialize(int poolSize, List<MoleConfig> moleConfigs)
    {
        for (int i = 0; i < poolSize; i++)
        {
            foreach (var config in moleConfigs)
            {
                Mole newMole = InstantiateMole(config);
                AddMole(newMole);
            }
        }
    }

    private Mole InstantiateMole(MoleConfig moleConfig)
    {
        GameObject newMoleGO = Instantiate(moleConfig.Appearance.Prefab);

        Mole mole;
        switch (moleConfig.Statistics.Type)
        {
            case MoleType.Light:
                mole = newMoleGO.AddComponent<LightMole>();
                break;

            case MoleType.Medium:
                mole = newMoleGO.AddComponent<MediumMole>();
                break;

            case MoleType.Heavy:
                mole = newMoleGO.AddComponent<HeavyMole>();
                break;

            default:
                throw new ArgumentException("Unknown mole type: " + moleConfig.Statistics.Type);
        }

        mole.Initialize(moleConfig, _player);
        return mole;
    }

    public void AddMole(Mole mole)
    {
        GameObject moleGO = mole.gameObject;
        moleGO.transform.SetParent(transform);
        moleGO.SetActive(false);
        _molesPool.Enqueue(mole);
    }
    
    public Mole GetMole(Vector3 position)
    {
        Mole mole = _molesPool.Dequeue();
        mole.gameObject.SetActive(true);
        mole.transform.position = position;

        return mole;
    }
}
