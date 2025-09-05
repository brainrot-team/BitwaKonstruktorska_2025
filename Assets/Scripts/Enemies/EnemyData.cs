using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects")]
public class EnemyData : ScriptableObject
{
    public float speed = 10;
    public float angleSpeed = 1;
    public int maxHP = 5;
    public float circlingRadius = 5.0f;
}
