using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaCalculator.Services
{
    public class Calculator
    {
        public readonly Dictionary<string, int> precedence = new Dictionary<string, int>
        {
            { "+", 1 },
            { "-", 1 },
            { "*", 2 },
            { "/", 2 },
            { "^", 3 },
            { "sqrt", 4 }
        };

        public double Evaluate(string expression)
        {
            Stack<double> values = new Stack<double>();
            Stack<string> operators = new Stack<string>();
            int i = 0;

            while (i < expression.Length)
            {
                char c = expression[i];

                if (char.IsDigit(c) || (c == '.' && i > 0 && char.IsDigit(expression[i - 1])))
                {
                    //Считаем число
                    string num = "";
                    while (i < expression.Length && char.IsDigit(expression[i]) || expression[i] == '.')
                    {
                        num += expression[i];
                        i++;
                    }
                    values.Push(double.Parse(num));
                    continue;
                }
                else if (precedence.ContainsKey(c.ToString()))
                {
                    while (operators.Count > 0 && precedence[operators.Peek()] >= precedence[c.ToString()])
                    {
                        ApplyOperator(operators, values);
                    }
                    operators.Push(c.ToString());
                }
                else if (c == '(')
                {
                    operators.Push(c.ToString());
                }
                else if (c == ')')
                {
                    while (operators.Count > 0 && operators.Peek() != "(")
                    {
                        ApplyOperator(operators, values);
                    }
                    operators.Pop();//Убираем "("
                }
                i++;
            }
            while (operators.Count > 0)
            {
                ApplyOperator(operators, values);
            }
            return values.Pop();
        }

        private void ApplyOperator(Stack<string> operators, Stack<double> values)
        {
            string op = operators.Pop();
            if (op == "sqrt")
            {
                double value = values.Pop();
                values.Push(Math.Sqrt(value));
            }
            else
            {
                double right = values.Pop();
                double left = values.Pop();
                switch (op)
                {
                    case "+":
                        values.Push(left + right);
                        break;
                    case "-":
                        values.Push(left - right);
                        break;
                    case "*":
                        values.Push(left * right);
                        break;
                    case "/":
                        values.Push(left / right);
                        break;
                    case "^":
                        values.Push(Math.Pow(left, right));
                        break;
                }
            }
        }
    }
}