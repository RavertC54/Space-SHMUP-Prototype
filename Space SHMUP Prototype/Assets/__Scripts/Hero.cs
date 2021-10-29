using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero S; //Singleton
    [Header("Set in Inspector")]
    //These fields control the movement of the ship
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 2f;

    [Header("Set Dynamically")]
    [SerializeField]
    private float _shieldLevel = 1;

    private GameObject lastTriggerGo = null;


    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.S!");
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);

    }

    void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;

        if (go == lastTriggerGo)
        {
            return;
        }
        lastTriggerGo = go;
        if (go.tag == "Enemy")
        {
            shieldLevel--;
            Destroy(go);
        }
        else
        {
            print("Triggered by Non-Enemy: " + go.name);
        }
    }

    public float shieldLevel
    {
        get 
        {
            return (_shieldLevel);
        }
        set
        {
            _shieldLevel = Mathf.Min(value, 4);
            if (value < 0)
            {
                Destroy(this.gameObject);
                Main.S.DelayedRestart(gameRestartDelay);
            }
        }


    }


}
