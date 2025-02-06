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

                _name = value;
            }
        }
        public virtual string _class { get; protected set; }
        private string _description { get; set; }
        private List<string> _food { get; set; }
        private string _colors { get; set; }

        private int _maxSize;
        public int MaxSize
        {
            get { return _maxSize; }
            set
            {
                if (MaxSize <= 0)
                {
                    _maxSize = MaxSize;
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
        private List<string> _personelComments { get; set; }


        public T CreateAnimal<T>(string name, string description, List<string> food, string colors, int maxSize,
            string nativeLocation, List<string> facts, List<byte[]> photos, List<string> personelComments) where T : AnimalBase, new()
        {
            // create methods for each of these questions

            string animalName = NameSetter(name);
            Name = animalName;
            string animalDescription = DescriptionSetter(description);
            _description = animalDescription;
            Console.WriteLine("What does this animal eat for food?");
            Console.WriteLine("What are the common colors of this animal?");
            Console.WriteLine("About how large are these animals? (please enter a whole number in pounds)");
            Console.WriteLine("What areas is this animal native to?");
            Console.WriteLine("Do you have any facts about this animal you would like to add? (press 1 for yes or 2 for no)");
            Console.WriteLine("Do you have any photos of this animal you would like to add?");
            Console.WriteLine("Do you have any comments or thoughts you would like to add about this animal? (press 1 for yes or 2 for no)");
        }

        public string NameSetter(string name)
        {
            Console.WriteLine("What is the name of the animal you would like to add?");
            while (true)
            {
                Console.WriteLine("Animal names must have at least two letters");
                string userInput = Console.ReadLine();
                WordStandardizer(userInput, "There are not enough letters in that animal name :(");
                if (userInput != null)
                {
                    name = userInput;
                    return name;
                }
                else
                {
                    Console.WriteLine("Please enter a valid name.");
                }
            }

        }
        public string DescriptionSetter(string description)
        {
            Console.WriteLine("What is a good description of the animal?");
            string userInput = Console.ReadLine();
            description = userInput;
            return description;
        }
        public string FoodSetter(List<string> finalFoodList)
        {
            Console.WriteLine("If you know what this animal eats for food, Press 1. Otherwise press any other key.");
            int userInput = 0;
            List<string> foods = new List<string>();
            
            
            //the next thing to do is to use the word standarizer method and finish writting this food method. 
            //then see line 52 comment for next task. 
                    

               
        }
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
    }
}



