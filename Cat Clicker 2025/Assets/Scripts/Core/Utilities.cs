using System;

public static class Utilities
{
    public readonly static string[] numberSuffixes =
    {
        "million", "billion", "trillion", "quadillion", "quintillion",
        "sextillion", "septillion", "octillion", "nonillion", "decillion",
        "undecillion", "duodecillion", "tredecillion", "quattuordecillion",
        "quindecillion", "sexdecillion", "septendecillion", "octodecillion",
        "novemdecillion", "vigintillion", "gigantillion"
    };

    public static string ToBitNotation(double value, int decimals = 3)
    {
        if (value == 0) return "0";

        double absValue = Math.Abs(value);

        // Only use special format for values greater than 1 million
        if (absValue < 1e6) return value.ToString("0.#");

        int suffixValue = -1;

        while (absValue >= 1e6 && suffixValue < numberSuffixes.Length - 1)
        {
            absValue /= 1000;
            ++suffixValue;
        }
        absValue /= 1000; // One more division to correct the offset

        // Returns the full #.### value always (preferred format)
        return (value < 0) ? "-" : ""
            + absValue.ToString($"F{decimals}")
            + $" {numberSuffixes[suffixValue]}";
    }
}
