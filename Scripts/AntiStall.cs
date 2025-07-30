namespace AeLa.Utilities
{
	/// <summary>
	/// Prevents loops from accidentally hard-locking the app
	/// </summary>
	public struct AntiStall
	{
		private const int defaultMax = 10000;

		private int remaining;

		private AntiStall(int maxIterations)
		{
			remaining = maxIterations;
		}

		public bool Check()
		{
			if (remaining-- <= 0)
			{
				throw new($"{nameof(AntiStall)} triggered");
			}

			return true;
		}

		public static AntiStall Create(int maxIterations = defaultMax)
		{
			return new(maxIterations);
		}
	}
}