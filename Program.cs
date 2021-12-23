using System;

namespace Project3
{
    class Program
    {
        public static string[,] QUEUE = new string[30, 3];

        static void Main(string[] args)
        {

            int[,] Branches = { {-1}, {-1}, {-1} };
            
            bool stop = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Select a Branch:");
                Console.WriteLine("1) Leeds");
                Console.WriteLine("2) London");
                Console.WriteLine("3) Sheffield");
                Console.WriteLine("9) quit applicaiton");
                int menuseleciton = Convert.ToInt32(Console.ReadLine());

                if (menuseleciton != 9 && menuseleciton > 0 && menuseleciton < 4)
                {
                    JobMenu(ref menuseleciton, ref Branches);
                }
                else
                {
                    stop = true;
                }

            } while (!stop);
            
        }

        public static void JobMenu(ref int menuseleciton, ref int[,] Branches)
        {
            int start = Branches[menuseleciton, 0];
            string[,] PreDefArray = { { "AAA", "1" }, { "CCC", "3" }, { "BBB", "2" }, { "DDD", "4" } };

            Console.WriteLine("Use predefined array?");
            Console.WriteLine("1) Yes");
            Console.WriteLine("2) No");
            int predefarray = Convert.ToInt32(Console.ReadLine());
            if (predefarray == 1){
                for (int i = 0; i < 4; i++)
                {
                    AddRecord(ref Branches, menuseleciton,ref start, PreDefArray[i,0], Convert.ToInt32(PreDefArray[i, 1]));
                }
            }

            bool stop = false;
            do {
                Console.Clear();
                Console.WriteLine("\nCurrent Jobs List :");
                int counter= 1;
                for (int i = start; i != -1; i = Convert.ToInt32(QUEUE[i, 2]))
                {
                    Console.WriteLine(counter + ": " + QUEUE[i, 0]);
                    counter++;
                }
                Console.WriteLine("\n");

                Console.WriteLine("Please select:");
                Console.WriteLine("1) Add new record");
                Console.WriteLine("2) delete a record");
                Console.WriteLine("3) find job priority");
                Console.WriteLine("9) back to branch menu");
                
                int switchseleciton = Convert.ToInt32(Console.ReadLine());

            switch (switchseleciton)
            {
                case 1:
                    Console.WriteLine("enter job name:");
                    string JobName = Console.ReadLine();
                    Console.WriteLine("enter priority:");
                    int JobPriority = Convert.ToInt32(Console.ReadLine());
                    AddRecord(ref Branches, menuseleciton, ref start, JobName, JobPriority);
                    Branches[menuseleciton, 0] = start;
                    break;
                case 2:
                        Console.WriteLine("\nEnter Job Name:");
                        string jobname = Console.ReadLine();
                        int previouseposition = 0;
                        bool found = false;
                        for (int i = start; i != -1; i = Convert.ToInt32(QUEUE[i, 2]))
                        {
                            if (QUEUE[i, 0] == jobname)
                            {
                                if (i == start)
                                {
                                    start = Convert.ToInt32(QUEUE[i, 2]);
                                }
                                else
                                {
                                    QUEUE[previouseposition, 2] = QUEUE[i, 2];
                                }
                                QUEUE[i, 0] = "null";
                                QUEUE[i, 1] = "null";
                                QUEUE[i, 2] = "null";
                                Console.WriteLine("Job Deleted!\n");
                                found = true;
                                break;
                            }
                            else
                            {
                                previouseposition = i;
                            }
                        }
                        if (!found)
                        {
                            Console.WriteLine("Job not found!\n");
                        }
                        break;
                case 3:
                        Console.WriteLine("\nEnter Job Name:");
                        string Name = Console.ReadLine();
                        bool priority = false;
                        for (int i = start; i != -1; i = Convert.ToInt32(QUEUE[i, 2]))
                        {
                            if (QUEUE[i, 0] == Name)
                            {
                                Console.WriteLine("\nJob: " + QUEUE[i, 0] + " Priority: " + QUEUE[i, 1]);
                                priority = true;
                            }
                        }
                        if (!priority)
                        {
                            Console.WriteLine("\nJob not found:");
                        }
                        Console.WriteLine("\nEnter any key to continue...");
                        Console.ReadKey();
                        break;
                case 9:
                    stop = true;
                    break;
            }
            } while (!stop);
        }

        public static void AddRecord(ref int[,] Branches, int menuselection, ref int start, string name, int priority)
        {

            int newslot = getavailableslot();
            
            if (!string.IsNullOrEmpty(Convert.ToString(newslot)))
            {
                if (start == -1)
                {
                    start = newslot;
                    Branches[menuselection, 0] = start;
                    QUEUE[newslot, 0] = name;
                    QUEUE[newslot, 1] = Convert.ToString(priority);
                    QUEUE[newslot, 2] = Convert.ToString(-1);
                }
                else
                {
                    int next = start;
                    int previouse = 0;
                    bool stop = false;
                    do
                    {
                        /*if (next != -1)
                        {
                            
                            if (Convert.ToInt32(QUEUE[next, 1]) > priority)
                            {
                                if (next == start)
                                {
                                    start = newslot;
                                }
                                else
                                {
                                    QUEUE[previouse, 2] = Convert.ToString(newslot);
                                }
                                QUEUE[newslot, 0] = name;
                                QUEUE[newslot, 1] = Convert.ToString(priority);
                                QUEUE[newslot, 2] = Convert.ToString(next);
                                
                                stop = true;
                            }
                            
                        }
                        else
                        {
                            QUEUE[newslot, 0] = name;
                            QUEUE[newslot, 1] = Convert.ToString(priority);
                            QUEUE[newslot, 2] = Convert.ToString(-1);
                            QUEUE[previouse, 2] = Convert.ToString(newslot);
                            stop = true;
                        }
                        
                        if (!stop)
                        {
                            previouse = next;
                            next = Convert.ToInt32(QUEUE[next, 2]);
                        }*/



                        if (next == -1)
                        {
                            QUEUE[newslot, 0] = name;
                            QUEUE[newslot, 1] = Convert.ToString(priority);
                            QUEUE[newslot, 2] = "-1";
                            QUEUE[previouse, 2] = Convert.ToString(newslot);

                            stop = true;
                        }
                        else
                        {
                            if (Convert.ToInt32(QUEUE[next, 1]) > priority)
                            {
                                if (next == start)
                                {
                                    start = newslot;
                                }
                                else
                                {
                                    QUEUE[previouse, 2] = Convert.ToString(newslot);
                                }
                                QUEUE[newslot, 0] = name;
                                QUEUE[newslot, 1] = Convert.ToString(priority);
                                QUEUE[newslot, 2] = Convert.ToString(next);

                                stop = true;
                            }

                            previouse = next;
                            next = Convert.ToInt32(QUEUE[next, 2]);
                        }
                        
                    } while (!stop);
                }
                
            }
            else
            {
                Console.WriteLine("Qeueu array is full");
            }
        }

        public static int getavailableslot()
        {
            for(int i = 1; i < (QUEUE.Length/3); i++)
            {
                if (string.IsNullOrEmpty(QUEUE[i,0]))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
