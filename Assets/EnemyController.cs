using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{

    public Vector3[] pathPoints;
    private NavMeshAgent agent;
    public float waitTime = 3;
    private float timer = 0;

    private int index = 0;

    private bool chasing = false;

    public GameObject player;

    private SphereCollider col;

    public float FOV = 90;

    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        col = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!chasing)
        {
            if (timer <= 0)
            {
                agent.SetDestination(pathPoints[index]);
                index++;
                timer = waitTime;
                if (index > pathPoints.Length - 1)
                {
                    index = 0;
                }
            }

            if (agent.remainingDistance != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
            {
                timer -= Time.deltaTime;
            }




            if(Input.GetKeyDown(KeyCode.Space) && Vector3.Distance(transform.position, player.transform.position) < 8 && Vector3.Dot(transform.TransformDirection(Vector3.forward), Vector3.Normalize(player.transform.position - transform.position)) < 0)
            Destroy(gameObject);
        }
        else
        {
            pos = player.transform.position;
            agent.SetDestination(pos);

            Vector3 direction = player.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            RaycastHit hit;
            Debug.DrawRay(transform.position + transform.up * 0.3f, direction.normalized * col.radius * 4, Color.red, 0);
            if (Physics.Raycast(transform.position + transform.up * 0.3f, direction.normalized, out hit, col.radius * 4))
            {
                if (hit.collider.gameObject != player || Vector3.Distance(transform.position, player.transform.position) > col.radius * 5)
                {
                    timer -= Time.deltaTime;
                    if (timer <= 0)
                    {
                        chasing = false;
                        timer = 3f;
                        agent.SetDestination(gameObject.transform.position);
                    }
                }
                else
                {
                    timer = 1f;
                }
            }
            if (Vector3.Distance(transform.position, player.transform.position) < 5) {
                Debug.Log("AH");
                SceneManager.LoadScene("george's updated rooms");
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject == player)
        {
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            RaycastHit hit;
            if (!chasing)
            {
                Debug.DrawRay(transform.position + transform.up * 0.3f, direction.normalized * col.radius * 4, Color.green, 1);
            }
            if (angle < FOV * 0.5)
            {
                if (Physics.Raycast(transform.position + transform.up * 0.3f, direction.normalized, out hit, col.radius * 4))
                {
                    if (hit.collider.gameObject == player)
                    {
                        chasing = true;
                        timer = 1f;
                    }
                }
            }
        }
    }

}
