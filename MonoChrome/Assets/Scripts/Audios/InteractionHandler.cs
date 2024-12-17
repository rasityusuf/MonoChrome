using System.Threading;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    public enum TagType { Door, Stair, Drawr, Book, Chest, Table, Key, Carpet, Wood}
    private TagType currentTag;
    public bool isInTrigger = false;
    private Collider2D currentCollider;

    private void Start()
    {
        currentTag = (TagType)System.Enum.Parse(typeof(TagType), gameObject.tag);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentCollider != null)
        {
            Debug.Log("E tuþuna basýldý");
            audioTagListeningKeycode(currentCollider);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        isInTrigger = true;
        currentCollider = other;
        audioTagListeningTrigger(currentCollider);
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        audioTagListeningToFollow(currentCollider);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        isInTrigger = false;
        currentCollider = null;
    }



    void audioTagListeningTrigger(Collider2D other)
    {
        TagType detectedTag = (TagType)System.Enum.Parse(typeof(TagType), other.tag);   
        switch (detectedTag)
        {
            case TagType.Door:
                Debug.Log("Door is open");
                AudioManager.Instance.PlaySFX(AudioManager.Instance.audioClips[3]);  
                break;
        }
    }
    
    void audioTagListeningKeycode(Collider2D other)
    {
        TagType detectedTag = (TagType)System.Enum.Parse(typeof(TagType), other.tag);
        switch (detectedTag)
        {
            case TagType.Stair:
                Debug.Log("Stair ile etkileþim");
                AudioManager.Instance.PlaySFX(AudioManager.Instance.audioClips[8]);
                break;
            case TagType.Drawr:
                Debug.Log("Drawr ile etkileþim");
                AudioManager.Instance.PlaySFX(AudioManager.Instance.audioClips[4]);
                break;
            case TagType.Book:
                Debug.Log("Book ile etkileþim");
                AudioManager.Instance.PlaySFX(AudioManager.Instance.audioClips[0]);
                break;
            case TagType.Chest:
                Debug.Log("Chest ile etkileþim");
                AudioManager.Instance.PlaySFX(AudioManager.Instance.audioClips[1]);
                break;
            case TagType.Table:
                Debug.Log("Table ile etkileþim");
                AudioManager.Instance.PlaySFX(AudioManager.Instance.audioClips[5]);
                break;
                
            case TagType.Key:
                Debug.Log("Key ile etkileþim");
                AudioManager.Instance.PlaySFX(AudioManager.Instance.audioClips[3]);
                break;
                
            default:
                Debug.Log("UnknownColliderTag");
                break;
        }
    }

    void audioTagListeningToFollow(Collider2D other)
    {
        TagType detectedTag = (TagType)System.Enum.Parse(typeof(TagType), other.tag);
        switch (detectedTag)
        {
            case TagType.Carpet:
                Debug.Log("CarpetGround");
                AudioManager.Instance.PlayWalk(AudioManager.Instance.audioClips[7]);
                break;
            case TagType.Wood:
                Debug.Log("WoodGround");
                AudioManager.Instance.PlayWalk(AudioManager.Instance.audioClips[6]);
                break;
            default:
                Debug.Log("UnknownColliderTag");
                break;
        }
    }
}
