using System;



class Program
{
    public static string ReverseText(string text)
    {
        //string result = "";

        char[] charArray = text.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);



        //return result;
    
    }
    public static string Palindrom(string i)
    {
        return i;
    }
   


    static void Main()
    {
        Console.WriteLine("Please enter a word to spell it backwards and to see if it's a palindrome");
        string word = Console.ReadLine();
        word = word.ToLower();
        Console.WriteLine(ReverseText(word));
        Console.WriteLine(Palindrom(word));
        
        
    }
}

