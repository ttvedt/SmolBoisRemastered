using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using boiClass;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private string sceneName;
    public int money = 1000;
    public GameObject boiPrefab;
    private int idCurrent = 0;
    private int population = 2;//0; //update to 0 when you remove the 2 starting bois
    [SerializeField]
    private int boisPerUnit = 50; //amount of bois that can be bred, does not apply to adoption center
    private int maxPopulation = -1; //increases with each room expansion
    private string[] prefixLines = System.IO.File.ReadAllLines("Assets/Resources/prefix.txt");
    private string[] nameLines = System.IO.File.ReadAllLines("Assets/Resources/names.txt");
    private string[] suffixLines = System.IO.File.ReadAllLines("Assets/Resources/suffix.txt");
    [SerializeField]
    private double prefixChance = 0.35;
    [SerializeField]
    private double nameChance = 0.98;
    [SerializeField]
    private double suffixChance = 0.45;
    private GameObject boiContainer;
    private GameObject baseGame;
    private bool pachinkoDrop = false;
    private bool isBaseGame = false;
    //private float dt;

    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("thereCanOnlyBeOne").Length > 1) Destroy(GameObject.FindGameObjectWithTag("thereCanOnlyBeOne"));
        boiContainer = GameObject.Find("Bois");
        baseGame = GameObject.Find("BaseGame");
        DontDestroyOnLoad(this.gameObject);
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        maxPopulation = boisPerUnit;
        //Debug.Log("GameManager.cs Start(): Game Start, " + sceneName);
        //Time.timeScale = 1.0f;
    }

    void Update()
    {
        //dt = Time.deltaTime;
        /*if (pachinkoTimer > 0f)
        {
            pachinkoTimer -= dt;
            if (pachinkoTimer <= 0f) { pachinkoTimer = 0f; Debug.Log("GameManager.cs Update(): pachinkoTimer = 0"); }
        }*/
        /*if (Input.GetKeyDown(KeyCode.Space) && sceneName == "BaseGame")//temporary thing to test spawning a boi
        {
            gene g1 = (gene)Random.Range(1, 10);
            gene g2 = (gene)Random.Range(1, 10);
            species s = (species)Random.Range(1, 4);
            Debug.Log("GameManager.cs Update(): space pressed");
            makeBoi("Space " + randomName(), s, g1, g2, 0, 0);
        }*/
    }

    // time delay after entering pachinko so the touch that presses the button doesn't also drop the boi
    public bool canPachinkoDrop() { return pachinkoDrop; }
    public void PachinkoStart() {
        pachinkoDrop = true;
        Debug.Log("GameManager.cs PachinkoStart()"); 
    }
    public void enterBaseGame() {
        isBaseGame = true;
        boiContainer.SetActive(true);
        baseGame.GetComponent<CameraControls>().enabled = true;
        pachinkoDrop = false; 
        Debug.Log("GameManager.cs enterBaseGame()");
        /*if (GameObject.Find("PachinkoBoimkII")==null || GameObject.Find("PachinkoBoimkII").GetComponent<Drop>().isDropped()) {
            Debug.Log("GameManager.cs PachinkoStop(): respawning"); GameObject.Find("SpawnPoint").GetComponent<Spawn>().spawnBall();}*/
    }
    public void leaveBaseGame() { 
        isBaseGame = false;
        boiContainer.SetActive(false); 
        baseGame.GetComponent<CameraControls>().enabled = false; 
    }

    //getters
    public bool getIsBaseGame() { return isBaseGame; }
    public string getSceneName() { return sceneName; }
    public int getPopulation() { return population; }
    public int getMaxPopulation() { return maxPopulation; }
    //public int claimID() { int temp = idCurrent; idCurrent++; return temp; }

    //setters
    public void setSceneName(string s) { sceneName = s; }
    public void populationSubtractOne() { population -= 1; }
    private void setMaxPopulation(int p) { maxPopulation = p; }
    //private void setMaxPopulation(float p) { maxPopulation = (int)p; }
    public void increaseMaxPopulation(){
        int numBois = maxPopulation;
        if (maxPopulation == boisPerUnit) {          numBois += boisPerUnit; }
        else if (maxPopulation == 2 * boisPerUnit) { numBois += 2 * boisPerUnit; }
        else if (maxPopulation == 4 * boisPerUnit) { numBois += 2 * boisPerUnit; }
        else if (maxPopulation == 6 * boisPerUnit) { numBois += 3 * boisPerUnit; }
        else { Debug.LogError("GameManager.cs increaseMaxPopulation(): cannot decrease max population further"); }
        setMaxPopulation(numBois);
    }
    public void decreaseMaxPopulation(){
        int numBois = maxPopulation;
        if (maxPopulation == 2 * boisPerUnit) {      numBois -= boisPerUnit; }
        else if (maxPopulation == 4 * boisPerUnit) { numBois -= 2 * boisPerUnit; }
        else if (maxPopulation == 6 * boisPerUnit) { numBois -= 2 * boisPerUnit; }
        else if (maxPopulation == 9 * boisPerUnit) { numBois -= 3 * boisPerUnit; }
        else { Debug.LogError("GameManager.cs increaseMaxPopulation(): cannot decrease max population further"); }
        setMaxPopulation(numBois);
    }
    public void changeMaxPopulation(int level){
        int numBois = maxPopulation;
        switch (level){
            case 0: numBois = boisPerUnit; break;
            case 1: numBois = 2*boisPerUnit; break;
            case 2: numBois = 4*boisPerUnit; break;
            case 3: numBois = 6*boisPerUnit; break;
            case 4: numBois = 9*boisPerUnit; break;
            default: Debug.LogWarning("GameManager.cs changeMaxPopulation(): Invalid level"); break;
        }
        setMaxPopulation(numBois);
    }

    //adoption center
    public void purchaseBois(){
        for (int i = 0; i < BoughtBois.numPurchased; i++){
            string name = BoughtBois.boughtBoi[i].getName();
            gene[] g = BoughtBois.boughtBoi[i].getGenes();
            species spec = BoughtBois.boughtBoi[i].getSpecies();
            Debug.Log("GameManager.cs purchaseBois(): " + BoughtBois.boughtBoi[i].getSpecies());
            try { makeBoi(name, spec, g[0], g[1], 0, 0); } catch { }
        }
        BoughtBois.numPurchased = 0;
    }

    //make new boi
    public void makeBoi(){ makeBoi(randomName(), species.Round, gene.none, gene.none, 0, 0); }
    public void makeBoi(species s, gene c1, gene c2){ makeBoi(randomName(), s, c1, c2, 0, 0); }
    public void makeBoi(species s, gene c1, gene c2, float x, float y){ makeBoi(randomName(), s, c1, c2, x, y); }
    public void makeBoi(string name, species s, gene c1, gene c2, float x, float y) {
        GameObject boi = (GameObject)Instantiate(boiPrefab, new Vector3(x, y, 0), transform.rotation, boiContainer.transform);
        idCurrent++;
        population++;
        boi.name = "boi #" + idCurrent;
        //Debug.Log("GameManager.cs makeBoi(): making boi #" + idCurrent + ", current population: " + population);
        boi.GetComponent<boiScript>().setBoi(name, s, c1, c2, idCurrent);
        //Debug.Log("GameManager.cs makeBoi(): made boi #" + idCurrent + ", current population: " + population);100*pop/mpop + "% capacity = " + pop +"/"+ mpop
        Debug.Log("GameManager.cs makeBoi(): made boi #" + idCurrent + ", current population at " + 100*population/maxPopulation + "% capacity: " + population+"/"+maxPopulation);
    }
    public string randomName(){
        string prefix = "";
        string name = "";
        string suffix = "";
        if (Random.value < prefixChance) { 
            prefix = prefixLines[Random.Range(0, prefixLines.Length)]; 
        }
        if (Random.value < nameChance) { 
            if (prefix != ""){
                name = " ";
            }
            name += nameLines[Random.Range(0, nameLines.Length)]; 
        }
        if (Random.value < suffixChance) { 
            if (name != ""){
                suffix = " ";
            } else if (prefix != ""){
                suffix = " ";
            }
            suffix += suffixLines[Random.Range(0, suffixLines.Length)]; 
        }
        string boiName = prefix + name + suffix;
        if (boiName == ""){ boiName = "Missingno"; }
        //Debug.Log("GameManager.cs randomName(): generated name = " + boiName);
        return boiName;
        //return "[random name #" + Random.Range(1, 1000) + "]";
    }
}

