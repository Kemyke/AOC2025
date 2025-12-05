var input = File.ReadAllLines("input.txt");

var ret1 = 0;
long ret2 = 0;
var ranges = new List<(long, long)>();
int i = 0;
foreach(var line in input)
{
    i++;
    if (line == "")
        break;
    var ss = line.Split("-");
    var nr = (long.Parse(ss[0]), long.Parse(ss[1]));

    if(ranges.Any(r => r.Item1<= nr.Item1 && r.Item2>=nr.Item2))
    {
        continue;
    }
    ranges = ranges.Except(ranges.Where(r => nr.Item1 <= r.Item1 && nr.Item2 >= r.Item2)).ToList();

    var nri1 = nr.Item1;
    var nri2 = nr.Item2;

    foreach(var r2 in ranges)
    {
        if (r2.Item1 < nri2 && r2.Item2 > nri2)
            nri2 = r2.Item1;

        if(r2.Item2 > nri1 && r2.Item1 < nri1)
            nri1 = r2.Item2;
    }
    
    ranges.Add((nri1, nri2));
    
    foreach(var r2 in ranges.ToList())
    {
        var x = ranges.SingleOrDefault(r => r.Item2 == r2.Item1);
        var y = ranges.SingleOrDefault(r => r.Item1 == r2.Item2);
        if (x == default && y == default)
            continue;

        var ni1 = r2.Item1;
        var ni2 = r2.Item2;

        ranges.Remove(r2);
        if (x != default)
        {
            ranges.Remove(x);
            ni1 = x.Item1;
        }

        if (y != default)
        {
            ranges.Remove(y);
            ni2 = y.Item2;
        }
        ranges.Add((ni1, ni2));
    }
}

foreach(var r in ranges)
{
    ret2 += r.Item2 - r.Item1 + 1;
}

foreach (var v in input.Skip(i))
{
    var vv = long.Parse(v);
    if (ranges.Any(r => r.Item1 <= vv && r.Item2 >= vv))
        ret1++;
}

Console.WriteLine(ret1);
Console.WriteLine(ret2);
Console.ReadLine();