using System;
using System.Windows.Controls;

namespace Ecran.GUI
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
            var output = $"{DateTime.Now.ToString(Properties.Resources.TimeFormat)}:\n{message}";

            textBox.Text = string.IsNullOrWhiteSpace(textBox.Text)
                ? $"{output}"
                : $"{output}\n\n{textBox.Text}";
        }
    }
}
