static Dictionary<long, Dictionary<long, Item>> ParseInput(List<string> input)
{
    var ret = new Dictionary<long, Dictionary<long, Item>>();
    var c = input[0].Length;
    for (int i = 0; i < input.Count; i++)
    {
        ret.Add(i, new Dictionary<long, Item>());
        for (int j = 0; j < c; j++)
        {
            var item = new Item { Coordinate = new Coordinate { X = j, Y = i }, ItemType = input[i][j] };
            ret[i].Add(j, item);
        }
    }

    return ret;
}


var input = File.ReadAllLines("input.txt").ToList();
var map = ParseInput(input);
var ret1 = 0;
var ret2 = 0;

bool removed = true;
while (removed)
{
    removed = false;
    foreach (var item in map.Values.SelectMany(v => v.Values))
    {
        if (item.ItemType == '@')
        {
            var atp = 0;
            foreach (var cc in item.Coordinate.Adjacent())
            {
                if (map.ContainsKey(cc.Y) && map[cc.Y].ContainsKey(cc.X))
                {
                    if (map[cc.Y][cc.X].ItemType == '@')
                        atp++;
                }
            }
            if (atp < 4)
            {
                removed = true;
                item.ItemType = '.';
                ret2++;
            }
        }
    }
}
    Console.WriteLine(ret1);
Console.WriteLine(ret2);
Console.ReadLine();

public class Coordinate
{
    public long X { get; set; }
    public long Y { get; set; }

    public List<Coordinate> Adjacent()
    {
        List<Coordinate> ret =
        [
            new Coordinate { Y = Y - 1, X = X },
                new Coordinate { Y = Y, X = X - 1 },
                new Coordinate { Y = Y, X = X + 1 },
                new Coordinate { Y = Y + 1, X = X },

                new Coordinate { Y = Y - 1, X = X - 1 },
                new Coordinate { Y = Y - 1, X = X + 1 },
                new Coordinate { Y = Y + 1, X = X - 1 },
                new Coordinate { Y = Y + 1, X = X + 1 },
            ];

        return ret;
    }
}

public class Item
{
    public Coordinate Coordinate { get; set; }
    public char ItemType { get; set; }
}
 