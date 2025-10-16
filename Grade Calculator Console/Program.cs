using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace Grade_Calculator_Console
{
    
    internal class Program
    {
        private static int _userGrade;
        private static float _percent;

        static void Main(string[] args)
        {
            Start();
        }
        public static void Start()
        {
            Console.WriteLine("========Grade Calculator =============");
            Dictionary<int, (float from,float to)> examGuardeRangePercent = MakeADictionaryOfScoreByPrecent();
            ShowRangeOfPercent(examGuardeRangePercent);
            Console.WriteLine("------------------------");
            Console.Write("Enter max score of exam(pkt): ");
            float maxScore = ScoreUserInputValidation();
            Console.Write("Enter ur score of exam(pkt): ");
            float studentScore = ScoreUserInputValidation();
            CalacGrade(maxScore, examGuardeRangePercent, studentScore);
            Dictionary<int, (float from, float to)> examGuardeRangePkt = MakeADictionaryOfScoreByPkt(examGuardeRangePercent, maxScore);




            ShowAllStats(examGuardeRangePercent, examGuardeRangePkt,maxScore,studentScore);

        }
        public static Dictionary<int,(float from,float to)> MakeADictionaryOfScoreByPrecent()
        {
            Console.WriteLine("Enter the exam grade range");
            Console.WriteLine($"3 is from (%) ");
            float threeFrom = UserInputValidation(0,100);
            Console.WriteLine("3 is to (%): ");
            float threeTo = UserInputValidation(threeFrom, 100);
            Console.WriteLine($"4 is from (%): ");
            float fourFrom = UserInputValidation(threeTo + 0.1f, 100);
            Console.WriteLine("4 is to (%):");
            float fourTo = UserInputValidation(fourFrom,100);
            Console.WriteLine($"5 is from (%): ");
            float fiveFrom = UserInputValidation(fourTo,100);


            return new Dictionary<int, (float,float)>() 
            {
                {3,(threeFrom,threeTo)},
                {4,(fourFrom,fourTo)},
                {5,(fiveFrom,100)}
            };
        }
        public static void CalacGrade(float maxScore, Dictionary<int, (float from, float to)> scoreByPercent, float userScore)
        {
            _percent = (userScore / maxScore) * 100;

            foreach(KeyValuePair<int,(float from, float to)> range in scoreByPercent)
            {
                if(_percent >= range.Value.from && _percent <= range.Value.to)
                    {
                        _userGrade = range.Key;
                        break;
                    }
            }
            if(_userGrade == 0)
            {
                _userGrade = 2;
            }
        }
        public static Dictionary<int, (float from, float to)> MakeADictionaryOfScoreByPkt(Dictionary<int, (float from, float to)> percentRanges, float maxScore)
        {
            var result = new Dictionary<int, (float from, float to)>();

            foreach (var range in percentRanges)
            {
                float fromScore = (range.Value.from / 100f) * maxScore;
                float toScore = (range.Value.to / 100f) * maxScore;

                result.Add(range.Key, (fromScore, toScore));
            }
            return result;
        }
        public static float ScoreUserInputValidation()
        {
            float maxScore;
            bool isValidNum;
            do
            {
                isValidNum = float.TryParse(Console.ReadLine(), out maxScore);

                if(!isValidNum)
                {
                    Console.WriteLine("Incorrect nuber!!! Try Again");
                }

            } while (!isValidNum);
            Console.Clear();

            return maxScore;
        }
        public static float UserInputValidation(float minValue,float maxValue)
        {
            bool isValidNum = false;
            float inputNum;

            do
            {
                Console.Write("Enter Number: ");
                isValidNum = float.TryParse(Console.ReadLine(), out inputNum);

                if(!isValidNum)
                {
                    Console.WriteLine("Incorrect Number!!! try Again!");
                }else if(inputNum < minValue || inputNum > maxValue)
                {
                    Console.WriteLine($"The number have to be >= {minValue} and <= {maxValue}. Try Again!");
                    isValidNum = false;
                }

            }
            while (!isValidNum);

            return inputNum;
        }

        public static void ShowRangeOfPercent(Dictionary<int,(float from,float to)> examGuareRange)
        {
            foreach(KeyValuePair<int,(float from,float to)> range in examGuareRange )
            {
                Console.WriteLine($"| {range.Key} is from {range.Value.from}% to {range.Value.to}%");
            }
        }
        public static void ShowRangeOfPkt(Dictionary<int, (float from, float to)> range)
        {
            foreach(KeyValuePair<int, (float from, float to)> items in range)
            {
                Console.WriteLine($"{items.Key} => {items.Value.from}pkt - {items.Value.to}pkt");
            }
        }
        public static void ShowAllStats(Dictionary<int, (float from, float to)> rangePercent, Dictionary<int, (float from, float to)> rangePkt, float maxScore, float userScore)
        {
            Console.Clear();
            Console.WriteLine("=========ALL STATS=========");

            ShowRangeOfPercent(rangePercent);
            Console.WriteLine("===========================");
            ShowRangeOfPkt(rangePkt);
            Console.WriteLine("===========================");
            Console.WriteLine($"Ur grade(pkt): {_userGrade} | {userScore} / {maxScore} | {_percent}%");
            Console.WriteLine("===========================");
        }

    }
}
