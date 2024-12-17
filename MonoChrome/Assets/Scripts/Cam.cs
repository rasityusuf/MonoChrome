using Cinemachine;
using UnityEngine;

public class Cam : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cam;
    void Start()
    {
        CinemachineBasicMultiChannelPerlin channel = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        GameManager.instance.blackBoard.SetValue("Channel", channel);
    }

  
}
