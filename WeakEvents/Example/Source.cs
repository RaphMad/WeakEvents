namespace WeakEvents.Example
{
   using System;

   public class Source
   {
      public event EventHandler StandardEvent;
      public WeakEvent<EventArgs> WeakEvent;

      public void TriggerEvents()
      {
         StandardEvent?.Invoke(this, EventArgs.Empty);
         WeakEvent?.Invoke(this, EventArgs.Empty);
      }
   }
}