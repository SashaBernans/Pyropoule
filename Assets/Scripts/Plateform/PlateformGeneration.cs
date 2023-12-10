using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformGeneration : MonoBehaviour
{
    [SerializeField] private GameObject dirtBlockPrefab;
    private List<GameObject> dirtBlockPool;
    private int dirtBlockPoolSize = 220;

    private float dirtBlockXSize;

    // Start is called before the first frame update
    void Start()
    {
        dirtBlockXSize = dirtBlockPrefab.GetComponent<BoxCollider2D>().size.x;
        InitializePlateformDirtPool();
        GenerateLongPlatform(Random.Range(1, 10));
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Vector3 previousBlockPosition = new Vector3(0,0,0);
        longPlatform.transform.position = new Vector3(3, 3, 0);

        for ( int i = 0; i < platformLenght; i++)
        {
            // Trouver platformDirt inactif dans la liste
            GameObject newPlatformDirt = dirtBlockPool.Find(p => !p.activeInHierarchy);
            newPlatformDirt.transform.parent = longPlatform.transform;
            newPlatformDirt.SetActive(true);
            newPlatformDirt.transform.position = new Vector3(previousBlockPosition.x+dirtBlockXSize*1.5f, longPlatform.transform.position.y,0);
            previousBlockPosition = newPlatformDirt.transform.position;
        }
        return longPlatform;
    }

    private void PlatformPlacing()
    {

    }
}
