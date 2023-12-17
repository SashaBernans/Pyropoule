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

    [SerializeField] private GameObject dirtBlockPrefab;
    [SerializeField] private GameObject waterBlockPrefab;
    [SerializeField] private GameObject pyropouleProjectilePrefab;
    [SerializeField] private GameObject playerProjectilePrefab;
    [SerializeField] private GameObject longPlatformPrefab;

    private List<GameObject> dirtBlockPool;
    private List<GameObject> waterBlockPool;
    private List<GameObject> pyropouleProjectilePool;
    private List<GameObject> playerProjectilePool;
    private List<GameObject> longPlatformPool;

    private int pyropouleProjectilePoolSize = 10;
    private int playerProjectilePoolSize = 10;
    private const int DIRT_BLOCK_POOL_SIZE = 64;
    private const int WATER_BLOCK_POOL_SIZE = 64;
    private const int LONG_PLATFORM_POOL_SIZE = 128;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        dirtBlockPool = InstanciateGameObjectPool(dirtBlockPrefab, DIRT_BLOCK_POOL_SIZE);
        waterBlockPool = InstanciateGameObjectPool(waterBlockPrefab, WATER_BLOCK_POOL_SIZE);
        pyropouleProjectilePool = InstanciateGameObjectPool(pyropouleProjectilePrefab, pyropouleProjectilePoolSize);
        playerProjectilePool = InstanciateGameObjectPool(playerProjectilePrefab, playerProjectilePoolSize);
        longPlatformPool = InstanciateGameObjectPool(longPlatformPrefab, LONG_PLATFORM_POOL_SIZE);
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
