using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSmog : MonoBehaviour
{
    public float smogSpeed;
    public Vector3 detectionRadius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * -smogSpeed * Time.deltaTime;
        StartCoroutine(infectCo());
        
    }
    private void OnDrawGizmos()
    {
         Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, detectionRadius);
    }

    IEnumerator infectCo()
    {
        RaycastHit[] hits = Physics.BoxCastAll(transform.position, detectionRadius, Vector3.forward);

        foreach (RaycastHit hit in hits)
        {
            yield return new WaitForSeconds(0.2f);

            Renderer renderer = hit.collider.GetComponent<Renderer>();
            if (renderer != null)
            {
                // Change the material color to black
                renderer.material.color = Color.black;
                yield return new WaitForSeconds(0.2f);
                hit.collider.gameObject.SetActive(false);
            }


            if (hit.collider.GetComponent<CubeRoll>())
            {
                yield return new WaitForSeconds(0.5f);
                LevelManager.Instance.GameOver();
            }

            yield return new WaitForSeconds(0.4f);
            hit.collider.gameObject.SetActive(false);


        }
    }
}
