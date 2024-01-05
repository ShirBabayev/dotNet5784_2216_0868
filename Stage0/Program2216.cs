/*************************************
Author: Shir Babayev & Renana Houri
ID:	shir-214242216 renana-326320868
ex: Stage0
************* Description: ************
 		                  
                           
    first requested exercise...                                                          
              C#                       
 		     	  		              
***************************************/

namespace Stage0;
partial class Program
{
    static void Main(string[] args)//main
    {
        welcome2216();//function below
        welcome0868();//another function below
        Console.WriteLine("press any key to continue");
        Console.ReadKey();//press any key to continue/finish
    }
    private static void welcome2216()
    {
        string name;
        Console.Write("Enter your name: ");
        name = Console.ReadLine()!;
        Console.WriteLine("{0}, welcome to my first console application ", name);
    }
    static partial void welcome0868();
}
/**********OUTPUT**********
Enter your name:
Shir
Shir, welcome to my first console application
I am also here!
press any key to continue
***************************/
