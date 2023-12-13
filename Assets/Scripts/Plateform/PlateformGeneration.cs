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
    [SerializeField] private GameObject player;
    private List<GameObject> dirtBlockPool;

    private int dirtBlockPoolSize = 220;

    private float lastLongPlateformLayerY = -4.4f;



    // Start is called before the first frame update
    void Start()
    {
        InitializePlateformDirtPool();
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
            PlatformPlacing(p, lastLongPlateformLayerY + UNITS_BETWEEN_LONG_PLATEFORM_LAYERS);
        }
        lastLongPlateformLayerY += UNITS_BETWEEN_LONG_PLATEFORM_LAYERS;
    }

    private void InitializePlateformDirtPool()
    {
        dirtBlockPool = new List<GameObject>();

        // Instancie une liste de newPlatformDirt inactif
        for (int i = 0; i < dirtBlockPoolSize; i++)
        {
            GameObject newPlatformDirt = Instantiate(dirtBlockPrefab, transform.position, Quaternion.identity);
            newPlatformDirt.SetActive(false);
            dirtBlockPool.Add(newPlatformDirt);
        }
    }

    private GameObject GenerateLongPlatform(int platformLenght)
    {
        GameObject longPlatform = new GameObject();
        Vector3 previousBlockPosition = new Vector3(0, 0, 0);
        //longPlatform.transform.position = new Vector3(3, 3, 0);

        for (int i = 0; i < platformLenght; i++)
        {
            // Trouver platformDirt inactif dans la liste
            GameObject newPlatformDirt = dirtBlockPool.Find(p => !p.activeInHierarchy);
            if (newPlatformDirt == null)
            {
                return longPlatform;
            }
            newPlatformDirt.transform.parent = longPlatform.transform;
            newPlatformDirt.SetActive(true);
            newPlatformDirt.transform.position = new Vector3(previousBlockPosition.x + UNITS_BETWEEN_ADJACENT_BLOCKS, longPlatform.transform.position.y, 0);
            previousBlockPosition = newPlatformDirt.transform.position;
        }
        return longPlatform;
    }

    private void PlatformPlacing(GameObject longPlatform, float y)
    {
        float x = Random.Range(MIN_X_FOR_BLOCK, MAX_X_FOR_BLOCK);

        Vector3 newPosition = new Vector3(
            x,
            y,
            0);

        longPlatform.transform.position = newPosition;
    }
}
