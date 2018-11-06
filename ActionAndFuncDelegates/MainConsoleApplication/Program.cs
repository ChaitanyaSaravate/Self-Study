namespace MainConsoleApplication
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            Helper helper = new Helper();

            Action action = helper.JustSayHi;
            action.Invoke();

            Action<string> actionString = helper.SayHiTo;
            actionString("Chaitanya"); // Action delegate does not return a value

            Func<string, string> func1 = helper.TellMySurname;
            Console.WriteLine("It's " + func1("Chaitanya")); // func delegate returns a value

            Predicate<string> predicate = helper.IsMyNameCorrect; // predicate is just special type of func delegate which always returns a boolean.
            Console.WriteLine("Is My name correct ? - " + predicate("Chaitanya"));

            helper.CookVegetable("Corn", helper.FryVegetable, 10); // Fry the corn
            helper.CookVegetable("Spinach", helper.SteamVegetable, 5); // Steam the spinach
            helper.CookVegetable("Greenbeans", helper.BakeVegetable, 20); // Bake the greenbeans

            Console.ReadLine();
        }
    }

    class Helper
    {
        public void JustSayHi()
        {
            Console.WriteLine("Hi");
        }

        public void SayHiTo(string name)
        {
            Console.WriteLine("Hi " + name);
        }

        public bool IsMyNameCorrect(string name)
        {
            return name.Equals("Chaitanya");
        }

        public string TellMySurname(string name)
        {
            Console.WriteLine("Telling surname of the " + name);
            return "Saravate";
        }

        public void CleanVegetable(string vegetableName)
        {
            Console.WriteLine("Cleaning " + vegetableName);
        }

        public void ServeVegetable(string vegetableName)
        {
            Console.WriteLine("Serving " + vegetableName);
        }

        public void BakeVegetable(string vegName, int time)
        {
            Console.WriteLine("Baking " + vegName + " for " + time + " minutes.");
        }

        public void SteamVegetable(string vegName, int time)
        { 
            Console.WriteLine("Steaming " + vegName + " for " + time + " minutes.");
        }

        public void FryVegetable(string vegName, int time)
        {
            Console.WriteLine("Frying " + vegName + " for " + time + " minutes.");
        }

        /// <summary>
        /// The only difference while cooking all vegetables is the method to be used to cook each type of vegetable along with the time to do it. Rest two things 
        /// like cleaning and serving are same. So this method accepts the "action" to be performed. So caller needs to specify the "action" applicable to the 
        /// type of the vegetable he wants to cook.
        /// </summary>
        /// <param name="vegName"></param>
        /// <param name="vegSpecificAction"></param>
        /// <param name="time"></param>
        public void CookVegetable(string vegName, Action<string, int> vegSpecificAction, int time)
        {
            this.CleanVegetable(vegName);
            vegSpecificAction(vegName, time);
            this.ServeVegetable(vegName);
        }
    }
}
