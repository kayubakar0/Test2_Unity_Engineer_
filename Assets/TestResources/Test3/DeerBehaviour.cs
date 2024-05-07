using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TestResources.Test3
{
    public class DeerBehaviour : MonoBehaviour
    {
        private IBaseState _currentState;
        public PatrolState patrolState = new PatrolState();
        public ChaseState chaseState = new ChaseState();
        
        
        //list waypoint
        public List<Transform> waypoints = new List<Transform>();
        
        //waypoint mushroom
        [Header("Mushroom")]
        public float chaseDistance = 1f;
        public GameObject mushroom;
        public float interval = 25f;
        private float _elapsedTime = 0f;
        
        //Navmesh Agent
        public NavMeshAgent NavMeshAgent { get; private set; }
        
        //Animator
        public Animator animatorDeer;

        private void Awake()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();
            animatorDeer = GetComponentInChildren<Animator>();
            
            _currentState = patrolState;
            _currentState.EnterState(this);
        }
        
        private void Update()
        {
            if (_currentState != null)
            {
                _currentState.UpdateState(this);
            }
            
            if (!mushroom.activeSelf)
            {
                _elapsedTime += Time.deltaTime;
                
                if (_elapsedTime >= interval)
                {
                    mushroom.SetActive(true);
                    _elapsedTime = 0f;
                }
            }
        }
        
        public void SwictchState(IBaseState state)
        {
            _currentState.ExitState(this);
            _currentState = state;
            _currentState.EnterState(this);
        }

        public void EatMushroom()
        {
            StartCoroutine(DelayEatMushroom());
        }
        
        IEnumerator DelayEatMushroom()
        {
            animatorDeer.SetBool("isWalking", false);
            yield return new WaitForSeconds(0.5f);
            animatorDeer.SetTrigger("eat");
            yield return new WaitForSeconds(3f);
            mushroom.SetActive(false);
            yield return new WaitForSeconds(2f);
            SwictchState(patrolState);
        }
        
    }
}
