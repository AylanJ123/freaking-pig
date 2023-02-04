using MyBox;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace freakingpig
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField, AutoProperty] private Seeker seeker;
        [SerializeField, AutoProperty] private Rigidbody2D rb;
        [SerializeField, InitializationField] float speed;
        [SerializeField, InitializationField] float searchArea;
        private Transform marta;
        private Path path;
        private int currentWaypoint;
        private Vector2 velocity;
        private bool reachedEnd;
        private float lockDownEnd;
        void Start()
        {
            marta = GameObject.FindGameObjectWithTag("Player").transform;
            InvokeRepeating(nameof(LookForPath), 0, .25f);
        }

        public void LockDown(float time)
        {
            lockDownEnd = Time.time + time;
        }

        public void LookForPath()
        {
            if (seeker.IsDone())
            {
                if ((marta.position - transform.position).magnitude < searchArea)
                    seeker.StartPath(transform.position, marta.position, PathFound);
                else if (reachedEnd)
                    seeker.StartPath(transform.position, transform.position + new Vector3(Random.Range(-8, 8), Random.Range(-8, 8), 0), PathFound);
            }
        }

        private void PathFound(Path p)
        {
            if (p.error) return;
            if (path != null && currentWaypoint < path.vectorPath.Count && path.vectorPath[currentWaypoint] == p.vectorPath[1]) return;
            path = p;
            currentWaypoint = 0;
        }

        private void Update()
        {
            if (path == null || lockDownEnd > Time.time) return;
            velocity = path.vectorPath[currentWaypoint >= path.vectorPath.Count ? path.vectorPath.Count - 1 : currentWaypoint] - transform.position;
            if (velocity.magnitude <= .1f) currentWaypoint = Mathf.Clamp(currentWaypoint + 1, 0, path.vectorPath.Count);
            reachedEnd = currentWaypoint >= path.vectorPath.Count;
            velocity.Normalize();
        }

        private void FixedUpdate()
        {
            rb.velocity = reachedEnd || lockDownEnd > Time.time ? Vector2.zero : velocity * speed;
            if (reachedEnd || lockDownEnd > Time.time)
            {
                LookForPath();
                return;
            }
            Vector3 diff = (transform.position + (Vector3) velocity - transform.position);
            diff.Normalize();
            float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new(0, 0, Mathf.LerpAngle(transform.eulerAngles.z, rotZ - 90, .2f));
        }
    }
}
