using UnityEngine;

namespace GameDevDemo.Scripts
{
    public class PlayerStart : MonoBehaviour
    {
        private void Start()
        {
            var player = FindFirstObjectByType<PlayerMarker>();

            if (player == null)
            {
                Debug.Log("Failed to find player", this);
                return;
            }
            
            var playerStartTransform = transform;

            // Find the character controller, disable it so we stop moving, then enable it post teleport so that the player appears in the right spot
            var playerCharacterController = player.GetComponentInChildren<CharacterController>();
            playerCharacterController.enabled = false;
            player.transform.SetPositionAndRotation(playerStartTransform.position, playerStartTransform.rotation);
            playerCharacterController.enabled = true;

            Debug.Log($"Found player, moving to {name}");
        }
    }
}