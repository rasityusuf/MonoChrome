using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bed : Belonging
{
    public override void OnEnterCheck()
    {
        base.OnEnterCheck();
    }

    public override void OnLeaveCheck()
    {
        base.OnLeaveCheck();
    }

    private void Update()
    {
        if (inCollision)
        {

            Debug.Log("OnStayCheck");
            if (Input.GetKeyDown(KeyCode.E) && !isChecked)
            {
                UIController.instance.interactionPanel.gameObject.SetActive(true);
                UIController.instance.interactionPanel.transform.Find("BelongingText").GetComponent<TextMeshProUGUI>().text = belongingText;
                UIController.instance.interactionPanel.transform.Find("BelongingImage").GetComponent<Image>().sprite = belongingImage;
                playerReference.isInInteraction = true;
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Checked");
                isChecked = true;
                playerReference.isInInteraction = false;
                OnCheck();
            }
        }
    }


}
