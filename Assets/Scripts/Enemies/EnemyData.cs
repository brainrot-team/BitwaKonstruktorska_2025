using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects")]
public class EnemyData : ScriptableObject
{
    public int maxHP = 5;

    [Header("Forward")]
    public float speed = 10;
    [Header("Spinning")]
    public float angleSpeed = 1;
    public float circlingRadius = 5.0f;
    [Header("Back to center")]
    public float backAngleSpeed = 1;
    public float backCirclingRadius = 5.0f;
    
    [Header("State Duration")]
    public float minStateDuration = 2.0f;
    public float maxStateDuaration = 5.0f;
}
