using UnityEngine;

namespace AeLa.Utilities.Physics
{
	[RequireComponent(typeof(Rigidbody))]
	public class Jiggle : MonoBehaviour 
	{
		[Header("Spring")]
		[SerializeField] private float strength = 2f;
		[SerializeField] private float damp = 0.1f;
		[SerializeField] private Vector3 scale = Vector3.one;

		[Header("Impact response")]
		[SerializeField] private Vector3 impactStrength = Vector3.one;
		[SerializeField] private float impactStrengthGlobal = 0.5f;

		private Rigidbody body;

		private Vector3 baseScale;
		private Vector3 displacement;
		private Vector3 velocity;
		private Vector3 lastRBVelo;

		private float currentDamp;

		private void Awake()
		{
			baseScale = transform.localScale;
			body = GetComponent<Rigidbody>();
			currentDamp = damp;
		}

		private void OnEnable()
		{
			lastRBVelo = body.velocity;
		}

		private void OnDisable()
		{
			transform.localScale = baseScale;
		}

		private void FixedUpdate()
		{
			var dt = Time.fixedDeltaTime;

			var deltaVelo = body.velocity - lastRBVelo;
			var impact = deltaVelo * -impactStrengthGlobal;
			impact.Scale(impactStrength);
			velocity += impact;

			var sA = -strength * displacement - currentDamp * velocity;
			velocity += sA;
			displacement += velocity * dt;

			// todo: clamp scale

			var scaledDisp = displacement;
			scaledDisp.Scale(scale);
			var t = transform;
			t.localScale = baseScale + t.InverseTransformDirection(scaledDisp);

			currentDamp = damp;

			lastRBVelo = body.velocity;
		}

		public void OverrideDamp(float damp)
		{
			currentDamp = damp;
		}
	}
}