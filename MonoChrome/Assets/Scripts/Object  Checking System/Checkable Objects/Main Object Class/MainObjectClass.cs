using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Belonging : MonoBehaviour, ICheckable
{
    public Sprite belongingImage;
    public string belongingText;
    protected PlayerController playerReference => 
        GameManager.instance.blackBoard.GetValue("PlayerController",out PlayerController _playerController) ? _playerController : null;

    [Header("Conditions")]
    public bool inCollision;
    public bool isChecked;
    public virtual void OnEnterCheck()
    {
        Debug.Log("OnEnterCheck");
        if (!isChecked)
        {
            UIController.instance.indicator.gameObject.SetActive(true);
        }
       
        inCollision = true;
    }

    public virtual void OnLeaveCheck()
    {
        Debug.Log("OnLeaveCheck");
        if (!isChecked)
        {
            UIController.instance.indicator.gameObject.SetActive(false);
            inCollision = false;
        }
       
      
    }

    public virtual void OnCheck()
    {
        UIController.instance.indicator.gameObject.SetActive(false);
        UIController.instance.interactionPanel.gameObject.SetActive(false);
    }
}
