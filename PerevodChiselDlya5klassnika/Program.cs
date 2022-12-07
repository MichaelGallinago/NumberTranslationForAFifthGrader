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
            if (input.IndexOf('.') == -1)
            {
                input += ".0";
            }
            else if (input[0] == '.')
            {
                input = "0" + input;
            }

            if (!float.TryParse(input, out float number))
            {
                ExceptionIndex = 1;
            }
            else
            {
                Console.WriteLine("Переводим число в двоичную систему счисления: дробную и целую часть отдельно");
                GetDecimialBinaryDescription(Calc.GetModulo(number));
                string fractionalPart = GetFloatBinary(input, true).TrimEnd('0').PadRight(1, '0');
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
                string result = $"{(number < 0 ? "1" : "0")}|{order}|{binary.Substring(2, Math.Min(binary.Length - 2, 23))}".PadRight(34, '0');
                Console.WriteLine($"Ответ: {input} => {result}");
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

        private static string GetFloatBinary(string value, bool writeDescription)
        {
            string fractionalPart = value.Substring(value.IndexOf('.') + 1);
            int length = fractionalPart.Length;
            long number = long.Parse(fractionalPart);
            StringBuilder builder = new StringBuilder();

            if (writeDescription)
            {
                Console.WriteLine($"0|{number.ToString().PadLeft(length, '0')} /*2");
                Console.WriteLine(new string('-', length + 2));
            }

            for (int i = 0; i < 23; i++)
            {
                number *= 2;
                long integerPart = (long)(number / Math.Pow(10, length));
                number -= (long)(integerPart * Math.Pow(10, length));
                builder.Append(integerPart.ToString());

                if (writeDescription)
                {
                    Console.WriteLine($"{integerPart}|{number.ToString().PadLeft(length, '0')} /*2");
                }

                if (number == 0)
                {
                    break;
                }
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

            input1 = input1.Replace(',', '.');
            input2 = input2.Replace(',', '.');
            if (input1.IndexOf('.') == -1)
            {
                input1 += ".0";
            }
            else if (input1[0] == '.')
            {
                input1 = "0" + input1;
            }

            if (input2.IndexOf('.') == -1)
            {
                input2 += ".0";
            }
            else if (input1[0] == '.')
            {
                input2 = "0" + input2;
            }

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
                (char, char) signs = GetSigns(number1, number2);
                char resultSign = signs.Item1;
                char operationSign = signs.Item2;
                float numberMax, numberMin;
                string inputMax, inputMin;
                if (number1 >= number2)
                {
                    numberMax = number1;
                    numberMin = number2;
                    inputMax = input1;
                    inputMin = input2;
                }
                else
                {
                    numberMax = number2;
                    numberMin = number1;
                    inputMax = input2;
                    inputMin = input1;
                }

                Console.WriteLine("Расположим первым наибольшее по модулю число");
                string resultMax = ConvertFloats(numberMax, inputMax, true, out int powerMax);
                string resultMin = ConvertFloats(numberMin, inputMin, true, out int powerMin);
                string separator = new string('-', resultMax.Length);
                Console.WriteLine(resultMax);
                Console.WriteLine('+');
                Console.WriteLine(resultMin);
                Console.WriteLine(separator);
                Console.WriteLine(separator);
                resultMax = "0|" + resultMax.Substring(2);
                resultMin = "0|" + resultMin.Substring(2);
                Console.WriteLine($"{resultSign}({resultMax}");
                Console.WriteLine(operationSign);
                Console.WriteLine($"  {resultMin})");
                Console.WriteLine(separator);
                int offset = powerMax - powerMin;
                if (offset > 0)
                {
                    resultMin = resultMin.Substring(11).Substring(0, Math.Max(0, 23 - offset));
                    resultMin = $"{resultMax.Substring(0, 11)}{(offset > 1 ? new string('0', offset - 1) : "")}1{resultMin}".Substring(0,34);
                    Console.WriteLine($"Сдвигаем мантиссу меньшего числа на {offset} разрядов вправо");
                    Console.WriteLine(separator);
                    Console.WriteLine($"{resultSign}({resultMax}");
                    Console.WriteLine(operationSign);
                    Console.WriteLine($"  {resultMin})");
                    Console.WriteLine(separator);
                }
                float sum = GetFloatSum(input1, input2, out string stringSum);
                Console.WriteLine($"Ответ: {sum} => {ConvertFloats(sum, stringSum, false, out _)}");
                Console.WriteLine("Нажмите ENTER, чтобы продолжить");
                Console.ReadLine();
            }

            Console.Clear();
        }

        private static float GetFloatSum(string input1, string input2, out string result)
        {
            int offset1 = input1.Length - input1.IndexOf('.') - 1;
            int offset2 = input2.Length - input2.IndexOf('.') - 1;
            long number1 = long.Parse(input1.Replace(".", ""));
            long number2 = long.Parse(input2.Replace(".", ""));

            if (offset1 >= offset2)
                number2 *= (long)Math.Pow(10, offset1 - offset2);
            else
                number1 *= (long)Math.Pow(10, offset2 - offset1);

            string sum = (number1 + number2).ToString();
            result = sum.Insert(sum.Length - Math.Max(offset1, offset2), ".");

            if (result[0] == '.')
                result = "0" + result;

            if (result[result.Length-1] == '.')
                result = result + "0";

            return float.Parse(result);
        }

        private static string ConvertFloats(float number, string input, bool writeDescription, out int power)
        {
            string fractionalPart = GetFloatBinary(input, false).TrimEnd('0').PadRight(1, '0');
            string integerPart = Calc.GetBinary(Calc.GetModulo(number)).TrimStart('0').PadLeft(1, '0');
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
            string order = Calc.GetBinary(127 + power);
            string result = $"{(number < 0 ? "1" : "0")}|{order}|{binary.Substring(2, Math.Min(binary.Length - 2, 23))}".PadRight(34, '0');
            if (writeDescription)
            {
                Console.WriteLine($"{integerPart}.{fractionalPart}");
                Console.WriteLine($"{binary} * 2^({power})");
                Console.WriteLine($"127 + {power} = {127 + power}(10) = {order.TrimStart('0').PadLeft(1, '0')}(2)");
                Console.WriteLine(result);
                Console.WriteLine(new string('-', result.Length));
            }
            return result;
        }

        private static (char, char) GetSigns(float number1, float number2)
        {
            Console.WriteLine("Рассмотрим конечный знак");
            char operationSign;
            char resultSign;
            if (number1 < 0 && number2 < 0)
            {
                resultSign = '-';
                operationSign = '+';
                Console.WriteLine("Оба числа меньше нуля - знак ответа '-'");
                Console.WriteLine("Будем считать положительные числа и подставим его в конце");
            }
            else if (number1 >= 0 && number2 >= 0)
            {
                resultSign = '+';
                operationSign = '+';
                Console.WriteLine("Оба числа не отрицательные - знак ответа '+'");
            }
            else
            {
                operationSign = '-';
                if (number1 >= 0)
                {
                    resultSign = number1 >= -number2 ? '+' : '-';
                }
                else
                {
                    resultSign = number2 >= -number1 ? '+' : '-';
                }
                Console.Write($"Вычитаем меньшее по модулю из большего по модулю - знак ответа: '{resultSign}'");
            }

            return (resultSign, operationSign);
        }
    }
}