
using System;

namespace Geometric {

	public class Point {

		private double _x;
		private double _y;

		public Point(double x, double y) {
			_x = x;
			_y = y;
		}

		public double x() { return _x; }
		public double y() { return _y; }

		public double distance(Point p) {
			double dx = p.x() - _x;
			double dy = p.y() - _y;
			return Math.Sqrt(dx*dx + dy*dy);
		}

		public override string ToString() {
			return String.Format("({0:F2},{1:F2})", _x, _y);
		}

	} // end class

} // end namespace
