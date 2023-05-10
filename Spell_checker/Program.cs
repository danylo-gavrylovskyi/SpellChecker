Console.WriteLine("Enter your string: ");
string userLine = Console.ReadLine()!;
string[] words = userLine.Split(new char[] { ' ', ',', '.', '!', '?', ':', '-' }, StringSplitOptions.RemoveEmptyEntries);

string[] wordsList = File.ReadAllLines("C:\\Users\\Марина\\Downloads\\words_list.txt");

List<string> misspelledWords = new List<string>();
for (int i = 0; i < words.Length; i++)
{
    if (!(wordsList.Contains(words[i].ToLower())))
    {
        misspelledWords.Add(words[i]);
    }
}

Console.Write("\nLooks like you have typos in next words: ");
for (int i = 0; i < misspelledWords.Count; i++)
{
    Console.Write($"'{misspelledWords[i]}' ");
}
Console.WriteLine();

int DamerauLevenshteinDistance(string userWord, string checkWord)
{
    int[,] wordsMatrixx = new int[userWord.Length + 1, checkWord.Length + 1];
    if (userWord.Length == 0)
    {
        return checkWord.Length;
    }

    if (checkWord.Length== 0)
    {
        return userWord.Length;
    }

    for (int i = 0; i < userWord.Length; i++)
    {
        wordsMatrixx[i, 0] = i;
    }
    for (int i = 0; i < checkWord.Length; i++)
    {
        wordsMatrixx[0, i] = i;
    }

    for (int i = 1; i < userWord.Length; i++)
    {
        for (int j = 1; j < checkWord.Length; j++)
        {
            int cost = userWord[i-1] == checkWord[j-1] ? 0 : 1;
            wordsMatrixx[i, j] = Math.Min(wordsMatrixx[i - 1, j - 1] + cost, Math.Min(wordsMatrixx[i - 1, j] + 1, wordsMatrixx[i, j - 1] + 1));
        }
    }

    for (int i = 0; i < userWord.Length; i++)
    {
        for (int j = 0; j < checkWord.Length; j++)
        {
            Console.Write(wordsMatrixx[i, j]);
        }
        Console.WriteLine(userWord, checkWord );
        
    }
    

    return wordsMatrixx[userWord.Length, checkWord.Length];
}


foreach (string userWord in misspelledWords)
{
    Dictionary<string, int> wordDistance = new Dictionary<string, int>();
    foreach (string checkWord in wordsList)
    {
        int distance = DamerauLevenshteinDistance(userWord, checkWord);
        wordDistance.Add(checkWord, distance);
    }
    var ourWords = wordDistance.OrderBy(x => x.Value).Take(5);
    
    string correctedWords = "";
    foreach (KeyValuePair<string, int> pair in ourWords)
    {
        correctedWords += pair.Key + "," + pair.Value + ";";
    }
    Console.WriteLine($"Maybe, you mean {correctedWords}");
}

