using UnityEngine;
using UnityEngine.Events;
using Unity.FPS;

//namespace Unity.FPS.Gameplay
//{
public class Health : MonoBehaviour
{
    [Tooltip("Maximum amount of health")]
    public float maxHealth = 10f;
    [Tooltip("Health ratio at which the critical health vignette starts appearing")]
    public float criticalHealthRatio = 0.3f;

    public UnityAction<float, GameObject> onDamaged;
    public UnityAction<float> onHealed;
    public UnityAction onDie;
    public bool isImmortal = false;
    public float playerLives = 3f;

    private Transform player;
    private Transform destination;

    public float currentHealth { get; set; }
    public bool invincible { get; set; }
    public bool canPickup() => currentHealth < maxHealth;

    public float getRatio() => currentHealth / maxHealth;
    public bool isCritical() => getRatio() <= criticalHealthRatio;

    bool m_IsDead;

    private void Start()
    {
        currentHealth = maxHealth;
        if (GameObject.Find("Player"))
            player = GameObject.Find("Player").transform;
        if (GameObject.Find("StartSpawn"))
            destination = GameObject.Find("StartSpawn").transform;
    }

    public void Heal(float healAmount)
    {
        float healthBefore = currentHealth;
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        // call OnHeal action
        float trueHealAmount = currentHealth - healthBefore;
        if (trueHealAmount > 0f && onHealed != null)
        {
            onHealed.Invoke(trueHealAmount);
        }
    }

    public void TakeDamage(float damage, GameObject damageSource)
    {
        if (invincible)
            return;

        float healthBefore = currentHealth;
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        // call OnDamage action
        float trueDamageAmount = healthBefore - currentHealth;
        if (trueDamageAmount > 0f && onDamaged != null)
        {
            onDamaged.Invoke(trueDamageAmount, damageSource);
        }

        HandleDeath();
    }

    public bool TryUnlock(Transform spawn)
   {
        destination = spawn;
        //if (isImmortal)
        //    return false;

        //onUnlockSwim.Invoke(true);
        //isImmortal = true;
        return true;
    }
    
    public void Kill()
    {
        currentHealth = 0f;

        // call OnDamage action
        if (onDamaged != null)
        {
            onDamaged.Invoke(maxHealth, null);
        }

        HandleDeath();
    }

    private void HandleDeath()
    {
        if (m_IsDead){
            return;
        }

        // call OnDie action
        if (currentHealth <= 0f)
        {
        	if (!isImmortal)
            {
                if (onDie != null)
                {
                    m_IsDead = true;
                    onDie.Invoke();
                }
            }
            else if (playerLives != 0f)
            {
                player.transform.position = destination.transform.position;
                playerLives = playerLives -1f;
                Heal(100f);
            }
            else 
            {
                isImmortal = false;
                if (onDie != null)
                {
                    m_IsDead = true;
                    onDie.Invoke();
                }
            }
        }
    }
//    }
}
