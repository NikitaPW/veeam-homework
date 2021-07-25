using System;
using System.Diagnostics;
using System.Threading;

namespace ProcessKiller
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Wrong number of parameters. Please, use the format: <process_name> <max_life_time_minutes> <check_time_minutes>");
                Environment.Exit(1);
            }
            Console.WriteLine("Process killer started\n");
            Console.WriteLine($"Provided parameters:\nProcess Name: {args[0]}\nMaximum Life Time: {args[1]} min\nCheck time: {args[2]} min\n");
            Console.WriteLine("The utility will monitor and kill the processes that are working more than specified time.");
            
            Process[] processes;
            string processName = args[0];
            int lifeTime = int.Parse(args[1]);
            int checkTime = int.Parse(args[2]);

            Console.WriteLine("Started monitoring...");
            while (true)
            {
                processes = Process.GetProcessesByName(processName);
                foreach(var p in processes)
                {
                    TimeSpan runTime;
                    try
                    {
                        runTime = DateTime.Now - p.StartTime;
                        if (runTime.TotalMinutes > lifeTime)
                        {
                            p.Kill();
                            Console.WriteLine($"The process {p.Id} '{p.ProcessName}' worked {Math.Truncate(runTime.TotalMinutes)} " +
                            $"minutes and {(int)(runTime.TotalSeconds - Math.Truncate(runTime.TotalMinutes) * 60)} seconds was killed.");
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                Thread.Sleep(checkTime * 60000);
            }
        }
    }
}
