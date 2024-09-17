using System.Runtime.InteropServices.Marshalling;

namespace sortQuick {

    class quickSort {
        //setup array
        private int[] array = new int[10];
        private int len;

        public void QuickSort(){
            sort(0, len-1);
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

        //find number of digits in a number
        public static int numDigits(quickSort qSort, int i) {
            return Convert.ToInt32(Math.Floor(Math.Log10(qSort.array[i]) + 1));
        }

        public static void Main() {
            quickSort qSort = new quickSort();

            //setup array and start sort
            int[] array = {47, 5, 12, 146, 2, 120, 80, 16, 34, 60};
            qSort.array = array;
            qSort.len = qSort.array.Length;
            qSort.QuickSort();
            int j = 0, biggestDigits = numDigits(qSort, qSort.len - 1);

            //print the array
            /*for (int i = 0; j < qSort.len; i++) {
                Console.WriteLine(qSort.array[i]);
            }*/
            Console.WriteLine("Numbers: " + string.Join(" ", qSort.array));

            //count biggest digit numbers in the array from the top of the sorted numbers
            for(int i = qSort.len - 1; i > -1; i--) {
                int digits = numDigits(qSort, i);

                if(digits < biggestDigits) {
                    i = 0;
                }
                else {
                    j++;
                }
            }
            Console.WriteLine("There are " + j + " numbers with " + biggestDigits + " digits in the list.");

            //read from file testing
            String line;
            List<int> lineNum = new List<int>();
            try{
                StreamReader sr = new StreamReader("Numbers.txt");
                line = sr.ReadToEnd()!;
                line.Replace("\r\n", " ");

                string[] strArray = line.Split(new char[] {'\r', '\n', ' '}, StringSplitOptions.RemoveEmptyEntries);
                foreach (string str in strArray) {
                    int temp = 0;
                    temp = Int32.Parse(str);

                    lineNum.Add(temp);
                    //Console.WriteLine(temp);
                }
                sr.Close();
            }
            catch (FormatException) {
                Console.WriteLine("Unable to parse text file");
            }

            Console.WriteLine(String.Join(" ", lineNum));
        }
    }
}