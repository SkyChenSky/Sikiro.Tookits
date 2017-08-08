using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Framework.Common.Extension;

namespace Framework.Common.Helper
{
    /// <summary>
    /// 代码性能计时器
    /// </summary>
    public class CodeTimerHelper
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Initialize()
        {
            // 将当前进程及线程的优先级设为最高，减少操作系统在调度上造成的干扰
            // 然后调用一次Time方法进行预热，以便让Time方法尽快进入状态
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            Time("", 1, () => { });
        }

        /// <summary>
        /// 计时
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="iteration">循环次数</param>
        /// <param name="action">方法体</param>
        public static void Time(string name, int iteration, Action action)
        {
            if (name.IsNullOrEmpty())
                return;

            // 1.保留当前控制台前景色，并使用黄色输出名称参数
            ConsoleColor currentForeColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(name);

            // 2.强制GC进行收集，并记录目前各代已经收集的次数
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            int[] gcCounts = new int[GC.MaxGeneration + 1];
            for (int i = 0; i <= GC.MaxGeneration; i++)
            {
                gcCounts[i] = GC.CollectionCount(i);
            }

            // 3.执行代码，记录下消耗的时间及CPU时钟周期
            var watch = new Stopwatch();
            watch.Start();
            var cycleCount = GetCycleCount();
            for (int i = 0; i < iteration; i++)
            {
                action();
            }
            ulong cpuCycles = GetCycleCount() - cycleCount;
            watch.Stop();

            // 4.恢复控制台默认前景色，并打印出消耗时间及CPU时钟周期
            Console.ForegroundColor = currentForeColor;
            Console.WriteLine("\tTime Elapsed(总耗时):\t" + watch.ElapsedMilliseconds.ToString("N0") + "ms");
            Console.WriteLine("\tTime AVG(平均用时):\t" + (watch.ElapsedMilliseconds / iteration).ToString("N0") + "ms");
            Console.WriteLine("\tCPU Cycles(CPU时钟周期):\t" + cpuCycles.ToString("N0"));

            // 5.打印执行过程中各代垃圾收集回收次数
            for (int i = 0; i <= GC.MaxGeneration; i++)
            {
                int count = GC.CollectionCount(i) - gcCounts[i];
                Console.WriteLine("\tGen " + i + ": \t\t" + count);
            }

            Console.WriteLine();
        }

        private static ulong GetCycleCount()
        {
            ulong cycleCount = 0;
            QueryThreadCycleTime(GetCurrentThread(), ref cycleCount);
            return cycleCount;
        }

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool QueryThreadCycleTime(IntPtr threadHandle, ref ulong cycleTime);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetCurrentThread();

    }
}
