namespace WeakEvents.Example
{
   using System;

   public class WeakConsumer
   {
      public WeakConsumer(Source source)
      {
         source.WeakEvent += HandleWeakEvent;
      }

      ~WeakConsumer()
      {
         Console.ForegroundColor = ConsoleColor.Red;
         Console.WriteLine("WeakConsumer finalized!");
         Console.ResetColor();
      }

      private void HandleWeakEvent(object sender, EventArgs e)
      {
         Console.ForegroundColor = ConsoleColor.Red;
         Console.WriteLine("Received weak event!");
         Console.ResetColor();
      }
   }
}