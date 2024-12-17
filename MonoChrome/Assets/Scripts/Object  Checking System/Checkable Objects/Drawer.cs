using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Drawer : Belonging
{
    public bool containsKey;
    public bool containsShadowPart;
    public bool containsCandle;
    public override void OnCheck()
    {
       
        UIController.instance.indicator.gameObject.SetActive(false);
        if (containsKey)
        {
            UIController.instance.keyDrawer.gameObject.SetActive(false);
            playerReference.keyCounter++;
            playerReference.hasKey = true;
            UIController.instance.inventoryKeyAmount.text = playerReference.keyCounter.ToString();
            
        }
        if (containsShadowPart)
        {
            UIController.instance.shadowPartDrawer.gameObject.SetActive(false);
            playerReference.shadowPartCounter++;
            playerReference.hasShadowPart = true;
            UIController.instance.inventoryShadowPartAmount.text = playerReference.shadowPartCounter.ToString() + "/3";
        }
        if (containsCandle)
        {
            UIController.instance.candleDrawer.gameObject.SetActive(false);
            playerReference.candleCounter++;
            playerReference.hasCandle = true;
            UIController.instance.inventoryCandleAmount.text = playerReference.candleCounter.ToString();
            UIController.instance.candleUI.gameObject.SetActive(true);
        }
    }

    public override void OnEnterCheck()
    {
        Debug.Log("OnEnterCheck"); 
        if (!isChecked)
        {
            UIController.instance.indicator.gameObject.SetActive(true);
        }

        inCollision = true;

    }  

    private void Update()
    {
        if (inCollision)
        {
            Debug.Log("OnStayCheck");
            if (Input.GetKeyDown(KeyCode.E) && !isChecked)
            {
               
                if (containsKey)
                {
                    UIController.instance.keyDrawer.gameObject.SetActive(true);
                    UIController.instance.candleUI.gameObject.SetActive(false);
                }
                if (containsCandle)
                {
                    UIController.instance.candleDrawer.gameObject.SetActive(true);
                    UIController.instance.candleUI.gameObject.SetActive(false);
                }
                if (containsShadowPart)
                {
                    UIController.instance.shadowPartDrawer.gameObject.SetActive(true);
                } 
                
                playerReference.isInInteraction = true;
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Checked");
                isChecked = true;
                playerReference.isInInteraction = false;
                
                OnCheck();
                containsCandle = false;
                
            }
        }

        playerReference.isInCollisionWithDrawer = inCollision;
    }

    
}
