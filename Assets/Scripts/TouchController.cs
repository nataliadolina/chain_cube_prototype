using System.Collections;
using UnityEngine;
using Zenject;


public class TouchController : MonoBehaviour
{
    [Inject]
    private CubeSpawner cubeSpawner;

    private float? _lastMousePositionX;
    private bool _shouldMoveCube;

    [HideInInspector]
    public bool IsLastMouseOperationDown;

    private void Update()
    {
        if (!_shouldMoveCube && Input.GetMouseButtonDown(0))
        {
            IsLastMouseOperationDown = true;
            StartCoroutine(WaitToSetPlayerIsPressingMouse());
            return;
        }

        if (!_shouldMoveCube)
        {
            return;
        }

        CubeBase currentCube = cubeSpawner.CurrentCube;

        if (currentCube == null)
        {
            cubeSpawner.SpawnCube();
            currentCube = cubeSpawner.CurrentCube;
        }

        if (_shouldMoveCube && IsLastMouseOperationDown)
        {
            MovePlayerCube(currentCube);
        }

        if (Input.GetMouseButtonUp(0) && _shouldMoveCube)
        {
            IsLastMouseOperationDown = false;
            _shouldMoveCube = false;
            currentCube.Push();
            _lastMousePositionX = null;
            StartCoroutine(WaitToSpawnNewCube());
        }
    }

    private IEnumerator WaitToSetPlayerIsPressingMouse()
    {
        yield return new WaitForSeconds(0.2f);
        if (IsLastMouseOperationDown)
        {
            _shouldMoveCube = true;
        }
    }

    private IEnumerator WaitToSpawnNewCube()
    {
        yield return new WaitForSeconds(0.2f);
        cubeSpawner.SpawnCube();
    }

    private void MovePlayerCube(CubeBase currentCube)
    {
        float mousePositionX = Input.mousePosition.x;
        if (_lastMousePositionX == null)
        {
            _lastMousePositionX = mousePositionX;
            return;
        }

        float deltaX = (float)_lastMousePositionX - mousePositionX;
        currentCube.MoveX(deltaX/30);
        _lastMousePositionX = mousePositionX;
    }
}
