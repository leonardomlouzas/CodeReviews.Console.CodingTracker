
using System.Globalization;

internal class GetUserInput
{
    CodingController codingController = new();
    internal void MainMenu()
    {
        bool closeApp = false;
        while (!closeApp)
        {
            Console.Clear();
            Console.WriteLine("-* MAIN MENU *-");
            Console.WriteLine("1: View records");
            Console.WriteLine("2: Add record");
            Console.WriteLine("3: Delete record");
            Console.WriteLine("4: Update record");
            Console.WriteLine("0: Exit Application\n");

            string commandInput = Console.ReadLine();

            while (string.IsNullOrEmpty(commandInput))
            {
                Console.WriteLine("Invalid command. Select one of the available options");
                commandInput = Console.ReadLine();
            }

            switch (commandInput)
            {
                case "0":
                    closeApp = true;
                    Environment.Exit(0);
                    break;
                case "1":
                    codingController.Get();
                    Console.ReadLine();
                    break;
                case "2":
                    ProcessAdd();
                    break;
                case "3":
                    ProcessDelete();
                    break;
                case "4":
                    ProcessUpdate();
                    break;
                default:
                    Console.WriteLine("Invalid command. Select one of the available options");
                    break;
            }
        }
    }
    private void ProcessAdd()
    {
        Console.Clear();
        var date = GetDateInput();
        var duration = GetDurationInput();

        Coding coding = new();

        coding.Date = date;
        coding.Duration = duration;

        codingController.Post(coding);
    }

    private void ProcessDelete()
    {
        codingController.Get();
        Console.WriteLine("Enter the ID of the category to be deleted or 0 to go back to main menu");
        string commandInput = Console.ReadLine();

        while (!Int32.TryParse(commandInput, out _) || string.IsNullOrEmpty(commandInput) || Int32.Parse(commandInput) < 0)
        {
            Console.WriteLine("Invalid Id. Enter a valid one or 0 to go back to main menu");
            commandInput = Console.ReadLine();
        }

        var id = Int32.Parse(commandInput);

        if (id == 0) MainMenu();

        var coding = codingController.GetById(id);

        while (coding.Id == 0)
        {
            Console.WriteLine("Invalid ID. Try again:");
            ProcessDelete();
        }

        codingController.Delete(id);
    }

    private void ProcessUpdate()
    {
        codingController.Get();
        Console.WriteLine("Enter the Id to be updated or 0 to go back to main menu");
        string commandInput = Console.ReadLine();

        while (!Int32.TryParse(commandInput, out _) || string.IsNullOrEmpty(commandInput) || Int32.Parse(commandInput) < 0)
        {
            Console.WriteLine("Invalid Id. Enter a valid one or 0 to go back to main menu");
            commandInput = Console.ReadLine();
        }
        var id = Int32.Parse(commandInput);

        if (id == 0) MainMenu();

        var coding = codingController.GetById(id);

        while (coding.Id == 0)
        {
            ProcessUpdate();
        }

        var updateInput = "";
        bool updating = true;
        while (updating == true)
        {
            Console.Clear();
            Console.WriteLine("Type 'd' for Date");
            Console.WriteLine("Type 'u' for dUration");
            Console.WriteLine("Type 'S' to Save");
            Console.WriteLine("Type '0' to go back to Main Menu");

            updateInput = Console.ReadLine();

            switch (updateInput)
            {
                case "d":
                    coding.Date = GetDateInput();
                    break;
                case "u":
                    coding.Duration = GetDurationInput();
                    break;
                case "0":
                    MainMenu();
                    updating = false;
                    break;
                case "s":
                    updating = false;
                    break;

                default:
                    Console.WriteLine($"Invalid option");
                    break;
            }
        }
        codingController.Update(coding);
    }

    internal string GetDateInput()
    {
        Console.WriteLine("Enter the date in dd-mm-yy format or 0 to go back to main menu");
        string dateInput = Console.ReadLine();

        if (dateInput == "0") MainMenu();

        while (!DateTime.TryParseExact(dateInput, "dd-MM-yy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
        {
            Console.WriteLine("Invalid date. Enter the date in dd-mm-yy format");
            dateInput = Console.ReadLine();
            if (dateInput == "0") MainMenu();
        }

        return dateInput;
    }

    internal string GetDurationInput()
    {
        Console.WriteLine("Enter the duration in hh:mm format or 0 to go back to main menu");
        string durationInput = Console.ReadLine();

        if (durationInput == "0") MainMenu();

        while (!TimeSpan.TryParseExact(durationInput, "h\\:mm", CultureInfo.InvariantCulture, out _))
        {
            Console.WriteLine("Invalid duration. Enter the duration in hh:mm format");
            durationInput = Console.ReadLine();
            if (durationInput == "0") MainMenu();
        }

        return durationInput;
    }
}