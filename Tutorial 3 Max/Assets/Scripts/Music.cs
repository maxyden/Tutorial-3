using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

   public GameObject background ;
   
   public GameObject winmusic;

   public GameObject losemusic;

   public GameObject losetextobject;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(losetextobject == true);
        {
            background.SetActive(false);
        }
    }
}
