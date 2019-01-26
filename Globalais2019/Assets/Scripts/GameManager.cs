using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float moneyCount;
    public float moneyGain;

    private float soifLevel;
    public float soifDiminution;
    public float soifGain;

    private float mangerLevel;
    public float mangerDiminution;
    public float mangerGain;

    private float pipiLevel;
    public float pipiDiminution;
    public float pipiGain;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetMoney() {
        moneyCount += moneyGain;
    }

    public void Drinking() {
        soifLevel += soifGain;
    }

    public void Pissing() {
        pipiLevel -= pipiDiminution;
    }

    public void Eating() {
        mangerLevel += mangerGain;
    }
}
