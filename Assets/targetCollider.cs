using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetCollider : MonoBehaviour
{
    public static targetCollider instance;  

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void onTriggerEnter(Collider other)
    {
        moveTarget();
    }

    public void moveTarget()
    {
        Vector3 temp;
        temp.x = Random.Range(-2.5f, 2.5f);
        temp.y = Random.Range(0.4f, 1f);
        temp.z = Random.Range(-2.5f, 2.5f);
        transform.position = new Vector3(temp.x, temp.y, temp.z);
    }
}
