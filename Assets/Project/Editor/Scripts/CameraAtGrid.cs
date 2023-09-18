using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraAtGrid : MonoBehaviour
{
    [Inject] private Spawner spawner;
    
    private const float CORRECTION_FACTOR = 0.7f;

    private Camera _camera;
    private GridConfig _gridConfig;

    private void Awake()
    {
        _camera = Camera.main;
        _gridConfig = spawner.GetGridConfig();
    }

    public void AdjustCamera()
    {
        float targetOrthographicSize = CalculateTargetOrthographicSize();
        float cameraZ = CalculateCameraZ(targetOrthographicSize);
        SetCamera(targetOrthographicSize, cameraZ);
    }

    private float CalculateTargetOrthographicSize()
    {
        float cellCount = spawner.Cells.Count * spawner.GetGridConfig().GridSideSize;
        return Mathf.Sqrt(cellCount) * _gridConfig.CellSpacing / 2f;
    }

    private float CalculateCameraZ(float targetOrthographicSize)
    {
        return -targetOrthographicSize * CORRECTION_FACTOR;
    }

    private void SetCamera(float targetOrthographicSize, float cameraZ)
    {
        Vector3 gridPosition = _gridConfig.GridTransform.position;
        _camera.orthographicSize = targetOrthographicSize;
        _camera.transform.position = new Vector3(gridPosition.x, gridPosition.y, cameraZ);
    }
}
