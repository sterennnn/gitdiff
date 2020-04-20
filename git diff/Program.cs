using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Reflection;

namespace git_diff
{
    class program
    {
        public static void Main(string[] args)
        {
        string path1 = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"GitRepositories_1a.txt");
        string path2 = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"GitRepositories_1b.txt");
        string path3 = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"GitRepositories_2a.txt");
        string path4 = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"GitRepositories_2b.txt");
        string path5 = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"GitRepositories_3a.txt");
        string path6 = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"GitRepositories_3b.txt");
        string[] paths = new string[] { path1, path2, path3, path4, path5, path6 };
        q1:
        Console.WriteLine("Which file would you like to choose?"); 
        for(int i = 0; i < paths.Length; i++)
            {
                Console.WriteLine($"To select {paths[i]} type {i}");
            }
        string Userinput = Console.ReadLine();
        int choice = int.Parse(Userinput);
            if(choice > 5 || choice < 0)
            {
                Console.WriteLine("Please select a valid option");
                goto q1;
            }
        Console.WriteLine($"You have selected {paths[choice]}");
        q2:
        Console.WriteLine("Which file would you like to compare with");
        for(int i = 0; i < paths.Length; i++)
            {
                Console.WriteLine($"To select {paths[i]} type {i}.");
            }
        string Userinput2 = Console.ReadLine();
        int choice2 = int.Parse(Userinput2);
        if (choice2 == choice || choice2 < 0 || choice2 > 5)
        {
            Console.WriteLine("Please select a different file to compare with.");
            goto q2;
        }
        comparing.compare(paths[choice], paths[choice2]);
        }
    }
    class comparing
    {
        static object lockobject = new object ();
        public static void compare(string j, string k)
        {
            string[] lines1 = File.ReadAllLines(j);
            string[] lines2 = File.ReadAllLines(k);
            if (lines1.Length == lines2.Length)
            {
                for (int n = 0; n < lines1.Length; n++)
                {
                    if(lines1[n] != lines2[n])
                    {
                        Console.WriteLine($"The files differ on line {n}");
                    }
                }
            }
            var lines1and2 = lines1.Zip(lines2, (n, m) => new { line1 = n, line2 = m });
            foreach(var nm in lines1and2)
            {
                int i = 0;
                string[] words1 = nm.line1.Split(" ");
                string[] words2 = nm.line2.Split(" ");
                foreach(string word in words1)
                {
                    if (i < words1.Length)
                    {
                        if (words1[i] == words2[i])
                        {
                            Console.Write($"{words1[i]} ");
                            i++;
                        }
                        else if (!nm.line2.Contains(words1[i]))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write($"{words1[i]} ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write($"{words2[i]} ");
                            Console.ResetColor();
                            i++;
                        }
                        else if (!nm.line1.Contains(words2[i]))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write($"{words2[i]} ");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write($"{words1[i]} ");
                            Console.ResetColor();
                            i++;
                        }
                    }
                }
            }
            Console.WriteLine("Press any key to close.");
            Console.ReadLine();
        }
    }
}

 
