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

   


    static void Main()
    {
        Console.WriteLine("Please enter a word to spell it backwards and see if it's an palindrome");
        string word = Console.ReadLine();
        word = word.ToLower();
        Console.WriteLine(ReverseText(word));
        Console.WriteLine(word);
        
    }
}

