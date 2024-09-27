using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;

namespace CountNSort {

    class CountNSort {
        //setup array
        private int[] array = new int[10];
        private int len;

        public void QuickSort(){
            sort(0, len);
        }

        public void sort(int left, int right) {
            int leftEnd, rightEnd, pivot;

            //save original values and set pivot
            leftEnd = left;
            rightEnd = right;
            pivot = array[left];

            while(left < right) {
                //move upperbound down if it hasn't hit the pivot partition
                while((array[right] >= pivot) && (left < right)) {
                    right--;
                }

                //move lowerbound up if the pass isnt completed
                if(left != right) {
                    array[left] = array[right];
                    left++;
                }

                //move lowerbound up if it hasn't hit the pivot partition
                while((array[left] <= pivot) && (left < right)) {
                    left++;
                }

                //move upperbound down if the pass isnt completed
                if(left != right) {
                    array[right] = array[left];
                    right--;
                }
            }

            //move partition and bounds
            array[left] = pivot;
            pivot = left;
            left = leftEnd;
            right = rightEnd;

            //continue if the pass hasn't completed
            if(left < pivot) {
                sort(left, pivot - 1);
            }

            if(right > pivot) {
                sort(pivot+1, right);
            }
        }

        public static int numDigits(CountNSort qSort, int i) {
            //find number of digits in a number
            return Convert.ToInt32(Math.Floor(Math.Log10(qSort.array[i]) + 1));
        }

        public static void bigDig(CountNSort qSort) {
            //count biggest digit numbers in the array from the top of the sorted numbers
            int j = 0, biggestDigits = numDigits(qSort, qSort.len);


            for(int i = qSort.len; i > 0; i--) {
                int digits = numDigits(qSort, i);
                
                if(digits < biggestDigits) {
                    i = -1;
                }
                else {
                    j++;
                }
            }

            //print amout of biggest digits
            Console.WriteLine("There are " + j + " numbers with " + biggestDigits + " digits in the list.");
        }

        public static List<int> getFile(List<int> lineNum ) {
            //read from file
            String line;
            try{
                //put file content into string and replace potential seperators
                StreamReader sr = new StreamReader("Numbers.txt");
                line = sr.ReadToEnd()!;
                line = line.Replace(",", " ").Replace("\r\n", " ").Replace(";", " ").Replace(":", " ");

                //make the list into a array of strings and convert to int list and clost file
                string[] strArray = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                foreach (string str in strArray) {
                    int temp = 0;
                    temp = Int32.Parse(str);

                    lineNum.Add(temp);
                }
                sr.Close();
            }
            catch (FormatException) {
                Console.WriteLine("Unable to parse the text file");
            }

            return lineNum;
        }

        public static void Main() {
            //create list of numbers from file
            List<int> lineNum = new List<int>();
            lineNum = getFile(lineNum);

            //print unsorted, put list of numbers into an array and sort, print again
            Console.WriteLine("Unsorted Numbers: " + String.Join(" ", lineNum));

            CountNSort qSort = new CountNSort();
            qSort.array = lineNum.ToArray();
            qSort.len = qSort.array.Length - 1;
            qSort.QuickSort();

            Console.WriteLine("Sorted Numbers: " + string.Join(" ", qSort.array));

            //Count biggest digits and print
            bigDig(qSort);
        }
    }
}