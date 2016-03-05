namespace WeakEvents.Example
{
   using System;

   public class Consumer
   {
      public Consumer(Source source)
      {
         source.StandardEvent += HandleStandardEvent;
      }

      ~Consumer()
      {
         Console.ForegroundColor = ConsoleColor.Red;
         Console.WriteLine("Consumer finalized!");
         Console.ResetColor();
      }

      private void HandleStandardEvent(object sender, EventArgs e)
      {
         Console.ForegroundColor = ConsoleColor.Red;
         Console.WriteLine("Received event!");
         Console.ResetColor();
      }
   }
}