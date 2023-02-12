using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://blog.yarsalabs.com/using-singletons-in-unity/#creating-and-using-generic-singleton

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    
    public static T Instance => _instance;

    protected virtual void Awake()
    {
        var instance = FindObjectOfType<T>();

        if (_instance != null && _instance != instance)
        {
            Destroy(instance.gameObject);
            return;
        }
        _instance = instance;

        DontDestroyOnLoad(gameObject);
    }
}
