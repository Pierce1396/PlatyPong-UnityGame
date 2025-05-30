using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject player1Prefab; // For WASD
    public GameObject player2Prefab; // For Arrows

    void Start()
    {
        // Spawn Player 1 with WASD
        var player1 = PlayerInput.Instantiate(
            player1Prefab,
            controlScheme: "WASD",
            pairWithDevice: Keyboard.current
        );

        // Spawn Player 2 with Arrows
        var player2 = PlayerInput.Instantiate(
            player2Prefab,
            controlScheme: "Arrows",
            pairWithDevice: Keyboard.current
        );
    }
}
