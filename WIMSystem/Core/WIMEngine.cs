namespace Cosmetics.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using WIMSystem.Models.Enums;
    using WIMSystem.Models;
    using WIMSystem.Core.Contracts;

    internal class WIMEngine : IEngine
    {
        private const string InvalidCommand = "Invalid command name: {0}!";
        private const string CategoryExists = "Category with name {0} already exists!";
        private const string CategoryCreated = "Category with name {0} was created!";
        private const string CategoryDoesNotExist = "Category {0} does not exist!";
        private const string ProductDoesNotExist = "Product {0} does not exist!";
        private const string ProductAddedToCategory = "Product {0} added to category {1}!";
        private const string ProductRemovedCategory = "Product {0} removed from category {1}!";
        private const string ShampooAlreadyExist = "Shampoo with name {0} already exists!";
        private const string ShampooCreated = "Shampoo with name {0} was created!";
        private const string ToothpasteAlreadyExist = "Toothpaste with name {0} already exists!";
        private const string ToothpasteCreated = "Toothpaste with name {0} was created!";
        private const string ProductAddedToShoppingCart = "Product {0} was added to the shopping cart!";
        private const string ProductDoesNotExistInShoppingCart = "Shopping cart does not contain product with name {0}!";
        private const string ProductRemovedFromShoppingCart = "Product {0} was removed from the shopping cart!";
        private const string TotalPriceInShoppingCart = "${0} total price currently in the shopping cart!";
        private const string InvalidGenderType = "Invalid gender type!";
        private const string InvalidUsageType = "Invalid usage type!";
        private const char SPLIT_CHAR = ',';


        private readonly IWIMFactory factory;
       // private readonly IShoppingCart shoppingCart;
        private readonly ICommandParser commandParser;
        protected readonly IDictionary<string, ICategory> categories;
        protected readonly IDictionary<string, IProduct> products;

        public CosmeticsEngine(
            ICosmeticsFactory factory,
            IShoppingCart shoppingCart,
            ICommandParser commandParser)
        {
            this.factory = factory;
            this.shoppingCart = shoppingCart;
            this.commandParser = commandParser;
            this.categories = new Dictionary<string, ICategory>();
            this.products = new Dictionary<string, IProduct>();
        }

        public void Start()
        {
            var commands = commandParser.ReadCommands();
            var commandResult = this.ProcessCommands(commands);
            this.PrintReports(commandResult);
        }

        // This is removed due to inadequate testing. You will need to use Console which is Integration testing.
        //private IList<ICommand> ReadCommands()
        //{
        //    var commands = new List<ICommand>();

        //    var currentLine = Console.ReadLine();

        //    while (!string.IsNullOrEmpty(currentLine))
        //    {
        //        var currentCommand = Command.Parse(currentLine);
        //        commands.Add(currentCommand);

        //        currentLine = Console.ReadLine();
        //    }

        //    return commands;
        //}

        private IList<string> ProcessCommands(IList<ICommand> commands)
        {
            var reports = new List<string>();

            foreach (var command in commands)
            {
                try
                {
                    var report = this.ProcessSingleCommand(command);
                    reports.Add(report);
                }
                catch (Exception ex)
                {
                    reports.Add(ex.Message);
                }
            }

            return reports;
        }

        private string ProcessSingleCommand(ICommand command)
        {
            switch (command.Name)
            {
                case "CreateTeam":
                    var teamName = command.Parameters[0];
                    return this.CreateTeam(categoryName);
                case "CreateMember":
                    var memberName = command.Parameters[0];
                    return this.CreateMember(categoryName);
                case "CreateBoard":
                    var boardName = command.Parameters[0];
                    return this.CreateBoard(categoryName);

                case "AddToTeam":
                    var teamNameToAdd = command.Parameters[0];
                    var memberNameForAdding = command.Parameters[1];
                    return this.AddToTeam(teamNameToAdd,memberNameForAdding);

                case "RemoveFromTeam":
                    var teamNameToRemove = command.Parameters[0];
                    var memberNameForRemoving = command.Parameters[1];
                    return this.RemoveFromTeam(teamNameToRemove, memberNameForRemoving);

                case "CreateBug":
                    var bugTitle = command.Parameters[0];
                    var bugDescription = command.Parameters[1];
                    var stepsToReproduce = command.Parameters[2].Trim().Split(SPLIT_CHAR).ToList();
                    var bugPriority = this.GetPriority(command.Parameters[3]);
                    var bugSeverity = this.GetSeverity(command.Parameters[4]);
                    var bugAssignee = this.GetMember(command.Parameters[5]);
                    var bugComments = command.Parameters[6].Trim().Split(SPLIT_CHAR).ToList();
                    return this.CreateBug(bugTitle,bugDescription,stepsToReproduce,bugPriority,bugSeverity,bugAssignee,bugComments);

                case "CreateStory":
                    var storyTitle = command.Parameters[0];
                    var storyDescription = command.Parameters[1];
                    var storyPriority = this.GetPriority(command.Parameters[3]);
                    var storySize = this.GetSize(command.Parameters[4]);
                    var storyAssignee = this.GetMember(command.Parameters[5]);
                    var storyComments = command.Parameters[6].Trim().Split(SPLIT_CHAR).ToList();
                    return this.CreateStory(storyTitle,storyDescription,storyPriority,storySize,storyAssignee,storyComments);

                case "CreateFeedback":
                    var feedbackTitle = command.Parameters[0];
                    var feedbackDescription = command.Parameters[1];
                    var feedbackComments = command.Parameters[6].Trim().Split(SPLIT_CHAR).ToList();
                    return this.CreateFeedback(feedbackTitle,feedbackDescription,feedbackComments);

                default:
                    return string.Format(InvalidCommand, command.Name);
            }
        }

        private void PrintReports(IList<string> reports)
        {
            var output = new StringBuilder();

            foreach (var report in reports)
            {
                output.AppendLine(report);
            }

            Console.Write(output.ToString());
            //writer.Write(output.ToString());
        }

        private string CreateCategory(string categoryName)
        {
            if (this.categories.ContainsKey(categoryName))
            {
                return string.Format(CategoryExists, categoryName);
            }

            var category = this.factory.CreateCategory(categoryName);
            this.categories.Add(categoryName, category);

            return string.Format(CategoryCreated, categoryName);
        }

        private string AddToCategory(string categoryNameToAdd, string productToAdd)
        {
            if (!this.categories.ContainsKey(categoryNameToAdd))
            {
                return string.Format(CategoryDoesNotExist, categoryNameToAdd);
            }

            if (!this.products.ContainsKey(productToAdd))
            {
                return string.Format(ProductDoesNotExist, productToAdd);
            }

            var category = this.categories[categoryNameToAdd];
            var product = this.products[productToAdd];

            category.AddProduct(product);

            return string.Format(ProductAddedToCategory, productToAdd, categoryNameToAdd);
        }

        private string RemoveCategory(string categoryNameToAdd, string productToRemove)
        {
            if (!this.categories.ContainsKey(categoryNameToAdd))
            {
                return string.Format(CategoryDoesNotExist, categoryNameToAdd);
            }

            if (!this.products.ContainsKey(productToRemove))
            {
                return string.Format(ProductDoesNotExist, productToRemove);
            }

            var category = this.categories[categoryNameToAdd];
            var product = this.products[productToRemove];

            category.RemoveProduct(product);

            return string.Format(ProductRemovedCategory, productToRemove, categoryNameToAdd);
        }

        private string ShowCategory(string categoryToShow)
        {
            if (!this.categories.ContainsKey(categoryToShow))
            {
                return string.Format(CategoryDoesNotExist, categoryToShow);
            }

            var category = this.categories[categoryToShow];

            return category.Print();
        }

        private string CreateShampoo(string shampooName, string shampooBrand, decimal shampooPrice, GenderType shampooGender, uint shampooMilliliters, UsageType shampooUsage)
        {
            if (this.products.ContainsKey(shampooName))
            {
                return string.Format(ShampooAlreadyExist, shampooName);
            }

            var shampoo = this.factory.CreateShampoo(shampooName, shampooBrand, shampooPrice, shampooGender, shampooMilliliters, shampooUsage);
            this.products.Add(shampooName, shampoo);

            return string.Format(ShampooCreated, shampooName);
        }

        private string CreateToothpaste(string toothpasteName, string toothpasteBrand, decimal toothpastePrice, GenderType toothpasteGender, IList<string> toothpasteIngredients)
        {
            if (this.products.ContainsKey(toothpasteName))
            {
                return string.Format(ToothpasteAlreadyExist, toothpasteName);
            }

            var toothpaste = this.factory.CreateToothpaste(toothpasteName, toothpasteBrand, toothpastePrice, toothpasteGender, toothpasteIngredients);
            this.products.Add(toothpasteName, toothpaste);

            return string.Format(ToothpasteCreated, toothpasteName);
        }

        private string AddToShoppingCart(string productName)
        {
            if (!this.products.ContainsKey(productName))
            {
                return string.Format(ProductDoesNotExist, productName);
            }

            var product = this.products[productName];
            this.shoppingCart.AddProduct(product);

            return string.Format(ProductAddedToShoppingCart, productName);
        }

        private string RemoveFromShoppingCart(string productName)
        {
            if (!this.products.ContainsKey(productName))
            {
                return string.Format(ProductDoesNotExist, productName);
            }

            var product = this.products[productName];

            if (!this.shoppingCart.ContainsProduct(product))
            {
                return string.Format(ProductDoesNotExistInShoppingCart, productName);
            }

            this.shoppingCart.RemoveProduct(product);

            return string.Format(ProductRemovedFromShoppingCart, productName);
        }

        private GenderType GetGender(string genderAsString)
        {
            switch (genderAsString)
            {
                case "men":
                    return GenderType.Men;
                case "women":
                    return GenderType.Women;
                case "unisex":
                    return GenderType.Unisex;
                default:
                    throw new InvalidOperationException(InvalidGenderType);
            }
        }

        private UsageType GetUsage(string usageAsString)
        {
            switch (usageAsString)
            {
                case "everyday":
                    return UsageType.EveryDay;
                case "medical":
                    return UsageType.Medical;
                default:
                    throw new InvalidOperationException(InvalidUsageType);
            }
        }
    }
}
