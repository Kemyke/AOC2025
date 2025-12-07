var input = File.ReadAllLines("input.txt");

long ret1 = 0;
long ret2 = 0;
var lines = input.Select(l => l.Split(" ", StringSplitOptions.RemoveEmptyEntries)).ToList();

var problem = new List<(string, List<string>)>();

for(int i = 0; i< lines[0].Length; i++)
{
    var p = new List<string>();
    var f = lines.Last()[i];
    long pr = f == "+" ? 0 : 1;
    for(int j = 0;j < lines.Count - 1; j++)
    {
        p.Add(lines[j][i]);
        if (f == "+")
            pr += long.Parse(lines[j][i]);
        else
            pr *= long.Parse(lines[j][i]);
    }
    problem.Add((f, p));
    ret1 += pr;
}

var sizes = input.Last().Split(new char[]{ '+', '*' }, StringSplitOptions.RemoveEmptyEntries).Select(v=>v.Length).ToList();
sizes[sizes.Count - 1]++;
var paddings = new List<string>();
var ps = input.SkipLast(1).ToList();
var cs = 0;
foreach(var s in sizes)
{
    if (input.All(s => s[cs] != ' '))
    {
        paddings.Add("R");
    }
    else
        paddings.Add("L");

    cs = input.Last().IndexOfAny(new char[] { '+', '*' }, cs + 1);
}
var ll = 0;
foreach (var p in problem)
{
    var m = p.Item2.Max(i => i.Length);
    var pp = p.Item2.Select(v => v.PadLeft(m));
   if (paddings[ll++] == "R")
        pp = p.Item2.Select(v => v.PadRight(m));
    long pr = p.Item1 == "+" ? 0 : 1;
    for (int i = 0; i<m;i++)
    {
        var x = string.Concat(pp.Select(l => l[i])).Trim();
        if (p.Item1 == "+")
            pr += long.Parse(x);
        else
            pr *= long.Parse(x);
    }
    ret2 += pr;
}

Console.WriteLine(ret1);
Console.WriteLine(ret2);
Console.ReadLine();