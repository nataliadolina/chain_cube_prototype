using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField]
    private CubeSettings cubeSettings;
    [SerializeField]
    private Cube cubePrefab;
    [SerializeField]
    private RainbowCube rainbowCubePrefab;
    [SerializeField]
    private CubeSpawner cubeSpawner;

    public override void InstallBindings() 
    {
        InstallCube();
    }
    private void InstallCube()
    {
        Container.Bind<CubeSettings>().FromInstance(cubeSettings).AsSingle().NonLazy();
        
        Container.BindFactory<Cube, Cube.Factory>().FromComponentInNewPrefab(cubePrefab).AsSingle().NonLazy();
        Container.BindFactory<RainbowCube, RainbowCube.Factory>().FromComponentInNewPrefab(rainbowCubePrefab).AsSingle().NonLazy();

        Container.Bind<CubeSpawner>().FromInstance(cubeSpawner).AsSingle().NonLazy();
    }
}
