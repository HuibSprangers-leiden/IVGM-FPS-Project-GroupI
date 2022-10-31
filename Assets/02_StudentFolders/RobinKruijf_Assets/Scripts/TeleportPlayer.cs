using Unity.FPS;
using UnityEngine;

namespace Unity.FPS.Gameplay
{
        [RequireComponent(typeof(Collider))]
    public class TeleportPlayers : MonoBehaviour
    {
                [Tooltip("Visible transform that will be destroyed once the objective is completed")]
        public Transform Destination;
        public Transform player;
        //public string TagList = "|Player|Boss|Friendly|";
        //public Vector3 myPosition = new Vector3 (0, 0, 0);
        //public Quaternion myRotation = new Quaternion(0, 0, 0, 0);

        PlayerCharacterController m_PlayerCharacterController;

        void Awake()
        {
            m_PlayerCharacterController = FindObjectOfType<PlayerCharacterController>();
            DebugUtility.HandleErrorIfNullFindObject<PlayerCharacterController, TeleportPlayers>(
                m_PlayerCharacterController, this);
            if (Destination == null)
                Destination = transform;
        }

        void OnTriggerEnter(Collider other)
        {
            player.transform.position = Destination.transform.position;
            //if (TagList.Contains(string.Format("|{0}|",other.tag))) {
             // Update other objects position and rotation
             //other.transform.position = Destination.transform.position;
             //other.transform.rotation = Destination.transform.rotation;
            }
         /*
            m_PlayerCharacterController.transform.SetPositionAndRotation(myPosition, myRotation);
            if (IsCompleted)
                return;

            var player = other.GetComponent<PlayerCharacterController>();
            // test if the other collider contains a PlayerCharacterController, then complete
            if (player != null)
            {
                CompleteObjective(string.Empty, string.Empty, "Objective complete : " + Title);

                // destroy the transform, will remove the compass marker if it has one
                Destroy(DestroyRoot.gameObject);
            }
            */
        }
    }
//}

/*
    // Debug script, teleports the player across the map for faster testing
    public class TeleportPlayer : MonoBehaviour
    {
        public KeyCode ActivateKey = KeyCode.F12;
        public Vector3 myPosition = new Vector3 (0, 0, 0);
        public Quaternion myRotation = new Quaternion(0, 0, 0, 0);

        PlayerCharacterController m_PlayerCharacterController;

        void Awake()
        {
            m_PlayerCharacterController = FindObjectOfType<PlayerCharacterController>();
            DebugUtility.HandleErrorIfNullFindObject<PlayerCharacterController, TeleportPlayer>(
                m_PlayerCharacterController, this);
        }

        void Update()
        {
            if (Input.GetKeyDown(ActivateKey))
            {
                m_PlayerCharacterController.transform.SetPositionAndRotation(myPosition, myRotation);
                Health playerHealth = m_PlayerCharacterController.GetComponent<Health>();
                if (playerHealth)
                {
                    playerHealth.Heal(999);
                }
            }
        }

    }
}


namespace Unity.FPS.Gameplay
{
    [RequireComponent(typeof(Collider))]
    public class ObjectiveReachPoint : Objective
    {
        [Tooltip("Visible transform that will be destroyed once the objective is completed")]
        public Transform DestroyRoot;

        void Awake()
        {
            if (DestroyRoot == null)
                DestroyRoot = transform;
        }

        void OnTriggerEnter(Collider other)
        {
            if (IsCompleted)
                return;

            var player = other.GetComponent<PlayerCharacterController>();
            // test if the other collider contains a PlayerCharacterController, then complete
            if (player != null)
            {
                CompleteObjective(string.Empty, string.Empty, "Objective complete : " + Title);

                // destroy the transform, will remove the compass marker if it has one
                Destroy(DestroyRoot.gameObject);
            }
        }
    }
}
*/