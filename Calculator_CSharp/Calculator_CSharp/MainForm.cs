using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator_CSharp {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }
        public void InputNumber(string num) {
            if (formulaBox.Text == "0") {
                formulaBox.Text = num;
            } else {
                int len = formulaBox.Text.Length;
                char lastChar = formulaBox.Text[len - 1];
                if (lastChar == ')' || lastChar == '!' || lastChar == '%') {
                    formulaBox.Text += "*";
                }
                formulaBox.Text += num;
            }
        }
        private void button0_Click(object sender, EventArgs e) {
            InputNumber("0");
        }
        private void button1_Click(object sender, EventArgs e) {
            InputNumber("1");
        }
        private void button2_Click(object sender, EventArgs e) {
            InputNumber("2");
        }
        private void button3_Click(object sender, EventArgs e) {
            InputNumber("3");
        }
        private void button4_Click(object sender, EventArgs e) {
            InputNumber("4");
        }
        private void button5_Click(object sender, EventArgs e) {
            InputNumber("5");
        }
        private void button6_Click(object sender, EventArgs e) {
            InputNumber("6");
        }
        private void button7_Click(object sender, EventArgs e) {
            InputNumber("7");
        }
        private void button8_Click(object sender, EventArgs e) {
            InputNumber("8");
        }
        private void button9_Click(object sender, EventArgs e) {
            InputNumber("9");
        }

        private void buttonClear_Click(object sender, EventArgs e) {
            formulaBox.Text = "0"; // 重置为0
        }

        private void buttonBackSpace_Click(object sender, EventArgs e) {
            int len = formulaBox.Text.Length;
            if (len > 0) {
                int i = len - 1;
                if (formulaBox.Text[len - 1] == '(') { // 一次性删除函数
                    for (i = len - 2; i >= 0; --i) {
                        if (!char.IsLetter(formulaBox.Text[i])) {
                            break;
                        }
                    }
                    ++i; // 重要！
                }
                formulaBox.Text = formulaBox.Text.Substring(0, i);
            }
            len = formulaBox.Text.Length;
            if (len == 0) {
                formulaBox.Text = "0"; // 如果空了，置为0
            }
        }

        public void InputOperator(string op) {
            int len = formulaBox.Text.Length;
            char lastChar = formulaBox.Text[len - 1];
            if (char.IsDigit(lastChar) || lastChar == ')' || lastChar == '!' || lastChar == 'π') { // 可以直接加
                formulaBox.Text = formulaBox.Text + op;
            } else if (lastChar == '+' || lastChar == '-' || lastChar == '*' || lastChar == '/') { // 改变最后一位的符号
                formulaBox.Text = formulaBox.Text.Substring(0, len - 1) + op;
            }
        }
        private void buttonPlus_Click(object sender, EventArgs e) {
            InputOperator("+");
        }
        private void buttonMinus_Click(object sender, EventArgs e) {
            InputOperator("-");
        }
        private void buttonMultiply_Click(object sender, EventArgs e) {
            InputOperator("*");
        }
        private void buttonDivide_Click(object sender, EventArgs e) {
            InputOperator("/");
        }
        private void buttonPower_Click(object sender, EventArgs e) {
            InputOperator("^");
        }

        public void InputFunction(string func) {
            if (formulaBox.Text == "0") {
                formulaBox.Text = func; // 替换0
            } else {
                int len = formulaBox.Text.Length;
                char lastChar = formulaBox.Text[len - 1];
                if (char.IsDigit(lastChar) || lastChar == ')') {
                    formulaBox.Text += "*"; // 2sin(x) -> 2*sin(x)
                }
                formulaBox.Text += func;
            }
        }
        private void buttonSin_Click(object sender, EventArgs e) {
            InputFunction("sin(");
        }
        private void buttonCos_Click(object sender, EventArgs e) {
            InputFunction("cos(");
        }
        private void buttonTan_Click(object sender, EventArgs e) {
            InputFunction("tan(");
        }
        private void buttonLog_Click(object sender, EventArgs e) {
            InputFunction("ln(");
        }

        private void buttonFactorial_Click(object sender, EventArgs e) {
            int len = formulaBox.Text.Length;
            char lastChar = formulaBox.Text[len - 1];
            if (char.IsDigit(lastChar)) { // 阶乘在数字后
                int i;
                for (i = len - 1; i >= 0; --i) { // 找到数字的起始位置
                    if (!char.IsDigit(formulaBox.Text[i]) && formulaBox.Text[i] != '.')
                        break;
                }
                ++i; // 重要！
                formulaBox.Text = formulaBox.Text.Substring(0, i) + "(" + formulaBox.Text.Substring(i, len - i) + ")!"; // 括起数字
            } else if (lastChar == ')') { // 阶乘在括号后
                formulaBox.Text += '!';
            }
        }
        private void buttonPercent_Click(object sender, EventArgs e) {
            int len = formulaBox.Text.Length;
            char lastChar = formulaBox.Text[len - 1];
            if (char.IsDigit(lastChar) || lastChar == ')') { // 百分号在数字或括号后
                formulaBox.Text += '%';
            }
        }
        private void buttonLeftBracket_Click(object sender, EventArgs e) {
            if (formulaBox.Text == "0") {
                formulaBox.Text = "(";
            } else {
                int len = formulaBox.Text.Length;
                char lastChar = formulaBox.Text[len - 1];
                if (char.IsDigit(lastChar) || lastChar == ')' || lastChar == '!' || lastChar == '%') {
                    formulaBox.Text += "*";
                }
                formulaBox.Text += "(";
            }
        }

        private void buttonRightBracket_Click(object sender, EventArgs e) {
            int len = formulaBox.Text.Length;
            char lastChar = formulaBox.Text[len - 1];
            if (char.IsDigit(lastChar) || lastChar == ')' || lastChar == '!' || lastChar == '%' || lastChar == 'π') { // 只能接在数字，右括号，阶乘后，π，%后
                formulaBox.Text += ")";
            }
        }

        private void buttonEqual_Click(object sender, EventArgs e) {
            Calculator calculator = new Calculator();
            calculator.SetFormula(formulaBox.Text); // 传表达式
            double ans = calculator.Calculate(); // 计算
            resultBox.Text = ans.ToString(); // 显示结果
        }

        private void buttonDot_Click(object sender, EventArgs e) {
            int len = formulaBox.Text.Length;
            char lastChar = formulaBox.Text[len - 1];
            if (char.IsDigit(lastChar)) {
                int i;
                for (i = len - 1; i >= 0; --i) {
                    if (!char.IsDigit(formulaBox.Text[i]))
                        break;
                }
                if (i < 0) {
                    formulaBox.Text += ".";
                } else {
                    if (formulaBox.Text[i] != '.') {
                        formulaBox.Text += ".";
                    }
                }
            }
        }

        public void InputSpecial(string sp) {
            if (formulaBox.Text == "0") {
                formulaBox.Text = sp; // 替换0
            } else {
                int len = formulaBox.Text.Length;
                char lastChar = formulaBox.Text[len - 1];
                if (char.IsDigit(lastChar) || lastChar == ')') {
                    formulaBox.Text += "*"; // 2sin(x) -> 2*sin(x)
                }
                formulaBox.Text += sp;
            }
        }
        private void buttonPi_Click(object sender, EventArgs e) {
            InputSpecial("π");
        }

    }
}
