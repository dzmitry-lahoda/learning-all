using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace csharp
{
//GetEnumerator can be used from several threads, but correctly.
//GetEnumerator() should call Enumerator.MoveNext until false or call Enumerator.Dispose in the end of iteration, which LINQ/foreach usually do. Otherwice may get hang when iterated manually, like in WPF DataGrid(does only couple of MoveNext, not all elements iterated, lock is not freed)
//.GetEnumerator()
//PresentationFramework.dll!System.Windows.Data.CollectionView.PlaceholderAwareEnumerator.MoveNext() Unknown Non-user code. Skipped loading symbols.
//PresentationFramework.dll!System.Windows.Controls.DataGrid.MakeFullRowSelection(System.Windows.Controls.ItemsControl.ItemInfo info, bool allowsExtendSelect, bool allowsMinimalSelect) Unknown Non-user code. Skipped loading symbols.
//PresentationFramework.dll!System.Windows.Controls.DataGrid.HandleSelectionForCellInput(System.Windows.Controls.DataGridCell cell, bool startDragging = true, bool allowsExtendSelect, bool allowsMinimalSelect) Unknown Non-user code.Skipped loading symbols.
//PresentationFramework.dll!System.Windows.Controls.DataGridCell.OnAnyMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e = { System.Windows.Input.MouseButtonEventArgs}) Unknown Non-user code.Skipped loading symbols.

    class enumerator_yield_lock : IEnumerable<int>
    {
        private const int max = 100;
        private List<int> items = Enumerable.Range(0, max + 2).ToList();
        private int sideEffect = 1;
        private Random rnd = new Random();

        public void Hang()
        {
            Parallel.For(0, 10, new ParallelOptions { MaxDegreeOfParallelism = 10 }, x =>
             {
                 var or = rnd.Next(max);
                 if (or % 2 == 0)
                 {
                     for (int i = 0; i < max; i++)
                     {
                         unchecked
                         {
                             Thread.Sleep(1);
                             sideEffect = this[i] * sideEffect + Math.Min(rnd.Next(max), rnd.Next(max));
                         }
                     }
                 }
                 else
                 {
                     var counter = 0;
                     IEnumerator<int> enumerator;
                     IDisposable disposable;
                     enumerator = this.GetEnumerator();
                     try
                     {
                         int item;
                         while (enumerator.MoveNext())
                         {
                             if (counter > max/2)
                                 break;
                             counter++;
                             item = enumerator.Current;
                             unchecked
                             {
                                 sideEffect = item * sideEffect + Math.Min(rnd.Next(max), rnd.Next(max));
                             }
                         }
                     }
                     finally
                     {
                         // Explicit cast used for IEnumerator<T>.
                         disposable = (IDisposable)enumerator;
                         //disposable.Dispose();
                         // IEnumerator will use the as operator unless IDisposable
                         // support is known at compile time.
                         // disposable = (enumerator as IDisposable);
                         // if (disposable != null)
                         // {
                         //   disposable.Dispose();
                         // }
                     }
                 }
             });
        }


        public int this[int index]
        {
            get
            {
                lock (items)
                {
                    return items[index];
                }
            }
        }

        public IEnumerator<int> GetEnumerator()
        {
            bool lockWasTaken = false;
            var temp = items;
            try
            {
                Monitor.Enter(temp, ref lockWasTaken);
                foreach (var item in items)
                    yield return item;
            }
            finally
            {
                if (lockWasTaken)
                {
                    Monitor.Exit(temp);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
