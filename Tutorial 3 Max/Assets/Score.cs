using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Score : MonoBehaviour

{
    public TextMeshProUGUI Fixed; 
    public int Fix; 
    public GameObject winTextObject;

    public GameObject Backgroundmusic;
    AudioSource audioSource;
    public AudioClip Winmusic;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        Fix = 0;

        SetFixed();

        winTextObject.SetActive(false);

    }

    public void ChangeFix()
    {
        Fix = Fix + 1;

        Fixed.text = "Fixed Robots: " + Fix.ToString() + "/5";

        if(Fix >= 5){  
           winTextObject.SetActive(true);

           Backgroundmusic.SetActive(false);

           
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

            if (Fix >= 5)

            {

              SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // this loads the currently active scene

            }

        }
    }
}
