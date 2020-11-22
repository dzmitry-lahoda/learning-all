
public enum OneTwo
{
    One = 1,
    Two = 2
}


OneTwo x = (OneTwo)3;

if (Enum.GetName(typeof(OneTwo), x) != null) throw new Exception();

