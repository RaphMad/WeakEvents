namespace WeakEvents
{
   using System;
   using Example;

   class Program
   {
      public static void Main()
      {
         var source = new Source();
         var consumer = new Consumer(source);
         //var consumer = new WeakConsumer(source);

         Console.WriteLine("Trigger event.");
         source.TriggerEvents();

         Console.WriteLine(Environment.NewLine + "Clear consumer.");
         consumer = null;
         TriggerGC();

         Console.WriteLine(Environment.NewLine + "Trigger event again.");
         source.TriggerEvents();

         Console.WriteLine(Environment.NewLine + "Clear source.");
         source = null;
         TriggerGC();

         Console.ReadKey();
      }

      private static void TriggerGC()
      {
         Console.WriteLine("GC started.");

         GC.Collect();
         GC.WaitForPendingFinalizers();
         GC.Collect();

         Console.WriteLine("GC finished.");
      }
   }
}