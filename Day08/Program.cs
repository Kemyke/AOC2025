var input = File.ReadAllLines("input.txt");

long ret1 = 0;
long ret2 = 0;

Dictionary<int, Point> points = new Dictionary<int, Point>();
var circuits = new List<HashSet<int>>();
var distances = new Dictionary<double, (int, int)>();

for (int i = 0; i < input.Length; i++)
{
    var s = input[i].Split(',');
    points.Add(i, new Point { id = i, x = int.Parse(s[0]), y = int.Parse(s[1]), z = int.Parse(s[2]) });
}

for (int i = 0; i < input.Length - 1; i++)
{
    for (int j = i + 1; j < input.Length; j++)
    {
        distances.Add(points[i].Dist(points[j]), (i,j));
    }
}

var minDists = distances.OrderBy(k => k.Key).ToList();
for(int i = 0; i < minDists.Count;i++)
{
    var x = minDists[i];

    var c1 = circuits.SingleOrDefault(k => k.Contains(x.Value.Item1));
    var c2 = circuits.SingleOrDefault(k => k.Contains(x.Value.Item2));
   
    if (c1 == null && c2 == null)
    {
        circuits.Add(new HashSet<int> { x.Value.Item1, x.Value.Item2 });
    }
    else if (c1 == c2)
    {

    }
    else if (c1 != null && c2 != null)
    {
        circuits.Remove(c1);
        circuits.Remove(c2);
        circuits.Add(c1.Union(c2).ToHashSet());
    }
    else if (c1 != null)
    {
        c1.Add(x.Value.Item1);
        c1.Add(x.Value.Item2);
    }
    else if (c2 != null)
    {
        c2.Add(x.Value.Item1);
        c2.Add(x.Value.Item2);
    }
    else
    {
        throw new Exception();
    }

    if (circuits.Count == 1 && circuits[0].Count == points.Count)
    {
        ret2 = points[x.Value.Item1].x * points[x.Value.Item2].x;
        break;
    }

    if(i == 1000)
    {
        ret1 = circuits.Select(c => c.Count).OrderByDescending(c => c).Take(3).Aggregate((a, b) => a * b);
    }
}

Console.WriteLine(ret1);
Console.WriteLine(ret2);
Console.ReadLine();

class Point
{
    public int id;
    public long x;
    public long y;
    public long z;

    public double Dist(Point p2)
    {
        return Math.Sqrt(Math.Pow(x - p2.x, 2) + Math.Pow(y - p2.y, 2) + Math.Pow(z - p2.z, 2));
    }
}