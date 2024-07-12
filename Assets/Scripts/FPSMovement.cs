using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
//using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using static Violin;

// THIS PLAYER MOVE CLASS WILL ALLOW THE GAMEOBJECT TO MOVE BASED ON CHARACTERCONTROLLER

    
public class FPSMovement : MonoBehaviour
{
    // VARS
    //public UnityEngine.CharacterController m_charControler;
    public CharacterController m_charController;
    public float m_movementSpeed = 12f;
    public float m_runSpeed = 1.5f;
    private float m_finalSpeed = 0;

    public float m_gravity = -9.81f;
    public float m_jumpHeight = 3f;
    private Vector3 m_velocity;

    public Transform m_groundCheckPoint;
    public float m_groundDistance = 0.4f;
    public LayerMask m_groundMask;
    private bool m_isGrounded; 

    public bool isInputting;
    public KeyCode m_forward;
    public KeyCode m_back;
    public KeyCode m_left;
    public KeyCode m_right;
    public KeyCode m_sprint;
    public KeyCode m_jump;
    public KeyCode m_crouch;
    // Coin Ability
    public KeyCode m_ability;

    //AbilityToggle Instrument 

    public KeyCode m_MelodyAbility;
    public KeyCode MelodyAbilitySwap;
    public float MelodyAbilityselected;

    public SoundBox soundBox;
    // crouching vars

    public bool crouching;
    public GameObject m_camera;
    public GameObject m_cameraStandPoint;
    public GameObject m_cameraCrouchPoint;
    public float lerpRate;
    public float headRoom;
    private bool crouchSwitched;

    public bool sprinting;
    //s public bool hasAbility = false;

    //Current Player State 
    
    public string PlayerState;
    public CurrentPlayerState status;

    /*
    State 0 = Crouching
    State 1 = Standing
    State 2 = Running
    State 3 = Idle ???
     */

    //Coin ability location 
    public CoinTossAbility CoinThrowposition;
    
    // ability activation and delay - Music
    public bool abilityActive_Music;
    public bool canUseAbility_Music;

    public float MusicabilityActiveSeconds;
    public float MusicabilityCooldownSeconds;

    public float MusicAbilityProjectileActiveSeconds;
    public float MusicAbilityProjectileCooldownSeconds;

    //Current Selected Ability 

    public string SelectedAbility;
    public AbilityChecking Checker;

    // ability activation and delay - Coin

    public bool canUseAbility_Coin;
    public bool AbilityActive_Coin;

    public float CoinabilityActiveSeconds;
    public float CoinabilityCooldownSeconds;

    public int CoinsRemaining;
    public int value;

    // Start is called before the first frame update
    void Awake()
    {
        m_finalSpeed = m_movementSpeed;

        canUseAbility_Music = true;
        canUseAbility_Coin = true;
        MelodyAbilityselected = 0f; 
    }

    // Update is called once per frame
    void Update()
    {
        m_isGrounded = HitGroundCheck(); // CHecks touching the ground every frame
        MoveInputCheck();
    }

    // Check if a button is pressed
    void MoveInputCheck()
    {
        float x = Input.GetAxis("Horizontal"); // Gets the x input value for the Gameobject vector
        float z = Input.GetAxis("Vertical"); // Gets the z input value for the Gameobject vector

        Vector3 move = Vector3.zero;

        if (Input.GetKey(m_forward) || Input.GetKey(m_back) || Input.GetKey(m_left) || Input.GetKey(m_right))
        {
            isInputting = true;
            move = transform.right * x + transform.forward * z; // calculate the move vector (direction)          melodyAbility
        }

        else 
        {
            isInputting = false;
        }

        if (Input.GetKeyDown(m_ability))
        {
          /*  if (canUseAbility_Music && !canUseAbility_Coin)
            {
                abilityActive_Music = true;
                canUseAbility_Music = false;
                StartCoroutine(musicAbilityCoroutine());
            }
          */
             if ( /*!canUseAbility_Music && */ canUseAbility_Coin)
            {
                AbilityActive_Coin = true;
                FindCoinThrow();
                canUseAbility_Coin = false;
                StartCoroutine(CoinAbilityCoroutine());

            }

           /* else if(canUseAbility_Coin && canUseAbility_Music)
            {
                Debug.LogError("Error:Both are active abilities");
            }*/
            
        }
        FindSoundOutput();

        //Activate Melody Ability
        if (Input.GetKeyDown(m_MelodyAbility))
        {
            Violin AbilityChange;
            AbilityChange = GetComponent<Violin>();

            if (canUseAbility_Music)
            {
                Debug.Log("Music ability played");
                abilityActive_Music = true;
                canUseAbility_Music = false;

                switch(AbilityChange.mode)
                {
                    case ViolinMode.Projectile:
                      
                        AbilityChange.Projectile();
                        StartCoroutine(ViolinProjectileCoroutine());
                        break;

                    case ViolinMode.Distraction:
                        soundBox.DistractionActive = true;
                        AbilityChange.Distraction();
                        Debug.Log("Inactive");
                        soundBox.DistractionActive = false;
                        StartCoroutine(ViolinProjectileCoroutine());
                        break;
                }
            }
            else
            {
                Checker.currentAbility = "Violin Ability on Cooldown";
                SelectedAbility = "Violin Ability on Cooldown";
            }
        }
        //Swapping Melody Ability 
        if (Input.GetKeyDown(MelodyAbilitySwap))
        {
            MelodySwap();
        }

        MovePlayer(move); // Run the MovePlayer function with the vector3 value move
        RunCheck(); // Checks the input for run
        JumpCheck(); // Checks if we can jump



        if (Input.GetKeyDown(m_crouch))
        {
            if (crouching)
            {
                crouching = false;
                crouchSwitched = true;
                RaycastHit hit;
                if (Physics.Raycast(m_camera.transform.position, Vector3.up * headRoom, out hit, headRoom))
                {
                    string name = hit.collider.gameObject.name;
                    if (hit.collider.tag != null)
                    {
                        Debug.Log("Object = " + name + " above, cannot stand.");
                        crouching = true;
                        crouchSwitched = false;
                    }
                }

            }
            else
            {
                crouching = true;
                crouchSwitched = true;
            }

            CrouchCheck();
        }

    }

    // MovePlayer
    void MovePlayer(Vector3 move)
    {
        m_charController.Move(move * m_finalSpeed * Time.deltaTime); // Moves the Gameobject using the Character Controller

        m_velocity.y += m_gravity * Time.deltaTime; // Gravity affects the jump velocity
        m_charController.Move(m_velocity * Time.deltaTime); //Actually move the player up
    }

    void CrouchCheck()
    {
        if (crouchSwitched == true)
        {
            if (crouching)
            {
                m_camera.transform.position = Vector3.Lerp(m_camera.transform.position, m_cameraCrouchPoint.transform.position, Time.deltaTime * lerpRate);
                m_charController.height = 0.75f; // If you want to change the crouching height, adjust this. Be careful doing so - Kept this non-exposed as it can lead to issues. 0.75 works
                crouchSwitched = false;
            }
            else
            {
                m_camera.transform.position = Vector3.Lerp(m_camera.transform.position, m_cameraStandPoint.transform.position, Time.deltaTime * lerpRate);
                m_charController.height = 1.8f;
                crouchSwitched = false;
            }
        }
    }

    // Player run
    void RunCheck()
    {
        if (Input.GetKeyDown(m_sprint)) // if key is down, sprint
        {
            m_finalSpeed = m_movementSpeed * m_runSpeed;
            sprinting = true;
            //if crouched, then sprinting, forces player to stand up
            m_camera.transform.position = Vector3.Lerp(m_camera.transform.position, m_cameraStandPoint.transform.position, Time.deltaTime * lerpRate);
            m_charController.height = 1.8f;
            crouching = false;
        } 
        else if (Input.GetKeyUp(m_sprint)) // if key is up, don't sprint
        {
            sprinting = false;
            m_finalSpeed = m_movementSpeed;
        }
    }

    // Ground check
    bool HitGroundCheck()
    {
        bool isGrounded = Physics.CheckSphere(m_groundCheckPoint.position, m_groundDistance, m_groundMask);

        //Gravity
        if (isGrounded && m_velocity.y < 0)
        {
            m_velocity.y = -2f;
        }

        return isGrounded;
    }

    // Jump Check
    void JumpCheck()
    {
        if (Input.GetKeyDown(m_jump))
        {
            if (m_isGrounded == true)
            {
                m_velocity.y = Mathf.Sqrt(m_jumpHeight * -2f * m_gravity);
            }
        }

    }
    //Ability Lists 
    void FindSoundOutput()
    {
        /*if (abilityActive_Music)
        {
            Debug.Log("MakeNoise");
            soundBox.gameObject.SetActive(true);
            soundBox.AbilitySoundRange();
            return;
        }*/
       
       // status = GetComponent<CurrentPlayerState>();

        if (!isInputting) 
        {
            status.currentState = "Idle";
            PlayerState = "Idle";
            soundBox.gameObject.SetActive(false);
            return;
        }

        while (crouching) 
        {
            status.currentState = "Crouching";
            PlayerState = "Crouching";
            soundBox.gameObject.SetActive(true);
            soundBox.CrouchSoundRange();
            return;
        }

        if (sprinting == true)
        {
            status.currentState = "Running";
            PlayerState = "Running";
            soundBox.gameObject.SetActive(true);
            soundBox.RunSoundRange();
        }

        else 
        {
            status.currentState = "Standing";
            PlayerState = "Standing";
            soundBox.gameObject.SetActive(true);
            soundBox.NormalSoundRange();
        }
    }
    void FindCoinThrow()
    {
        /* if (canUseAbility_Music == true)
         {
             canUseAbility_Coin = false;
         }*/
       // CoinCounter coinCounter;
       // coinCounter = GetComponent<CoinCounter>();

        if (AbilityActive_Coin && CoinsRemaining > 0)
        {
            CoinThrowposition.CoinThrow();
            CoinsRemaining = CoinsRemaining - 1;
            CoinCounter.instance.LoseCoins(value);
            
        }
        if (CoinsRemaining <= 0)
        {
            CoinCounter.instance.OutofCoins();

        }
      /*  if (canUseAbility_Music == true)
        {
            canUseAbility_Coin = false;
        }*/
    }
    public void MelodySwap()
    {
        Violin AbilityChange;
        AbilityChange = GetComponent<Violin>();
        
        //Proof of changing abilities 
        if (Input.GetKeyDown(MelodyAbilitySwap))
        {
            if (AbilityChange.mode == Violin.ViolinMode.Projectile)
            {
                Checker.currentAbility = "Distraction";
                SelectedAbility = "Distraction";
                Debug.Log("Ability Changed to Distraction");
                AbilityChange.mode = Violin.ViolinMode.Distraction;
            }
            else if (AbilityChange.mode == Violin.ViolinMode.Distraction)
            {
                Checker.currentAbility = "Deafen";
                Debug.Log("Ability Changed to Projectile");
                AbilityChange.mode = Violin.ViolinMode.Projectile;
            }
        }
    }



    // Ability Coroutines - Timer
    private IEnumerator ViolinProjectileCoroutine()
    {
        //...............
        
        yield return new WaitForSeconds(MusicAbilityProjectileActiveSeconds);
        Debug.Log("Projectile Ability has ended");
        abilityActive_Music = false;

        yield return new WaitForSeconds(MusicAbilityProjectileCooldownSeconds);
        Debug.Log("Projectile Ability is recharged");
        canUseAbility_Music = true;
    }

    private IEnumerator DistractionMusicAbilityCooldown()
    {
        yield return new WaitForSeconds(MusicabilityActiveSeconds);
        Debug.Log("Distraction Ability has ended");
        abilityActive_Music = false;

        yield return new WaitForSeconds(MusicabilityCooldownSeconds);
        Debug.Log("Distraction Ability is recharged");
        canUseAbility_Music = true;
    }

    private IEnumerator CoinAbilityCoroutine()
    {
        yield return new WaitForSeconds(CoinabilityActiveSeconds);
        Debug.Log("Coin Ability has ended");
        AbilityActive_Coin = false;

        yield return new WaitForSeconds(CoinabilityCooldownSeconds);
        Debug.Log("Coin Ability is recharged");
        canUseAbility_Coin = true;
    }
}