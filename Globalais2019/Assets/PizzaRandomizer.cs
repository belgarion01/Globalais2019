using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PizzaRandomizer : MonoBehaviour
{

    public Sprite goodPizza;
    public Sprite pineapplePizza;

    public bool canRandomize;
    int pineappleSlices = 0;


    public Image[] mySlicesList;
    public List<int> randomNumber = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };

    private void Awake()
    {
        canRandomize = true;
    }


    public void RandomizePizza()
    {

        int pineappleSlices = Random.Range(1, 4);
        Debug.Log(pineappleSlices);
        int goodSlices = 8 - pineappleSlices;
        Debug.Log(goodSlices);
        int i = 0;
        int j;

        for (i = pineappleSlices; i != 0; i--)
        {
            int rIndex = Random.Range(0, randomNumber.Count);
            Debug.Log(rIndex + " = rIndex");
            int pickedNumber = randomNumber[rIndex];
            Debug.Log(pickedNumber + " = pickedNumber");
            mySlicesList[pickedNumber].sprite = pineapplePizza;
            randomNumber.RemoveAt(rIndex);
        }

        if(i==0)
        {
            for (j = goodSlices; j != 0; j--)
            {
                int rIndex = Random.Range(0, randomNumber.Count);
                int pickedNumber = randomNumber[rIndex];
                mySlicesList[pickedNumber].sprite = goodPizza;
                randomNumber.RemoveAt(rIndex);
            }
        }



    }

}
