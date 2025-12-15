using Google.OrTools.LinearSolver;

var input = File.ReadAllLines("input.txt");

long ret1 = 0;
long ret2 = 0;

var machines = new List<Machine>();

var iii = 0;
foreach(var line in input)
{
    var m = new Machine();
    m.id = iii++;
    var s = line.Split(' ');
    m.slights = s[0].Trim(['[', ']']);
    m.lights = s[0].Trim(['[', ']']).Select(l=>l == '#' ? true : false).ToList();
    var bs = s.Skip(1).SkipLast(1).ToList();
    foreach (var ss in bs)
    {
        var b = ss.Trim(['(', ')']).Split(',').Select(l => int.Parse(l)).ToHashSet();
        m.buttons.Add(b);
    }

    m.joltages= s[s.Length - 1].Trim(['{', '}']).Split(',').Select(l=>int.Parse(l)).ToList();
    machines.Add(m);
}

foreach (var machine in machines.Skip(0))
{
    ret1 += machine.SwitchOn();

  
    int[,] m = new int[machine.joltages.Count, machine.buttons.Count];
    for (int j = 0; j < machine.joltages.Count; j++)
    {
        for (int b = 0; b < machine.buttons.Count; b++)
        {
            if (machine.buttons[b].Contains(j))
            {
                m[j, b] = 1;
            }
        }
    }

    Solver solver = Solver.CreateSolver("CBC");
    int numVars = machine.buttons.Count;
    int numConstraints = machine.joltages.Count;
    Variable[] x = new Variable[numVars];
    for (int ii = 0; ii < numVars; ii++)
    {
        x[ii] = solver.MakeIntVar(0, double.PositiveInfinity, $"x{ii}");
    }

    for (int i = 0; i < numConstraints; i++)
    {
        LinearExpr expr = new LinearExpr();
        for (int j = 0; j < numVars; j++)
        {
            expr += m[i, j] * x[j];
        }
        solver.Add(expr == machine.joltages[i]);
    }

    Objective objective = solver.Objective();
    foreach (var v in x)
    {
        objective.SetCoefficient(v, 1);
    }
    objective.SetMinimization();

    Solver.ResultStatus resultStatus = solver.Solve();

    long step = 0;
    for (int i = 0; i < numVars; i++)
    {
        step += (long)x[i].SolutionValue();
    }
    ret2 += step;
}

Console.WriteLine(ret1);
Console.WriteLine(ret2);
Console.ReadLine();

class Machine
{
    public int id;
    public List<bool> lights;
    public string slights;
    public List<HashSet<int>> buttons = new List<HashSet<int>>();
    public List<int> joltages;

    Dictionary<string, int> cache = new Dictionary<string, int>();
    Dictionary<string, int> cache2 = new Dictionary<string, int>();

    public int SwitchOn()
    {
        int step = 0;
        List<string> pss = new List<string> { new string('.', lights.Count) };
        while (true)
        {
            var npss = new List<string>();
            foreach (var nps in pss)
            {
                foreach (var b in buttons)
                {
                    var ns = string.Concat(nps.Select((c, i) => b.Contains(i) ? (c == '#' ? '.' : '#') : c));
                    if (ns == slights)
                        return step + 1;

                    if (!cache.ContainsKey(ns) || cache[ns] > step + 1)
                    {
                        cache[ns] = step + 1;
                        npss.Add(ns);
                    }
                }
            }
            pss = npss;
            step++;
        }
    }
}