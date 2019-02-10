namespace WIMSystem.Menu.Contracts
{
    public interface IMainMenu
    {
        void ShowCredits();

        void ShowLogo();

        void ShowMenu();

        void Start();

        void ConsoleBatchCommands();

        void ConsoleParameters(int indexOfItem);
    }
}