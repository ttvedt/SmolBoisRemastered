using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AverageNeeds : MonoBehaviour
{
    public static int[] totalFood;
    public static int[] totalWater;
    public static int[] totalEntertain;
    private float food;
    private float water;
    private float entertain;
    private float count;
    public GameObject foodBar;
    public GameObject waterBar;
    public GameObject entertainBar;
    private float averageFood;
    private float averageWater;
    private float averageEntertain;
    Vector3 foodScale;
    Vector3 waterScale;
    Vector3 entertainScale;

    // Start is called before the first frame update
    void Start()
    {
        foodScale = foodBar.transform.localScale;
        waterScale = waterBar.transform.localScale;
        entertainScale = entertainBar.transform.localScale;

        totalFood = new int[2000];
        totalWater = new int[2000];
        totalEntertain = new int[2000];
    }

    // Update is called once per frame
    void Update()
    {
        food = 0;
        water = 0;
        entertain = 0;
        count = 0;
        for (int i = 0; i < 2000; i++)
            if (totalFood[i] != 0)
            {
                food += totalFood[i];
                count++;
            }

        for (int i = 0; i < 2000; i++)
            if (totalWater[i] != 0)
                water += totalWater[i];

        for (int i = 0; i < 2000; i++)
            if (totalEntertain[i] != 0)
                entertain += totalEntertain[i];

        averageFood = food / count / 1000 / 100;
        averageWater = water / count /1000 / 100;
        averageEntertain = entertain / count / 1000 / 100;
        foodScale.Set(1, averageFood, 1);
        waterScale.Set(1, averageWater, 1);
        entertainScale.Set(1, averageEntertain, 1);

        foodBar.transform.localScale = foodScale;
        waterBar.transform.localScale = waterScale;
        entertainBar.transform.localScale = entertainScale;
    }
}
