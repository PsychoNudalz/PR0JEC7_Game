using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GemColour {Green,Red,Blue,Purple,Yellow};
public class GemCollection : MonoBehaviour
{
    private List<GemColour> coloursCollected;
    // Start is called before the first frame update
    void Start()
    {
        coloursCollected = new List<GemColour>();
    }

    public void CollectGem(GemColour colour){
        coloursCollected.Add(colour);
    }

    public bool CheckIfColourCollected(GemColour colour){
        return coloursCollected.Contains(colour);
    }
    public int GetGemsCollected(){
        return coloursCollected.Count;
    }
}
