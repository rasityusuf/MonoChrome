
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public BlackBoard blackBoard = new BlackBoard();
    public int litCandleAmount = 0;
    
    private void Awake()
    {
        instance = this;
    }
    //change transfrom with room
    public List<Room> rooms;

    private void Update()
    {
        Debug.Log("Lit Candle Amount:" + litCandleAmount);
        if(litCandleAmount == 5)
        {
            UIController.instance.gameOverSecene.SetActive(true);
        }
    }

    public Room GetCurrentRoom()
    {
        for(int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].isPlayerInHere)
            {
                return rooms[i];
            }
        }
        return null;
    }

}
