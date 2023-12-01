using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// do not attatch this script to anything
namespace boiClass
{
    public class smolBoi
    {
        private int ID;
        private species Species;
        private string Name;
        private gene[] Color = new gene[2];
        private needs Needs = new needs();
        private float[] Position = new float[2];
        private float Age = 0;
        private int maxNeeds;

        // constructors
        public smolBoi(species s, string n, gene a, gene b, float posx, float posy, int m)
        {
            ID = -200;
            Species = s;
            Name = n;
            Color[0] = a;
            Color[1] = b;
            Position[0] = posx;
            Position[1] = posy;
            maxNeeds = m;
            Needs.food = maxNeeds;
            Needs.water = maxNeeds;
            Needs.entertainment = maxNeeds;
        }
        public smolBoi(species s, string n, gene a, gene b, float posx, float posy, int m, int newID)
        {
            ID = newID;
            Species = s;
            Name = n;
            Color[0] = a;
            Color[1] = b;
            Position[0] = posx;
            Position[1] = posy;
            maxNeeds = m;
            Needs.food = maxNeeds;
            Needs.water = maxNeeds;
            Needs.entertainment = maxNeeds;
        }

        // default constructor
        public smolBoi()
        {
            ID = -100;
            Species = species.Round;
            Name = "[unnamed boi]";
            Color[0] = gene.none;
            Color[1] = gene.none;
            Position[0] = 0;
            Position[1] = 0;
            maxNeeds = 100000;
            Needs.food = maxNeeds;
            Needs.water = maxNeeds;
            Needs.entertainment = maxNeeds;
        }

        //getters
        public species getSpecies() { return Species; }
        public string getName() { return Name; }
        public float getAge() { return Age; }
        public float getX() { return Position[0]; }
        public float getY() { return Position[1]; }
        public float[] getPos() { return Position; }
        public float getFood() { return Needs.food; }
        public float getWater() { return Needs.water; }
        public float getEntertainment() { return Needs.entertainment; }
        public gene[] getGenes() { return Color; }
        public int getPallit(){
            int pallit = 0;
            gene a = Color[0];
            gene b = Color[1];
            if (a>b) { //put them in order
                gene temp = a;
                a=b;
                b=temp;
            }
            if (a==gene.none || b==gene.none) {
                return 1; // if either gene is set to none, display RGBYCM colors
            }
            switch (a) {
                case gene.red:
                    switch (b){
                        case gene.red: pallit = 2; break;
                        case gene.orange: pallit = 11; break;
                        case gene.yellow: pallit = 12; break;
                        case gene.green: pallit = 13; break;
                        case gene.cyan: pallit = 14; break;
                        case gene.blue: pallit = 15; break;
                        case gene.magenta: pallit = 16; break;
                        case gene.black: pallit = 17; break;
                        case gene.white: pallit = 18; break;
                        default: Debug.LogWarning("smolBoi.cs getPallit(): Invalid gene for Boi #" + ID + ": " + Name); break;}
                    break;
                case gene.orange:
                    switch (b){
                        case gene.orange: pallit = 3; break;
                        case gene.yellow: pallit = 19; break;
                        case gene.green: pallit = 20; break;
                        case gene.cyan: pallit = 21; break;
                        case gene.blue: pallit = 22; break;
                        case gene.magenta: pallit = 23; break;
                        case gene.black: pallit = 24; break;
                        case gene.white: pallit = 25; break;
                        default: Debug.LogWarning("smolBoi.cs getPallit(): Invalid gene for Boi #" + ID + ": " + Name); break;}
                    break;
                case gene.yellow:
                    switch (b){
                        case gene.yellow: pallit = 4; break;
                        case gene.green: pallit = 26; break;
                        case gene.cyan: pallit = 27; break;
                        case gene.blue: pallit = 28; break;
                        case gene.magenta: pallit = 29; break;
                        case gene.black: pallit = 30; break;
                        case gene.white: pallit = 31; break;
                        default: Debug.LogWarning("smolBoi.cs getPallit(): Invalid gene for Boi #" + ID + ": " + Name); break;}
                    break;
                case gene.green:
                    switch (b){
                        case gene.green: pallit = 5; break;
                        case gene.cyan: pallit = 32; break;
                        case gene.blue: pallit = 33; break;
                        case gene.magenta: pallit = 34; break;
                        case gene.black: pallit = 35; break;
                        case gene.white: pallit = 36; break;
                        default: Debug.LogWarning("smolBoi.cs getPallit(): Invalid gene for Boi #" + ID + ": " + Name); break;}
                    break;
                case gene.cyan:
                    switch (b){
                        case gene.cyan: pallit = 6; break;
                        case gene.blue: pallit = 37; break;
                        case gene.magenta: pallit = 38; break;
                        case gene.black: pallit = 39; break;
                        case gene.white: pallit = 40; break;
                        default: Debug.LogWarning("smolBoi.cs getPallit(): Invalid gene for Boi #" + ID + ": " + Name); break;}
                    break;
                case gene.blue:
                    switch (b){
                        case gene.blue: pallit = 7; break;
                        case gene.magenta: pallit = 41; break;
                        case gene.black: pallit = 42; break;
                        case gene.white: pallit = 43; break;
                        default: Debug.LogWarning("smolBoi.cs getPallit(): Invalid gene for Boi #" + ID + ": " + Name); break;}
                    break;
                case gene.magenta:
                    switch (b){
                        case gene.magenta: pallit = 8; break;
                        case gene.black: pallit = 44; break;
                        case gene.white: pallit = 45; break;
                        default: Debug.LogWarning("smolBoi.cs getPallit(): Invalid gene for Boi #" + ID + ": " + Name); break;}
                    break;
                case gene.black:
                    switch (b){
                        case gene.black: pallit = 9; break;
                        case gene.white: pallit = 46; break;
                        default: Debug.LogWarning("smolBoi.cs getPallit(): Invalid gene for Boi #" + ID + ": " + Name); break;}
                    break;
                case gene.white:
                    switch (b){
                        case gene.white: pallit = 10; break;
                        default: Debug.LogWarning("smolBoi.cs getPallit(): Invalid gene for Boi #" + ID + ": " + Name); break;}
                    break;
                default:
                    Debug.LogWarning("smolBoi.cs getPallit(): Invalid gene for Boi #" + ID + ": " + Name);
                    break;
            }
            return pallit;
        }

        //setters
        public void setName(string n) { Name = n; }
        public void setAge(float a) { Age = a; }
        public void setSpecies(species s) { Species = s; }
        public void setPosition(float x, float y)
        {
            Position[0] = x;
            Position[1] = y;
        }
        public void setColor(gene a, gene b)
        {
            Color[0] = a;
            Color[1] = b;
        }
        public void setFood(int f) { Needs.food = f; }
        public void setWater(int w) { Needs.water = w; }
        public void setEntertainment(int e) { Needs.entertainment = e; }

        //increment needs
        public void incrementFood(int f) { Needs.food += f; }
        public void incrementWater(int w) { Needs.water += w; }
        public void incrementEntertainment(int e) { Needs.entertainment += e; }

        //print
        public void printBoi()
        {
            if (Color[0] == Color[1]) { 
                Debug.Log("smolBoi.cs printBoi(): Printing Boi #" + ID + ": " + Name + " is a solid " + Color[0] + " " + Species);
            }
            else{
                Debug.Log("smolBoi.cs printBoi(): Printing Boi #" + ID + ": " + Name + " is a " + Color[0] + " & " + Color[1] + " " + Species);
            }
                       //+ " at position (" + Position[0] + ", " + Position[1] + ").");
        }
        public void printNeeds()
        {
            Debug.Log("smolBoi.cs printNeeds(): Printing Needs for Boi #" + ID + ": " + Name + " has " + Needs.food*100/maxNeeds + "% Food, " +
                 Needs.water*100/maxNeeds + "% Water, and " + Needs.entertainment*100/maxNeeds + "% Entertainment.");
        }
    }

    public enum species //custom data type
    {
        Round,  //0
        Birb,   //1
        Cat,    //2
        Hamst    //3
    }

    public enum gene //custom data type
    {//  0    1    2       3       4      5     6     7        8      9
        none, red, orange, yellow, green, cyan, blue, magenta, black, white
    }

    public struct needs //struct is like class but simpler
    {
        public int food;
        public int water;
        public int entertainment;
    }
}