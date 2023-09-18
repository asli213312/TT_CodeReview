using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MoleFactory : MonoBehaviour
{
    [Inject] private MolePool _molePool;
    [Inject] private Spawner _spawner;

    public Mole CreateMole(Vector3 position)
    {
        Mole newMole = _molePool.GetMole(position);
        newMole.transform.SetParent(_spawner.transform);
        newMole.SetInitialPosZ(position.z);
        newMole.InitializeComponents
            (
                _spawner.GameStarter.GetPointCounter(),
                _molePool,
                _spawner
            );

        return newMole;
    }
}