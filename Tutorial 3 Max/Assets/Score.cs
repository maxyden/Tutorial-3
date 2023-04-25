using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;



public class Score : MonoBehaviour

{
    public TextMeshProUGUI Fixed; 
    public int Fix; 
    public static int level;
    public GameObject winTextObject;

    public GameObject SecTextObject;

     public GameObject backgroundmusic;

    // Start is called before the first frame update
    void Start()
    {
        Fix = 0;

        SetFixed();

        winTextObject.SetActive(false);

        SecTextObject.SetActive(false);


    }

    public void ChangeFix()
    {
        Fix = Fix + 1;

        Fixed.text = "Fixed Robots: " + Fix.ToString() + "/5" ;

        if(level >= 2 && Fix >= 5 ){  
           winTextObject.SetActive(true);

           FindObjectOfType<RubyController>().WinS();


        }

        if (level <= 2 && Fix >= 5)
        {
            SecTextObject.SetActive(true);
        }

        if(Fix >= 4){  
            
            

            level = level + 1;


        } 
    }

     void SetFixed()
	{
		Fixed.text = "Fixed Robots: " + Fix.ToString() + "/5" ;
    }

    // Update is called once per frame
    void Update()
    {
          if (Input.GetKey(KeyCode.R))

        {

            if (level >= 2 && Fix >= 5) 

            {
             
              SceneManager.LoadScene("tutorial 3"); // this loads the currently active scene

            }

        }
    }

    public void Stage2()
    {
        if(Fix >= 5){  
            
            SceneManager.LoadScene("Scene2");

            


        } 
    }

    

}
