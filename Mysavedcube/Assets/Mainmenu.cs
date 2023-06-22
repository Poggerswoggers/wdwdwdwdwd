using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public Color[] primaryColors;
    public Color[] BgColors;

    public int beatcount;
    [SerializeField] private Image imageUi;
    public AudioSource audioSource;
    public GameObject cubeMeshFaces;
    [Header("Beat threshold")]
    public float cubeBeatThreshold;
    public float delayTime;

    private float averageIntensity = 0f;


    // Start is called before the first frame update
    void Start()
    {
        primaryColors = new Color[] { Color.red, Color.blue, Color.yellow, Color.green, Color.magenta, Color.white};

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        InvokeRepeating("SetRandomBgColor", delayTime,delayTime);
    }

    // Update is called once per frame
    void Update()
    {
        float[] spectrumData = new float[1024];
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Hamming);

        averageIntensity = 0f;
        for (int i = 0; i < spectrumData.Length; i++)
        {
            averageIntensity += spectrumData[i];
        }
        averageIntensity /= spectrumData.Length;

        if (averageIntensity > cubeBeatThreshold)
        {
            beatcount++;
            SetRandomPrimaryColor();
        }


    }


    void SetRandomPrimaryColor()
    {
        for (int i = 0; i < cubeMeshFaces.transform.childCount; i++)
        {

            int randomIndex = Random.Range(0, primaryColors.Length);
            cubeMeshFaces.transform.GetChild(i).GetComponent<Renderer>().material.color = primaryColors[randomIndex];
        }
    }
    void SetRandomBgColor()
    {
        int randomIndex = Random.Range(0, BgColors.Length);
        imageUi.GetComponent<Image>().color = BgColors[randomIndex];

    }
   
}
