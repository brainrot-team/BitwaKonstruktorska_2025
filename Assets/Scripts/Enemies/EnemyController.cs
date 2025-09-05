using UnityEngine;

public class EnemyController : MonoBehaviour
{

    EnemyState state = new RandomPatrol();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state.Enter(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        state.Update();

    }

    public void HitByProjectile()
    {
        Destroy(gameObject);

    }
}
