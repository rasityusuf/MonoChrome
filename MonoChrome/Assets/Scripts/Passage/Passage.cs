using UnityEngine;

public class Passage : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerController>(out PlayerController controller))
        {
            if (controller.roomCollisions.Count == 1)
            {
                if (controller.silhoutte != null)
                {
                    
                    if (controller.GetComponent<SpriteRenderer>().color != Color.black)
                    {
                        GetComponent<Collider2D>().isTrigger = false;
                    }
                    else
                    {
                        GetComponent<Collider2D>().isTrigger = true;
                    }
                }
                else
                {

                    GetComponent<Collider2D>().isTrigger = true;
                }
            }

        }
              
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController controller))
        {
            GetComponent<Collider2D>().isTrigger = true;
        }
       
    }
}
