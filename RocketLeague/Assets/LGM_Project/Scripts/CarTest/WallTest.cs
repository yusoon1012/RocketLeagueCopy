using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTest : MonoBehaviour
{
    private GameObject inCar;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Car")
        {
            inCar = collision.gameObject;
            inCar.GetComponent<CarTest2>().InWall();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Car")
        {
            inCar = collision.gameObject;
            inCar.GetComponent<CarTest2>().OutWall();
        }
    }
}
