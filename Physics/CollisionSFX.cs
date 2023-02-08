using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AeLa.Utilities.Physics
{
	/// <summary>
	/// Plays sound effects on collision
	/// </summary>
	[RequireComponent(typeof(AudioSource))]
	public class CollisionSFX : MonoBehaviour
	{
		public enum ImpactCalculationMethod
		{
			RelativeVelocity,
			Impulse
		}

		public AudioClip[] Clips;

		public AnimationCurve VolumeCurve = AnimationCurve.Linear(0f, 0f, 15f, 1f);
		public float MinPitch = 0.8f;
		public float MaxPitch = 1.1f;
		public float Cooldown = 0.05f;
		public ImpactCalculationMethod ImpactMethod = ImpactCalculationMethod.RelativeVelocity;

		private AudioSource source;
		private float lastPlayTime;

		private void Awake()
		{
			source = GetComponent<AudioSource>();
		}

		public void Play(float volume)
		{
			if (Time.time - lastPlayTime < Cooldown) return;

			source.pitch = Random.Range(MinPitch, MaxPitch);
			source.PlayOneShot(Clips.RandomElement(), volume);

			lastPlayTime = Time.time;
		}

		private void OnCollisionEnter(Collision collision) => Play(VolumeCurve.Evaluate(GetImpactForce(collision)));

		private float GetImpactForce(Collision collision)
		{
			return ImpactMethod switch
			{
				ImpactCalculationMethod.RelativeVelocity => collision.relativeVelocity.magnitude,
				ImpactCalculationMethod.Impulse => collision.impulse.magnitude,
				_ => throw new ArgumentOutOfRangeException()
			};
		}
	}
}