
using System;
using System.Linq;

#nullable enable

class Scanner {
	public static string ReadLine() {
		string? data = Console.ReadLine();
		return data ?? "";
	}

	public static int ReadLineAsInt() {
		string? data = Console.ReadLine();
		return int.Parse(data ?? "0");
	}

	public static int[] ReadLineAsInts(char separator=' ') {
		string? data = Console.ReadLine();
		return (data ?? "").Trim().Split(separator).Select(p => int.Parse(p)).ToArray();
	}

	public static float[] ReadLineAsFloats(char separator=' ') {
		string? data = Console.ReadLine();
		return (data ?? "").Trim().Split(separator).Select(p => float.Parse(p)).ToArray();
	}

	public static double[] ReadLineAsDoubles(char separator=' ') {
		string? data = Console.ReadLine();
		return (data ?? "").Trim().Split(separator).Select(p => double.Parse(p)).ToArray();
	}

}
