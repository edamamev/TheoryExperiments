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
        static int findDuplicate0(int[] nums) {
            int a = 0;
            for (int i = 0; i < nums.Length; i++) {
                for (int j = 0; j < nums.Length; j++) {
                    if (nums[i] == nums[j]) {
                        a = nums[j];
                    }
                }
            }
            return a;
        }
        
        /// <summary>
        /// The Fast Tortoise Hare Method.
        /// </summary>
        /// <param name="nums">Integer Array Input.</param>
        /// <returns></returns>
        static int findDuplicate1(int[] nums) {
            int tortoise = nums[0];
            int hare = nums[0];
            while (true){
                tortoise = nums[tortoise];
                hare = nums[nums[hare]];
                if (tortoise == hare) {
                    break;
                }
            }
            int ptr1 = nums[0];
            int ptr2 = tortoise;
            while (ptr1 != ptr2) {
                ptr1 = nums[ptr1];
                ptr2 = nums[ptr2];
            }
            return ptr1;
        }

        static void Main(string[] args)
        {
            int[] a = { 4, 1, 5, 6, 4};
            Console.WriteLine(findDuplicate0(a));
            Console.ReadLine();
        }
    }
}
