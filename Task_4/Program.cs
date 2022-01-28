
using System.Diagnostics;
using System.Numerics;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Game rules: \n select two numbers in the range from 0 to 1,000,000 " +
                      " \n The program take random a number from your range" +
                      "\n You have unlimit attempts to guess this number " +
                      "\n press 's' key button for start game " +
                      "\n press 'q' key button for exit: ");
        
        ConsoleKeyInfo user_input = default;
        do
        {
            user_input = Console.ReadKey();

            if (user_input.Key == ConsoleKey.S)
            {
                StartGame();
            }
            else if (user_input.Key == ConsoleKey.Q)
            {
                Console.WriteLine("\n Bye.");
                Environment.Exit(0);
            }
            else
            {
                Console.Write("\nIncorrect input, try again: ");
            }
        } while (user_input.Key != ConsoleKey.Q);
    }

    static void StartGame()
    {
        int array_len = 2;
        try
        {
            int[] array = new int[array_len];
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write($"\nEnter value for element [{i+1}]: ");
                array[i] = int.Parse(Console.ReadLine());
                if (array[i] < 0 | array[i] > 1000000)
                {
                    Console.WriteLine("Incorrect range, use numbers from 0 to 1 000 000 only");
                    StartGame();
                }
            }
            GuessingNumber(array);
        }
        catch
        {
            Console.WriteLine("\nIncorrect input, use positive numbers only, according the range");
        }
    }

    static int GuessingNumber(int[] array)
    {
        int guess_counter = 0;
        int? user_guess = null;
        int random_value = Randomizer(array);
        Console.WriteLine("Random number generated!");
        
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();
        do
        {
            Console.WriteLine("try to guess: ");
            user_guess = int.Parse(Console.ReadLine());
            if (user_guess < random_value)
            {
                Console.WriteLine("too low, try again");
                guess_counter++;
            }
            else if (user_guess > random_value)
            {
                Console.WriteLine("too higth, try again");
                guess_counter++;
            }
            else if (user_guess == random_value)
            {
                Console.WriteLine("You win!");
            }
        } while (user_guess != random_value);
        int score = ScoreSettlement(array, guess_counter);
        stopWatch.Stop();
        TimeSpan ts = stopWatch.Elapsed;
        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}",
            ts.Hours, ts.Minutes, ts.Seconds);
        Console.WriteLine("Game time: " + elapsedTime);
        
        Console.WriteLine("Attempts count: " + guess_counter);
        Console.WriteLine("Your score: " + score);
        return 0;

    }

    static int Randomizer(int[] array)
    {
        Random rand = new Random();
        int value = rand.Next(array[0], array[1]);
        // Console.WriteLine(value);
        return value;
    }

    static int ScoreSettlement(int[] array, int guessCounter)
    {
        int v_range = array[1] - array[0] + 1;
        int floor_pow = PowerFloor(v_range);
        int ceil_pow = PowerCeil(v_range);
        int nearest_pow = NearestPowToVRange( floor_pow, ceil_pow, v_range);
        double result = Math.Ceiling((double) (100 * (nearest_pow - guessCounter) / nearest_pow));
        if (result == 0)
        {
            return 0;
        }
        return (int) result;
    }
    
    static int PowerFloor(int x) 
    {
        int power = 1;
        while ((x >>= 1) != 0) power <<= 1;
        return power;
    }

    static int PowerCeil(int x) 
    {
        if (x <= 1) return 1;
        int power = 2;
        x--;
        while ((x >>= 1) != 0) power <<= 1;
        return power;
    }

    static int NearestPowToVRange(int floor_pow, int ceil_pow, int v_range)
    {
        int a = v_range - floor_pow;
        int b = ceil_pow - v_range;
        int pow;
        if (a < b)
        {
            pow = (int) Math.Log2(floor_pow);
        }
        else
        {
            pow = (int) Math.Log2(ceil_pow);
        }
        return pow;
    }
}

