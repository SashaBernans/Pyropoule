using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformGeneration : MonoBehaviour
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
            GenerateTerrain();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void GenerateTerrain()
    {
        if(CheckInactiveCount(assetRecycler.DirtBlockPool, MAX_BLOCKS_PER_LAYER) && CheckInactiveCount(assetRecycler.WaterBlockPool, MAX_BLOCKS_PER_LAYER))
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject p = GenerateLongPlatform(Random.Range(1, 5));
                PlatformPlacing(p, lastLongPlateformLayerY + UNITS_BETWEEN_LONG_PLATEFORM_LAYERS, i);
            }
            lastLongPlateformLayerY += UNITS_BETWEEN_LONG_PLATEFORM_LAYERS;
        }
    }

    private GameObject GenerateLongPlatform(int platformLenght)
    {
        GameObject longPlatform = assetRecycler.LongPlatformPool.Find(p => !p.activeInHierarchy);
        longPlatform.SetActive(true);
        Vector3 previousBlockPosition = new Vector3(0, 0, 0);
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
            newBlock.transform.position = new Vector3(previousBlockPosition.x + UNITS_BETWEEN_ADJACENT_BLOCKS, longPlatform.transform.position.y, 0);
            previousBlockPosition = newBlock.transform.position;
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
        Vector3 newPosition = new Vector3(x, y, 0);

        longPlatform.transform.position = newPosition;
    }
}
