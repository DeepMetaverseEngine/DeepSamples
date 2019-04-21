namespace SampleFileUpdater
{
#if FALSE

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (true)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    OutputStream input = new OutputStream(ms, null);
                    InputStream output = new InputStream(ms, null);
                    ulong j = 0;
                    for (uint i = 0; i < uint.MaxValue; i++)
                    {
                        input.PutVU32(i);
                        ms.Position = 0;
                        j = output.GetVU32();
                        if (j != i)
                        {
                            throw new Exception("VLQ Error");
                        }
                    }
                    for (ulong i = 0; i < ulong.MaxValue; i++)
                    {
                        input.PutVU64(i);
                        ms.Position = 0;
                        j = output.GetVU64();
                        if (j != i)
                        {
                            throw new Exception("VLQ Error");
                        }
                    }
                }
            }
            if (false)
            {
                ThreadExecutor threadpool = new ThreadExecutor(10);
                TaskTest[] tasks = new TaskTest[10];
                for (int i = 0; i < tasks.Length; i++)
                {
                    tasks[i] = new TaskTest(threadpool);
                }
                Console.In.ReadLine();
                threadpool.Shutdown();
                Console.WriteLine("done");
                Console.In.ReadLine();
            }
            Launcher.Main();
        }
    }


    public class TaskTest : IDisposable
    {
        private static Random random = new Random();
        private static int index = 0;
        private Future future;

        public TaskTest(ThreadExecutor threadpool)
        {
            future = threadpool.ScheduleAtFixedRate(update, 10, 10);
           
        }
        public void Dispose()
        {
            future.Cancel();
        }
        private void update()
        {
            lock (this)
            {
                index++;
                Console.WriteLine("Index = " + index); 
                Thread.Sleep(random.Next(1000, 10000));
            }
            Console.WriteLine("TC = " + Process.GetCurrentProcess().Threads.Count);
        }
    }
    //---------------------------------------------------------------------------------------------------------------------------

    class Program
    {
        static void Main(string[] args)
        {

            Type t = typeof(Person);
            Person person = new Person();
            string word = "hello";
            Person p = null;
            MethodInfo methodInfo = t.GetMethod("Say");
            object[] param = new object[] { word, p, 3 };
            //             MethodInfo methodInfo = t.GetMethod("Good");
            //             object[] param = new object[] { word };

            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < 1000000; i++)
            {
                methodInfo.Invoke(person, param);
            }
            watch.Stop();
            Console.WriteLine("1000000 times invoked by Reflection: " + watch.ElapsedMilliseconds + "ms");

            Stopwatch watch1 = new Stopwatch();
            var fastInvoker = DynamicMethodHelper.GetMethodInvoker(methodInfo);
            watch1.Start();
            for (int i = 0; i < 1000000; i++)
            {
                fastInvoker(person, param);
            }
            watch1.Stop();
            Console.WriteLine("1000000 times invoked by FastInvoke: " + watch1.ElapsedMilliseconds + "ms");

            Stopwatch watch2 = new Stopwatch();
            watch2.Start();
            for (int i = 0; i < 1000000; i++)
            {
                person.Say(ref word, out p, 3);
            }
            watch2.Stop();
            Console.WriteLine("1000000 times invoked by DirectCall: " + watch2.ElapsedMilliseconds + "ms");

            Console.WriteLine("Person.index=" + Person.index);
            Console.ReadLine();
        }
    }
    public class Person
    {
        public static int index = 0;

        public Person Say(ref string word, out Person p, int avi)
        {
            word = "ttt" + avi.ToString();
            p = new Person();
            return p;
        }

        public void Good(string abc)
        {
            index++;
        }
        public void Best(string abc)
        {

        }
    }
#endif

    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}