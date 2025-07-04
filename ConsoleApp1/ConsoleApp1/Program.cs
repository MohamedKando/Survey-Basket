// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
//void method1(object n)
//{
//    for (int i = 0; i < Convert.ToInt32(n); i++)
//    {
//        //Console.WriteLine(i);
//        //Console.WriteLine("Out from method1");

//    }
   
//}
//void method2()
//{
//    for (int i = 0; i < 5; i++)
//    {
//        //Console.WriteLine(i);
//        //Console.WriteLine("Out from method2");
//        //Thread.Sleep(1000);
//    }
//}
//void method3()
//{
//    for (int i = 0; i < 5; i++)
//    {
//        //Console.WriteLine(i);
//        //Console.WriteLine("Out from method3");
//    }
//}
//ParameterizedThreadStart start = new ParameterizedThreadStart(method1);
//Thread t1 = new Thread(start)
//{
//    Name = "tmethod1"
//};
//Thread t2 = new Thread(method2)
//{
//    Name = "tmethod2"
//};
//Thread t3 = new Thread(method3)
//{
//    Name = "tmethod3"
//};

//t1.Start(10);


//void hello(string n)
//{
//    Console.WriteLine(n);
//}
//mydelegate del = new mydelegate(hello);
//del("hello");
//delegate void mydelegate(string n);
static void m1()
{
    Console.WriteLine("thread 1 is running");
}
static void m2()
{
    Console.WriteLine("thread 2 is running");
}
static void mainm()
{
    Console.WriteLine("main thresh is running");
    Thread ts = new Thread(m1);
    Thread ts2 = new Thread(m2);
    ts.Start();
    
    Console.WriteLine("end of ts1");

    ts2.Start();
    ts.Join();
    ts2.Join();
    Console.WriteLine("end of ts2");

    Console.WriteLine("end of main thread");
}
//mainm();
ParallelOptions parallelOptions = new ParallelOptions();
parallelOptions.MaxDegreeOfParallelism = 1;

Parallel.For(0, 10, parallelOptions, x => Console.WriteLine("{0} parallel thread val {1}",Thread.CurrentThread.ManagedThreadId,x));
int[] arr = { 1, 2, 3, 4, 5 };
int sum = 0;
for (int i = 0; i < arr.Length; i++)
{
    if (arr[i] % 2 == 0)
       
        sum += arr[i];
}
Console.WriteLine(sum);





