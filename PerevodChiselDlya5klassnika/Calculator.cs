using System.Text;

namespace Calculator
{
    internal class Calc
    {
        public static string GetBinary(short number)
        {
            number = Math.Abs(number);
            StringBuilder builder = new StringBuilder();
            while (number > 0)
            {
                builder.Insert(0, number % 2 == 0 ? '0' : '1');
                number /= 2;
            }
            return builder.ToString().PadLeft(8, '0');
        }

        public static byte GetNumber(string binary)
        {
            byte number = 0;
            for (byte i = 0; i < binary.Length; i++)
            {
                if (binary[binary.Length - i - 1] == '1')
                    number += (byte)Math.Pow(2, i);
            }
            return number;
        }

        public static string GetInverted(string binary)
        {
            return binary.Replace('0', '2').Replace('1', '0').Replace('2', '1');
        }

        public static string GetExtraCode(sbyte number)
        {
            string binary = GetBinary(number);
            return number >= 0 ? binary : GetBinary((byte)(GetNumber(GetInverted(binary)) + 1));
        }
    }
}
