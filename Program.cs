// this is preparation
//===============================================================================
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


// LCS algorithm
//===============================================================================

// first, we find the length of the LCS
var s1 = "AoKfE";
var s2 = "gAKrE";
var result = new List<char>();

var rowN = s1.Length;
var colN = s2.Length;

// 2d array for storing lcs
var table = new int[rowN + 1, colN + 1];

// we go through each cell in the table
for (int r = 1; r <= rowN; r++) {
    for (int c = 1; c <= colN; c++) {
        if (s1[r - 1] == s2[c - 1]) { // because 1&1 cell in the table compares 0&0 characters from the strings
            table[r, c] = table[r - 1, c - 1] + 1; // if matching
        }
        else {
            table[r, c] = Math.Max(table[r, c - 1], table[r - 1, c]); //if not matching, we choose max from left and top
        }
        //PrintMatrix(table, s1, s2);
    }
}
PrintMatrix(table, s1, s2);

//===============================================================================

// second, we find the actual letters of the LCS
// Start from the bottom-right corner of the filled table and trace back to find the LCS
while (rowN > 0 && colN > 0) {
    
    if (s1[rowN - 1] == s2[colN - 1]) {
        result.Add(s1[rowN - 1]); //if match, add to result and move back by diagonal
        rowN--; 
        colN--;
    }
    else if (table[rowN - 1, colN] > table[rowN, colN - 1]) {
        rowN--; // if top is max, move up
    }
    else {
        colN--; // if left is max, move left
    }
}

result.Reverse();
foreach (var ch in result)
{
    Console.Write(ch);
}


static List<string> GetWordSuggestions(string str1, List<string> correctWords)
{
    List<string> suggestions = new List<string>();

    int minDistance = int.MaxValue;

    foreach (string correctWord in correctWords)
    {
        int distance = LevenshteinDistance(str1, correctWord);
        if (distance < minDistance)  // перевіряємо, чи поточна відстань менша за мінімальну
        {
            minDistance = distance;  // якщо так, оновлюємо мінімальну відстань
            suggestions.Clear();      // очищаємо список попередніх найближчих слів
            suggestions.Add(correctWord);  // додаємо поточне слово в список
        }
        else if (distance == minDistance)  // якщо відстань така ж, як мінімальна
        {
            suggestions.Add(correctWord);  // додаємо слово в список
        }
    }

    return suggestions;
}

static int LevenshteinDistance(string str1, string str2)
{
    int[,] distanceMatrix = new int[str1.Length + 1, str2.Length + 1]; // створюється 2д матриця 

    for (int i = 0; i <= str1.Length; i++) // перший рядок матриці заповнюється послідовними числами від 0 до str1.Length
    {
        distanceMatrix[i, 0] = i;
    }

    for (int j = 0; j <= str2.Length; j++) //  перший стовпець матриці заповнюється послідовними числами від 0 до str2.Length
    {
        distanceMatrix[0, j] = j;
    }

    for (int i = 1; i <= str1.Length; i++)
    {
        for (int j = 1; j <= str2.Length; j++)
        {
            int cost;
            if (str1[i - 1] == str2[j - 1])
            {
                cost = 0; // якщо символи рівні, вартість редагування 0
            }
            else
            {
                cost = 1; // якщо символи відмінні, вартість редагування 1
            }
            distanceMatrix[i, j] = Math.Min(distanceMatrix[i - 1, j] + 1, Math.Min(distanceMatrix[i, j - 1] + 1, distanceMatrix[i - 1, j - 1] + cost)); //додавання, видалення та заміна символа в str1
        }
    }

    return distanceMatrix[str1.Length, str2.Length];
}



// debugging function
void PrintMatrix(int[,] table, string s1, string s2)
{
    Console.Write("    ");
    foreach (var letter in s2)    { Console.Write($"{letter} "); }
    Console.WriteLine("");
    for (var i = 0; i < table.GetLength(0); i++)
    {
        if (i>0) {Console.Write($"{s1[i-1]} ");} else { Console.Write("  ");}
        for (var j = 0; j < table.GetLength(1); j++) {
            Console.Write($"{table[i, j]} ");
        }
        Console.WriteLine();
    }
}