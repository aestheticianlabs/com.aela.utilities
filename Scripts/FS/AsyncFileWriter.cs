using System.IO;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace AeLa.Utilities.FS
{
	public class AsyncFileWriter
	{
		public readonly string FilePath;

		private readonly SemaphoreSlim semaphore = new(1, 1);
		private bool directoryExists;
		private int activeTaskCount;

		[PublicAPI]
		public bool IsWriting => activeTaskCount > 0;

		public AsyncFileWriter(string filePath)
		{
			FilePath = filePath;
		}

		public void Write(string data)
		{
			// start a write thread
			Task.Run(async () => await WriteAsync(data)).ContinueWith(
				t =>
				{
					if (t.IsCompleted)
					{
						activeTaskCount--;
					}

					if (t.IsFaulted)
					{
						Debug.LogException(t.Exception);
						throw t.Exception!;
					}
				}, TaskScheduler.FromCurrentSynchronizationContext()
			);
			activeTaskCount++;
		}

		private void CreateDirectoryIfNotExists()
		{
			if (directoryExists) return;

			directoryExists = Directory.Exists(Path.GetDirectoryName(FilePath));
			if (directoryExists) return;

			// create target directory
			Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
		}

		public async Task WriteAsync(string data)
		{
			await semaphore.WaitAsync();

			CreateDirectoryIfNotExists();
			await File.WriteAllTextAsync(FilePath, data);

			semaphore.Release();
		}
	}
}