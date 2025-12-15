var input = File.ReadAllLines("input.txt");

long ret1 = 0;

var boxes = new Dictionary<int, int>();

var trees = new List<(long, List<long>)>();
int cb = 0;
int i = 0;
int linecount = -1;
foreach(string line in input)
{
    linecount++;
    if (line.Contains("x"))
        break;
    if (line == "")
    {
        boxes.Add(i++, cb);
        cb = 0;
        continue;
    }

    cb += line.Count(c => c == '#');
}

foreach(var line in input.Skip(linecount))
{
    var sp1 = line.Split(":");
    var sp2 = sp1[0].Split("x");

    var sp3 = sp1[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(s => long.Parse(s)).ToList();

    var area = long.Parse(sp2[0]) * long.Parse(sp2[1]);

    trees.Add((area, sp3));
}

foreach(var t in trees)
{
    var ba = t.Item2.Select((p, i) => boxes[i] * p).Sum();
    if (ba <= t.Item1)
        ret1++;
}

Console.WriteLine(ret1);
Console.ReadLine();