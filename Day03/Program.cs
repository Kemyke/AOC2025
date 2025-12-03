var input = File.ReadAllLines("input.txt");

long ret1 = 0;
long ret2 = 0;

foreach (var line in input)
{
    char fm=line.Substring(0,line.Length - 1).Max();
    char sm = line.Substring(line.IndexOf(fm) + 1).Max();

    List<char> js = new List<char>();
    var cp = -1;
    long cret = 0;
    for(int i=11;i>=0;i--)
    {
        char xm = line.Substring(cp + 1, line.Length - cp - i - 1).Max();
        cp = line.IndexOf(xm, cp + 1);
        cret += (long)Math.Pow(10, i) * int.Parse(xm.ToString());
    }

    ret2 += cret;
    ret1 += int.Parse(fm.ToString()) * 10 + int.Parse(sm.ToString());
}

Console.WriteLine(ret1);
Console.WriteLine(ret2);
Console.ReadLine();