using UnityEngine;
using Zenject;

public class MoleComponentsInstaller : MonoInstaller
{
    [SerializeField] private Spawner spawner;
    [SerializeField] private MolePool molePool;
    [SerializeField] private MoleFactory moleFactoryInstance;
    public override void InstallBindings()
    {
        Container.BindInstance(moleFactoryInstance).AsSingle();
        Container.BindInstance(molePool).AsSingle();
        Container.BindInstance(spawner).AsSingle();
    }
}