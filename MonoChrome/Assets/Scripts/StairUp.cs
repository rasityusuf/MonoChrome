using System.Collections.Generic;
using UnityEngine;

public class StairUp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    PlayerController controller =>
        GameManager.instance.blackBoard.GetValue("PlayerController", out PlayerController _controller) ? _controller : null;
    public Transform downPoint;
    float duration = 1f;
    bool isOnTrigger = false;
    public void GetDown()
    {
        controller.transform.position = downPoint.transform.position;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isOnTrigger)
        {
            GetDown();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController controller))
        {
            if(GameManager.instance.litCandleAmount >= 2)
                UIController.instance.indicator.SetActive(true);
            isOnTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController controller))
        {
            isOnTrigger = false;
            if (GameManager.instance.litCandleAmount >= 2)
                UIController.instance.indicator.SetActive(false);
        }
    }
}
