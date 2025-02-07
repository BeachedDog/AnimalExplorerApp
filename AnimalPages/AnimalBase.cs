using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace AnimalPages.Data
{
    public abstract class AnimalBase
    {
        private string _name { get; set; }
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("The name cannot be empty");
                else
                    _name = value;
            }
        }
        public virtual string _class { get; protected set; }
        private string _description { get; set; }
        private List<string> _foods { get; set; }
        private List<string> _colors { get; set; }

        private int _weight;
        public int Weight
        {
            get { return _weight; }
            set
            {
                if (Weight >= 0)
                {
                    _weight = Weight;
                }
                else
                {
                    Console.WriteLine("Please enter a valid whole number.");
                }
            }
        }
        private string _nativeLocation { get; set; }
        private List<string> _facts { get; set; }
        private List<byte[]> _photos { get; set; }
        private List<string> _comments { get; set; }


        public T SetAnimalValues<T>( List<string> facts, List<byte[]> photos, List<string> personelComments) where T : AnimalBase, new()
        {
            // create methods for each of these questions
            T animal = new T();

            //the way this is structured, this method must be called as the following example using the subclass fish: 'var fish = SetAnimalValues<Fish>'.
            //This will create an instance of the subclass with the correct typing. 

            string animalName = NameSetter();
            animal.Name = animalName;
            string animalDescription = DescriptionSetter();
            animal._description = animalDescription;
            List<string> initialAnimalFoods = FoodAdder(_foods);
            animal._foods = initialAnimalFoods;
            List<string> animalColors = ColorSetter();
            animal._colors = animalColors;
            int animalWeight = WeightSetter();
            animal._weight = animalWeight;
            string animalNativeLocation = NativeLocationSetter();
            animal._nativeLocation = animalNativeLocation;
            List<string> animalFacts = FactsAdder(_facts);
            animal._facts = animalFacts;
            Console.WriteLine("**no current method to add photos**"); // add this method when not using a console for UI
            List<string> animalComments = CommentAdder(_comments);
            animal._comments = animalComments;

            return animal;
        }

        public string NameSetter()
        {
            Console.WriteLine("What is the name of the animal you would like to add?");
            while (true)
            {
                Console.WriteLine("Animal names must have at least two characters");
                string userInput = Console.ReadLine();
                WordStandardizer(userInput, "There are not enough characters in that animal name :(");
                if (userInput != null)
                {
                    var name = userInput;
                    return name;
                }
                else
                {
                    Console.WriteLine("Please enter a valid name.");
                }
            }

        }

        public string DescriptionSetter()
        {
            Console.WriteLine("What is a good description of the animal?");
            string userInput = Console.ReadLine();
            var description = userInput;
            return description;
        }

        public List<string> FoodAdder(List<string> foods)
        {
            if (foods == null)
            {
                foods = new List<string>();
            }
            Console.WriteLine("Enter 1 if you have foods to add. Enter any other key if not.");
            int userInput = 0;
            
            try
            {
                userInput = int.Parse(Console.ReadLine());
            }
            catch (Exception) 
            {
                return foods;
            }
            if (userInput == 1) 
            {
                Console.WriteLine("Please enter the foods you wish to add");
                string stringToBeListed = Console.ReadLine();
                List<string> newList = ListCreator(stringToBeListed);
                foreach (var item in newList)
                    foods.Add(item);
                return foods;
            }
            else
            {
                return foods;
            }
        }

        public List<string> ColorSetter()
        {
            Console.WriteLine("What colors are the animal made of?");
            string colorsString = Console.ReadLine();
            List<string> colors = ListCreator(colorsString);
            return colors;
        }

        public int WeightSetter()
        {
            Console.WriteLine("Please enter a normal weight for this animal in whole pounds.");
            try
            {
                int weight = int.Parse(Console.ReadLine());
                return weight;
            }
            catch (Exception)
            {
                int weight = 0;
                return weight;
            }
        }


        public string NativeLocationSetter()
        {
            Console.WriteLine("What areas is this animal native to?");
            string nativeLocation = Console.ReadLine();
            nativeLocation = WordStandardizer(nativeLocation, "Please enter a location with more characters");
            return nativeLocation;
        }


        public List<string> FactsAdder(List<string>_facts)
        {
            List<string> facts= InputCheckerSentences(_facts, "Do you have any interesting facts you would like to add for this animal? " +
                "Enter 1 if you so, Enter any other key if not for now.", "Please enter a fact you would like to add.");
            return facts;

        }

        public void PhotosAddrer()
        {
            //add this method when not using console for UI.
        }


        public List<string> CommentAdder(List<string> comments)
        {
            List<string> updatedComments = InputCheckerSentences(comments, "Enter 1 if there are comments or thoughts you would like to add.enter any other key if not.",
                "Please enter your comment or thought.");
            return updatedComments;
        }



        //Below is where the logic for resued items for the method setters/adders is held.
        public string WordStandardizer(string word, string errorMessage)
        {
            try
            {
                string standardizedWord = word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower();
                return standardizedWord;
            }
            catch (Exception)
            {
                Console.WriteLine($"{errorMessage}");
                return null;
            }
        }


        public List<string> ListCreator(string words)
        {
            List<string> newList = words.Split(new[] { ' ', ',', }, StringSplitOptions.RemoveEmptyEntries)
                .Where(word => !word.Contains("and"))
                .ToList();
            for (var i = 0; i < newList.Count; i++)
            {
                newList[i] = WordStandardizer(newList[i], "Please enter a word that has at least 2 characters");
            }
            return newList;
        }


        public List<string> InputCheckerSentences(List<string> items, string initialPrompt, string secondPrompt)
        {
            if (items == null)
            {
                items = new List<string>();
            }
            Console.WriteLine(initialPrompt);
            int userInput = 0;

            while(true)
            {
                try
                {
                    userInput = int.Parse(Console.ReadLine());
                }
                catch (Exception) 
                {
                    return items;
                }
                if (userInput == 1) 
                {
                    Console.WriteLine(secondPrompt);
                    string item = Console.ReadLine();
                    items.Add(item);
                    Console.WriteLine("This has been added! Anything else you would like to add? Press 1 if so, press any other key if not.");
                    userInput = 0;
                }
                else
                {
                    return items;
                }   
            }
        }
    }
}



