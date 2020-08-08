using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_CSharp {
    class Calculator {
        private string formula;
        private double ans;
        private Stack<double> result = new Stack<double>();
        private Stack<char> sign = new Stack<char>();

        public Calculator() {
            this.formula = "";
            this.ans = 0;
            Console.WriteLine("Welcome");
        }

        public override string ToString() {
            return "Calculator []";
        }

        public string GetFormula() {
            return this.formula;
        }

        public void SetFormula(string formula) {
            this.formula = formula;
        }

        public double GetAns() {
            return this.ans;
        }

        public void SetAns(double ans) {
            this.ans = ans;
        }

        public void Reset() { // 清零
            SetAns(0);
            SetFormula("");
            while (sign.Count > 0) {
                sign.Pop();
            }
            while (result.Count > 0) {
                result.Pop();
            }
            result.Push(0.0);
            Console.WriteLine("Reset");
        }

        public bool CheckBracket() { // 检查括号匹配
            int cntLeft = 0;
            int len = formula.Length;

            for (int i = 0; i < len; ++i) {
                char currentBit = formula[i]; // 当前的字符
                if (currentBit == '(') {
                    ++cntLeft;
                }
                if (currentBit == ')') {
                    --cntLeft;
                    if (cntLeft < 0) {
                        Console.WriteLine("Missing left bracket");
                        return false;
                    }
                }
            }

            if (cntLeft > 0) {
                Console.WriteLine("Missing right bracket");
                return false;
            } else {
                Console.WriteLine("Correct bracket");
                return true;
            }
        }

        public int PriorityOfSign(char c) { // 运算符优先级比较
            int priLevel;

            switch (c) {
                case '+': // plus
                case '-': // minus
                    priLevel = 1;
                    break;

                case '*': // times
                case '/': // divide
                    priLevel = 2;
                    break;

                case '^': // power
                case '√': // square root
                    priLevel = 3;
                    break;

                case 's': // sin
                case 'c': // cos
                case 't': // tan
                    priLevel = 4;
                    break;

                case '!': // factorial
                case 'l': // log
                case '%': // percent
                    priLevel = 5;
                    break;

                default:
                    priLevel = 0;
                    break;
            }
            return priLevel;
        }

        public bool DoubleEq(double number, int tar) { // double的相等
            double eps = 5e-6;
            return (number - tar < eps);
        }

        public double Factorial(double number) { // 阶乘
            double ans = 1.0;
            if (DoubleEq(number, (int)(number)) == false) {
                Console.WriteLine("When using n! , n should be a natural number.");
            }
            for (int i = 1; i <= number; ++i) {
                ans *= i;
            }
            return ans;
        }

        public double ToRadian(double angle) {
            return angle * Math.PI / 180;
        }

        public double CalOne(char c) { // 一元运算符计算
            double ans = 0.0;
            double temp = result.Peek();
            result.Pop();
            Console.WriteLine("\t Pop " + temp + " from result");

            switch (c) {
                case '√': // sqrt
                    ans = Math.Pow(temp, 0.5);
                    break;

                case 's': // sin
                    ans = Math.Sin(ToRadian(temp));
                    break;

                case 'c': // cos
                    ans = Math.Cos(ToRadian(temp));
                    break;

                case 't': // tan
                    ans = Math.Tan(ToRadian(temp));
                    break;

                case 'a': // abs
                    ans = Math.Abs(temp);
                    break;

                case '!': // factorial
                    ans = Factorial(temp);
                    break;

                case 'l': // log
                    ans = Math.Log(temp);
                    break;

                case '%': // percent
                    ans = temp / 100;
                    break;

                default:
                    break;
            }

            result.Push(ans);
            Console.WriteLine("\t Push " + ans + " to result");
            return ans;
        }

        public double CalTwo(char c) { // 二元运算符计算
            double ans = 0.0;

            double temp1 = result.Peek();
            result.Pop();
            Console.WriteLine("\t Pop " + temp1 + " from result");

            double temp2 = result.Peek();
            result.Pop();
            Console.WriteLine("\t Pop " + temp2 + " from result");

            switch (c) {
                case '+':
                    ans = temp2 + temp1;
                    break;

                case '-':
                    ans = temp2 - temp1;
                    break;

                case '*':
                    ans = temp2 * temp1;
                    break;

                case '/': // 判断除法分母是否为0
                    try {
                        ans = temp2 / temp1;
                    } catch (ArithmeticException) {
                        Console.WriteLine("cannot divided by 0!");
                    }
                    break;

                case '^':
                    ans = Math.Pow(temp2, temp1);
                    break;

                default:
                    break;
            }

            Console.WriteLine("\t Push " + ans + " to result");
            result.Push(ans);
            return ans;
        }

        public int OpCheck(char c) { // 判断是几元运算符
            int opType = 0;
            switch (c) {
                case '√': // sqrt
                case 's': // sin
                case 'c': // cos
                case 't': // tan
                case 'a': // log
                case '!': // factorial
                case 'l': // log
                case '%': // percent
                    opType = 1;
                    break;

                case '+': // plus
                case '-': // minus
                case '*': // times
                case '/': // divide
                case '^': // power
                    opType = 2;
                    break;

                default:
                    opType = 0;
                    break;
            }
            return opType;
        }

        public double Cal(char c) { // 计算
            double ans = 0;
            int opType = OpCheck(c);

            if (opType == 1) { // 是一元运算符
                ans = CalOne(c);
                Console.WriteLine("一元运算");
            } else if (opType == 2) { // 二元运算符
                ans = CalTwo(c);
                Console.WriteLine("二元运算");
            } else {
                Console.WriteLine("Wrong operator" + c);
            }
            return ans;
        }

        public double Calculate() { // 计算全过程

            int len = formula.Length; // 表达式长度
            if (len == 0) {
                formula = "0=";
            } else if (formula[len - 1] != '=') { // 补足末尾的等号
                formula = formula + "=";
            }

            len = formula.Length; // 表达式长度
            string num = ""; // 暂存数字，后转换为 double 对象入栈

            for (int i = 0; i < len; ++i) { // 遍历表达式

                char currentBit = formula[i]; // 当前的字符
                char previousBit = formula[Math.Max(0, i - 1)]; // 上一位字符

                if (currentBit == '=') { // 是等号就结束
                    break;
                }

                // 是数字的一部分 如 12.34
                while (char.IsDigit(currentBit) || currentBit == '.') {
                    num += currentBit; // num是string，+的作用是在后面补字符
                    ++i;
                    currentBit = formula[i];

                    if (!char.IsDigit(currentBit) && currentBit != '.') {
                        double d = double.Parse(num); // string转double
                        result.Push(d);
                        Console.WriteLine("\t Push " + d + " to result");
                        num = ""; // 清空num，准备存下一个
                        break;
                    }
                }

                // 特殊字符
                if (currentBit == 'e') { // 特殊字符 e
                    if (previousBit == ')' || char.IsDigit(previousBit)) {
                        sign.Push('*');
                        Console.WriteLine("\t Push * to sign");
                    }
                    result.Push(Math.E);
                    Console.WriteLine("\t Push e to result");
                    continue;
                } else if (currentBit == 'π') { // 特殊字符 pi
                    if (previousBit == ')' || char.IsDigit(previousBit)) {
                        sign.Push('*');
                        Console.WriteLine("\t Push * to sign");
                    }
                    result.Push(Math.PI);
                    Console.WriteLine("\t Push π to result");
                    continue;
                }

                // 不是数字（是运算符号）
                if (currentBit == 's' || currentBit == 'c' || currentBit == 't' || currentBit == 'a') {
                    i += 2; // sin cos tan abs
                }

                if (sign.Count == 0) { // 目前栈中无运算符，直接入栈
                    sign.Push(currentBit);
                    Console.WriteLine("\t Push " + currentBit + " to sign");
                } else { // 栈中已有运算符
                    if (currentBit == '(') { // ( 直接入栈
                        sign.Push(currentBit);
                        Console.WriteLine("\t Push " + currentBit + " to sign");
                    } else if (currentBit == ')') { // ) 特判，一直弹出到 (
                        while (sign.Peek() != '(') { // 弹出 ( 之前的符号
                            Cal(sign.Peek());
                            Console.WriteLine("\t Pop " + sign.Pop() + " from sign");
                        }
                        Console.WriteLine("\t Pop " + sign.Pop() + " from sign"); // 弹出 (
                    } else { // 其他符号
                        while (sign.Count > 0 && PriorityOfSign(sign.Peek()) >= PriorityOfSign(currentBit)) {
                            // 栈顶运算符的优先级不低于当前运算符，计算并弹出栈顶
                            Cal(sign.Peek());
                            Console.WriteLine("\t Pop " + sign.Pop() + " from sign");
                        }
                        if (currentBit != '=') { // 入栈
                            sign.Push(currentBit);
                            Console.WriteLine("\t Push " + currentBit + " to sign");
                        }
                    }
                }
            }

            while (sign.Count > 0) { // 算完栈中剩余的运算符
                Cal(sign.Peek());
                Console.WriteLine("\t Pop " + sign.Pop() + " from sign");
            }

            Console.WriteLine("\t Pop " + result.Peek() + " as answer"); // 弹出 (
            return result.Pop();
        }
    }
}
