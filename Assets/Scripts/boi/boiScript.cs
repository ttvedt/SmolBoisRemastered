using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using boiClass;
using UnityEngine.SceneManagement;

// attatch this script to the smol boi
public class boiScript : MonoBehaviour
{
    private GameManager gameManager;
    string sceneName;
    public smolBoi thisBoi;
    [SerializeField]
    private string Name = "[name of boi]";
    [SerializeField]
    private gene color1 = gene.none;
    [SerializeField]
    private gene color2 = gene.none;
    [SerializeField]
    private species spec = species.Round;
    [SerializeField]
    private int id = -300;
    private int loseFood = 2;
    private int loseWater = 3;
    private int loseEntertainment = 1;
    private int maxNeeds = 100000;
    private int happyEnough = 0; // this is how filled the needs have to be for the boi to be able to breed
    [SerializeField]
    private float breedCooldown = -6.0f; // can't breed for (waitTime - breedCooldown) seconds after spawning in
    [SerializeField]
    private float waitTime = 10.0f; // (waitTime) seconds
    [SerializeField]
    private bool justBred = true;
    //private bool message = true;
    private Animator animator;
    private Material[] materials;
    private RuntimeAnimatorController[] animators;
    private float dt;

    public static int food = 100000;
    public static int water = 100000;
    public static int entertainment = 100000;
    public float speed = 100;
    public bool move = false;
    public Vector3 targetPosition;
    public Vector3 randomMovement;
    System.Random rnd = new System.Random();
    public int boiPallit = 0;
    public SetSpritePallitNumber pallitScript;
    public int shaderColor;
    public ReplaceBowl replaceBowlScript;
    private static string saveFood;
    private static string saveWater;
    private static string saveEntertain;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Time.timeScale = 1.0f;
        happyEnough = maxNeeds / 3; // needs have to be at least a third full for the boi to be able to breed
        pallitScript = GetComponent<SetSpritePallitNumber>();

        materials = new Material[] { 
            Resources.Load("Round A Mat", typeof(Material)) as Material,
            Resources.Load("Bird A Mat", typeof(Material)) as Material,
            Resources.Load("Cat A Mat", typeof(Material)) as Material, 
            Resources.Load("Ham A Mat", typeof(Material)) as Material };
        animators = new RuntimeAnimatorController[] {
            Resources.Load("boiRound", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController,
            Resources.Load("boiBird", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController,
            Resources.Load("boiCat", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController,
            Resources.Load("boiHam", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController };
        setRender();
        animator = GetComponent<Animator>();
        updateNeeds();
        food = PlayerPrefs.GetInt(saveFood);
        water = PlayerPrefs.GetInt(saveWater);
        entertainment = PlayerPrefs.GetInt(saveEntertain);

        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        thisBoi = new smolBoi(spec, Name, color1, color2, 0, 0, maxNeeds, id);
        thisBoi.printBoi();

        if (GameObject.FindGameObjectWithTag("bowlSpawnPoint")){ replaceBowlScript = GameObject.FindGameObjectWithTag("bowlSpawnPoint").GetComponent<ReplaceBowl>(); }

        thisBoi.setFood((int)food);
        thisBoi.setWater((int)water);
        thisBoi.setEntertainment((int)entertainment);

        boiPallit = thisBoi.getPallit();
        pallitScript.SetColor(boiPallit);

        saveFood = "food" + id;
        saveWater = "water" + id;
        saveEntertain = "entertain" + id;

        thisBoi.setPosition(0, -1);
    }

    void Update()
    {
        dt = Time.deltaTime;
        if (sceneName == "BaseGame"){
            if (justBred) {
                breedCooldown += dt;
                if (breedCooldown >= waitTime){
                    breedCooldown = breedCooldown - waitTime;
                    justBred = false;
                }
            }
            //thisBoi.printNeeds();
            int f = (int)Mathf.Round(-loseFood * dt * 100);
            int w = (int)Mathf.Round(-loseWater * dt * 100);
            int e = (int)Mathf.Round(-loseEntertainment * dt * 100);
            thisBoi.incrementFood(f);
            thisBoi.incrementWater(w);
            thisBoi.incrementEntertainment(e);
            food = (int)thisBoi.getFood();
            water = (int)thisBoi.getWater();
            entertainment = (int)thisBoi.getEntertainment();

            AverageNeeds.totalFood[Mathf.Abs(id)] = food;
            AverageNeeds.totalWater[Mathf.Abs(id)] = water;
            AverageNeeds.totalEntertain[Mathf.Abs(id)] = entertainment;
            updateNeeds();
        }
        if (thisBoi==null){
            Debug.LogError("boiScript.cs Update(): null thisBoi: " + getMe());
        }
        else if (thisBoi.getFood() <= 0 || thisBoi.getWater() <= 0 || thisBoi.getEntertainment() <= 0)        {
            gameManager.populationSubtractOne();
            Debug.Log("boiScript.cs Update(): " + getMe() + " Destroid, population is now " + gameManager.getPopulation());
            Destroy(gameObject);
        }
    }

    public smolBoi getBoi() { return thisBoi; }
    public void setName(string n) { Name = n; thisBoi.setName(Name); }
    public void setBoi(string n, species s, gene c1, gene c2){ setBoi(n, s, c1, c2, -400); }
    public void setBoi(string n, species s, gene c1, gene c2, int idNew){
        Name = n;
        color1 = c1;
        color2 = c2;
        spec = s;
        id = idNew;
        thisBoi = new smolBoi(s, n, c1, c2, 0, 0, maxNeeds, idNew);
        setRender();
        try {
            boiPallit = thisBoi.getPallit();
            pallitScript.SetColor(boiPallit);
        } catch { }
    }
    private void setRender(){int s = -1;
        switch (spec){
            case species.Round: s = 0; break;
            case species.Birb: s = 1; break;
            case species.Cat: s = 2; break;
            case species.Hamst: s = 3; break;
            default: Debug.LogWarning("boiScript.cs setRender(): " + getMe() + " Invalid species"); s = 0; break;
        }
        try { renderHelper(s); } catch { }
    }
    private void renderHelper(int n){ GetComponent<Renderer>().material = materials[n]; GetComponent<Animator>().runtimeAnimatorController = animators[n]; }
    
    public bool canBreed(){
        bool result = false;
        if(spec!=species.Round){ result=(gameManager.getIsBaseGame() && !justBred && 
                (int)thisBoi.getFood() > happyEnough && (int)thisBoi.getWater() > happyEnough && (int)thisBoi.getEntertainment() > happyEnough);}
        return result;
    }

    public string getMe() { return "boi #" + id + " (" + Name + ")"; }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("FullFoodBowl") && sceneName == "BaseGame")
        {
            if (FoodBowl.FoodSource > 9999)
            {
                thisBoi.setFood(maxNeeds);
                food = (int)thisBoi.getFood();
                updateNeeds();
            }
        }
        if (other.CompareTag("FullWaterBowl") && sceneName == "BaseGame")
        {
            if (WaterBowl.WaterSource > 9999)
            {
                thisBoi.setWater(maxNeeds);
                water = (int)thisBoi.getWater();
                updateNeeds();
            }
        }
        if (other.CompareTag("furn") && sceneName == "BaseGame")
        {
            thisBoi.setEntertainment(maxNeeds);
            entertainment = (int)thisBoi.getEntertainment();
            updateNeeds();
        }
        if (other.CompareTag("boi") && sceneName == "BaseGame"){ // if this boi collides with another boi
            smolBoi otherBoi = other.GetComponent<boiScript>().getBoi();
            string otherName = other.GetComponent<boiScript>().getMe();
            bool canMakeChild = spec!=species.Round && canBreed() && other.GetComponent<boiScript>().canBreed();
            // if both bois can breed
            if (canMakeChild){
                //Debug.Log("boiScript.cs OnCollisionEnter2D(): NEW CHILD " + getMe() + " & " + otherName);
                justBred = true; // Update() will handle breedCooldown
                Vector2 pos = collision.contacts[0].point; // position at point of contact
                // inherit genes
                species gene0;
                gene gene1, gene2;
                int num0 = Random.Range(0, 2);
                int num1 = Random.Range(0, 4);
                int num2 = Random.Range(0, 4);
                switch (num0) {
                    case 0: gene0 = spec; break;
                    case 1: gene0 = otherBoi.getSpecies(); break;
                    default: gene0 = species.Round; break; }
                switch (num1) {
                    case 0: gene1 = color1; break;
                    case 1: gene1 = color2; break;
                    case 2: gene1 = otherBoi.getGenes()[0]; break;
                    case 3: gene1 = otherBoi.getGenes()[1]; break;
                    default: gene1 = gene.none; break; }
                switch (num2) {
                    case 0: gene2 = color1; break;
                    case 1: gene2 = color2; break;
                    case 2: gene2 = otherBoi.getGenes()[0]; break;
                    case 3: gene2 = otherBoi.getGenes()[1]; break;
                    default: gene2 = gene.none; break; }
                other.GetComponent<boiScript>().tryBreed(gene0, gene1, gene2, (float)pos[0], (float)pos[1]);
            }
        }
    }

    public void tryBreed(species s, gene c1, gene c2, float x, float y){
        justBred = true;
        float pop = gameManager.getPopulation();
        float mpop = gameManager.getMaxPopulation();
        if (Random.value > pop/mpop) {
            //Debug.Log("boiScript.cs tryBreed(): " + getMe() + " MADE CHILD, population = " + pop +"/"+ mpop);
            gameManager.makeBoi(s, c1, c2, x, y); 
        }
        else { Debug.Log("boiScript.cs tryBreed(): " + getMe() + " does not make child because of overcrowding, " + 100*pop/mpop + "% capacity = " + pop +"/"+ mpop); }
    }

    public void feed()
    {
        thisBoi.setFood(maxNeeds);
        food = (int)thisBoi.getFood();
        updateNeeds();
    }
    public void thirst()
    {
        thisBoi.setWater(maxNeeds);
        water = (int)thisBoi.getWater();
        updateNeeds();
    }

    public static void updateNeeds()
    {
        PlayerPrefs.SetInt(saveFood, food);
        food = PlayerPrefs.GetInt(saveFood);
        PlayerPrefs.SetInt(saveWater, water);
        water = PlayerPrefs.GetInt(saveWater);
        PlayerPrefs.SetInt(saveEntertain, entertainment);
        entertainment = PlayerPrefs.GetInt(saveEntertain);
        PlayerPrefs.Save();
    }
}
