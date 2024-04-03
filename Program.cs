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