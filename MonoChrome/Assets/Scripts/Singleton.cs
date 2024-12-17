using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingleTon<T> : MonoBehaviour where T : Component
{
    public static T instance;

    public static bool HasInstance() => instance != null;

    public static T GetInstance() => HasInstance() ? instance : null;

    public static T Instance
    {

        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<T>();
                if (instance != null)
                {
                    return instance;
                }
                else
                {
                    GameObject singletonObj = new GameObject(nameof(T));
                    instance = singletonObj.AddComponent<T>();
                    return instance;
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this as T;
            if(SceneManager.GetActiveScene().buildIndex == 0)
                DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
        
    }

}
