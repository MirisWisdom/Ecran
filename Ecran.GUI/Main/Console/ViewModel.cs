using System;

namespace Ecran.GUI.Main.Console
{
    public class ViewModel
    {
        private Model model;

        public string Output {
            get {
                return model.History;
            }
            set {
                model.History = value;
            }
        }

        public void AddLog(string message)
        {
            model.Message = $"{DateTime.Now.ToString("h:mm:ss tt")}: {message}";

            if (string.IsNullOrWhiteSpace(model.History))
            {
                model.History = model.Message;
            }
            else
            {
                model.History = $"{model.History}\n{model.Message}";
            }

        }

        public ViewModel(Model consoleModel)
        {
            model = consoleModel;
        }
    }
}
