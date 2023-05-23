namespace DendriteTracer.Core;

public static class PathOperations
{
    /// <summary>
    /// Walk along a multi-point line and place evenly spaced subpoints along the way.
    /// </summary>
    public static Pixel[] GetSubPoints(IEnumerable<Pixel> points, double spacing)
    {
        List<Pixel> subPoints = new() { points.First() };
        double nextSetback = 0;
        Pixel lastPoint = points.First();
        foreach (Pixel point in points.Skip(1))
        {
            (List<Pixel> segmentPoints, double setback) = GetSubPoints(lastPoint, point, spacing, nextSetback);
            nextSetback = setback;
            subPoints.AddRange(segmentPoints);
            lastPoint = point;
        }
        return subPoints.ToArray();
    }

    /// <summary>
    /// Walk from point 1 to point 2 and place new subpoints along the way.
    /// The first subpoint will be set back by the given amount.
    /// The distance remaining between the last subpoint and point 2 is returned.
    /// </summary>
    private static (List<Pixel> points, double nextSetback) GetSubPoints(Pixel pt1, Pixel pt2, double spacing, double setback)
    {
        double dx = pt2.X - pt1.X;
        double dy = pt2.Y - pt1.Y;
        double distanceBetweenPoints = Math.Sqrt(dx * dx + dy * dy);
        double angle = Math.Atan(dy / dx);
        if (dx < 0)
            angle += Math.PI;

        List<Pixel> points = new();
        double travelled = spacing - setback;
        while (travelled <= distanceBetweenPoints)
        {
            double x = pt1.X + travelled * Math.Cos(angle);
            double y = pt1.Y + travelled * Math.Sin(angle);
            points.Add(new((int)x, (int)y));
            travelled += spacing;
        }

        double interPointTotal = (points.Count - 1) * spacing;
        double totalDistanceTravelled = spacing - setback + interPointTotal;
        double nextSetback = distanceBetweenPoints - totalDistanceTravelled;

        return (points, nextSetback);
    }
}
