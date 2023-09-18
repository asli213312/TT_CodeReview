using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zenject;

public enum MoleType
{
    Light,
    Medium,
    Heavy
}

public abstract class Mole : MonoBehaviour
{

    #region Mole Parameters

    public float TimeToDisappear {get; private set; }
    protected float Speed;
    protected float PointMultiplier;
    public float Damage { get; private set; }
    public int Health { get; set; }
    protected GameObject Prefab;
    protected MoleType Type;
    
    private ParticleSystem _particleOnDeath;
    public ParticleSystem ParticleOnDisappear { get; private set; }

    #endregion
    
    #region Reference Objects

    private Player _player;
    private PointCounter _pointCounter;
    private MolePool _molePool;
    private Spawner _spawner;
    private MoleConfig _moleConfig;

    #endregion

    private const float MAX_DISTANCE_AT_MOVE = 4f;
    private float _initialPosZ;
    private float _timeOnScene;

    private int _initialHealth;

    private bool _isDead;

    private GameObject _currentCell;

    private event Action IsDead;

    public void InitializeComponents(PointCounter pointCounter, MolePool molePool, Spawner spawner)
    {
        _pointCounter = pointCounter;
        _molePool = molePool;
        _spawner = spawner;
    }

    public virtual void Initialize(MoleConfig config, Player player)
    {
        _moleConfig = config;
        
        _player = player;
        PointMultiplier = config.Statistics.PointMultiplier;
        Damage = config.Statistics.Damage;
        Health = config.Statistics.Health;
        Speed = config.Statistics.Speed;
        Prefab = config.Appearance.Prefab;
        Type = config.Statistics.Type;
        TimeToDisappear = config.Statistics.TimeToDisappear;
    }

    private void Awake()
    {
        IsDead += OnMoleIsDead;
    }

    private void Start()
    {
        _initialPosZ = transform.position.z;
        _initialHealth = Health;

        CreateEffects();
    }

    private void OnDestroy()
    {
        IsDead -= OnMoleIsDead;
    }

    private void CreateEffects()
    {
        _particleOnDeath = Instantiate(_moleConfig.Effects.ParticleOnDeath, transform.position, Quaternion.identity);
        ParticleOnDisappear = Instantiate(_moleConfig.Effects.ParticleOnDisappear, transform.position, Quaternion.identity);
    }

    private void LateUpdate()
    {
        Move();
        _timeOnScene += Time.deltaTime;

        if (_timeOnScene >= TimeToDisappear)
        {
            _isDead = true;
            if (_isDead)
            {
                Invoke("ReduceHealthAtTimeOnScene", TimeToDisappear);
                StartCoroutine(ReturnToPool());
                
                _isDead = false;
                _timeOnScene = 0f;
            }
        }
    }

    private void Move()
    {
        float newZ = Mathf.PingPong(Time.time * Speed, MAX_DISTANCE_AT_MOVE) + _initialPosZ;
        
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
    }

    private void OnMouseDown()
    {
        TakeDamage(_player.Damage);

        Debug.Log("Mole was hit");        
    }

    private void TakeDamage(int value)
    {
        if (Health > 0 && Health >= value)
            Health -= value;

        if (Health <= 0)
        {
            IsDead?.Invoke();
        }
        
        Debug.Log("Health: " + Health, gameObject);
    }

    private void OnMoleIsDead()
    {
        Debug.Log("Mole is dead");
        _pointCounter.ChangeValue(_initialHealth * (int)PointMultiplier);

        GameObject currentCell = GetCurrentCell();
        _spawner.FreeCells.Add(currentCell);
        _particleOnDeath.Play();
        _molePool.AddMole(this);
    }
    
    private IEnumerator ReturnToPool()
    {
        yield return new WaitForSeconds(TimeToDisappear);

        GameObject currentCell = GetCurrentCell();
        
        if (currentCell != null)
        {
            _spawner.FreeCells.Add(currentCell);
        }
        
        ParticleOnDisappear.Play();
        _molePool.AddMole(this);
    }

    private void ReduceHealthAtTimeOnScene()
    {
        int totalDamage = Mathf.CeilToInt(_timeOnScene * Damage);

        HealthMode healthMode = _spawner.GameStarter.GetHealthMode();
        healthMode.ReduceHealth(totalDamage);
        
        _timeOnScene = 0f;
    }

    public void SetCurrentCell(GameObject cell)
    {
        _currentCell = cell;
    }

    public GameObject GetCurrentCell()
    {
        return _currentCell;
    }

    public void SetInitialPosZ(float initialPosZ) => initialPosZ = _initialPosZ;
}

[Serializable]
public class LightMole : Mole
{
    public override void Initialize(MoleConfig config, Player player)
    {
        base.Initialize(config, player);
        Type = config.Statistics.Type;
    }
}

[Serializable]
public class MediumMole : Mole
{
    public override void Initialize(MoleConfig config, Player player)
    {
        base.Initialize(config, player);
        Type = config.Statistics.Type;
    }
}

[Serializable]
public class HeavyMole : Mole
{
    public override void Initialize(MoleConfig config, Player player)
    {
        base.Initialize(config, player);
        Type = config.Statistics.Type;
    }
}
