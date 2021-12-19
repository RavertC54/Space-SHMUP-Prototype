using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static public Main S; //Singleton

    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies;
    public Material[] enemyMats;
    public Material newMat;
    public string matName;
    public float enemySpawnPerSecond = 0.5f;
    public float enemyDefaultPadding = 1.5f;
    

    private BoundsCheck bndCheck;

    private void Awake()
    {
        S = this;
        bndCheck = GetComponent<BoundsCheck>();
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }

    public void SpawnEnemy()
    {
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);
        
        go.GetComponent<Renderer>().material = newMat;

        //Assigns a material named "Assets/Resources/DEV_Orange" to the object.
        //Material newMat = Resources.Load("DEV_Orange", typeof(Material)) as Material;
        //gameObject.renderer.material = newMat;


        float enemyPadding = enemyDefaultPadding;
        if (go.GetComponent<BoundsCheck>() != null)
        {
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }

        //Initial position of spawned enemy
        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyPadding;
        float xMax = bndCheck.camWidth - enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyPadding;
        go.transform.position = pos;

        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }

    

    public void DelayedRestart(float delay)
    {
        Invoke("Restart", delay);
    }

    public void Restart()
    {
        SceneManager.LoadScene("_Scene_1");
    }

    // Start is called before the first frame update
    void Start()
    {
        int matNdx = Random.Range(0, enemyMats.Length);

        string matName = "";
        if (matNdx == 0)
        {
            matName = "EnemyMat_0";

        }
        if (matNdx == 1)
        {
            matName = "EnemyMat_1";
        }
        if (matNdx == 2)
        {
            matName = "EnemyMat_2";
        }

        Material newMat = Resources.Load(matName, typeof(Material)) as Material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
