using System;
using System.Windows.Controls;

namespace Ecran.GUI.Main
{
    class ConsoleTextBox
    {
        private TextBox textBox;

        public ConsoleTextBox(TextBox textBox)
        {
            this.textBox = textBox;
        }

        public void Show(string message)
        {
            var output = $"{DateTime.Now.ToString(Resource.TimeFormat)} :: {message}";

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = $"{output}";
            }
            else
            {
                textBox.Text = $"{output}\n\n{textBox.Text}";
            }
        }
    }
}
