using System;
using NaughtyAttributes;
using UnityEngine;

namespace AeLa.Utilities.Physics
{
	public class DetectGround : MonoBehaviour
	{
		[System.Serializable]
		public class Raycast
		{
			/// <summary>
			/// The LayerMask used when raycasting
			/// </summary>
			[Tooltip("The LayerMask used when raycasting")]
			[SerializeField] private LayerMask layerMask = ~0;

			/// <summary>
			/// Tag to ignore on raycast, if any
			/// </summary>
			[Tooltip("Tag to ignore on raycast, if any")]
			[SerializeField] private string ignoreTag;

			/// <summary>
			/// The maximum distance of the raycast
			/// </summary>
			[Tooltip("The maximum distance of the raycast")]
			[SerializeField] private float distance = 0.2f;

			/// <summary>
			/// Whether the ray should point down relative to global Y or the local Y
			/// </summary>
			[Tooltip("Whether the ray should point down relative to global Y or the local Y")]
			[SerializeField] private bool orientToWorld = true;

			/// <summary>
			/// Offset applied to the ray position
			/// </summary>
			[Tooltip("Offset applied to the ray position")]
			[SerializeField] private Vector3 offset = Vector3.zero;

			public float Distance => distance;

			public bool CastFrom(Transform transform, Rigidbody body, out RaycastHit hit, float distanceScale = 1f)
			{
				var ray = GetRayFor(transform, body);
				return UnityEngine.Physics.Raycast(
					ray, out hit, distance * distanceScale, layerMask,
					QueryTriggerInteraction.Ignore
				) && (string.IsNullOrEmpty(ignoreTag) ||!hit.collider.CompareTag(ignoreTag));
			}

			public Ray GetRayFor(Transform transform, Rigidbody body)
			{
				return body ? GetRayFor(body) : GetRayFor(transform);
			}

			public Ray GetRayFor(Transform transform)
			{
				var offset = transform.TransformDirection(this.offset);
				return GetRayFor(transform.position, transform.up, offset);
			}

			public Ray GetRayFor(Rigidbody body)
			{
				var rot = body.rotation;
				return GetRayFor(body.position, rot * Vector3.up, rot * offset);
			}

			private Ray GetRayFor(Vector3 position, Vector3 up, Vector3 offset)
			{
				return new(position + offset, -(orientToWorld ? Vector3.up : up));
			}
		}

		/// <summary>
		/// Frames between raycasts
		/// </summary>
		[Tooltip("Frames between raycasts")]
		public int PollRate = 1;

		// hack: an ez way to make the ground check bigger
		public float GlobalScale = 1f;

		public BoolUnityEvent OnGroundChanged;

		[SerializeField] Rigidbody body;
		[SerializeField] private Raycast[] raycasts = { new() };

		private bool isOnGround;

		/// <summary>
		/// Whether or not the object is currently on the ground
		/// </summary>
		[ShowNativeProperty]
		public bool IsOnGround
		{
			get => isOnGround;
			private set
			{
				if (isOnGround == value) return;
				isOnGround = value;
				OnGroundChanged.Invoke(value);
			}
		}

		/// <summary>
		/// The most recent <see cref="RaycastHit"/> that hit the ground
		/// </summary>
		public RaycastHit LastHit { get; private set; }
		
		public int LastPollFrame { get; private set; }

		public void SetGlobalScale(float value)
		{
			GlobalScale = value;
		}

		private void Reset()
		{
			body = GetComponentInParent<Rigidbody>();
		}

		private void OnDisable()
		{
			IsOnGround = false;
		}

		private void FixedUpdate()
		{
			if (Time.frameCount - LastPollFrame < PollRate) return;
			CheckOnGround();
		}

		private void CheckOnGround()
		{
			// check for each cast
			foreach (var raycast in raycasts)
			{
				if (raycast.CastFrom(transform, body, out var hit, GlobalScale))
				{
					LastHit = hit; // need to set before value in case any on change listeners need it
					IsOnGround = true;
					return;
				}
			}

			IsOnGround = false;
		}

		private void OnDrawGizmosSelected()
		{
			foreach (var raycast in raycasts)
			{
				var ray = raycast.GetRayFor(transform, Application.isPlaying ? body : null);

				Gizmos.color = Color.yellow;
				Gizmos.DrawRay(ray.origin, ray.direction * raycast.Distance * GlobalScale);
			}
		}
	}
}