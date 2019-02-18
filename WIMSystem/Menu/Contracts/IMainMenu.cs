﻿using System.Collections.Generic;

namespace WIMSystem.Menu.Contracts
{
    public interface IMainMenu
    {
        void ShowCredits();

        void ShowLogo();

        string ShowMenu(IList<MenuItem> mainMenuItems);

        //void Start();

        //void ConsoleBatchCommands();

        string ConsoleParameters(int indexOfItem, IList<MenuItem> mainMenuItems);
    }
}