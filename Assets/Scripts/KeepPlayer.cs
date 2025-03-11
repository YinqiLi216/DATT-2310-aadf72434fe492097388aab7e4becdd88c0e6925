using UnityEngine;

public class KeepPlayer : MonoBehaviour
{
   private void Awake()
{
    DontDestroyOnLoad(gameObject);
    Debug.Log("Player Awake called. " + gameObject.name);
}
}


