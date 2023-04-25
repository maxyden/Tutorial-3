using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;
    
    public int maxHealth = 5;

    public int cog;

    public TextMeshProUGUI cogs; 
    public GameObject loseTextObject;

    public ParticleSystem HealthEffect;

    public ParticleSystem damage;
    
    public GameObject projectilePrefab;
    
    public AudioClip throwSound;
    public AudioClip hitSound;

    public AudioClip winsound;

    public AudioClip winssound;

    public AudioClip jambi;

    public AudioClip Klang;

    public GameObject backgroundmusic;

  
    public int health { get { return currentHealth; }}
    int currentHealth;
    
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
    
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    
    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);

    
    
    AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        currentHealth = maxHealth;

        audioSource = GetComponent<AudioSource>();

        loseTextObject.SetActive(false);

        cog = 5;

        cogs.text = "Cogs: " + cog.ToString();
    }

    // Update is called once per frame
    void Update()
    {

         if (Input.GetKey(KeyCode.R))

        {

            if (currentHealth <= 0)

            {

              SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // this loads the currently active scene

            }

        }


        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        Vector2 move = new Vector2(horizontal, vertical);
        
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        
        if(Input.GetKeyDown(KeyCode.C) && cog >= 1)
        {
            Launch();
            cog = cog - 1;
            cogs.text = "Cogs: " + cog.ToString();
        }

      
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    PlaySound(jambi);
                    character.DisplayDialog();
                    FindObjectOfType<Score>().Stage2(); 
                }
            }
        }
    }
    
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }
    public void SpeedUp()

    {
        speed = speed +4;
        HealthEffect.Play(); 
        Invoke ("StopSpeedUp",3);
    }

     public void StopSpeedUp() {
     speed = speed -4; }

    public void SlowDown() {
        speed = speed -3;
        Invoke ("StopSlowdown",3);
        PlaySound(hitSound);
        damage.Play();
    }
     public void StopSlowdown()
     {
        speed = speed +3;

     }


    public void ChangeHealth(int amount)

    
    
    {
         if(currentHealth <= 1){
        loseTextObject.SetActive(true);

        PlaySound(winsound);

        backgroundmusic.SetActive(false);

        speed = 0;
        }
  
       
        if (amount < 0)
        {   


            if (isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;
            
            PlaySound(hitSound);

            damage.Play(); 
            

            
        }
         if (amount > 0)
        {
            
            HealthEffect.Play(); 
        }




        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }
    
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
        
        PlaySound(throwSound);
    } 
    
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void WinS()
    {
         PlaySound(winssound);

        backgroundmusic.SetActive(false);
     }


     private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "COG")
        {
            cog = cog + 4;
            cogs.text = "Cogs: " + cog.ToString();
            Destroy(collision.collider.gameObject);
            PlaySound(Klang);
        }

    }

    
}