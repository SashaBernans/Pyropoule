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
                GameObject p = GenerateLongPlatform(Random.Range(1, 5));
                PlatformPlacing(p, lastLongPlateformLayerY + UNITS_BETWEEN_LONG_PLATEFORM_LAYERS, i);
                int random =  Random.Range(1,10);
                if (random == 1)
                {
                    GenerateEnemy(p);
                }
                else if (random == 2)
                {
                    SpawnBucket(p);
                }
            }
            lastLongPlateformLayerY += UNITS_BETWEEN_LONG_PLATEFORM_LAYERS;
        }
    }

    private void SpawnBucket(GameObject p)
    {
        GameObject bucket = assetRecycler.BucketPool.Find(p => !p.activeInHierarchy);
        if (bucket != null)
        {
            bucket.transform.position = new Vector2(
                p.transform.position.x + UNITS_BETWEEN_ADJACENT_BLOCKS,
                p.transform.position.y + UNITS_BETWEEN_ADJACENT_BLOCKS);
            bucket.SetActive(true);
            /*bucket.transform.position = new Vector2(
                p.transform.position.x + UNITS_BETWEEN_ADJACENT_BLOCKS,
                p.transform.position.y + UNITS_BETWEEN_ADJACENT_BLOCKS);*/
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

    private void GenerateEnemy(GameObject platform)
    {
        GameObject pyropoule = assetRecycler.PyropoulePool.Find(p => !p.activeInHierarchy);
        if (pyropoule!=null)
        {
            pyropoule.SetActive(true);
            pyropoule.transform.position = new Vector2(
                platform.transform.position.x + UNITS_BETWEEN_ADJACENT_BLOCKS, 
                platform.transform.position.y + UNITS_BETWEEN_ADJACENT_BLOCKS);
        }
    }
}
