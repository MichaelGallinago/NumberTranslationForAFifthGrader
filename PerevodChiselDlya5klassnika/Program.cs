using System.Text;

namespace Calculator
{
    internal class Program
    {
        private static byte ExceptionIndex = 0;
        private static byte Section = 0;

        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            Console.OutputEncoding = Encoding.UTF8;
            while (true)
            {
                switch (Section)
                {
                    case 0: ProcessMenu();      break;
                    case 1: ProcessExtraCode(); break;
                    case 2: ProcessAdd();       break;
                    case 3: ProcessFloat();     break;
                    case 4: ProcessFloatAdd();  break;
                    default: return;
                }
            }
        }

        private static void ProcessMenu()
        {
            Console.WriteLine("Добро пожаловать в программу для перевода целых и вещественных чисел!");
            Console.WriteLine("Программу подготовил студент 1 курса ИИТ ЧелГУ группы ПрИ 102 - Бекасов Михаил Юрьевич");
            Console.WriteLine();
            Console.WriteLine("Введите НОМЕР нужного раздела из списка ниже, чтобы продолжить");
            Console.WriteLine("1 - перевод целых чисел в дополнительный код");
            Console.WriteLine("2 - сложение целых (положительных и отрицательных) чисел с использованием дополнительного кода");
            Console.WriteLine("3 - перевод вещественных чисел в формат с плавающей точкой");
            Console.WriteLine("4 - сложение вещественных чисел в формате с плавающей точкой");
            Console.WriteLine("5 - выход");
            Console.WriteLine();

            if (ExceptionIndex == 1)
            {
                Console.WriteLine("Кажется, вы ошиблись номером, введите его снова");
                Console.WriteLine();
                ExceptionIndex = 0;
            }

            bool isNumber = byte.TryParse(Console.ReadLine(), out Section);
            if (!isNumber || Section > 5)
            {
                ExceptionIndex = 1;
                Section = 0;
            }

            Console.Clear();
        }

        private static bool CheckForReturn(string word)
        {
            if (word.ToLower() == "назад")
            {
                Section = 0;
                Console.Clear();
                return true;
            }
            return false;
        }

        private static void ProcessExtraCode()
        {
            Console.WriteLine("Здесь вы можете перевести целое число в дополнительный код");
            Console.WriteLine("Чтобы вернуться к списку разделов, введите НАЗАД и нажмите ENTER");
            Console.WriteLine("Чтобы получить развёрнутый ответ, напишите ЧИСЛО и нажмите ENTER");
            Console.WriteLine("Вы можете использовать числа ТОЛЬКО от -128 до 127 включительно");
            Console.WriteLine();

            if (ExceptionIndex == 1)
            {
                Console.WriteLine("Вы ошиблись числом, которое надо перевести, введите число снова");
                Console.WriteLine();
                ExceptionIndex = 0;
            }

            string input = Console.ReadLine();
            if (CheckForReturn(input)) return;

            if (!sbyte.TryParse(input, out sbyte number))
            {
                ExceptionIndex = 1;
            }
            else
            {
                string binary = Calc.GetBinary(number);
                string extraCode = Calc.GetExtraCode(number);

                Console.WriteLine("Переводим модуль числа в двоичную систему");
                Console.WriteLine($"{(number < 0 ? -number : number)}(10) = {binary}(2)");
                Console.WriteLine();
                Console.WriteLine("Если изначальное число отрицательное - инвертируем его");
                Console.WriteLine("Это значит заменяем единицы на нули, а нули на единицы");

                if (number < 0)
                {
                    string invertedBinary = Calc.GetInverted(binary);
                    Console.WriteLine(binary);
                    Console.WriteLine(invertedBinary);
                    Console.WriteLine();
                    Console.WriteLine("Теперь нужно добавить единицу");
                    Console.WriteLine($"{invertedBinary} + 1 = {extraCode}");
                    Console.WriteLine();
                }
                else 
                {
                    Console.WriteLine();
                    Console.WriteLine("Данное число больше или равно нулю");
                    Console.WriteLine("Дополнительный код такой же, как и прямой (число в двоичной системе)");
                    Console.WriteLine();
                }

                Console.WriteLine($"Ответ: {extraCode}(2*) = {(sbyte)Calc.GetNumber(extraCode)}(10)");
                Console.WriteLine("Нажмите ENTER, чтобы продолжить");
                Console.ReadLine();
            }

            Console.Clear();
        }

        private static void ProcessAdd()
        {
            Console.WriteLine("Здесь вы можете сложить два целых числа с использованием дополнительного кода");
            Console.WriteLine("Чтобы вернуться к списку разделов, введите НАЗАД и нажмите ENTER");
            Console.WriteLine("Чтобы получить развёрнутый ответ, напишите ЧИСЛО и нажмите ENTER,");
            Console.WriteLine("Затем напишите ВТОРОЕ ЧИСЛО и нажмите ENTER");
            Console.WriteLine("Вы можете использовать числа ТОЛЬКО от -128 до 127 включительно");
            Console.WriteLine();

            if (ExceptionIndex > 0)
            {
                string text = "";
                switch (ExceptionIndex)
                {
                    case 1: text = " первым числом"; break;
                    case 2: text = " вторым числом"; break;
                }
                Console.WriteLine($"Вы ошиблись{text}, введите числа снова");
                Console.WriteLine();
                ExceptionIndex = 0;
            }

            string input1 = Console.ReadLine();
            if (CheckForReturn(input1)) return;
            string input2 = Console.ReadLine();
            if (CheckForReturn(input2)) return;

            if (!sbyte.TryParse(input1, out sbyte number1))
            {
                ExceptionIndex = 1;
            }
            else if (!sbyte.TryParse(input2, out sbyte number2))
            {
                ExceptionIndex = 2;
            }
            else 
            {
                string binary1 = Calc.GetBinary(number1);
                string binary2 = Calc.GetBinary(number2);
                string extraCode1 = Calc.GetExtraCode(number1);
                string extraCode2 = Calc.GetExtraCode(number2);
                string sum = Calc.GetBinary((short)(Calc.GetNumber(extraCode1) + Calc.GetNumber(extraCode2)));
                string result = sum.Substring(sum.Length - 8);
                int absoluteNumber1 = Calc.GetModulo(number1);
                int absoluteNumber2 = Calc.GetModulo(number2);

                Console.WriteLine("Переводим модули чисел в двоичную систему");
                GetDecimialBinaryDescription(absoluteNumber1);
                Console.WriteLine($"{absoluteNumber1}(10) = {binary1}(2)");
                Console.WriteLine();
                GetDecimialBinaryDescription(absoluteNumber2);
                Console.WriteLine($"{absoluteNumber2}(10) = {binary2}(2)");
                Console.WriteLine();
                Console.WriteLine("Получаем их дополнительный код");
                WriteProcessAddExtraCode(number1, binary1, extraCode1);
                WriteProcessAddExtraCode(number2, binary2, extraCode2);
                Console.WriteLine("Складываем полученные числа в двоичной системе");
                Console.WriteLine($"  {extraCode1}");
                Console.WriteLine($"+ {extraCode2}");
                Console.WriteLine($"={sum.PadLeft(9)}");
                Console.WriteLine("В ответ записываем только первые 8 цифр справа");
                Console.WriteLine($"Ответ: {result}(2*) = {(sbyte)Calc.GetNumber(result)}(10)");
                Console.WriteLine("Нажмите ENTER, чтобы продолжить");
                Console.ReadLine();
            }

            Console.Clear();
        }

        private static void WriteProcessAddExtraCode(sbyte number, string binary, string extraCode)
        {
            Console.WriteLine(binary);
            if (number < 0)
            {
                Console.WriteLine(Calc.GetInverted(binary));
                Console.WriteLine(extraCode);
            }
            Console.WriteLine();
        }

        private static void ProcessFloat()
        {
            Console.WriteLine("Здесь вы можете перевести вещественное число в формат с плавающей точкой");
            Console.WriteLine("Чтобы вернуться к списку разделов, введите НАЗАД и нажмите ENTER");
            Console.WriteLine("Чтобы получить развёрнутый ответ, напишите ЧИСЛО и нажмите ENTER");
            Console.WriteLine();

            if (ExceptionIndex == 1)
            {
                Console.WriteLine("Вы ошиблись числом, которое надо перевести, введите число снова");
                Console.WriteLine();
                ExceptionIndex = 0;
            }

            string input = Console.ReadLine();
            if (CheckForReturn(input)) return;

            input = input.Replace(',', '.');
            if (!float.TryParse(input, out float number))
            {
                ExceptionIndex = 1;
            }
            else
            {
                Console.WriteLine("Переводим число в двоичную систему счисления: дробную и целую часть отдельно");
                GetDecimialBinaryDescription(Calc.GetModulo(number));
                string fractionalPart = GetFloatBinaryDescription(input).TrimEnd('0').PadRight(1, '0');
                string integerPart = Calc.GetBinary(Calc.GetModulo(number)).TrimStart('0').PadLeft(1, '0');
                Console.WriteLine("Записываем левый столбик сверху вниз");
                Console.WriteLine();
                Console.WriteLine("Записываем число:");
                Console.WriteLine($"{integerPart}.{fractionalPart}");
                Console.WriteLine();
                Console.WriteLine("Записываем число в нормализованной экспоненциальной форме");
                int power;
                string binary;
                if (integerPart == "0")
                {
                    binary = $"{fractionalPart.TrimStart('0').PadRight(2, '0').Insert(1, ".")}";
                    power = -fractionalPart.IndexOf('1') - 1;
                }
                else
                {
                    binary = $"{integerPart.Insert(1, ".")}{fractionalPart}";
                    power = integerPart.Length - 1;
                }
                Console.WriteLine($"{binary} * 2^({power})");
                Console.WriteLine();
                Console.WriteLine("Рассчитываем смещённый порядок числа");
                Console.WriteLine($"127 + {power} = {127 + power}");
                Console.WriteLine("Переводим в двоичную систему счисления");
                GetDecimialBinaryDescription(127 + power);
                string order = Calc.GetBinary(127 + power);
                Console.WriteLine(order.TrimStart('0').PadLeft(1, '0'));
                Console.WriteLine();
                Console.WriteLine("Записываем знак, порядок и мантиссу");
                string result = $"{(number < 0 ? "1" : "0")}|{order}|{binary.Substring(2, Math.Min(binary.Length - 2, 23))}";
                Console.WriteLine($"Ответ: {result}");
                Console.WriteLine("Нажмите ENTER, чтобы продолжить");
                Console.ReadLine();
            }

            Console.Clear();
        }

        private static void GetDecimialBinaryDescription(long number)
        {
            if (number == 0)
            {
                Console.WriteLine("0(10)=0(2)");
            }
            else
            {
                int length = number.ToString().Length;
                while (number > 0L)
                {
                    Console.WriteLine($"{number.ToString().PadLeft(length)}|2|{number % 2L}");
                    number /= 2L;
                }
                Console.WriteLine("Записываем правый столбик снизу вверх");
            }
            Console.WriteLine();
        }

        private static string GetFloatBinaryDescription(string value)
        {
            string fractionalPart = value.Substring(value.IndexOf('.') + 1);
            int length = fractionalPart.Length;
            long number = long.Parse(fractionalPart);
            StringBuilder builder = new StringBuilder();
            Console.WriteLine($"0|{number.ToString().PadLeft(length, '0')} /*2");
            Console.WriteLine(new string('-', length + 2));
            for (int i = 0; i < 24; i++)
            {
                number *= 2;
                long integerPart = (long)(number / Math.Pow(10, length));
                number -= (long)(integerPart * Math.Pow(10, length));
                Console.WriteLine($"{integerPart}|{number.ToString().PadLeft(length, '0')} /*2");
                builder.Append(integerPart.ToString());
                if (number == 0)
                    break;
            }
            return builder.ToString();
        }

        private static void ProcessFloatAdd()
        {
            Console.WriteLine("Здесь вы можете сложить два вещественных числа в формате с плавающей точкой");
            Console.WriteLine("Чтобы вернуться к списку разделов, введите НАЗАД и нажмите ENTER");
            Console.WriteLine("Чтобы получить развёрнутый ответ, напишите ЧИСЛО и нажмите ENTER,");
            Console.WriteLine("Затем напишите ВТОРОЕ ЧИСЛО и нажмите ENTER");
            Console.WriteLine();

            if (ExceptionIndex > 0)
            {
                string text = "";
                switch (ExceptionIndex)
                {
                    case 1: text = " первым числом"; break;
                    case 2: text = " вторым числом"; break;
                }
                Console.WriteLine($"Вы ошиблись{text}, введите числа снова");
                Console.WriteLine();
                ExceptionIndex = 0;
            }

            string input1 = Console.ReadLine();
            if (CheckForReturn(input1)) return;
            string input2 = Console.ReadLine();
            if (CheckForReturn(input2)) return;

            if (!float.TryParse(input1, out float number1))
            {
                ExceptionIndex = 1;
            }
            else if (!float.TryParse(input2, out float number2))
            {
                ExceptionIndex = 2;
            }
            else
            {

            }

            Console.Clear();
        }
    }
}