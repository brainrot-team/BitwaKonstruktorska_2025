using UnityEngine;

public enum ProjectileOrigin
{
    Player,
    Enemy
}

public class TrashProjectile : MonoBehaviour
{
    private LayerMask targetLayer;
    
    private Rigidbody2D rb;

    public void TurnToTrash()
    {
        gameObject.tag = "Trash";

    }

    public void SetOrigin(ProjectileOrigin origin)
    {
        switch (origin)
        {
            case ProjectileOrigin.Player:
                tag = "PlayerProjectile";
                targetLayer = LayerMask.NameToLayer("Enemy");
                break;
            case ProjectileOrigin.Enemy:
                tag = "EnemyProjectile";
                targetLayer = LayerMask.NameToLayer("Player");
                break;
            default:
                Debug.LogError("Unknown ProjectileOrigin: " + origin);
                break;
        }
    }
}