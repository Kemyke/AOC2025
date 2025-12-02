var input = File.ReadAllText("input.txt");
var l = input.Split(",").Select(x => x.Split("-"));

long ret1 = 0;
long ret2 = 0;

foreach(var r in l)
{
    var s = long.Parse(r[0]);
    var e = long.Parse(r[1]);

    for (var i = s; i <= e; i++)
    {
        var istr = i.ToString();
        if (istr.Length % 2 == 0)
        {
            if (istr.Substring(0, istr.Length / 2) == istr.Substring(istr.Length / 2))
            {
                ret1 += i;
            }
        }

        for (var p = 1; p <= istr.Length / 2; p++)
        {
            var ptrn = istr.Substring(0, p);
            if(istr.Replace(ptrn, "").Length==0)
            {
                ret2 += i;
                break;
            }

        }
    }
}

Console.WriteLine(ret1);
Console.WriteLine(ret2);
Console.ReadLine();