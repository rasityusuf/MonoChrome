using UnityEngine;

public class HitTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController controller))
        {
            Debug.Log("Collided");
            if (controller.isInCollisionWithDrawer)
            {
                UIController.instance.candleDrawer.SetActive(false);
                UIController.instance.candleUI.SetActive(true);
                controller.isInInteraction = false;
            }
            controller.OnCollideWithEnemy(GetComponentInParent<Enemy>());
            
            if (controller.currentCollidedEnemy != null)
            {
                Debug.Log("Enemy not null");
            }
        }
    }
}
