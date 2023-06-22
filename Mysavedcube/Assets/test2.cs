using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{
    public static test2 Instance { get; private set; }

    public bool moveforever = false;
    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        print(direction);
    }

    // Update is called once per frame
    void Update()
    {
        if (moveforever)
        {
            //transform.position = new Vector3()
        }
    }
    public void rollforever()
    {
        
    }


}
