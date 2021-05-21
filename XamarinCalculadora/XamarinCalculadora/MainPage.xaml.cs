using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Globalization;


namespace XamarinCalculadora
{
    public partial class MainPage : ContentPage
    {
        int currentState = 1;
        string mathOperator;
        double firstNumber, secondNumber;

        public MainPage()
        {
            InitializeComponent();
            OnClear(new object(), new EventArgs());
        }

        private void OnClear(object sender, EventArgs e)
        {
            firstNumber = 0;
            secondNumber = 0;
            currentState = 1;
            this.resultText.Text = "0";
        }

        // Recebe os valores para o calculo

        private void OnSelectNumber(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string pressed = button.Text;

            // Verificação se já há numeros pressionados ou se foi pressionado algum operador
            if(this.resultText.Text == "0" || currentState < 0)
            {
                this.resultText.Text = "";
                if (currentState < 0)
                    currentState *= -1;
            }

            this.resultText.Text += pressed;

            double number; 
            if(double.TryParse(this.resultText.Text, out number))
            {
                this.resultText.Text = number.ToString("N0");
                if(currentState == 1)
                {
                    // É salvo no primeiro número se o estado ainda for 1
                    firstNumber = number;
                }
                else
                {
                    // É salvo no segundo número se o estado for diferente de 1
                    secondNumber = number;
                }
            }
        }

        // Seleção do operador
        private void OnSelectOperator(object sender, EventArgs e)
        {
            currentState = -2;
            Button button = (Button)sender;
            string pressed = button.Text;
            mathOperator= pressed;
        }

        private void OnCalculate(object sender, EventArgs e)
        {
            Double result = 0;

            // Definindo o tipo de operação

            if(mathOperator == "+")
            {
                result = firstNumber + secondNumber;
            }
            if (mathOperator == "-")
            {
                result = firstNumber - secondNumber;
            }
            if (mathOperator == "X")
            {
                result = firstNumber * secondNumber;
            }
            if (mathOperator == "/")
            {
                result = firstNumber / secondNumber;
            }

            // Devolve o resultado para o resultText e seta o resultado como primeiro numero para o caso de mais operações

            string resulta= result.ToString();
            this.resultText.Text = string.Format(new CultureInfo("pt-BR"), "{0:N2}", resulta);
            firstNumber = result;
            currentState = -1;
        }
    }
}
