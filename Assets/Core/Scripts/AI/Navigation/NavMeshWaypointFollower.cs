using UnityEngine;
using UnityEngine.UIElements;

namespace CGL.Navigation
{
	[RequireComponent(typeof(NavMeshMover))]
	public class NavMeshWaypointFollower : WaypointFollower
	{
		private NavMeshMover navMeshMover;

		void Awake()
		{
			navMeshMover = GetComponent<NavMeshMover>();
		}

		protected override void Update()
		{
			if (currentWaypoint == null) return;

			// AtDestination fallback in case trigger doesn't fire
			navMeshMover.Destination = currentWaypoint.transform.position;
			float distance = Vector3.Distance(navMeshMover.transform.position, currentWaypoint.transform.position);
		}

		public override void SetNextWaypoint(Waypoint waypoint)
		{
			base.SetNextWaypoint(waypoint);
			if (currentWaypoint == null) return;
			navMeshMover.Destination = currentWaypoint.transform.position;
			navMeshMover.Speed = speed;
		}
	}
}