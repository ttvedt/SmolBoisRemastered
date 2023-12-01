using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using boiClass;

namespace randomizedBoi
{
    public class RandomizeBoi : MonoBehaviour
    {
        private GameManager gameManager;
        public smolBoi thisboi;
        public boiScript bs;
        public int price;
        public bool center;
        public bool off = false;
        public GameObject me;
        // Start is called before the first frame update
        void Start()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            thisboi = new smolBoi();
            randomizeGenes();
            
            bs = GetComponent<boiScript>();
            if (BoiBucks.boiBucks > 0)
            {
                bs.setBoi(thisboi.getName(), thisboi.getSpecies(), thisboi.getGenes()[0], thisboi.getGenes()[0]);
            } else if (center)
            {
                bs.setBoi(thisboi.getName(), (species)0, thisboi.getGenes()[0], thisboi.getGenes()[0]);
                price = 0;
            }
            else
            {
                Destroy(gameObject);
            }
            //bs.Spec = s;
            //string n, species s, gene c1, gene c2
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void randomizeGenes()
        { //Range(0, 10) will return a value between 0 and 9
            me.SetActive(true);
            gene g1 = (gene)Random.Range(1, 10);
            gene g2 = (gene)Random.Range(1, 10);
            species s = (species)Random.Range(1, 4); // 1, 2, or 3
            //Debug.Log("RandomizeBoi.cs randomizeGenes(): Color is " + g1 + ", Species is " + s);
            thisboi.setColor(g1, g1);
            thisboi.setSpecies(s);
            thisboi.setName(gameManager.randomName());
            bs.setBoi(thisboi.getName(), thisboi.getSpecies(), thisboi.getGenes()[0], thisboi.getGenes()[0]);
            off = false;
            //Debug.Log("colors set to " + g1 + " " + g1 + " Species:" + s);
        }
    }
}
