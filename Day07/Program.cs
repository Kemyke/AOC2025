var input = File.ReadAllLines("input.txt");

var ret1 = 0;
long ret2 = 1;

Dictionary<int, long> beams = new Dictionary<int, long> { { input[0].IndexOf('S'), 1 } };
for(int i = 1; i < input.Length - 1; i++)
{
    var nbs = new Dictionary<int, long>();
    foreach(var beam in beams)
    {
        if (input[i][beam.Key] == '.')
        {
            if (nbs.ContainsKey(beam.Key))
                nbs[beam.Key] += beam.Value;
            else
                nbs.Add(beam.Key, beam.Value);
        }
        if (input[i][beam.Key] == '^')
        {
            ret1++;
            if (nbs.ContainsKey(beam.Key - 1))
                nbs[beam.Key - 1] += beam.Value;
            else
                nbs.Add(beam.Key - 1, beam.Value);

            if (nbs.ContainsKey(beam.Key + 1))
                nbs[beam.Key + 1] += beam.Value;
            else
                nbs.Add(beam.Key + 1, beam.Value);

        }
    }
    beams = nbs;
}

ret2 = beams.Values.Sum();

Console.WriteLine(ret1);
Console.WriteLine(ret2);
Console.ReadLine();