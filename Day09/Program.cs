using System.ComponentModel.Design.Serialization;
using System.Text;

bool Inside(List<Point> reds, List<HLine> hlines, List<VLine> vlines, Point p)
{
    if (reds.Any(r => r.x == p.x && r.y == p.y))
        return true;
    if (hlines.Any(r => r.On(p)))
        return true;

    if (vlines.Any(r => r.On(p)))
        return true;

    HLine h1 = new HLine { p1 = new Point { x = 0, y = p.y }, p2 = p };
    HLine h2 = new HLine { p1 =  p, p2 = new Point { x = 100000, y = p.y } };
    VLine v1 = new VLine { p1 = new Point { x = p.x, y = 0 }, p2 = p };
    VLine v2 = new VLine { p1 = p, p2 = new Point { x = p.x, y = 100000 } };

    var ret1 = vlines.Count(l => l.Intersect(h1));
    ret1 += hlines.Count(l => l.Intersect(h1));
    var ret2 = vlines.Count(l => l.Intersect(h2));
    ret2 += hlines.Count(l => l.Intersect(h2));

    var ret3 = vlines.Count(l => l.Intersect(v1));
    ret3 += hlines.Count(l => l.Intersect(v1));
    var ret4 = vlines.Count(l => l.Intersect(v2));
    ret4 += hlines.Count(l => l.Intersect(v2));
    return (ret1 % 2 == 1) && (ret2 % 2 == 1) && (ret3 % 2 == 1) && (ret4 % 2 == 1);
}

bool Outside(List<Point> reds, List<HLine> hlines, List<VLine> vlines, Point p)
{
    HLine h1 = new HLine { p1 = new Point { x = 0, y = p.y }, p2 = p };
    HLine h2 = new HLine { p1 = p, p2 = new Point { x = 100000, y = p.y } };
    VLine v1 = new VLine { p1 = new Point { x = p.x, y = 0 }, p2 = p };
    VLine v2 = new VLine { p1 = p, p2 = new Point { x = p.x, y = 100000 } };

    var ret1 = vlines.Count(l => l.Intersect(h1));
    var ret2 = vlines.Count(l => l.Intersect(h2));
    var ret3 = hlines.Count(l => l.Intersect(v1));
    var ret4 = hlines.Count(l => l.Intersect(v2));
    return ret1 == 0 || ret2 == 0 || ret3 == 0 || ret4 == 0;
}

var input = File.ReadAllLines("input.txt");

long ret1 = 0;
long ret2 = 0;
var reds = new List<Point>();
var hlines = new List<HLine>();
var vlines = new List<VLine>();

foreach (string line in input)
{
    var s = line.Split(',');
    var p = new Point { x = long.Parse(s[0]), y = long.Parse(s[1])};
    reds.Add(p);
}

for(int i = 0; i < reds.Count; i++)
{
    var p1 = reds[i];
    Point p2;
    if (i == reds.Count - 1)
    {
        p2 = reds[0];
    }
    else
    {
        p2 = reds[i + 1];
    }

    if (p1.y == p2.y)
    {
        if (p1.x < p2.x)
        {
            hlines.Add(new HLine { p1 = p1, p2 = p2 });
        }
        else
        {
            hlines.Add(new HLine { p1 = p2, p2 = p1 });
        }
    }
    else
    {
        if (p1.y < p2.y)
        {
            vlines.Add(new VLine { p1 = p1, p2 = p2 });
        }
        else
        {
            vlines.Add(new VLine { p1 = p2, p2 = p1 });
        }
    }
}

foreach(var p in reds)
{
    var p1 = new Point { x = p.x + 1, y = p.y };
    var p2 = new Point { x = p.x - 1, y = p.y };
    var p3 = new Point { x = p.x, y = p.y + 1 };
    var p4 = new Point { x = p.x, y = p.y - 1 };

    var o1 = Outside(reds, hlines, vlines, p1);
    var o2 = Outside(reds, hlines, vlines, p2);
    var o3 = Outside(reds, hlines, vlines, p3);
    var o4 = Outside(reds, hlines, vlines, p4);

    p.insideEdge = !o1 && !o2 && !o3 && !o4;
}

hlines.Clear();
vlines.Clear();

for (int i = 0; i < reds.Count; i++)
{
    var p1 = reds[i];
    Point p2;
    if (i == reds.Count - 1)
    {
        p2 = reds[0];
    }
    else
    {
        p2 = reds[i + 1];
    }

    if (p1.y == p2.y)
    {
        if (p1.x < p2.x)
        {
            hlines.Add(new HLine { p1 = p1.insideEdge ? new Point { x = p1.x + 1, y = p1.y } : p1, p2 = p2.insideEdge ? new Point { x = p2.x - 1, y = p2.y } : p2 });
        }
        else
        {
            hlines.Add(new HLine { p1 = p2.insideEdge ? new Point { x = p2.x + 1, y = p2.y } : p2, p2 = p1.insideEdge ? new Point { x = p1.x - 1, y = p1.y } : p1 });
            //hlines.Add(new HLine { p1 = p2, p2 = p1 });
        }
    }
    else
    {
        if (p1.y < p2.y)
        {
            vlines.Add(new VLine { p1 = p1.insideEdge ? new Point { x = p1.x, y = p1.y + 1 } : p1, p2 = p2.insideEdge ? new Point { x = p2.x, y = p2.y - 1 } : p2 });

            //vlines.Add(new VLine { p1 = p1, p2 = p2 });
        }
        else
        {
            vlines.Add(new VLine { p1 = p2.insideEdge ? new Point { x = p2.x, y = p2.y + 1 } : p2, p2 = p1.insideEdge ? new Point { x = p1.x, y = p1.y - 1 } : p1 });

            //vlines.Add(new VLine { p1 = p2, p2 = p1 });
        }
    }
}

for (int i = 0; i < reds.Count - 1; i++)
{
    for (int j = i + 1; j < reds.Count; j++)
    {
        var r1 = reds[i];
        var r2 = reds[j];

        if(r1.x == 9 && r1.y == 5)
        {

        }

        var a = r2.Area(r1);
        if (a > ret1)
            ret1 = a;

        if (a > ret2)
        {
            if (a == 50)

            {

            }
            var p1 = new Point { x = Math.Min(r1.x, r2.x) + 1, y = Math.Min(r1.y, r2.y) + 1 };
            var p2 = new Point { x = Math.Max(r1.x, r2.x) - 1, y = Math.Min(r1.y, r2.y) + 1 };
            var p3 = new Point { x = Math.Min(r1.x, r2.x) + 1, y = Math.Max(r1.y, r2.y) - 1 };
            var p4 = new Point { x = Math.Max(r1.x, r2.x) - 1, y = Math.Max(r1.y, r2.y) - 1 };

            var ip1 = !Outside(reds, hlines, vlines, p1);
            var ip2 = !Outside(reds, hlines, vlines, p2);
            var ip3 = !Outside(reds, hlines, vlines, p3);
            var ip4 = !Outside(reds, hlines, vlines, p4);

            var h1 = new HLine { p1 = p1, p2 = p2 };
            var h2 = new HLine { p1 = p3, p2 = p4 };
            var v1 = new VLine { p1 = p1, p2 = p3 };
            var v2 = new VLine { p1 = p2, p2 = p4 };

            var v1i = vlines.Count(l => l.Intersect(h1));
            var v2i = vlines.Count(l => l.Intersect(h2));

            var h1i = hlines.Count(l => l.Intersect(v1));
            var h2i = hlines.Count(l => l.Intersect(v2));


            if (ip1 && ip2 && ip3 && ip4 && v1i < 1 && v2i < 1 && h1i < 1 && h2i < 1)
            {
                ret2 = a;
            }
        }
    }
}

Console.WriteLine(ret1);
Console.WriteLine(ret2);
//4583207265
//4507884615
//377637900
//2994210000
//2966707500
//1510545727
Console.ReadLine();

class VLine
{
    public Point p1;
    public Point p2;

    public bool Intersect(VLine vline)
    {
        if (vline.p1.x == p1.x)
        {
            if (vline.p2.y < p1.y || p2.y < vline.p1.y)
            {
                return false;
            }
            return true;
        }
        return false;
    }

    public bool Intersect(HLine hline)
    {
        if (hline.p1.x <= p1.x && hline.p2.x >= p1.x)
        {
            if (p1.y <= hline.p1.y && p2.y >= hline.p2.y)
            {
                return true;
            }
        }
        return false;
    }

    public bool On(Point p)
    {
        if(p.x == p1.x)
        {
            if (p.y >= p1.y && p.y <= p2.y)
                return true;
        }
        return false;
    }
}


class HLine
{
    public Point p1;
    public Point p2;

    public bool Intersect(HLine hline)
    {
        if (hline.p1.y == p1.y)
        {
            if(hline.p2.x < p1.x || p2.x < hline.p1.x)
            {
                return false;
            }
            return true;
        }
        return false;
    }

    public bool Intersect(VLine vline)
    {
        if(vline.p1.y <= p1.y && vline.p2.y >= p1.y)
        {
            if(p1.x <= vline.p1.x && p2.x >= vline.p2.x)
            {
                return true;
            }
        }
        return false;
    }

    public bool On(Point p)
    {
        if (p.y == p1.y)
        {
            if (p.x >= p1.x && p.x <= p2.x)
                return true;
        }
        return false;
    }
}

class Point
{
    public long x;
    public long y;
    public bool insideEdge = false;

    public long Area(Point p2)
    {
        return (Math.Abs(x - p2.x) + 1) * (Math.Abs(y - p2.y) + 1);
    }
}