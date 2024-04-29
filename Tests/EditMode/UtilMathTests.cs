using NUnit.Framework;

namespace AeLa.Utilities.Tests.EditMode.Tests.EditMode
{
	public class UtilMathTests
	{
		[Test]
		public void Wrap_Int_IsWithinRange()
		{
			// within range
			Assert.AreEqual(0, Util.Math.Wrap(0, -5, 5));

			// lower bound inclusive
			Assert.AreEqual(0, Util.Math.Wrap(0, -5, 5));

			// upper bound exclusive
			Assert.AreEqual(-5, Util.Math.Wrap(5, -5, 5));

			// positive wrap
			Assert.AreEqual(-4, Util.Math.Wrap(6, -5, 5));

			// negative wrap
			Assert.AreEqual(4, Util.Math.Wrap(-6, -5, 5));
		}

		[Test]
		public void Wrap_Float_IsWithinRange()
		{
			// within range
			Assert.AreEqual(0f, Util.Math.Wrap(0f, -5f, 5f));

			// lower bound inclusive
			Assert.AreEqual(0f, Util.Math.Wrap(0f, -5f, 5f));

			// upper bound exclusive
			Assert.AreEqual(-5f, Util.Math.Wrap(5f, -5f, 5f));

			// positive wrap
			Assert.AreEqual(-4f, Util.Math.Wrap(6f, -5f, 5f));

			// negative wrap
			Assert.AreEqual(4f, Util.Math.Wrap(-6f, -5f, 5f));
		}
	}
}