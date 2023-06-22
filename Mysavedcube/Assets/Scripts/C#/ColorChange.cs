using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ColouringPlatform")
        {
            GetComponent<MeshRenderer>().material = other.GetComponent<MeshRenderer>().material;
            GetComponent<Renderer>().material.color = other.GetComponent<Renderer>().material.GetColor("_Color");
        }

        if(other.tag == "ColourPlatform")
        {        
            if(GetComponent<Renderer>().material.color != other.GetComponent<Renderer>().material.GetColor("_Color"))
            {

                LevelManager.Instance.Respawn(transform.parent.parent.gameObject);
                Debug.Log(transform.parent.parent.gameObject);

            }
        }

        if(other.tag == "KillColor")
        {
            LevelManager.Instance.Respawn(transform.parent.parent.gameObject);
        }
    }

}
