Console.WriteLine("Enter your string: ");
string userLine = Console.ReadLine()!;
string[] words = userLine.Split(new char[] { ' ', ',', '.', '!', '?', ':', '-' }, StringSplitOptions.RemoveEmptyEntries);

string[] wordsList = File.ReadAllLines("C:\\Users\\danya\\Spell_checker\\Spell_checker\\words_list.txt");

List<string> misspelledWords = new List<string>();
for (int i = 0;i < words.Length; i++)
{
    if (!(wordsList.Contains(words[i])))
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
