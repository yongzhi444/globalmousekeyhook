﻿using System;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using MouseKeyHook.Rx;

namespace ConsoleHook.Rx
{
    internal class DetectCombinations
    {
        public static void Do(AutoResetEvent quit)
        {
            var quitTrigger = Trigger.FromString("Control+Q");
            var triggers = new[]
            {
                quitTrigger,
                Trigger.On(Keys.H).Alt().Shift(),
                Trigger.On(Keys.E).And(Keys.Q).And(Keys.W)
            };


            Hook
                .GlobalEvents()
                .KeyDownObservable()
                .Matching(triggers)
                .ForEachAsync(trigger =>
                {
                    if (trigger==quitTrigger) quit.Set();
                    Console.WriteLine(trigger);
                });

            Console.WriteLine("Press Control+Q to quit.");
            Console.WriteLine("Monitoring folowing combinations:");
            foreach (var name in triggers)
            {
                Console.WriteLine("\t" + name);
            }
        }
    }
}