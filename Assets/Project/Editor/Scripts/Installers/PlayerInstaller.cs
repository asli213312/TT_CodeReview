using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Player playerPrefab;
    [SerializeField] private GameStarter gameStarter;
    
    public override void InstallBindings()
    {
        var playerInstance = Container.InstantiatePrefabForComponent<Player>(playerPrefab);

        Container.BindInstance(playerInstance).AsSingle();
        Container.BindInstance(gameStarter.GetPointCounter()).AsSingle();
        Container.BindInstance(gameStarter).AsSingle();
    }
}