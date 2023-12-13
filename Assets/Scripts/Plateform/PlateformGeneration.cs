using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformGeneration : MonoBehaviour
{
    private const float UNITS_BETWEEN_ADJACENT_BLOCKS = 0.79f;
    private const float MIN_X_FOR_BLOCK = -8.44f;
    private const float MAX_X_FOR_BLOCK = 8.44f;
    private const float UNITS_BETWEEN_LONG_PLATEFORM_LAYERS = 2.1f;

    [SerializeField] private GameObject dirtBlockPrefab;
    [SerializeField] private GameObject waterBlockPrefab;
    [SerializeField] private GameObject player;
    private List<GameObject> dirtBlockPool;
    private List<GameObject> waterBlockPool;

    private int dirtBlockPoolSize = 220;
    private int waterBlockPoolSize = 220;

    private float lastLongPlateformLayerY = -4.4f;



    // Start is called before the first frame update
    void Start()
    {
        InitializeBlockPools();
        /*GameObject p1 =GenerateLongPlatform(Random.Range(1, 5));
        GameObject p2 = GenerateLongPlatform(Random.Range(1, 5));
        GameObject p3 = GenerateLongPlatform(Random.Range(1, 5));
        PlatformPlacing(p1,-2.3f);
        PlatformPlacing(p2, -0.2f);
        PlatformPlacing(p3, -0.2f);*/
    }

    // Update is called once per frame
    void Update()
    {
        GenerateTerrain();
    }

    private void GenerateTerrain()
    {
        for (int i = 0;i<3;i++)
        {
            GameObject p = GenerateLongPlatform(Random.Range(1, 5));
            PlatformPlacing(p, lastLongPlateformLayerY + UNITS_BETWEEN_LONG_PLATEFORM_LAYERS, i);
        }
        lastLongPlateformLayerY += UNITS_BETWEEN_LONG_PLATEFORM_LAYERS;
    }

    private void InitializeBlockPools()
    {
        dirtBlockPool = new List<GameObject>();

        // Instancie une liste de dirtBlock inactif
        for (int i = 0; i < dirtBlockPoolSize; i++)
        {
            GameObject newDirtBlock = Instantiate(dirtBlockPrefab, transform.position, Quaternion.identity);
            newDirtBlock.SetActive(false);
            dirtBlockPool.Add(newDirtBlock);
        }

        waterBlockPool = new List<GameObject>();

        // Instancie une liste de waterBlock inactif
        for (int i = 0; i < dirtBlockPoolSize; i++)
        {
            GameObject newWaterBlock = Instantiate(waterBlockPrefab, transform.position, Quaternion.identity);
            newWaterBlock.SetActive(false);
            waterBlockPool.Add(newWaterBlock);
        }
    }

    private GameObject GenerateLongPlatform(int platformLenght)
    {
        GameObject longPlatform = new GameObject();
        Vector3 previousBlockPosition = new Vector3(0, 0, 0);
        int random = Random.Range(0, 3);

        for (int i = 0; i < platformLenght; i++)
        {
            GameObject newBlock = null;

            if (random ==0)
            {
                newBlock = waterBlockPool.Find(p => !p.activeInHierarchy);
            }
            else
            {
                newBlock = dirtBlockPool.Find(p => !p.activeInHierarchy);
            }

            if (newBlock == null)
            {
                return longPlatform;
            }
            newBlock.transform.parent = longPlatform.transform;
            newBlock.SetActive(true);
            newBlock.transform.position = new Vector3(previousBlockPosition.x + UNITS_BETWEEN_ADJACENT_BLOCKS, longPlatform.transform.position.y, 0);
            previousBlockPosition = newBlock.transform.position;
        }
        return longPlatform;
    }

    private void PlatformPlacing(GameObject longPlatform, float y, int numberOfPlatformsInLayer)
    {
        float xSize = longPlatform.transform.childCount*0.79f;


        float x = 0;
        if (numberOfPlatformsInLayer==0)
        {
            x = Random.Range(MIN_X_FOR_BLOCK, -2.81f - xSize);
        }
        if (numberOfPlatformsInLayer == 1)
        {
            x = Random.Range(-2.81f, 2.82f - xSize);
        }
        if (numberOfPlatformsInLayer == 2)
        {
            x = Random.Range(2.82f, MAX_X_FOR_BLOCK - xSize);
        }
        Vector3 newPosition = new Vector3(x,y,0);

        longPlatform.transform.position = newPosition;
    }
}
