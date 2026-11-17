using UnityEngine;

public class MeleeAttacks : MonoBehaviour
{
    private InputHandler _inputHandler;
    private Collider _collider;

    [HideInInspector]public Collider[] hitColliders;
    [SerializeField] private Transform attackPoint; // The point from which the attack originates
    [SerializeField] AttackHitbox SwordSlashAttack;
    [SerializeField] AttackData attackData;

    private float lastAttackTime = 0f;

    void Start()
    {
        _collider = GetComponent<Collider>();
        _inputHandler = GetComponent<InputHandler>();       
        _inputHandler.OnAttackEvent += Attack;
    }

    void Attack()
    {
        if (Time.time - lastAttackTime >= attackData.attackCooldown)
        {
            lastAttackTime = Time.time;

            hitColliders = HitDetection();

            foreach (Collider collider in hitColliders)
            {
                IHitable hitable = collider.GetComponent<IHitable>();
                if (hitable != null)
                {
                    hitable.OnHit(_collider, attackData.damage);
                }
            }
        }
    }

    Collider[] HitDetection()
    { 
        SwordSlashAttack.InitPostions(attackPoint.position);
        Collider[] hitColliders = Physics.OverlapCapsule(SwordSlashAttack.StartPosition, SwordSlashAttack.EndPosition, SwordSlashAttack.AttackRadius);
        return hitColliders;
    }

    void OnDrawGizmos()
    {
        if (SwordSlashAttack != null)
        {
            SwordSlashAttack.InitPostions(attackPoint.position);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(SwordSlashAttack.StartPosition, SwordSlashAttack.AttackRadius);
            Gizmos.DrawWireSphere(SwordSlashAttack.EndPosition, SwordSlashAttack.AttackRadius);
            Gizmos.DrawLine(SwordSlashAttack.StartPosition, SwordSlashAttack.EndPosition);
        }
    }
}
