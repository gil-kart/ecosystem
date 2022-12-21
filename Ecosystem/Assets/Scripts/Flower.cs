using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Flower : MonoBehaviour
{
    private bool isDestroyed = false;
    private float timePassed = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDestroyed)
        {
            Vector3 decVec = new Vector3(-0.04f, -0.04f, -0.04f);
            transform.localScale += decVec;
            timePassed -= Time.deltaTime;

            if (transform.localScale.x < 0f)
            {
                Destroy(gameObject);    
            }

        }
    }

    public void setToDestroyed()
    {
        isDestroyed = true;
        timePassed = 4;

    }

    public static explicit operator Flower(GameObject v)
    {
        throw new NotImplementedException();
    }
}
