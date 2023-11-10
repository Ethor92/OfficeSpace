using System.Collections;
using System.Collections.Generic;

using UnityEngine;


    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float heightOfAI = 1.5f;
        [SerializeField] bool canSeePlayer = false;
        CapsuleCollider playerCollider;
        Fighter fighter;
        GameObject player;

        private void Start() {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            playerCollider = GameObject.FindWithTag("Player").GetComponent<CapsuleCollider>();
        }

        private void Update()
        {
            if(CanSeePlayer(player))
            {
            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                fighter.Attack(player);
            }
            }
            else
            {
                fighter.Cancel();
            }
            
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }

      bool CanSeePlayer(GameObject player)
    {
        float heightOfPlayer = 1.2f;
 
        Vector3 startVec = transform.position;
        startVec.y += heightOfPlayer;
        Vector3 startVecFwd = transform.forward;
        startVecFwd.y += heightOfPlayer;
 
        RaycastHit hit;
        Vector3 rayDirection = player.transform.position - startVec;
        Debug.DrawRay(startVec, rayDirection, Color.yellow, 5f);
        // If the ObjectToSee is close to this object and is in front of it, then return true
        if ((Vector3.Angle(rayDirection, startVecFwd)) < 110 &&
            (Vector3.Distance(startVec, player.transform.position) <= 20f))
        {
            Debug.Log("close");
            //return true;
        }
        if ((Vector3.Angle(rayDirection, startVecFwd)) < 90 &&
            Physics.Raycast(startVec, rayDirection, out hit, 100f))
        { // Detect if player is within the field of view
 
            if (hit.collider.tag == "Player")
            {
                Debug.Log("Can see player");
                return true;
            }
            else
            {
                Debug.Log("Can not see player");
                return false;
            }
        }
        return false;
    }
    }

