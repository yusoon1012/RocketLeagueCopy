using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Xml.Serialization;
using UnityEngine;

public class CarItemManager : MonoBehaviour
{
    public GameObject myCollider;
    private string targetTag;
    float radius = 50f;
    Vector3 newPosition;
    private Collider nearestCollider;
    // Start is called before the first frame update
    void Start()
    {
        if (myCollider.tag=="OrangeCar")
        {
            targetTag="BlueCar";

        }
        else
        {
            targetTag="OrangeCar";

        }
    }

    // Update is called once per frame
    void Update()
    {
        newPosition=transform.position;
        newPosition.y=2.4f;
        Collider[] colliders =
                   Physics.OverlapSphere(newPosition, radius);
        float nearestDistance = float.MaxValue;
        foreach (Collider col in colliders)
        {
            if (myCollider.tag==col.tag) continue;
            if(col.tag==targetTag)
            {
            Debug.LogFormat("내 태그 {0} 상대 태그 {1}",myCollider.tag, col.tag);

                float distance = Vector3.Distance(newPosition, col.transform.position);
                if (distance < nearestDistance)
                {
                    nearestCollider = col;
                    nearestDistance = distance;
                }
                if (nearestCollider != null)
                {
                    Debug.LogFormat("내 태그 {0}, 가장 가까운 상대 태그 {1}", myCollider.tag, nearestCollider.tag);
                    Rigidbody rb = nearestCollider.GetComponent<Rigidbody>();
                    Vector3 dir = (col.transform.position-transform.position).normalized;
                    if (rb != null)
                    {
                        if (Input.GetKey(KeyCode.Q))
                        {
                            rb.AddForce(dir*200, ForceMode.Impulse);

                        }
                    }
                    // nearestCollider를 사용하여 필요한 작업을 수행할 수 있습니다.
                }
            }
            


        }
       
    }

   

   
    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawSphere(newPosition, radius);
    //}

}
