/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimPickUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/
using UnityEngine;

public class SwimPickUp : MonoBehaviour
{
    Pickup m_Pickup;

    void Start()
    {
        m_Pickup = GetComponent<Pickup>();
        DebugUtility.HandleErrorIfNullGetComponent<Pickup, SwimPickUp>(m_Pickup, this, gameObject);

        // Subscribe to pickup action
        m_Pickup.onPick += OnPicked;
    }

    void OnPicked(PlayerCharacterController byPlayer)
    {
        var swim = byPlayer.GetComponent<Swim>();
        if (!swim)
            return;

        if (swim.TryUnlock())
        {
            m_Pickup.PlayPickupFeedback();

            Destroy(gameObject);
        }
    }
}