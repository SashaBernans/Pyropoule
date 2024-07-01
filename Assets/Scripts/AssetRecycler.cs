using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetRecycler : MonoBehaviour
{
    private static AssetRecycler instance = null;
    public static AssetRecycler Instance { get { return instance; } }

    public List<GameObject> DirtBlockPool { get => dirtBlockPool; set => dirtBlockPool = value; }
    public List<GameObject> WaterBlockPool { get => waterBlockPool; set => waterBlockPool = value; }
    public List<GameObject> PyropouleProjectilePool { get => pyropouleProjectilePool; set => pyropouleProjectilePool = value; }
    public List<GameObject> PlayerProjectilePool { get => playerProjectilePool; set => playerProjectilePool = value; }
    public List<GameObject> LongPlatformPool { get => longPlatformPool; set => longPlatformPool = value; }
    public List<GameObject> PyropoulePool { get => pyropoulePool; set => pyropoulePool = value; }
    public List<GameObject> TurkeyPool { get => turkeyPool; set => turkeyPool = value; }
    public List<GameObject> BucketPool { get => bucketPool; set => bucketPool = value; }
    public List<GameObject> LaserPool { get => laserPool; set => laserPool = value; }

    [SerializeField] private GameObject dirtBlockPrefab;
    [SerializeField] private GameObject waterBlockPrefab;
    [SerializeField] private GameObject pyropouleProjectilePrefab;
    [SerializeField] private GameObject playerProjectilePrefab;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject longPlatformPrefab;
    [SerializeField] private GameObject pyropoulePrefab;
    [SerializeField] private GameObject turkeyPrefab;
    [SerializeField] private GameObject bucketPrefab;

    private List<GameObject> dirtBlockPool;
    private List<GameObject> waterBlockPool;
    private List<GameObject> pyropouleProjectilePool;
    private List<GameObject> playerProjectilePool;
    private List<GameObject> longPlatformPool;
    private List<GameObject> pyropoulePool;
    private List<GameObject> bucketPool;
    private List<GameObject> turkeyPool;

    private static int pyropouleProjectilePoolSize = 100;
    private static int playerProjectilePoolSize = 10;
    private static int pyropoulePoolSize = 10;
    private static int turkeyPoolSize = 10;
    private static int bucketPoolSize = 10;
    private List<GameObject> laserPool;
    private const int laserPoolSize = 30;
    private const int DIRT_BLOCK_POOL_SIZE = 64;
    private const int WATER_BLOCK_POOL_SIZE = 64;
    private const int LONG_PLATFORM_POOL_SIZE = 128;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);

        dirtBlockPool = InstanciateGameObjectPool(dirtBlockPrefab, DIRT_BLOCK_POOL_SIZE);
        waterBlockPool = InstanciateGameObjectPool(waterBlockPrefab, WATER_BLOCK_POOL_SIZE);
        pyropouleProjectilePool = InstanciateGameObjectPool(pyropouleProjectilePrefab, pyropouleProjectilePoolSize);
        playerProjectilePool = InstanciateGameObjectPool(playerProjectilePrefab, playerProjectilePoolSize);
        longPlatformPool = InstanciateGameObjectPool(longPlatformPrefab, LONG_PLATFORM_POOL_SIZE);
        pyropoulePool = InstanciateGameObjectPool(pyropoulePrefab, pyropoulePoolSize);
        turkeyPool = InstanciateGameObjectPool(turkeyPrefab, turkeyPoolSize);
        bucketPool = InstanciateGameObjectPool(bucketPrefab, bucketPoolSize);
        laserPool = InstanciateGameObjectPool(laserPrefab, laserPoolSize);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<GameObject> InstanciateGameObjectPool(GameObject prefab, int poolSize)
    {
        List<GameObject>  pool = new List<GameObject>();

        // Instancie une liste de GameObject inactif
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newGameObject = Instantiate(prefab, transform.position, Quaternion.identity);
            newGameObject.SetActive(false);
            pool.Add(newGameObject);
        }
        return pool;
    }
}
