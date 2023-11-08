public class Frame
{
    public int FirstRoll;
    public int SecondRoll;
    public int ThirdRoll;
    public int ExtraPoints;
    public int Overall;

    public int SumOfRolls()
    {
        Overall = FirstRoll + SecondRoll + ThirdRoll + ExtraPoints;
        return Overall;
    }

    public override string ToString()
    {
        return SumOfRolls().ToString();
    }
}
