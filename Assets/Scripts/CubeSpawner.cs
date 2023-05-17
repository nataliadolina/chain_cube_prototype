using UnityEngine;
using Zenject;
using System.Collections.Generic;
using System.Linq;

public class CubeSpawner : MonoBehaviour
{
    [Inject]
    private Cube.Factory _cubeFactory;
    [Inject]
    private RainbowCube.Factory _rainbowCubeFactory;
    public CubeBase CurrentCube { get; set; }

    private Vector3 thisPosition;
    public List<CubeBase> CubesOnScene;

    private void Awake()
    {
        thisPosition = transform.position;
    }

    private void Start()
    {
        SpawnCube();
    }

    public CubeBase GetNeighborWithTheSameScore(int score, CubeBase sender, CubeBase otherSimpleCube)
    {
        CubeBase cb = CubesOnScene.Where(x=> x.Score == score).FirstOrDefault();
        if (cb != sender && cb != CurrentCube && cb != otherSimpleCube)
        {
            return cb;
        }
        return null;
    }

    public void SpawnCube()
    {
        if (CurrentCube != null)
        {
            Destroy(CurrentCube.gameObject);
        }

        CurrentCube = _cubeFactory.Create();
        Transform currentTransform = CurrentCube.transform;
        currentTransform.position = thisPosition;
        currentTransform.parent = transform;
        CubesOnScene.Add(CurrentCube);
    }

    public void SpawnRainbowCube()
    {
        if (CurrentCube != null)
        {
            Destroy(CurrentCube.gameObject);
        }

        CurrentCube = _rainbowCubeFactory.Create();
        Transform currentTransform = CurrentCube.transform;
        currentTransform.position = thisPosition;
        currentTransform.parent = transform;
        CubesOnScene.Add(CurrentCube);
    }
}
