using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "Scriptable Objects/AttackData")]
public class AttackData : ScriptableObject
{
    [Header("Attack Properties")]
    public float damage;
    public float knockbackForce;
    public float attackDuration;
    public float attackCooldown;
    
}
