using System;
using System.Text.RegularExpressions;

namespace Ejercicio1
{
    class Program
    {
        private static double firstNumberWritten;
        private static double secondNumberWritten;
        private static char symbol;

        static void Main(string[] args)
        {
            MainMenu();
        }

        /// <summary>
        /// Método encargado de obtener la opción deseada del menú principal.
        /// </summary>
        private static void GetOptionMenu()
        {
            string chosenOption;
            chosenOption = ReadChoiceMenu();

            if (chosenOption.Equals("1"))
            {
                MenuManualMode();
            }
            else if (chosenOption.Equals("2"))
            {
                MenuAutomaticMode();

            }
            else if (chosenOption.Equals("0"))
            {
                Console.Clear();
                Console.WriteLine("\nMuchas gracias por usar Calculator. ¡Hasta la próxima!");
                Console.ReadKey();
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Opción incorrecta.");
                Console.ReadKey();
                MainMenu();
            }
        }

        /// <summary>
        /// Método encargado de mostrar la información del menú principal
        /// </summary>
        public static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("\nBienvenid@ a Calculator!");
            Console.WriteLine("\t1.- Modo manual.");
            Console.WriteLine("\t2.- Modo automático.");
            Console.WriteLine("\t0.- Salir.");
            Console.Write("Seleccione la opción deseada: ");

            GetOptionMenu();

        }

        /// <summary>
        /// Método encargado de comprobar y obtener la elección deseada del menú principal.
        /// </summary>
        /// <returns>Devuelve la elección deseada de forma correcta.</returns>
        public static string ReadChoiceMenu()
        {
            string choice = Console.ReadLine();

            if (!(choice.Equals("0")) && !(choice.Equals("1")) && !(choice.Equals("2")))
            {
                Console.WriteLine("No ha introducido un valor válido.");
                Console.ReadKey();
                MainMenu();

            }

            return choice;
        }

        /// <summary>
        /// Método encargado de realizar la lógica del modo manual.
        /// </summary>
        public static void MenuManualMode()
        {
            Console.Clear();

            Console.WriteLine("Ha elegido Modo Manual: ");

            GetFirstNumber();

            GetSecondNumber();

            GetOperatorManualMode();

            Calculate();

            ReturnMainMenu();

        }



        /// <summary>
        /// Método encargado de realizar la lógica del modo automático. 
        /// </summary>
        public static void MenuAutomaticMode()
        {
            Console.Clear();

            Console.WriteLine("Ha elegido Modo Automático: ");

            Console.WriteLine("\nIntroduzca la operación que desea realizar.");
            Console.Write("Añada los números seperados por el símbolo (+, -, *, /, ^): ");

            string operation = Console.ReadLine();

            GetValues(operation);

            Calculate();

            ReturnMainMenu();

        }

        private static void ReturnMainMenu()
        {
            Console.Write("Pulse una tecla para volver al menú principal. ");
            Console.ReadKey();
            MainMenu();
        }

        /// <summary>
        /// Método encargado de hacer las llamadas a los métodos del modo automático.
        /// </summary>
        private static void Calculate()
        {
            switch (symbol)
            {
                case '+':
                    Sum();
                    break;
                case '-':
                    Subtraction();
                    break;
                case '*':
                    Multiply();
                    break;
                case '/':
                    Divide();
                    break;
                case '^':
                    Pow();
                    break;
            }
        }

        /// <summary>
        /// Método encargado de obtener los valores numéricos y operador del modo automático.
        /// Comprueba que la información introducida es correcta y separa los diferentes valores.
        /// </summary>
        /// <param name="operation">Operación que se quiere realizar. Ej.: 20+20</param>
        private static void GetValues(string operation)
        {

            // Se quitan los espacios en blanco que contenga la cadena.
            var operationWithoutWhiteSpace = operation.Replace(" ", "");

            operation = operationWithoutWhiteSpace;


            // Patrón que comprobará si la operación introducida es correcta. 
            // Para que sea correcta debe contener: números símbolo números.
            Regex regex = new Regex("^[0-9]+[+-/*^][0-9]+$");

            // Comprueba que la operación introducida cumple con el patrón establecido.
            if (regex.IsMatch(operation))
            {
                int symbolIndex = GetOperatorIndex(operation);

                // Se obtienen los números de la cadena. 
                string firstPart = operation.Substring(0, symbolIndex);
                string secondPart = operation.Substring(symbolIndex + 1);

                firstNumberWritten = ConvertToNumber(firstPart);
                secondNumberWritten = ConvertToNumber(secondPart);

            }
            else
            {
                Console.Clear();
                Console.WriteLine("No ha añadido un operador a su operación.");
                Console.ReadKey();
                MenuAutomaticMode();
            }
        }

        private static int GetOperatorIndex(string operation)
        {
            int symbolIndex = 0;

            // Se obtiene el operador.
            if (operation.Contains("+"))
            {
                symbolIndex = operation.IndexOf("+");

                symbol = '+';

            }
            else if (operation.Contains("-"))
            {
                symbolIndex = operation.IndexOf("-");

                symbol = '-';

            }
            else if (operation.Contains("*"))
            {
                symbolIndex = operation.IndexOf("*");

                symbol = '*';

            }
            else if (operation.Contains("/"))
            {
                symbolIndex = operation.IndexOf("/");

                symbol = '/';

            }
            else if (operation.Contains("^"))
            {
                symbolIndex = operation.IndexOf("^");

                symbol = '^';

            }

            return symbolIndex;
        }

        /// <summary>
        /// Método encargado de solicitar al usuario el operador deseado para realizar la operación del modo manual. 
        /// </summary>
        private static void GetOperatorManualMode()
        {
            byte chosenNumber;

            bool isValid = false;

            while (!isValid)
            {
                bool isNumber;

                OperatorsMenu();
                string chosenOption = ReadOperatorOption();
                ConvertTextToNumber(out chosenNumber, out isNumber, chosenOption);

                if (isNumber)
                {
                    GetOperator(chosenNumber);

                    isValid = true;

                }
                else
                {
                    Console.WriteLine("No ha elegido una opción correcta.");
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// Método que identifica el operador a partir del número elegido por el menú operador. 
        /// </summary>
        /// <param name="chosenNumber">Opción que identifica al operador.</param>
        private static void GetOperator(int chosenNumber)
        {
            switch (chosenNumber)
            {
                case 1:
                    symbol = '+';
                    break;
                case 2:
                    symbol = '-';
                    break;
                case 3:
                    symbol = '*';
                    break;
                case 4:
                    symbol = '/';
                    break;
                case 5:
                    symbol = '^';
                    break;
            }
        }

        /// <summary>
        /// Método encargado de obtener el segundo número con el que operar. 
        /// </summary>
        private static void GetSecondNumber()
        {
            string secondNumberString;
            bool isValid = false;

            while (!isValid)
            {
                Console.Write("\nIntroduzca el segundo número: ");
                secondNumberString = Console.ReadLine();

                isValid = double.TryParse(secondNumberString, out secondNumberWritten);

                if (!isValid)
                {
                    Console.WriteLine("Número inválido.");
                }
            }
        }

        /// <summary>
        /// Método encargado de obtener el primer número con el que operar. 
        /// </summary>
        private static void GetFirstNumber()
        {
            string firstNumberString;
            bool isValid = false;

            while (!isValid)
            {
                Console.Write("\nIntroduzca el primer número: ");

                firstNumberString = Console.ReadLine();

                isValid = double.TryParse(firstNumberString, out firstNumberWritten);

                if (!isValid)
                {
                    Console.WriteLine("Número inválido.");
                }
            }
        }


        private static void ConvertTextToNumber(out byte chosenNumber, out bool isNumber, string chosenOption)
        {
            isNumber = byte.TryParse(chosenOption, out chosenNumber);
        }

        /// <summary>
        /// Método encargado de convertir un texto en número. 
        /// Cuenta con comprobación de errores.
        /// </summary>
        /// <param name="NumberString">Cadena de texto que se desea convertir a número.</param>
        /// <returns></returns>
        private static double ConvertToNumber(string NumberString)
        {
            double convertedNumber;

            if (double.TryParse(NumberString, out convertedNumber))
            {
                return convertedNumber;

            }
            else
            {
                return 0;
            }

        }

        /// <summary>
        /// Método encargado de leer la opción correspondiente al operador.
        /// </summary>
        /// <returns>Opción correspondiente al operador</returns>
        private static string ReadOperatorOption()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Método encargado de mostrar un menú que permite seleccionar el operador con el que trabajar.
        /// Lo utiliza el modo manual. 
        /// </summary>
        private static void OperatorsMenu()
        {
            Console.WriteLine("\nOperación: ");
            Console.WriteLine("\t1.- Sumar.");
            Console.WriteLine("\t2.- Restar.");
            Console.WriteLine("\t3.- Multiplicar.");
            Console.WriteLine("\t4.- Dividir.");
            Console.WriteLine("\t5.- Potencia.");
            Console.Write("Seleccione la operación: ");
        }

        /* Métodos que realizan los cálculos */

        /// <summary>
        /// Método encargado de sumar dos números. 
        /// </summary>
        private static void Sum()
        {
            double result = firstNumberWritten + secondNumberWritten;
            Console.WriteLine($"\nLa suma de {firstNumberWritten} y {secondNumberWritten} es {result}.");
        }

        /// <summary>
        /// Método encargado de restar dos números.
        /// </summary>
        private static void Subtraction()
        {
            double result = firstNumberWritten - secondNumberWritten;
            Console.WriteLine($"\nLa resta de {firstNumberWritten} y {secondNumberWritten} es {result}.");
        }

        /// <summary>
        /// Método encargado de multiplicar dos números.
        /// </summary>
        private static void Multiply()
        {
            double result = firstNumberWritten * secondNumberWritten;
            Console.WriteLine($"\nLa multiplicación de {firstNumberWritten} y {secondNumberWritten} es {result}.");
        }

        /// <summary>
        /// Método encargado de dividir dos números.
        /// </summary>
        private static void Divide()
        {
            double result = firstNumberWritten / secondNumberWritten;
            Console.WriteLine($"\nLa división de {firstNumberWritten} entre {secondNumberWritten} es {result}.");
        }

        /// <summary>
        /// Método encargado de elevar un númnero. 
        /// </summary>
        private static void Pow()
        {
            double result = 1;

            for (byte i = 0; i < secondNumberWritten; i++)
            {
                result *= firstNumberWritten;
            }

            Console.WriteLine($"\nEl número {firstNumberWritten} elevado a {secondNumberWritten} es {result}.");

        }
    }
}