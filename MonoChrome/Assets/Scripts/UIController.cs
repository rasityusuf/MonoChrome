using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public GameObject indicator;
    public GameObject interactionPanel;
    public GameObject alertPanel;
    public TextMeshProUGUI dialogue;
    public GameObject shadowPartDrawer;
    public TextMeshProUGUI inventoryShadowPartAmount;
    public GameObject candleDrawer;
    public TextMeshProUGUI inventoryCandleAmount;
    public GameObject keyDrawer;
    public TextMeshProUGUI inventoryKeyAmount;
    public GameObject candleUI;
    public GameObject gameOverSecene;
    

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
                                
    }

    // Update is called once per frame
    void Update()
    {
                
    }
}
