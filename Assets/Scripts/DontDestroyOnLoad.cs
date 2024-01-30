using System.Linq;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    // This script is used to keep the GameManager object alive between scenes
    void Awake()
    {
        Object.DontDestroyOnLoad(gameObject);
    }
}
