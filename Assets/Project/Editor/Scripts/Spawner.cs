using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("Options")] 
    [SerializeField] private float spawnInterval;
    [SerializeField] private GridConfig gridConfig;
    [SerializeField] private List<MoleConfig> moleConfigs;
    private float _timeToSpawn;

    public List<GameObject> FreeCells { get; } = new();
    public List<GameObject> Cells { get; } = new();
    
    private GameObject _currentFreeCell;
    private Mole _currentMole;

    private CameraAtGrid _camera;

    [Inject] private MoleFactory _moleFactory;
    [Inject] private MolePool _molePool;
    [Inject] public GameStarter GameStarter { get; private set; }

    private void Awake()
    {
        _molePool.Initialize(10, moleConfigs);

        _camera = GetComponent<CameraAtGrid>();
    }

    private void Start()
    {
        SpawnGrid();
        _camera.AdjustCamera();
    }

    private void Update()
    {
        _timeToSpawn += Time.deltaTime;

        if (_timeToSpawn >= spawnInterval)
        {
            SpawnMole();
            _timeToSpawn = 0;
        }
    }

    private void SpawnMole()
    {
        Vector3 cellPosition = GetRandomFreeCellPosition();

        if (cellPosition == Vector3.zero)
        {
            Debug.LogWarning("No available cell position to spawn mole.");
            return;
        }
        
        Vector3 offsetZ = new Vector3(0, 0, -3f);

        Vector3 totalPosition = cellPosition + offsetZ;

        Mole newMole = _moleFactory.CreateMole(totalPosition);
        newMole.SetCurrentCell(_currentFreeCell);
    }

    private void SpawnGrid()
    {
        int sideCount = gridConfig.GridSideSize;
        float cellSpacing = gridConfig.CellSpacing;
        for (int x = 0; x < sideCount; x++)
        {
            for (int y = 0; y < sideCount; y++)
            {
                float xOffset = (x - sideCount / 2f) * cellSpacing + cellSpacing / 2f;
                float yOffset = (y - sideCount / 2f) * cellSpacing + cellSpacing / 2f;
                Vector3 spawnPosition = gridConfig.GridTransform.position + new Vector3(xOffset, yOffset, 0);
                GameObject newCell = Instantiate(gridConfig.PrefabCell, spawnPosition, Quaternion.identity);
                newCell.transform.SetParent(gridConfig.GridTransform);

                Cells.Add(newCell);
                FreeCells.Add(newCell);
            }
        }
    }

    private Vector3 GetRandomFreeCellPosition()
    {
        List<GameObject> freeCellPositions = FreeCells;
        
        if (freeCellPositions.Count == 0)
        {
            Debug.LogWarning("No free cell positions left.");
            return Vector3.zero;
        }
        
        int randomIndex = Random.Range(0, freeCellPositions.Count);
        GameObject randomCell = freeCellPositions[randomIndex];
        _currentFreeCell = randomCell;
        
        Vector3 randomPosition = randomCell.transform.position;
        
        freeCellPositions.RemoveAt(randomIndex);

        return randomPosition;
    }

    public GridConfig GetGridConfig() => gridConfig;
}
