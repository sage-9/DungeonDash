using UnityEngine;

public interface IHitable
{
    void OnHit(Collider attacker, float damage);

}
