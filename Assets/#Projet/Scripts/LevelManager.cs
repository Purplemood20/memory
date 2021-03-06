using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;          // using pour unity event

public class LevelManager : MonoBehaviour
{
    private int row;  
    private int column;

    private float seconds;

    public float gapRow = 1.5f;
    public float gapColumn = 1.5f;

    [Range(0f,5f)]                // fait un niveau "echelle"
    public float timeBeforeReset = 1f;

    private bool resetOnGoing = false;

    public GameObject itemPrefab;

    public Material[] materials;
    public Material defaultMaterial;

    public ItemBehavior[] items;   // on d?clare un tableau d'objet qui prend moins de place que la liste et ne varie pas avec le temps dc moins utile de faire avec liste

    public List<int> selected = new List<int>();
    public List<int> matches = new List<int>();  // pas besoin de les mettre en public juste pour voir qu elle se remplissent

    private Dictionary<int, Material> itemMaterial = new Dictionary<int, Material>();

    public UnityEvent WhenPlayerWins;    // creation unity event

    // Start is called before the first frame update


    void Start()
    {
        row = PlayerPrefs.GetInt("row", 3); // 3 est une valeur par defaut
        column = PlayerPrefs.GetInt("column", 4);
        items = new ItemBehavior[row * column];
        int index = 0;
        for(int x=0 ; x<column; x++)
        {
            for (int z=0; z<row; z++)
            {
                Vector3 position = new Vector3(x * gapColumn, 0, z * gapRow);
                GameObject item = Instantiate(itemPrefab, position, Quaternion.identity);
                item.GetComponent<Renderer>().material = defaultMaterial;
                
                items[index] = item.GetComponent<ItemBehavior>();               

                items[index].id = index;
                items[index].manager = this;

                index++;
            }
        }
        GiveMaterials();
               
    }

    

    private void GiveMaterials()
    {
        List<int> possibilities = new List<int>();
        for (int i = 0; i < row * column; i++)           // foreach non car prend bcp de place ds la m?moire surtt jeu video -> attention au FPS(frame par sec de l'application)
        {
            possibilities.Add(i);
        }

        for(int i=0; i< materials.Length; i++)
        {
            if (possibilities.Count < 2) break;   // s?curit? si declarer plus de materiaux que d'objet
            int idPoss = Random.Range(0, possibilities.Count);
            int id1 = possibilities[idPoss];
            possibilities.RemoveAt(idPoss);

            idPoss = Random.Range(0, possibilities.Count);
            int id2 = possibilities[idPoss];
            possibilities.RemoveAt(idPoss);

            itemMaterial.Add(id1, materials[i]);           // pour ?tre sur que l'identifiant 1 et 2 ont le m?me material
            itemMaterial.Add(id2, materials[i]);

            // check que tout est ok
            //items[id1].GetComponent<Renderer>().material = materials[i];
            //items[id2].GetComponent<Renderer>().material = materials[i];
        }
    }

    private IEnumerator ResetMaterials(int id1, int id2)
    {
        resetOnGoing = true;
        yield return new WaitForSeconds(timeBeforeReset);
        ResetMaterial(id1);
        ResetMaterial(id2);
        resetOnGoing = false;
    }
    private IEnumerator Win()
    {
        
        yield return new WaitForSeconds(timeBeforeReset);
        WhenPlayerWins?.Invoke();                             // "?" verifier si "WhenPlayerWins" n'est pas vide -> on evite de faire planter avec le Invoke
        
    }


    public void RevealMaterial(int id)
    {
        if (resetOnGoing == false && !selected.Contains(id) && !matches.Contains(id))
        {
            selected.Add(id);
            Material material = itemMaterial[id];
            items[id].GetComponent<Renderer>().material = material;
            items[id].HasBeenSelected(true);
        }   
    }

    private void ResetMaterial(int id)
    {
        items[id].GetComponent<Renderer>().material = defaultMaterial;
        items[id].HasBeenSelected(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;
        if (selected.Count == 2)
        {
            if (itemMaterial[selected[0]] == itemMaterial[selected[1]])
            {
                matches.Add(selected[0]);
                matches.Add(selected[1]);
                items[selected[0]].HasBeenMatched();
                items[selected[1]].HasBeenMatched();

                if(matches.Count >= row * column)
                {
                    PlayerPrefs.SetFloat("seconds", seconds);
                    StartCoroutine(Win());                   
                                                // co routine2
                }
                

            }
            else
            {
                StartCoroutine(ResetMaterials(selected[0], selected[1]));
                

            }
            selected.Clear();
        }
        
    }
}
