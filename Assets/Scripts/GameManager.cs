using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject food;
    public float[] pathZPos;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            SpawnFood();
        }
    }

    void SpawnFood() 
    {
        float xPos = Random.Range(-35f, 35f);
        int zPosRandom = Random.Range(0, pathZPos.Length);
        float zPos = pathZPos[zPosRandom];
        
        Vector3 foodPos = new Vector3(xPos, -0.85f, zPos);
        food.transform.position = foodPos;
    }
}
