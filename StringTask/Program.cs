 

public class Program {

    //entry point
    static void Main()
    {
        string userInput;

        do
        {
            Console.Write("Enter a binary string (or 'exit' to quit): ");
            userInput = Console.ReadLine();

            if (userInput.ToLower() != "exit" && userInput.ToLower() != "test")
            {
                if (IsValidBinaryString(userInput))
                {
                    try
                    {
                        bool isGoodBinaryString = IsGoodBinaryString(userInput);
                        Console.WriteLine($"Is the binary string good? {isGoodBinaryString}");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid binary string.");
                }
            }
            if (userInput.ToLower() == "test")
                TestCases();

        } while (userInput.ToLower() != "exit");

      
    }
    static void TestCases()
    {

        // Test cases
        Console.WriteLine(IsGoodBinaryString("1100"));      // True
        Console.WriteLine(IsGoodBinaryString("101"));       // False
        Console.WriteLine(IsGoodBinaryString("0011"));      // True
        Console.WriteLine(IsGoodBinaryString("111000"));    // False
        Console.WriteLine(IsGoodBinaryString("010101"));    // True
        Console.WriteLine(IsGoodBinaryString("1101010"));   // False

        // Additional test cases
        Console.WriteLine(IsGoodBinaryString(""));           // True (empty string)
        Console.WriteLine(IsGoodBinaryString("0"));          // False
        Console.WriteLine(IsGoodBinaryString("1"));          // False
        Console.WriteLine(IsGoodBinaryString("011"));        // False
        Console.WriteLine(IsGoodBinaryString("1001"));       // True
        Console.WriteLine(IsGoodBinaryString("111111"));     // True
        Console.WriteLine(IsGoodBinaryString("000000"));     // True
    }

    /// <summary>
    ///  custom algo for checking binary string validity
    /// </summary>
    /// <param name="binaryString"></param>
    /// <returns></returns>
    static bool IsGoodBinaryString(string binaryString)
    {
        int countZeros = 0;
        int countOnes = 0;

        foreach (char bit in binaryString)
        {
            if (bit == '0')
                countZeros++;
            else  
                countOnes++;

            if (countOnes < countZeros)
                return false;
        }

        return countZeros == countOnes;
    }
    // validate the input string 
    static bool IsValidBinaryString(string input)
    {
        foreach (char bit in input)
        {
            if (bit != '0' && bit != '1')
            {
                return false;
            }
        }

        return true;
    }
}
