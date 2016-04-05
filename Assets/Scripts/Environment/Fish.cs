using UnityEngine;
using System.Collections;

public class Fish : MonoBehaviour
{

    public Transform[] goals;
    private Transform goal;
    private int currentGoal = 0;

    void Start()
    {
        goal = goals[0];
    }

    void Update()
    {
        if (Mathf.Abs(transform.position.x - goal.position.x) < 0.1f && Mathf.Abs(transform.position.z - goal.position.z) < 0.1f)
        {
            if (currentGoal == goals.Length-1)
                currentGoal = 0;
            else
                currentGoal++;
            goal = goals[currentGoal];
        }
        Vector3 toTarget = goal.position - transform.position;
        toTarget.y = 0;

        transform.forward = Vector3.MoveTowards(transform.forward, toTarget, 2 * Time.deltaTime);
        transform.position += transform.forward * Time.deltaTime * 0.8f;
    }
}