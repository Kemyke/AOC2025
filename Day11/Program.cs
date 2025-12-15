void Reset(Dictionary<string, Node> nodes)
{
    foreach (var n in nodes)
        n.Value.opt = null;
}

var input = File.ReadAllLines("input.txt");

long ret1 = 0;
long ret2 = 0;

var nodes = new Dictionary<string, Node>();


foreach (var line in input)
{
    var s1 = line.Split(":");
    var s3 = s1[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
    var n = new Node { name = s1[0], outNodes = s3.ToHashSet() };
    nodes.Add(n.name, n);
}
nodes.Add("out", new Node { name = "out", outNodes = new HashSet<string>() });

ret1 = nodes["you"].Options(nodes, "out");
Reset(nodes);
var svrdac = nodes["svr"].Options(nodes, "dac");
var fftdac = nodes["fft"].Options(nodes, "dac");
Reset(nodes);
var svrfft = nodes["svr"].Options(nodes, "fft");
var dacfft = nodes["dac"].Options(nodes, "fft");
Reset(nodes);
var dacout = nodes["dac"].Options(nodes, "out");
var fftout = nodes["fft"].Options(nodes, "out");

ret2 = svrdac * dacfft * fftout + svrfft * fftdac * dacout;


Console.WriteLine(ret1);
Console.WriteLine(ret2);
Console.ReadLine();

class Node
{
    public string name;
    public HashSet<string> outNodes = null;

    public long? opt = null;
    public long Options(Dictionary<string, Node> nodes, string goal)
    {
        if (opt.HasValue)
            return opt.Value;
        if (name == goal)
        {
            opt = 1;
            return 1;
        }

        opt = outNodes.Sum(n => nodes[n].Options(nodes, goal));
        return opt.Value;
    }
}