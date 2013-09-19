using System;
using System.Linq;


class Program
{
    public static string ReverseText(string text)
    {        
        char[] charArray = text.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    public static bool Palindrom(string i)
    {
        return i.SequenceEqual(i.Reverse());
    }

    static void Main()
    {
        Console.WriteLine("Please enter a word to spell it backwards and to see if it's a palindrome\n");
        string word = Console.ReadLine();
        word = word.ToLower();
        Console.Write("\nHere is your word spelled backwards: \n\n" + ReverseText(word) + "\n");
        Console.WriteLine("\n Is your word a palindrome? \n\n" + Palindrom(word) + "\n");
               
    }
}

