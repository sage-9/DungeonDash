using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public float Speed;

    [Header("Dash Stats")]
    public float DashAmount;
    public float DashDuration;
    public float DashCooldown = 0.2f;

    [Header("Jump Stats")]
    public float JumpHeight;
    public float gravity = -20f;
    
    
}
