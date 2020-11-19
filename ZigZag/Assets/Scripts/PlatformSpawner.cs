using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public static PlatformSpawner instance;

    public GameObject platform;
    public GameObject diamond;

    public bool gameOver;
    private Vector3 lastPos;
    private float size;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        lastPos = platform.transform.position;
        size = platform.transform.localScale.x;

        for (int i = 0; i < 20; i++)
        {
            SpawnPlatforms();
        }

        //Calls any function repeatedly after amount of time - (name of the function
        //after how much time is the first call, repeat rate of the calling)
    }

    public void StartSpawningPlatforms()
    {
        InvokeRepeating("SpawnPlatforms", 0.1f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameOver)
        {
            //The opposite of Invoke() - stops SpawnPlatforms() from invoking
            CancelInvoke("SpawnPlatforms");
        }
    }

    void SpawnPlatforms()
    {
        if (gameOver)
        {
            return;
        }

        //Create random number between range - from 0 to 5
        int rand = Random.Range(0, 6);

        if (rand < 3) //0,1,2
        {
            SpawnX();
        }
        else if (rand >= 3) //3,4,5
        {
            SpawnZ();
        }
    }

    void SpawnX()
    {
        Vector3 pos = lastPos;
        //The position of the spawner will be moved on x by the size of the platform
        pos.x += size;
        lastPos = pos;
        //Instantiate prefab objects (what to instantiate, in which position, specify the rotation)
        //Quaternion.identity - no rotation
        Instantiate(platform, pos, Quaternion.identity);

        int rand = Random.Range(0, 4);
        if (rand < 1)
        {
            //Instantiate diamond
            Instantiate(diamond, new Vector3(pos.x, pos.y + 1.5f, pos.z), diamond.transform.rotation);
        }
    }

    //Same as SpawnX(), but for z
    void SpawnZ()
    {
        //Get the position, move the position.z by the size, lastPos = position, instantiate platform
        Vector3 pos = lastPos;
        pos.z += size;
        lastPos = pos;
        Instantiate(platform, pos, Quaternion.identity);

        int rand = Random.Range(0, 4);
        if (rand < 1)
        {
            //Instantiate diamond
             Instantiate(diamond, new Vector3(pos.x, pos.y + 1.5f, pos.z), diamond.transform.rotation);
        }
    }
}
