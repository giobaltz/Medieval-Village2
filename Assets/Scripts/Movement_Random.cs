using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement_Random : MonoBehaviour
{

    NavMeshAgent navMeshAgent;
    public float timeForNewPath = 1f;
    bool inCoRoutine;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    Vector3 getNewRandomPosition()
    {
        float x = Random.Range(-100, 100);
        float z = Random.Range(-100, 100);

        Vector3 pos = new Vector3(x, 0, z);
        return pos;
    }

    IEnumerator DoSomething()
    {
        inCoRoutine = true;
        yield return new WaitForSeconds(timeForNewPath);
        GetNewPath();
        inCoRoutine = false;
    }

    void GetNewPath()
    {
        navMeshAgent.SetDestination(getNewRandomPosition());
    }

    // Update is called once per frame
    void Update()
    {
        if (!inCoRoutine)
        {
            StartCoroutine(DoSomething());
        }
    }
}
