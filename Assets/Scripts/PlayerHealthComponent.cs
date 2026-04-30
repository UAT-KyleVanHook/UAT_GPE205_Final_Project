using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class PlayerHealthComponent : HealthComponent
{
    public Image healthImage;
    //private AudioSource audioSource;

    private AudioSource audio;
    public AudioClip hurtClip;

    public GameObject spawnedAudioPlayer;

    //get controller to track lives
    [HideInInspector] public Controller controller;

    public override void Start()
    {

        //currentHealth = maxHealth;

    }

    public override void Awake()
    {
        currentHealth = maxHealth;

        healthImage.fillAmount = currentHealth / maxHealth;

        audio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void TakeDamage(float amount, Pawn source)
    {
        //calculate health
        currentHealth = currentHealth - amount;

        //change the fill amount of heatlh bar image
        healthImage.fillAmount = currentHealth / maxHealth;

        //clamp health so current health doesn't go above  max health
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth > 0 && hurtClip != null)
        {
            audio.PlayOneShot(hurtClip);
        }

        if (currentHealth <= 0)
        {
            Die(source);
        }

    }

    public override void Heal(float amount)
    {

        currentHealth += amount;

        //change the fill amount of heatlh bar image
        healthImage.fillAmount = currentHealth / maxHealth;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

    }

    public override void IncreaseMaxHealth(float amount)
    {
        //increae max heatlh by the amount
        maxHealth += amount;

        //set the current health to the new max health
        currentHealth = maxHealth;

        //change the fill amount of health bar image
        healthImage.fillAmount = currentHealth / maxHealth;
    }

    public override void Die(Pawn source)
    {
        //check that the destruction clip isn't null
        if (destructionClip != null && spawnedAudioPlayer != null)
        {

            //spawn an object to play an audioclip
            GameObject tempObject = Instantiate<GameObject>(spawnedAudioPlayer, transform.position, Quaternion.identity);

            PlayAudioClipBeforeDestroy TempAudioPlayer = tempObject.GetComponent<PlayAudioClipBeforeDestroy>();

            TempAudioPlayer.SetAudioClip(destructionClip);

        }
        
        //check that the source isn't null
        if(source != null)
        {
            Controller sourceController = source.GetController();

            //check that the source that desrtoyed this object has a controller
            if (sourceController != null)
            {
                Pawn tempPawn = sourceController.GetComponent<Pawn>();

                //check that the destroyed object has a pawn.
                if (tempPawn != null)
                {
                    Controller tempController = tempPawn.GetController();

                    //check that destroyed object has a controller. Then get the scoreAmount form the controller
                    if (tempController != null)
                    {
                        sourceController.AddToScore(tempController.scoreAmount);
                    }

                }


            }
        }
        
        //deincremnt player lives in controller
        controller.lives -= 1;

        Debug.Log(gameObject.name + " has respawned.");

        //respawn the player
        if (controller.lives > 0)
        {
            GameManager.instance.RespawnPlayer();
        }
        else
        {
            Destroy(gameObject);
        }
        

    }

    public void AssignController(Controller playerController)
    {
        controller = playerController;
    }
}
