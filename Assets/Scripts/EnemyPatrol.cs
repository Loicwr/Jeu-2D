using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public SpriteRenderer graphics;
    public float speed;
    public Transform[] waypoints;

    private Transform target;
    private int destPoint = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {    // système de déplacement du serpant
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        // si l'ennemi est quasiment arrivé à sa destination
        if(Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];
            graphics.flipX = !graphics.flipX;
        }
    }
}
