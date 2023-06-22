using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBlockTileTrigger : MonoBehaviour
{
    public enum WhatIsTriggered { TileActivation, TileDeactivation };
    public WhatIsTriggered TileFunction;

    private GameObject spawnedObject;

    [Header("SpawnTile/DespawnTile")]
    [SerializeField] private GameObject tileBridge;

    public void Start()
    {

        for (int i = 0; i < tileBridge.transform.childCount; i++)
        {
            tileBridge.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PushBlockColorChange>())
        {
            TriggerTileFunnction();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PushBlockColorChange>())
        {
            TriggerTileFunnction();
        }
    }


    public void TriggerTileFunnction()
    {
        switch (TileFunction)
        {
            case WhatIsTriggered.TileActivation:
                Debug.Log("ran");
                StartCoroutine(SpawnTileCo());
                TileFunction = WhatIsTriggered.TileDeactivation;
                break;

            case WhatIsTriggered.TileDeactivation:
                DespawnTile();
                TileFunction = WhatIsTriggered.TileActivation;
                break;

        }
    }

    private void SpawnTile()
    {
        
    }

    IEnumerator SpawnTileCo()
    {

        for(int i =0; i < tileBridge.transform.childCount; i++)
        {
            tileBridge.transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            Debug.Log(i);
        }    
        
    }
    

    private void DespawnTile()
    {
        StartCoroutine(DespawnTileCo());
    }
    
    IEnumerator DespawnTileCo()
    {
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < tileBridge.transform.childCount; i++)
        {
            tileBridge.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

}
