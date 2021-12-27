using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints;
    [SerializeField] float moveSpeed = 3f;
    private int waypointIndex = 0;

    //private GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[1].transform.position;
    }

    // Update is called once per frame
    void Update() 
    {
        Move();
    }

    void Move()
    {
        print(waypoints.Count);
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetposition = waypoints[waypointIndex].transform.position;
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetposition, step);

            if (transform.position == targetposition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }

    }

}
