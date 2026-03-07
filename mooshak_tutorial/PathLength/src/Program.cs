
using System;

using Path = Geometric.Path;

public class Program
{
	public static void Main(string[] args)
	{
		Path p = new();

		int n = Scanner.ReadLineAsInt();

		for (int i=0; i<n; i++) {
			float[] data = Scanner.ReadLineAsFloats();
			p.add(new(data[0], data[1]));
		}

		//System.Console.WriteLine(p);
		System.Console.WriteLine("{0:F3}", p.length());
	}
}
