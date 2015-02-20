﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"test.csv";
            using (StreamReader sr = File.OpenText(path))
            {
                string[] mask;
                string s = " ";
                int numcol = 0;
                int check = 0;
                int numRow = 0;

                //// Save data from CSV file in a list
                List<List<string>> data = new List<List<string>>();
                while ((s = sr.ReadLine()) != null)
                {
                    numRow++;
                    check++;
                    int countCreate = 0;
                    int i = 0;
                    mask = s.Split(' ', ',');
                    if (check == 1)
                    {
                        numcol = mask.Length;
                    }
                    if (countCreate == 0)
                    {

                        for (int j = 0; j < mask.Length; j++)
                        {
                            data.Add(new List<string>());
                        }
                        countCreate++;
                    }

                    foreach (var element in mask)
                    {
                        data[i].Add(element);
                        i++;
                    }

                    i = 0;


                }
                /// List of names of cols
                List<string> listcols = new List<string>();
                for (int i = 0; i < numcol; i++)
                {

                    listcols.Add(data[i][0]);
                }


                string query = Console.ReadLine();
                string[] commands = query.Split(' ', ',');
                bool checkPrint = false;
                bool checkFound = false;

                /// the switch takes the first command of user
                switch (commands[0])
                {
                    case "SHOW":
                        {
                            for (int i = 0; i < numcol; i++)
                            {
                                Console.Write("|");
                                Console.Write(String.Format("{0, -10}", data[i][0]));
                                Console.Write("|");
                            }
                            Console.WriteLine();
                            break;
                        }
                    case "FIND":
                        {
                            for (int i = 0; i < numcol; i++)
                            {
                                Console.Write("|");
                                Console.Write(" ");
                                Console.Write(String.Format("{0, -10}", data[i][0]));
                                Console.Write(" ");
                                Console.Write("|");
                            }
                            Console.WriteLine();
                            string contain = commands[1].Trim(new Char[] { '"' });
                            List<int> foundRow = new List<int>();
                            for (int i = 0; i < numRow; i++)
                            {
                                for (int k = 0; k < numcol; k++)
                                {
                                    if (data[k][i].Contains(contain))
                                    {
                                        checkFound = true;
                                        string rowMatch = " ";
                                        for (int m = 0; m < numcol; m++)
                                        {
                                            rowMatch += data[m][i].ToString() + " ";
                                            Console.Write("|");
                                            Console.Write(" ");
                                            Console.Write(String.Format("{0, -10}", data[m][i]));
                                            Console.Write(" ");
                                            Console.Write("|");
                                        }
                                        Console.WriteLine(" ");

                                        //// match the choosen symbol with each row in which it is
                                        Regex Contain = new Regex(@contain);
                                        Match isMatch = Contain.Match(rowMatch);



                                        if (i + 1 < numRow)
                                        {
                                            k = 0;
                                            i = i + 1;
                                        }
                                    }
                                }
                            }

                            if (checkFound == false)
                            {
                                Console.WriteLine("Nothing found!");
                            }
                            break;
                        }
                    case "SUM":
                        {
                            int choosencol = 0;

                            for (int i = 0; i < listcols.Count; i++)
                            {
                                if (listcols[i] == commands[1])
                                {
                                    checkPrint = true;
                                    choosencol = i;
                                }

                            }

                            /// Check whether the query contains correct col
                            if (checkPrint == false)
                            {
                                Console.WriteLine("Wrong col!");
                                break;
                            }

                            int sum = 0;
                            for (int i = 0; i < data[choosencol].Count; i++)
                            {
                                int x;
                                if (int.TryParse(data[choosencol][i], out x))
                                {
                                    sum += int.Parse(data[choosencol][i]);
                                }
                            }
                            Console.WriteLine(sum);
                            break;
                        }
                    case "SELECT":
                        {

                            bool checkLim = false;
                            List<int> result = new List<int>();
                            int choosencol = 0;
                            ////Check whether the query contains LIMIT
                            int lim = 0;
                            for (int i = 1; i < commands.Length; i++)
                            {
                                for (int k = 0; k < commands.Length; k++)
                                {
                                    if (commands[k] == "LIMIT")
                                    {
                                        lim = int.Parse(commands[k + 1]) + 1;


                                        /// Check whether a limit is a valid number
                                        if (lim <= 0 || lim > numRow)
                                        {
                                            checkLim = true;
                                        }
                                    }
                                }

                            }


                            for (int i = 1; i < commands.Length; i++)
                            {
                                for (int k = 0; k < listcols.Count; k++)
                                {
                                    /// Checks whick cols are chosen
                                    if (commands[i] == listcols[k])
                                    {
                                        checkPrint = true;
                                        choosencol = k;
                                        result.Add(k);
                                    }
                                }
                            }

                            /// Check whether the query contains correct col
                            if (checkPrint == false)
                            {
                                Console.WriteLine("Wrong col!");
                                break;
                            }

                            /// Check whether the limit is valid number
                            if (checkLim == true)
                            {
                                Console.WriteLine("The LIMIT should be more than 0 and less than {0}", numRow);
                                break;
                            }

                            ////Prints the table the if/else statements determines whether there is a limit or no 
                            if (lim == 0)
                            {
                                for (int m = 0; m < numRow; m++)
                                {
                                    for (int i = 0; i < result.Count; i++)
                                    {
                                        Console.Write("|");
                                        Console.Write(" ");
                                        Console.Write(String.Format("{0, -10}", data[result[i]][m]));
                                        Console.Write(" ");
                                        Console.Write("|");
                                    }
                                    Console.WriteLine(" ");

                                }
                            }
                            else
                            {
                                for (int m = 0; m < lim; m++)
                                {
                                    for (int i = 0; i < result.Count; i++)
                                    {
                                        Console.Write("|");
                                        Console.Write(" ");
                                        Console.Write(String.Format("{0, -10}", data[result[i]][m]));
                                        Console.Write(" ");
                                        Console.Write("|");
                                    }
                                    Console.WriteLine(" ");

                                }

                            }
                            break;
                        }

                    default:
                        {
                            Console.WriteLine("Please write correct query!");
                            break;
                        }
                }

            }
        }
    }
}