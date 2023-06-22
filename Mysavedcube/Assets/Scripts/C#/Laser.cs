using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    LineRenderer lr;
    public float maxDistance = 5f;
    
    const int max_bounces = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //


        lr.positionCount = 2;
        Vector3 dir = transform.forward,   //vector3(dir,a,b)
            a = transform.position,
            b;

        int bounces = 1;

        while (bounces < lr.positionCount)
        {
            RaycastHit[] hits = Physics.RaycastAll(a, transform.forward, maxDistance); //Boom raycast
            b = a + dir * maxDistance;
            foreach(RaycastHit hit in hits)
            {
                if (hit.collider.gameObject == gameObject) continue; //If gameobject hits itself, re-loops

                //otherwise stop the laser's position
                b = hit.transform.position;
                if (hit.transform.CompareTag("Mirror"))
                {
                    dir = hit.transform.right;     //sets the dir to the mirror's transform.right
                    lr.positionCount++;            
                }
                break; //we only want the first hit
            }
            lr.SetPosition(bounces, b);
            bounces++;
            a = b;

            if (bounces > max_bounces) break;
            
        }
    }
}
