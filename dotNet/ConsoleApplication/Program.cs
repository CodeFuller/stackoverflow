using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ConsoleApplication
{
	class Program
	{
		private static DateTime[] times = new DateTime[3];

		static void Main(string[] args)
		{
			var baseDir = Path.Combine(Path.GetTempPath(), "ACL-PROBLEM");
			var paths = new[]
			{
				Path.Combine(baseDir, "LEVEL-1"),
				Path.Combine(baseDir, "LEVEL-1", "LEVEL-2"),
				Path.Combine(baseDir, "LEVEL-1", "LEVEL-2", "LEVEL-3")
			};
			var directoryInfos = paths.Select(p => new DirectoryInfo(p)).ToArray();

			if (Directory.Exists(baseDir))
			{
				Directory.Delete(baseDir, true);
			}

			//create folders and files, so the ACL takes some time to apply
			foreach (var dir in paths)
			{
				Directory.CreateDirectory(dir);

				for (int i = 0; i < 1000; i++)
				{
					var id = string.Format("{0:000}", i);
					File.WriteAllText(Path.Combine(dir, id + ".txt"), id);
				}
			}

			var sids = new[]
			{
				"S-1-5-21-448539723-725345543-1417001333-1111111",
				"S-1-5-21-448539723-725345543-1417001333-2222222",
				"S-1-5-21-448539723-725345543-1417001333-3333333"
			};

			var directorySecurities = directoryInfos.Select(di => di.GetAccessControl()).ToArray();

			var securityIdentifiers = sids.Select(s => new SecurityIdentifier(s));

			var fileSystemAccessRules = securityIdentifiers.Select(s => new FileSystemAccessRule(s, FileSystemRights.Modify | FileSystemRights.Synchronize,
				InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
				//InheritanceFlags.None,
				PropagationFlags.None,
				AccessControlType.Allow)).ToArray();

			var taskList = new List<Task>();
			for (int i = 0; i < directoryInfos.Length; i++)
			{
				directorySecurities[i].ResetAccessRule(fileSystemAccessRules[i]);
				taskList.Add(CreateTask(i + 1, directoryInfos[i], directorySecurities[i]));
			}

			//for (int i = directoryInfos.Length - 1; i >= 0; --i)
			////for (int i = 0; i < directoryInfos.Length; ++i)
			//{
			//	taskList[i].Start();
			//	taskList[i].Wait();
			//}

			Parallel.ForEach(taskList, t => t.Start());
			Task.WaitAll(taskList.ToArray());

			foreach (var t in times)
			{
				Console.WriteLine(t.ToString("hh:mm:ss.fff"));
			}

			foreach (var dirInfo in directoryInfos)
			{
				DumpAclForDirectory(dirInfo);
			}
		}

		private static Task CreateTask(int i, DirectoryInfo directoryInfo, DirectorySecurity directorySecurity)
		{
			return new Task(() =>
			{
				var sw = Stopwatch.StartNew();
				var start = DateTime.Now;
				//Debug.WriteLine("Task {0} start:  {1:HH:mm:ss.fffffff}", i, start);
				directoryInfo.SetAccessControl(directorySecurity);
				sw.Stop();
				times[i - 1] = DateTime.Now;
				//Debug.WriteLine("Task {0} finish: {1:HH:mm:ss.fffffff} ({2} ms)", i, DateTime.Now, (DateTime.Now - start).TotalMilliseconds);
				Console.WriteLine($"Task {i}: {sw.ElapsedMilliseconds}");
			});
		}

		private static void DumpAclForDirectory(DirectoryInfo directoryInfo)
		{
			DirectorySecurity directorySecurity = directoryInfo.GetAccessControl();
			Console.WriteLine($"ACL for {directoryInfo.Name}:");
			foreach (FileSystemAccessRule rule in directorySecurity.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount)))
			{
				var sid = rule.IdentityReference.Value;
				if (sid.StartsWith("S-1-5-21-448539723-725345543-1417001333"))
				{
					Console.WriteLine($"\t{sid}{(rule.IsInherited ? "\tInherited" : String.Empty)}");
				}
			}
			Console.WriteLine();
		}
	}
}
