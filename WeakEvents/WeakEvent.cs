namespace WeakEvents
{
   using System;
   using System.Collections.Generic;
   using System.Linq;

   /// <summary>
   /// Represents a weak which only holds weak references to its subscribers.
   /// </summary>
   /// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
   public class WeakEvent<TEventArgs> where TEventArgs : EventArgs
   {
      /// <summary>
      /// The event subscribers.
      /// </summary>
      private readonly IList<WeakReference<Action<object, TEventArgs>>> _subscribers = new List<WeakReference<Action<object, TEventArgs>>>();

      /// <summary>
      /// Adds a new subscriber to a WeakEvent.
      /// </summary>
      /// <param name="weakEvent">The weak event.</param>
      /// <param name="subscriber">The subscriber to add.</param>
      /// <returns>
      /// The WeakEvent with an updated subscription.
      /// </returns>
      public static WeakEvent<TEventArgs> operator +(WeakEvent<TEventArgs> weakEvent, Action<object, TEventArgs> subscriber)
      {
         if (weakEvent == null)
         {
            weakEvent = new WeakEvent<TEventArgs>();
         }

         lock (weakEvent._subscribers)
         {
            weakEvent._subscribers.Add(new WeakReference<Action<object, TEventArgs>>(subscriber));
         }

         return weakEvent;
      }

      /// <summary>
      /// Attempts to remove a subscriber from a WeakEvent.
      /// </summary>
      /// <param name="weakEvent">The weak event.</param>
      /// <param name="subscriber">The subscriber to remove.</param>
      /// <returns>
      /// The WeakEvent with an updated subscription.
      /// </returns>
      public static WeakEvent<TEventArgs> operator -(WeakEvent<TEventArgs> weakEvent, Action<object, TEventArgs> subscriber)
      {
         if (weakEvent == null)
         {
            return null;
         }

         lock (weakEvent._subscribers)
         {
            foreach (var existingSubscriber in weakEvent._subscribers.ToList())
            {
               Action<object, TEventArgs> handler;

               if (existingSubscriber.TryGetTarget(out handler))
               {
                  if (handler == subscriber)
                  {
                     weakEvent._subscribers.Remove(existingSubscriber);
                     break;
                  }
               }
               else
               {
                  weakEvent._subscribers.Remove(existingSubscriber);
               }
            }
         }

         return weakEvent;
      }

      /// <summary>
      /// Notifies all subscribers.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="TEventArgs"/> instance containing the event data.</param>
      public void Invoke(object sender, TEventArgs e)
      {
         lock (_subscribers)
         {
            foreach (var subscriber in _subscribers.ToList())
            {
               Action<object, TEventArgs> handler;

               if (subscriber.TryGetTarget(out handler))
               {
                  handler.Invoke(sender, e);
               }
               else
               {
                  _subscribers.Remove(subscriber);
               }
            }
         }
      }
   }
}