
using System.Collections;
using UnityEngine;
public abstract class Room : MonoBehaviour
{
    public float hitBackMagnitude = 15f;
    public Enemy levelEnemyReference;
    [SerializeField] private Fake _silhoutte;
    public bool isPlayerInHere = false;
    float duration = 0.3f;
    public virtual void SetPlayerColorToBlack(PlayerController controller)
    {   
        if(controller.silhoutte == null)
        {
            controller.GetComponent<SpriteRenderer>().color = Color.black;
            Fake fake = Instantiate(_silhoutte, controller.transform.position, Quaternion.identity);
            controller.silhoutte = fake.transform;
            fake.transform.localScale = new Vector2(controller.transform.localScale.x,1);
            Debug.Log("PlayerColorBlack");
            //StartCoroutine(AddForce(fake,controller));
        }
        
    }

    public virtual void SetPlayerColorToWhite(PlayerController controller)
    {
        //controller.transform.position = controller.silhoutte.position;
        Destroy(controller.silhoutte.gameObject);
        controller.silhoutte = null;
        isPlayerInHere = false;
        controller.GetComponent<SpriteRenderer>().color = Color.white;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            levelEnemyReference = enemy;
        }
    }

    private IEnumerator AddForce(Fake fake,PlayerController controller)
    {
        Debug.Log("Force init");    
        fake.rb. AddForce(new Vector2(controller.isFacingRight ? -1 * hitBackMagnitude : hitBackMagnitude, fake.rb.linearVelocity.y),ForceMode2D.Impulse);
        yield return new WaitForSeconds(duration);
        fake.rb.linearVelocity = Vector2.zero;
    }

}
