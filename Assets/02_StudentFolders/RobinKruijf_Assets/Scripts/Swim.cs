using UnityEngine;
using UnityEngine.Events;
using Unity.FPS;


 public class Swim : MonoBehaviour
 {
     //public float movementSpeed = 5f;
     public KeyCode ActivateKey;
     public Transform Ocean;
     //public Transform myPlayer;
     public bool isSwimUnlocked = false;
     public UnityAction<bool> onUnlockSwim;

     private Vector3 waterGravity = new Vector3(0, -3, 0);
     private bool under = false;
     CharacterController m_Controller; 


    PlayerCharacterController m_PlayerCharacterController;
    PlayerInputHandler m_InputHandler;

     void Start()
    {
        m_PlayerCharacterController = GetComponent<PlayerCharacterController>();
        m_Controller = GetComponent<CharacterController>();
        m_InputHandler = GetComponent<PlayerInputHandler>();
        /*
        if (transform.position.y < 0) {
            m_PlayerCharacterController.characterVelocity = waterGravity;
        }
        */
    }
 
     void Update()
     {
         //float hInput = Input.GetAxis("Horizontal");
         //float vInput = Input.GetAxis("Vertical");
        if (transform.position.y < Ocean.transform.position.y && isSwimUnlocked) {
            m_Controller.Move(transform.TransformVector(m_InputHandler.GetMoveInput())/2f);
            /*
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                m_Controller.Move(-1f* m_InputHandler.GetMoveInput());//new Vector3(4f * Time.deltaTime, 0f, 0f));
                //transform.position = new Vector3(transform.position.x + 4f * Time.deltaTime, transform.position.y, transform.position.z);
                //transform.position.x = transform.position.x + 2f * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                m_Controller.Move(-1f* m_InputHandler.GetMoveInput());
                //m_Controller.Move(new Vector3(-4f * Time.deltaTime, 0f, 0f));
                //transform.position = new Vector3(transform.position.x - 4f * Time.deltaTime, transform.position.y, transform.position.z);
                //transform.position.x += 2f * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                m_Controller.Move(-1f* m_InputHandler.GetMoveInput());
                //m_Controller.Move(new Vector3(0f, 0f, -4f * Time.deltaTime));
                //transform.position = new Vector3(transform.position.x, transform.position.y , transform.position.z - 4f * Time.deltaTime);
                //transform.position.y -= 2f * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                m_Controller.Move(-1f *m_InputHandler.GetMoveInput());
                //m_Controller.Move(new Vector3(0f, 0f, 4f * Time.deltaTime));
                //transform.position = new Vector3(transform.position.x, transform.position.y , transform.position.z + 4f * Time.deltaTime);
                //transform.position.y += 2f * Time.deltaTime;
            }
            */
            if (under){
                m_PlayerCharacterController.characterVelocity = waterGravity * -1f;
            } else {
                m_PlayerCharacterController.characterVelocity = waterGravity; 
            }

        } //else if (transform.position.y < Ocean.transform.position.y && !under){
            //m_PlayerCharacterController.characterVelocity = waterGravity;
        //}


        if (transform.position.y < Ocean.transform.position.y && isSwimUnlocked) {
            if (Input.GetKeyDown(ActivateKey)){
                under = !under;
            }
        }
        if (under && transform.position.y > Ocean.transform.position.y && isSwimUnlocked){
            under = !under;
        }
        //m_PlayerCharacterController.characterVelocity = waterGravity; //* m_PlayerCharacterController.gravityDownForce;
        //m_PlayerCharacterController.characterVelocity += ;// + Vector3.up * 5f * Time.deltaTime;
/*
         if (vInput > 0)
         {
             transform.position += transform.forward * Time.deltaTime *movementSpeed;
         }
         if (vInput < 0)
         {
             transform.position -= transform.forward * Time.deltaTime *movementSpeed;
         }

         if(hInput > 0)
         {
             transform.Rotate(0, 1f, 0, Space.Self);
         }
         if (hInput < 0)
         {
             transform.Rotate(0, -1f, 0, Space.Self);
         }
         */
     }
     public bool TryUnlock()
    {
        if (isSwimUnlocked)
            return false;

        //onUnlockSwim.Invoke(true);
        isSwimUnlocked = true;
        return true;
    }
 }


/*
[RequireComponent(typeof(AudioSource))]
public class Swim : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Audio source for jetpack sfx")]
    public AudioSource audioSource;
    [Tooltip("Particles for jetpack vfx")]
    public ParticleSystem[] jetpackVfx;

    [Header("Parameters")]
    [Tooltip("Whether the jetpack is unlocked at the begining or not")]
    public bool isJetpackUnlockedAtStart = false;
    //[Tooltip("The strength with which the jetpack pushes the player up")]
    //public float jetpackAcceleration = 7f;
    [Range(0f, 1f)]
    [Tooltip("This will affect how much using the jetpack will cancel the gravity value, to start going up faster. 0 is not at all, 1 is instant")]
    public float jetpackDownwardVelocityCancelingFactor = 1f;

    [Header("Durations")]
    [Tooltip("Time it takes to consume all the jetpack fuel")]
    public float consumeDuration = 1.5f;
    [Tooltip("Time it takes to completely refill the jetpack while on the ground")]
    //public float refillDurationGrounded = 2f;
    [Tooltip("Time it takes to completely refill the jetpack while in the air")]
    //public float refillDurationInTheAir = 5f;
    [Tooltip("Delay after last use before starting to refill")]
    //public float refillDelay = 1f;

    [Header("Audio")]
    [Tooltip("Sound played when using the jetpack")]
    public AudioClip jetpackSFX;

    bool m_CanUseJetpack;
    PlayerCharacterController m_PlayerCharacterController;
    PlayerInputHandler m_InputHandler;
    //float m_LastTimeOfUse;

    // stored ratio for jetpack resource (1 is full, 0 is empty)
    public float currentFillRatio { get; private set; }
    public bool isJetpackUnlocked { get; private set; }

    public bool isPlayergrounded() => m_PlayerCharacterController.isGrounded;

    public UnityAction<bool> onUnlockJetpack;

    void Start()
    {
        isJetpackUnlocked = isJetpackUnlockedAtStart;

        m_PlayerCharacterController = GetComponent<PlayerCharacterController>();
        DebugUtility.HandleErrorIfNullGetComponent<PlayerCharacterController, Jetpack>(m_PlayerCharacterController, this, gameObject);

        m_InputHandler = GetComponent<PlayerInputHandler>();
        DebugUtility.HandleErrorIfNullGetComponent<PlayerInputHandler, Jetpack>(m_InputHandler, this, gameObject);

        //currentFillRatio = 1f;

        audioSource.clip = jetpackSFX;
        audioSource.loop = true;
    }

    void Update()
    {
        // jetpack can only be used if not grounded and jump has been pressed again once in-air
        if(isPlayergrounded())
        {
            m_CanUseJetpack = false;
        }
        else if (!m_PlayerCharacterController.hasJumpedThisFrame && m_InputHandler.GetJumpInputDown())
        {
            m_CanUseJetpack = true;
        }

        // jetpack usage
        bool jetpackIsInUse = m_CanUseJetpack && isJetpackUnlocked && m_InputHandler.GetJumpInputHeld();
        if(jetpackIsInUse)
        {
            // store the last time of use for refill delay
            m_LastTimeOfUse = Time.time;

            float totalAcceleration = jetpackAcceleration;

            // cancel out gravity
            totalAcceleration += m_PlayerCharacterController.gravityDownForce;

            if (m_PlayerCharacterController.characterVelocity.y < 0f)
            {
                // handle making the jetpack compensate for character's downward velocity with bonus acceleration
                totalAcceleration += ((-m_PlayerCharacterController.characterVelocity.y / Time.deltaTime) * jetpackDownwardVelocityCancelingFactor);
            }

            // apply the acceleration to character's velocity
            m_PlayerCharacterController.characterVelocity += Vector3.up * totalAcceleration * Time.deltaTime;

            // consume fuel
            currentFillRatio = currentFillRatio - (Time.deltaTime / consumeDuration);

            for (int i = 0; i < jetpackVfx.Length; i++)
            {
                var emissionModulesVFX = jetpackVfx[i].emission;
                emissionModulesVFX.enabled = true;
            }

            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            // refill the meter over time
            if (isJetpackUnlocked && Time.time - m_LastTimeOfUse >= refillDelay)
            {
                float refillRate = 1 / (m_PlayerCharacterController.isGrounded ? refillDurationGrounded : refillDurationInTheAir);
                currentFillRatio = currentFillRatio + Time.deltaTime * refillRate;
            }

            for (int i = 0; i < jetpackVfx.Length; i++)
            {
                var emissionModulesVFX = jetpackVfx[i].emission;
                emissionModulesVFX.enabled = false;
            }

            // keeps the ratio between 0 and 1
            currentFillRatio = Mathf.Clamp01(currentFillRatio);

            if (audioSource.isPlaying)
                audioSource.Stop();
        }
    }

    public bool TryUnlock()
    {
        if (isJetpackUnlocked)
            return false;

        onUnlockJetpack.Invoke(true);
        isJetpackUnlocked = true;
        m_LastTimeOfUse = Time.time;
        return true;
    }
}*/
