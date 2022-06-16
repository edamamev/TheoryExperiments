using System;

namespace Tortoise_Hare_Duplicate
{
    class Program
    {
        /// <summary>
        /// The Slow Iterative Method.
        /// </summary>
        /// <param name="nums">Integer Array Input.</param>
        /// <returns></returns>
        static int findDuplicate0(int[] b) {
            for (int i = 0; i < b.Length; i++) {
                for (int j = 0; j < b.Length; j++) {
                    if (b[i] == b[j]) {
                        return b[j];
                    }
                }
            }
            return -1;
        }
        
        /// <summary>
        /// The Fast Tortoise Hare Method.
        /// </summary>
        /// <param name="nums">Integer Array Input.</param>
        /// <returns></returns>
        static int findDuplicate1(int[] a) {
            int t = a[0];
            int h = a[0];
            while (true){
                t = a[t];
                h = a[a[h]];
                if (t == h) {
                    break;
                }
            }
            int a1 = a[0];
            int a2 = t;
            while (a1 != a2) {
                a1 = a[a1];
                a2 = a[a2];
            }
            return a1;
        }

        static void Main(string[] args)
        {
            int[] a = { 8, 6, 3, 4, 1, 3, 2, 5, 7 };
            Console.WriteLine(findDuplicate1(a));
            Console.ReadLine();
        }
    }
}
