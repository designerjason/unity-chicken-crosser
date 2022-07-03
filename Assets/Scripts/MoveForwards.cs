using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwards : MonoBehaviour
{
    public float speed = 30;
    public int bounds = 60;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if(gameObject.transform.position.x > bounds || gameObject.transform.position.x < -bounds)
        {
            Destroy(gameObject);
        }
    }
}
