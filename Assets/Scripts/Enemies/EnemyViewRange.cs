using UnityEngine;

[System.Serializable]
public class EnemyViewRange : MonoBehaviour
{

    [SerializeField] private EnemyController enemy;
    private bool enemyDetected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == WorldManager.Instance.playerObject)
        {
            Debug.Log("PLAYER DETECT!!!");
            enemyDetected = true;
        } 
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject == WorldManager.Instance.playerObject)
        {
            Debug.Log("PLAYER OUT!!!");
            enemyDetected = false;
        } 
    }

    public bool GetEnemyDetected()
    {
        return enemyDetected;
    }

    public float GetDistanceToPlayer(Vector3 point)
    {
        return Vector2.Distance(point, WorldManager.Instance.GetPlayerPosition());
    }
}
