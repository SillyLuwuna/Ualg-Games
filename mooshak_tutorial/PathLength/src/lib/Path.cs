
using System.Collections.Generic;

namespace Geometric {

	public class Path {
		private List<Point> points = new();

		public void add(Point p) { points.Add(p);}

		public double length() {
			double l = 0;
			for (int i=1; i<points.Count; i++)
				l += points[i-1].distance(points[i]);
			return l;
		}

		public override string ToString() {
			string s = "";

			foreach (Point p in points)
				s += p + " ";

			return s.TrimEnd(); // remove trailing space
		}
	} // end class

} //end namespace

