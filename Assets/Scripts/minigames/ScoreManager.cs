using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text buckTotal;

    //change the bucks to pass in the players amount of bucks later
    public int bucks = 1000;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
        
    }
    void Start()
    {
        //buckTotal.text = bucks.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void gamble()
    {
        BoiBucks.boiBucks -= 200;
        BoiBucks.updateBoiBucks();
        PointDisplay.points -= 200;

    }

    public void gain100()
    {
        BoiBucks.boiBucks += 100;
        BoiBucks.updateBoiBucks();
        PointDisplay.points += 100;
    }
    public void gain200()
    {
        BoiBucks.boiBucks += 200;
        BoiBucks.updateBoiBucks();
        PointDisplay.points += 200;
    }
    public void gain300()
    {
        BoiBucks.boiBucks += 300;
        BoiBucks.updateBoiBucks();
        PointDisplay.points += 300;
    }
    public void gain500()
    {
        BoiBucks.boiBucks += 500;
        BoiBucks.updateBoiBucks();
        PointDisplay.points += 500;
    }
}



//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class ScoreManager : MonoBehaviour
//{
//    public static ScoreManager instance;
//    public Text buckTotal;

//    //change the bucks to pass in the players amount of bucks later
//    public int bucks = 1000;
//    // Start is called before the first frame update

//    private void Awake()
//    {
//        instance = this;
//    }
//    void Start()
//    {
//        buckTotal.text = bucks.ToString();
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//    public void gamble()
//    {
//        bucks -= 200;
//        buckTotal.text = bucks.ToString();
//    }

//    public void gain100()
//    {
//        bucks += 100;
//        buckTotal.text = bucks.ToString();
//    }
//    public void gain200()
//    {
//        bucks += 200;
//        buckTotal.text = bucks.ToString();
//    }
//    public void gain300()
//    {
//        bucks += 300;
//        buckTotal.text = bucks.ToString();
//    }
//    public void gain500()
//    {
//        bucks += 500;
//        buckTotal.text = bucks.ToString();
//    }
//}
