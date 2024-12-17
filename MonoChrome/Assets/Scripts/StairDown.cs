using System.Collections;
using UnityEngine;

public class Stair : MonoBehaviour
{
    PlayerController controller => 
        GameManager.instance.blackBoard.GetValue("PlayerController",out PlayerController _controller) ? _controller : null;
    public Transform downPoint;
    float duration = 1f;
    bool isOnTrigger = false;
    public void UseStair()
    {
        controller.transform.position = downPoint.transform.position;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isOnTrigger && GameManager.instance.litCandleAmount >= 2)
        {
            UseStair();
        }
    }

    private IEnumerator InitTransfer()
    {
        yield return new WaitForSeconds(duration);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerController>(out PlayerController controller))
        { 
            UIController.instance.indicator.SetActive(true);
            isOnTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController controller))
        {
            isOnTrigger = false;
            UIController.instance.indicator.SetActive(false);
        }
    }
}
