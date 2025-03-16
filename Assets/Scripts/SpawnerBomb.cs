using UnityEngine;

[RequireComponent(typeof(InfoBomb))]

public class SpawnerBomb : Spawner<Bomb>
{
    [SerializeField] private SpawnerCube _spawnerCube;

    private MyObjectPool<Bomb> _pool;
    private InfoBomb _info;

    private Transform _transformCubePosition;

    private readonly int _maxCountBomb = 15;
    private int _getObjectsCount = 0;

    private void Awake()
    {
        _info = GetComponent<InfoBomb>();
        Changer = GetComponent<ColorChanger>();

        _pool = new MyObjectPool<Bomb>(Prefab, _maxCountBomb);
    }

    private void OnEnable()
    {
        _spawnerCube.RelesedCube += CreateBomb;

        _pool.ChangedCountActive += ShowCountActiveObjects;
        _pool.ChangedCountCreateObjects += ShowCountInstantiatedObjects;
    }

    private void OnDisable()
    {
        _spawnerCube.RelesedCube -= CreateBomb;

        _pool.ChangedCountActive -= ShowCountActiveObjects;
        _pool.ChangedCountCreateObjects -= ShowCountInstantiatedObjects;
    }

    protected override void Spawn(Bomb @object)
    {
        int randomTime = GetRandomTime();

        @object.Released += Changer.GetDefaultAlpha;


        @object.transform.position = _transformCubePosition.position;

        @object.ChangeLifeTimer(randomTime);

        @object.StartCorutins();

        _info.ShowText(this);
    }

    private void CreateBomb(Cube cube)
    {
        _transformCubePosition = cube.transform;
        var bomb = _pool.GetObject();
        Spawn(bomb);

        ShowCountGetObjects(_getObjectsCount++);

        bomb.Released += ReleaseObjectPool;
    }

    private void ReleaseObjectPool(Bomb bomb)
    {
        _pool.Release(bomb);
        bomb.Released -= ReleaseObjectPool;
        bomb.Released -= Changer.GetDefaultAlpha;

        _info.ShowText(this);
    }
}
