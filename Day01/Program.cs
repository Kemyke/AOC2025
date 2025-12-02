var input = File.ReadAllLines("input.txt");
long ret1 = 0;
long ret2 = 0;

var pos = 50;
foreach(var line in input)
{
    var d = line[0];
    var a = int.Parse(line.Substring(1));
    var z = pos == 0;

    if(d=='L')
    {
        pos -= a;
    }
    else
    {
        pos += a;
    }
    if (pos <= 0)
    {
        if (pos % 100 == 0)
            ret1++;
        ret2 += (-1 * pos / 100) + ((z) ? 0 : 1);
    }
    if (pos >= 100)
    {
        if (pos % 100 == 0)
            ret1++;
        ret2 += pos / 100;

    }
    pos %= 100;
    if (pos < 0)
        pos += 100;
}


Console.WriteLine(ret1);
Console.WriteLine(ret2);
//2473 too low
//6008 nem jó
Console.ReadLine();