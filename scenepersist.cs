using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scenepersist : MonoBehaviour
{
    private void Awake()
    {
        int Noscenepersists = FindObjectsOfType<scenepersist>().Length;
        if(Noscenepersists>1)
        {
            Destroy(gameObject);
        }
        else
        {
              DontDestroyOnLoad(gameObject);
        }
    }
    public void resetscenepersist()
    {
        Destroy(gameObject);
    }
}
