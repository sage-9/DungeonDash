using System;
using UnityEngine;


[Serializable]
[CreateAssetMenu(fileName = "AttackHitbox",menuName = "Scriptable Objects/Attack Hitbox")]
public class AttackHitbox:ScriptableObject
{
    // Define an area of effect for the attack

    [HideInInspector] public Vector3 AttackAreaCenter;
    [HideInInspector]public Vector3 StartPosition;
    [HideInInspector]public Vector3 EndPosition;
    public float AttackRange; // The range of the attack
    public float AttackRadius; // The radius of the attack area
    public AttackType attackType; // The type of attack (e.g., horizontal sweep, vertical sweep, stab)

    public void InitPostions(Vector3 AttackAreaCenter = new Vector3())
    {
        this.AttackAreaCenter = AttackAreaCenter;

        switch (attackType)
        {
            case AttackType.HorizontalSweep:
                // Set up the attack area for a horizontal sweep
                StartPosition = new Vector3(AttackAreaCenter.x - AttackRange / 2, AttackAreaCenter.y, AttackAreaCenter.z);
                EndPosition = new Vector3(AttackAreaCenter.x + AttackRange / 2, AttackAreaCenter.y, AttackAreaCenter.z);
                break;
            case AttackType.verticalSweep:
                // Set up the attack area for a vertical sweep
                StartPosition = new Vector3(AttackAreaCenter.x, AttackAreaCenter.y - AttackRange / 2, AttackAreaCenter.z);
                EndPosition = new Vector3(AttackAreaCenter.x, AttackAreaCenter.y + AttackRange / 2, AttackAreaCenter.z);
                break;
            case AttackType.Stab:
                // Set up the attack area for a stab
                StartPosition = new Vector3(AttackAreaCenter.x, AttackAreaCenter.y, AttackAreaCenter.z - AttackRange / 2);
                EndPosition = new Vector3(AttackAreaCenter.x, AttackAreaCenter.y, AttackAreaCenter.z + AttackRange / 2);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(attackType), attackType, null);
        }
                
    }

}

public enum AttackType
{
    HorizontalSweep,
    verticalSweep,
    Stab,
}
