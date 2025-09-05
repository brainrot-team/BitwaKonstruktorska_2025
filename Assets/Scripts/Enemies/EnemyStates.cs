using System;
using UnityEngine;




public abstract class EnemyState
{

    protected GameObject gameobject;

    public virtual void Enter(GameObject go)
    {
        gameobject = go;

    }

    public virtual void Update()
    {

    }

    public virtual void Exit()
    {

    }
    
}


public class RandomPatrol : EnemyState
{
    Vector2 targetPosition;
    Transform transform;
    Rigidbody2D rb;
    float speed = 10;

    public override void Enter(GameObject go)
    {
        base.Enter(go);
        transform = go.GetComponent<Transform>();
        rb = go.GetComponent <Rigidbody2D>();
        

        FindNewTargetPosition();

    }


    public override void Update()
    {
        if(Vector2.Distance(transform.position, targetPosition) < 0.25)
        {
            FindNewTargetPosition();
        }


        rb.MovePosition(Vector2.MoveTowards(transform.position,targetPosition,Time.deltaTime * speed));
        Debug.DrawLine(transform.position,rb.position);

    }
     
    private void FindNewTargetPosition()
    {
        targetPosition = (Vector2)gameobject.transform.position + new Vector2(UnityEngine.Random.Range(-5, 5), UnityEngine.Random.Range(-5, 5));

        var bounds = WorldManager.Instance.WorldBounds;
        targetPosition.x = Mathf.Clamp(targetPosition.x, -bounds.x, bounds.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -bounds.y, bounds.y);
        
    }



}
