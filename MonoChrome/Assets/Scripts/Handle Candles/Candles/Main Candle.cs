using Unity.VisualScripting;
using UnityEngine;

public class Candle : MonoBehaviour,InteractCandle
{
    PlayerController controller => 
        GameManager.instance.blackBoard.GetValue("PlayerController", out PlayerController _controller) ? _controller : null;
    public int CandleIndex {  get; set; }
    public bool isInteracted;
    public bool isInCollision;
    public string toSay;

    [Header("Room Objects")]
    [SerializeField] private GameObject candleLit;
    [SerializeField] private GameObject prevCandleBackground;
    [SerializeField] private GameObject newCandleBackground;
    [SerializeField] private GameObject candle;
    [SerializeField] private GameObject blackLayer;
 

    public virtual void OnStartInteract()
    {
        Debug.Log("Candle");
        candleLit.gameObject.SetActive(true);
        isInCollision = true; 
    }

    public virtual void OnInteract()
    {
        
        Instantiate(newCandleBackground, prevCandleBackground.transform.position, Quaternion.identity);
        Instantiate(candle, candleLit.transform.position, Quaternion.identity);
        candleLit.gameObject.SetActive(false);
        Color color = blackLayer.GetComponent<SpriteRenderer>().color;
        color.a = 0;
        blackLayer.GetComponent<SpriteRenderer>().color = color;
        controller.GetComponent<SpriteRenderer>().color = Color.white;
        Destroy(prevCandleBackground.gameObject);
        controller.transform.position = controller.silhoutte.transform.position;
        if(controller.silhoutte != null && !controller.silhoutte.gameObject.IsDestroyed())
        {
            Destroy(controller.silhoutte.gameObject);
            controller.silhoutte = null;

        }

        Room room = GameManager.instance.GetCurrentRoom();
        Destroy(room.levelEnemyReference);
        if(room.levelEnemyReference != null)
        {
            room.levelEnemyReference.gameObject.SetActive(false); 
            room.levelEnemyReference.GetComponent<Enemy>().enabled = false;
            
        }
        room.GetComponent<Collider2D>().enabled = false;

        room.isPlayerInHere = false;
        controller.candleCounter--;
        UIController.instance.inventoryCandleAmount.text = controller.candleCounter.ToString();

        GameManager.instance.litCandleAmount++;
        Time.timeScale = 1;
    }

    public virtual void OnLeaveInteract()
    {
        candleLit.gameObject.SetActive(false);
        isInCollision = false;
    }

    private void Update()
    {
        if (isInCollision && !isInteracted && Input.GetKeyDown(KeyCode.E))
        {
            OnInteract();
            isInteracted = true;
        }
    }
}
