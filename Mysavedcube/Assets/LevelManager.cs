using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject playerCubePrefab;
    [SerializeField] private GameObject pushBlockPrefab;


    private Vector3 cubeRespawnPos;

    [Header("Lives count")]
    public int livesCount, maxLives = 3;
    public Image lifeBar;
    public Sprite fullLife;   //The lives sprite

    [Header("Face colour")]
    public GameObject MiddleFace;
    public GameObject LeftFace;
    public GameObject RightFace;
    public GameObject Upface;
    public GameObject DownFace;

    public static LevelManager Instance { get; private set; }
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        cubeRespawnPos = FindObjectOfType<CubeRoll>().transform.position;
        livesCount = maxLives;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Respawn(GameObject cube)
    {
        StartCoroutine(RespawnCo(cube));
    }
    IEnumerator RespawnCo(GameObject cube)
    {
        Destroy(cube, 0);
        if (cube.tag == "Player")
        {
            UpdateLvesCount();
            if (livesCount > 0)
            {              
                yield return new WaitForSeconds(1f);
                var _playerCube = Instantiate(playerCubePrefab, cubeRespawnPos, Quaternion.identity);
                playerCamera.GetComponent<CameraFollow>().target = _playerCube;
            }
            else
            {
                GameOver();
            }
        }
        else
        {
            Vector3 PushBlockrespawnPos = cube.GetComponent<PushBlock>().respawnPos;
            yield return new WaitForSeconds(1f);
            Instantiate(pushBlockPrefab, PushBlockrespawnPos, Quaternion.identity);
        }
    }

    public void UpdateLvesCount()
    {
        if (livesCount > maxLives) { livesCount = maxLives; }
        livesCount --;

        for (int i = 0; i < maxLives; i++)
            if (i < livesCount)
                {
                lifeBar.transform.GetChild(i).gameObject.SetActive(true);
                }
            else
                {
                lifeBar.transform.GetChild(i).gameObject.SetActive(false);
                }
            Debug.Log(livesCount);
        
    }



    public void GameOver()
    {
        SceneManager.LoadScene("Lose");
    }


}
