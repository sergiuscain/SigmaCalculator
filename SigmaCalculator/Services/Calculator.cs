using Flee.PublicTypes;
using System;
using static Android.Renderscripts.ScriptGroup;

namespace SigmaCalculator.Services
{
    public class Calculator
    {
        public string EvaluateExpression(string expression)
        {
            var pattern = @"Sqrt\((.*?)\)";
            var replacement = "$1^(1/2)";
            try
            {
                // Проверка на деление на ноль
                if (expression.Contains("/0"))
                {
                    return "Error";
                }

                // Создаем контекст для вычисления выражения
                var context = new ExpressionContext();

                // Добавляем поддержку функции Sqrt
                context.Variables["Sqrt"] = new Func<double, double>(Math.Sqrt);

                //Заменяем Sqrt на результат вычисления квадратного корня
                var newExpression = System.Text.RegularExpressions.Regex.Replace(expression, pattern, replacement);

                // Вычисляем результат
                var expr = context.CompileGeneric<double>(newExpression);
                var result = expr.Evaluate();

                return result.ToString(); // Преобразуем результат в строку
            }
            catch (Exception)
            {
                return "Error"; // Общая обработка ошибок
            }
        }
    }
}
