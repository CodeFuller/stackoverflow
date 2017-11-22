using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication
{
	class Program
	{
		public static IEnumerable<List<T>> SplitArray<T>(T[] arr, int splitsNumber)
		{
			var list = arr.ToList();
			int size = list.Count / splitsNumber;
			int pos = 0;
			for (int i = 0; i + 1 < splitsNumber; ++i, pos += size)
			{
				yield return list.GetRange(pos, size);
			}

			yield return list.GetRange(pos, list.Count - pos);
		}

		static void Main(string[] args)
		{
			var arr = new int[] {1, 2, 3, 4, 5, 6, 7};
			var res = SplitArray(arr, 3).ToList();
		}
	}
}
