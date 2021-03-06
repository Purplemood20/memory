using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public int id = -1;
    public LevelManager manager;
    public bool mouseOver = false;

    public Animator animator;

    public AudioSource source;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && mouseOver) // click ? gauche a le num?ro ''0''
        {

            manager.RevealMaterial(id);
            //Debug.Log($"Click: {id}");
        }
            
    }

    void OnMouseOver()
    {
           
        mouseOver = true;
        animator.SetBool("MouseOver", true);
    }

    void OnMouseExit()
    {
        
        mouseOver = false;
        animator.SetBool("MouseOver", false);
    }

    public void HasBeenSelected(bool selected)
    {
        animator.SetBool("ItemSelected", selected);
        source.PlayOneShot(clip);
    }

    public void HasBeenMatched()
    {
        animator.SetBool("RightItem", true);
    }





}
