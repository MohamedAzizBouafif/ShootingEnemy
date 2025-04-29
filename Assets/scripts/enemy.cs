using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class enemy : MonoBehaviour
{

    NavMeshAgent myPath;
    public Transform target;

    void Start()
    {
        myPath = GetComponent<NavMeshAgent>();
       
        StartCoroutine(UpdatePath());
    }
    IEnumerator UpdatePath()
    {
        float refreshPath = 0.25f;

       
        while(target != null)
        {
            Vector3 targetToDirection = new Vector3(target.position.x, 2, target.position.z);
            myPath.SetDestination(targetToDirection);
            yield return new WaitForSeconds(refreshPath);
        }
    }


}
