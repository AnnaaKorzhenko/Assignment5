Console.Write("Please, enter some words > ");
var userInput = Console.ReadLine();
var userWords = userInput.Split();
var badWords = new List<string>();
var correctWords = File.ReadAllLines("../../../words_list.txt");

foreach (var word in userWords)
{
    Console.WriteLine(word);
    var isFound = false;
    foreach (var correctWord in correctWords)
    {
        if (correctWord == word)
        {
            Console.WriteLine($"[OK] {word} found!");
            isFound = true;
            break;
        }
    }

    if (isFound == false)
    {
        Console.WriteLine($"[Error] {word} not found!");
        badWords.Add(word);
    }
}

Console.Write("Looks like you have typos in those words: ");
foreach (var badWord in badWords)
{
    Console.Write(badWord);
    Console.Write(" ");
}
////// this is preparation