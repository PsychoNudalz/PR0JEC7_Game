using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GemColour {Green,Blue,Red,Purple,Yellow};
public class GemCollection : MonoBehaviour
{
    private int gemsCollected;
    // Start is called before the first frame update
    void Start()
    {
        gemsCollected = 0;
    }

    public void collectGem(){
        gemsCollected ++;
    }

    public int GetGemsCollected(){
        return gemsCollected;
    }
}
