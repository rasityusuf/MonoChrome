using System.Text;
using UnityEngine;


public class BlackLayers : MonoBehaviour
{
    [SerializeField] GameObject candle;
    public bool isOpenLights;
    public SpriteRenderer fadeRenderer;
    public float fadeSpeed = 50f;
    private float targetAlpha = 0f;


    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("lights Open");

           
            Color color = new Color(0, 0, 0, targetAlpha);  
            fadeRenderer.color = color;
        }


    }
}
