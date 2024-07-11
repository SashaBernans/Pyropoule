using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    private const float UNITS_BETWEEN_ADJACENT_BLOCKS = 0.79f;
    private const float MIN_X_FOR_BLOCK = -8.44f;
    private const float MAX_X_FOR_BLOCK = 8.44f;
    private const float UNITS_BETWEEN_LONG_PLATEFORM_LAYERS = 2.1f;
    private const int MAX_BLOCKS_PER_LAYER = 12;
    [SerializeField] private int pyropouleSpawnrate;
    [SerializeField] private int turkeySpawnRate;
    [SerializeField] private int bucketSpawnRatePercentage;
    [SerializeField] private AssetRecycler assetRecycler;

    private float lastLongPlateformLayerY = -4.4f;



    // Start is called before the first frame update
    void Start()
    {
        assetRecycler = AssetRecycler.Instance;
        StartCoroutine(GenerateTerrainCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool CheckInactiveCount(List<GameObject> objects, int countThreshold)
    {
        int inactiveCount = 0;

        for (int i = 0; i < objects.Count; i++)
        {
            if (!objects[i].activeSelf)
            {
                inactiveCount++;
                if (inactiveCount >= countThreshold)
                {
                    return true;
                }
            }
        }
        return false;
    }

    IEnumerator GenerateTerrainCoroutine()
    {
        while (true)
        {
            GenerateLayer();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void GenerateLayer()
    {
        if(CheckInactiveCount(assetRecycler.DirtBlockPool, MAX_BLOCKS_PER_LAYER) && CheckInactiveCount(assetRecycler.WaterBlockPool, MAX_BLOCKS_PER_LAYER))
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject longPlatform = GenerateLongPlatform(Random.Range(1, 5));
                PlatformPlacing(longPlatform, lastLongPlateformLayerY + UNITS_BETWEEN_LONG_PLATEFORM_LAYERS, i);
                if (pyropouleSpawnrate >= Random.Range(0, 100))
                {
                    //addEntityToPlatform(p, assetRecycler.PyropoulePool.Find(p => !p.activeInHierarchy));

                    addEntitiesToPlatform(longPlatform, assetRecycler.getActiveGameObjects(longPlatform.transform.childCount, assetRecycler.PyropoulePool));
                }
                else if (turkeySpawnRate >= Random.Range(0, 100))
                {
                    addEntityToPlatform(longPlatform, assetRecycler.TurkeyPool.Find(p => !p.activeInHierarchy));
                }
                else if (bucketSpawnRatePercentage >= Random.Range(0, 100))
                {
                    addEntityToPlatform(longPlatform,assetRecycler.BucketPool.Find(p => !p.activeInHierarchy));
                }
            }
            lastLongPlateformLayerY += UNITS_BETWEEN_LONG_PLATEFORM_LAYERS;
        }
    }

    private GameObject GenerateLongPlatform(int platformLenght)
    {
        GameObject longPlatform = assetRecycler.LongPlatformPool.Find(p => !p.activeInHierarchy);
        longPlatform.SetActive(true);
        Vector2 previousBlockPosition = new Vector2(0, 0);
        int random = Random.Range(0, 3);

        for (int i = 0; i < platformLenght; i++)
        {
            GameObject newBlock = null;

            if (random ==0)
            {
                newBlock = assetRecycler.WaterBlockPool.Find(p => !p.activeInHierarchy);
            }
            else
            {
                newBlock = assetRecycler.DirtBlockPool.Find(p => !p.activeInHierarchy);
            }

            if (newBlock == null)
            {
                return longPlatform;
            }
            newBlock.transform.parent = longPlatform.transform;
            newBlock.SetActive(true);
            newBlock.transform.localPosition = new Vector2(previousBlockPosition.x + UNITS_BETWEEN_ADJACENT_BLOCKS, 0);
            previousBlockPosition = newBlock.transform.localPosition;
        }
        return longPlatform;
    }

    private void PlatformPlacing(GameObject longPlatform, float y, int numberOfPlatformsInLayer)
    {
        float xSize = longPlatform.transform.childCount * 0.79f;


        float x = 0;
        if (numberOfPlatformsInLayer == 0)
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
        Vector2 newPosition = new Vector2(x, y);

        longPlatform.transform.position = newPosition;
    }

    private void addEntityToPlatform(GameObject platform, GameObject entity)
    {
        if (entity != null)
        {
            entity.transform.position = new Vector2(
                platform.transform.position.x + UNITS_BETWEEN_ADJACENT_BLOCKS,
                platform.transform.position.y + UNITS_BETWEEN_ADJACENT_BLOCKS);
            entity.SetActive(true);
        }
    }

    private void addEntitiesToPlatform(GameObject platform, List<GameObject> entities)
    {
        Transform[] platformTransforms = platform.GetComponentsInChildren<Transform>();

        for (int i = 0; i<entities.Count; i++)
        {
            if (entities[i] !=null)
            {
                entities[i].transform.position = new Vector2(
                platformTransforms[i].position.x + UNITS_BETWEEN_ADJACENT_BLOCKS,
                platformTransforms[i].position.y + UNITS_BETWEEN_ADJACENT_BLOCKS);
                entities[i].SetActive(true);
            }
        }
    }
}
